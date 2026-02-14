import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { Sparkles, TrendingUp, Users, Zap } from 'lucide-react';

export function HomePage() {
  const { isAuthenticated, user } = useAuth();

  if (isAuthenticated && user) {
    const redirectPath = user.role === 'admin' ? '/admin' : user.role === 'influencer' ? '/influencer' : '/brand';
    return <Navigate to={redirectPath} replace />;
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-900 via-purple-900 to-gray-900">
      {/* Background gradient elements */}
      <div className="absolute inset-0 overflow-hidden pointer-events-none">
        <div className="absolute top-1/3 left-1/4 w-96 h-96 bg-purple-500 rounded-full mix-blend-multiply filter blur-3xl opacity-10"></div>
        <div className="absolute top-1/2 right-1/4 w-96 h-96 bg-purple-300 rounded-full mix-blend-multiply filter blur-3xl opacity-10"></div>
      </div>

      <div className="relative z-10">
        {/* Hero Section */}
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-20">
          <div className="text-center mb-20">
            <div className="inline-block mb-6">
              <div className="w-16 h-16 bg-gradient-to-br from-purple-600 to-purple-400 rounded-2xl flex items-center justify-center">
                <span className="text-white font-bold text-4xl">B</span>
              </div>
            </div>

            <h1 className="text-5xl md:text-6xl font-bold text-white mb-6">
              Connect Brands
              <br />
              <span className="bg-gradient-to-r from-purple-400 to-purple-200 bg-clip-text text-transparent">
                with Influencers
              </span>
            </h1>

            <p className="text-xl text-gray-300 mb-8 max-w-2xl mx-auto">
              BeautySquad is the platform that brings together brands and influencers for authentic,
              impactful collaborations. Create campaigns, discover talent, and measure success.
            </p>

            <div className="flex flex-col sm:flex-row items-center justify-center gap-4">
              <a
                href="/login"
                className="px-8 py-3 bg-purple-600 text-white rounded-lg font-semibold hover:bg-purple-700 transition"
              >
                Sign In
              </a>
              <a
                href="/signup"
                className="px-8 py-3 bg-white text-purple-600 rounded-lg font-semibold hover:bg-gray-100 transition"
              >
                Join as Influencer
              </a>
            </div>
          </div>

          {/* Features Grid */}
          <div className="grid md:grid-cols-3 gap-8 mb-20">
            {[
              {
                icon: TrendingUp,
                title: 'Smart Matching',
                description: 'Our AI helps brands find the perfect influencers for their campaigns.',
              },
              {
                icon: Users,
                title: 'Easy Collaboration',
                description: 'Streamlined workflows make managing campaigns effortless.',
              },
              {
                icon: Sparkles,
                title: 'Real Analytics',
                description: 'Track performance metrics and ROI for every campaign.',
              },
            ].map((feature, idx) => {
              const Icon = feature.icon;
              return (
                <div
                  key={idx}
                  className="bg-white/10 backdrop-blur-lg rounded-xl p-8 border border-white/20 hover:border-purple-500/50 transition hover:bg-white/15"
                >
                  <Icon className="text-purple-400 mb-4" size={32} />
                  <h3 className="text-xl font-bold text-white mb-2">{feature.title}</h3>
                  <p className="text-gray-300">{feature.description}</p>
                </div>
              );
            })}
          </div>

          {/* Stats */}
          <div className="bg-white/5 backdrop-blur-lg rounded-2xl p-8 border border-white/10 mb-20">
            <div className="grid md:grid-cols-4 gap-8 text-center">
              {[
                { label: 'Active Campaigns', value: '250+' },
                { label: 'Connected Influencers', value: '5K+' },
                { label: 'Brands', value: '500+' },
                { label: 'Avg. ROI Increase', value: '340%' },
              ].map((stat, idx) => (
                <div key={idx}>
                  <p className="text-3xl font-bold text-purple-400 mb-2">{stat.value}</p>
                  <p className="text-gray-300">{stat.label}</p>
                </div>
              ))}
            </div>
          </div>
        </div>
      </div>

      {/* Footer CTA */}
      <div className="relative z-10 bg-gradient-to-r from-purple-600/20 to-purple-400/20 border-t border-purple-500/20 py-16 px-4">
        <div className="max-w-4xl mx-auto text-center">
          <h2 className="text-3xl font-bold text-white mb-4">Ready to get started?</h2>
          <p className="text-gray-300 mb-8">
            Join thousands of brands and influencers already using BeautySquad
          </p>
          <div className="flex flex-col sm:flex-row items-center justify-center gap-4">
            <a
              href="/signup"
              className="px-8 py-3 bg-purple-600 text-white rounded-lg font-semibold hover:bg-purple-700 transition"
            >
              Get Started Free
            </a>
            <a href="#" className="text-purple-400 hover:text-purple-300 font-semibold">
              Learn more →
            </a>
          </div>
        </div>
      </div>
    </div>
  );
}
