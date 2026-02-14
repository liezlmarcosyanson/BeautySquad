using System;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using AutoMapper;
using IAT.Application;
using IAT.Application.Services;
using IAT.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(IAT.WebApi.Startup))]

namespace IAT.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure Web API
            var httpConfig = new HttpConfiguration();
            ConfigureWebApi(httpConfig);
            ConfigureJwtSecurity(app);
            ConfigureDependencyInjection(httpConfig);
            ConfigureAutoMapper();

            app.UseWebApi(httpConfig);
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Enable CORS
            config.EnableCors(new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*"));

            // Register global exception filter
            config.Filters.Add(new GlobalExceptionFilter());

            // JSON formatting
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Newtonsoft.Json.Formatting.Indented;

            // Swagger/Swashbuckle
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "BeautySquad API");
                c.IncludeXmlComments(GetXmlCommentsPath());
            }).EnableSwaggerUi();
        }

        private void ConfigureJwtSecurity(IAppBuilder app)
        {
            var issuer = "BeautySquad";
            var audience = "BeautySquadUsers";
            var key = System.Text.Encoding.ASCII.GetBytes("your-secret-key-min-32-chars-long-for-hs256");

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(5)
                }
            });
        }

        private void ConfigureDependencyInjection(HttpConfiguration config)
        {
            var container = new SimpleContainer();

            // Register DbContext and UnitOfWork
            container.Register<AppDbContext>(() => new AppDbContext());
            container.Register<IUnitOfWork, UnitOfWork>();

            // Register Services
            container.Register<ICampaignService, CampaignService>();
            container.Register<IInfluencerService, InfluencerService>();
            container.Register<IContentSubmissionService, ContentSubmissionService>();
            container.Register<IApprovalService, ApprovalService>();
            container.Register<IPerformanceMetricsService, PerformanceMetricsService>();

            // Register Controllers
            var controllerResolver = new SimpleAssemblyResolver();
            config.Services.Replace(typeof(IHttpControllerTypeResolver), controllerResolver);

            config.DependencyResolver = new SimpleDependencyResolver(container);
        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
            Mapper.AssertConfigurationIsValid();
        }

        private string GetXmlCommentsPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + @"bin\IAT.WebApi.xml";
        }
    }

    /// <summary>
    /// Simple DI container for OWIN
    /// </summary>
    public class SimpleContainer
    {
        private readonly System.Collections.Generic.Dictionary<Type, Func<object>> _registrations =
            new System.Collections.Generic.Dictionary<Type, Func<object>>();

        public void Register<TInterface, TImplementation>(Func<TImplementation> factory = null)
            where TImplementation : class, TInterface
        {
            if (factory == null)
            {
                _registrations[typeof(TInterface)] = () => Activator.CreateInstance(typeof(TImplementation));
            }
            else
            {
                _registrations[typeof(TInterface)] = () => factory();
            }
        }

        public void Register<T>(Func<T> factory) where T : class
        {
            _registrations[typeof(T)] = () => factory();
        }

        public object Resolve(Type type)
        {
            if (_registrations.ContainsKey(type))
            {
                return _registrations[type]();
            }

            return Activator.CreateInstance(type);
        }
    }

    /// <summary>
    /// Simple dependency resolver for WebAPI
    /// </summary>
    public class SimpleDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        private readonly SimpleContainer _container;

        public SimpleDependencyResolver(SimpleContainer container)
        {
            _container = container;
        }

        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return new[] { _container.Resolve(serviceType) };
            }
            catch
            {
                return new object[0];
            }
        }

        public void Dispose()
        {
        }
    }

    /// <summary>
    /// Simple assembly resolver for controller discovery
    /// </summary>
    public class SimpleAssemblyResolver : IHttpControllerTypeResolver
    {
        private System.Collections.Generic.ICollection<Type> _controllerTypes;

        public System.Collections.Generic.ICollection<Type> GetControllerTypes(System.Web.Http.Dispatcher.IAssembliesResolver assembliesResolver)
        {
            if (_controllerTypes == null)
            {
                var types = new System.Collections.Generic.List<Type>();
                var asm = System.Reflection.Assembly.GetExecutingAssembly();
                foreach (var type in asm.GetTypes())
                {
                    if (typeof(ApiController).IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        types.Add(type);
                    }
                }
                _controllerTypes = types;
            }
            return _controllerTypes;
        }
    }
}
