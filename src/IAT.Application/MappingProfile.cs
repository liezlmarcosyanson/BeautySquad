using AutoMapper;
using IAT.Domain;

namespace IAT.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Influencer, InfluencerDto>()
                .ForMember(d => d.Tags, opt => opt.MapFrom(src => src.Tags != null ? System.Linq.Enumerable.Select(src.Tags, t => t.Tag.Name).ToArray() : new string[0]));

            CreateMap<InfluencerCreateRequest, Influencer>()
                .ForMember(d => d.IsDeleted, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.Tags, opt => opt.Ignore());
            
            // Campaign
            CreateMap<Domain.Campaign, CampaignDto>();
            CreateMap<CampaignCreateRequest, Domain.Campaign>();
            CreateMap<Domain.CampaignDeliverable, CampaignDeliverableDto>();
            CreateMap<CampaignDeliverableDto, Domain.CampaignDeliverable>();

            // ContentSubmission
            CreateMap<Domain.ContentSubmission, ContentSubmissionDto>()
                .ForMember(d => d.State, opt => opt.MapFrom(src => src.State.ToString()));

            // Approval
            CreateMap<Domain.Approval, ApprovalDto>()
                .ForMember(d => d.Decision, opt => opt.MapFrom(src => src.Decision.ToString()));

            // PerformanceMetrics
            CreateMap<Domain.PerformanceMetric, PerformanceMetricsDto>();
        }
    }
}
