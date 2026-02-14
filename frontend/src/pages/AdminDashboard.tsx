import React, { useState, useEffect } from 'react';
import { campaignService, influencerService, contentSubmissionService, approvalService } from '../services/api';
import { Users, TrendingUp, CheckCircle, AlertCircle, Loader } from 'lucide-react';

export function AdminDashboard() {
  const [stats, setStats] = useState({
    totalCampaigns: 0,
    totalInfluencers: 0,
    pendingApprovals: 0,
    totalRevenue: 0,
  });
  const [campaigns, setCampaigns] = useState([]);
  const [submissions, setSubmissions] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [campaignsData, influencersData, submissionsData] = await Promise.all([
        campaignService.getAll(),
        influencerService.getAll(),
        contentSubmissionService.getAll(),
      ]);

      setCampaigns(campaignsData || []);
      setSubmissions(submissionsData || []);

      setStats({
        totalCampaigns: campaignsData?.length || 0,
        totalInfluencers: influencersData?.length || 0,
        pendingApprovals: submissionsData?.filter((s: any) => s.status === 'pending').length || 0,
        totalRevenue: campaignsData?.reduce((sum: number, c: any) => sum + (c.budget || 0), 0) || 0,
      });
    } catch (err) {
      console.error('Failed to load data', err);
    } finally {
      setIsLoading(false);
    }
  };

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <Loader className="animate-spin" size={32} />
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50 py-8 px-4">
      <div className="max-w-7xl mx-auto">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-4xl font-bold text-gray-900 mb-2">Admin Dashboard</h1>
          <p className="text-gray-600">Platform overview and management</p>
        </div>

        {/* Key Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
          {[
            {
              label: 'Total Campaigns',
              value: stats.totalCampaigns,
              icon: TrendingUp,
              color: 'purple',
            },
            {
              label: 'Active Influencers',
              value: stats.totalInfluencers,
              icon: Users,
              color: 'blue',
            },
            {
              label: 'Pending Approvals',
              value: stats.pendingApprovals,
              icon: AlertCircle,
              color: 'yellow',
            },
            {
              label: 'Total Budget',
              value: `$${(stats.totalRevenue / 1000000).toFixed(1)}M`,
              icon: CheckCircle,
              color: 'green',
            },
          ].map((stat, idx) => {
            const Icon = stat.icon;
            const bgColor = {
              purple: 'bg-purple-50',
              blue: 'bg-blue-50',
              yellow: 'bg-yellow-50',
              green: 'bg-green-50',
            }[stat.color];
            const iconColor = {
              purple: 'text-purple-600',
              blue: 'text-blue-600',
              yellow: 'text-yellow-600',
              green: 'text-green-600',
            }[stat.color];

            return (
              <div key={idx} className={`${bgColor} rounded-xl p-6 border border-gray-200`}>
                <div className="flex items-center justify-between">
                  <div>
                    <p className="text-gray-600 text-sm font-medium">{stat.label}</p>
                    <p className="text-3xl font-bold text-gray-900 mt-2">{stat.value}</p>
                  </div>
                  <Icon className={`${iconColor} opacity-40`} size={32} />
                </div>
              </div>
            );
          })}
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* Recent Campaigns */}
          <div className="bg-white rounded-xl border border-gray-200 p-6">
            <h2 className="text-xl font-bold text-gray-900 mb-6">Recent Campaigns</h2>
            {campaigns.length > 0 ? (
              <div className="space-y-4">
                {campaigns.slice(0, 5).map((campaign: any) => (
                  <div
                    key={campaign.id}
                    className="flex items-center justify-between p-4 border border-gray-100 rounded-lg hover:bg-gray-50 transition"
                  >
                    <div className="flex-1">
                      <p className="font-medium text-gray-900 truncate">{campaign.title}</p>
                      <p className="text-sm text-gray-600">Budget: ${campaign.budget?.toLocaleString()}</p>
                    </div>
                    <span
                      className={`px-3 py-1 rounded-full text-xs font-medium capitalize flex-shrink-0 ${
                        campaign.status === 'active'
                          ? 'bg-green-50 text-green-700'
                          : 'bg-gray-50 text-gray-700'
                      }`}
                    >
                      {campaign.status}
                    </span>
                  </div>
                ))}
              </div>
            ) : (
              <p className="text-gray-500 text-center py-8">No campaigns</p>
            )}
          </div>

          {/* Pending Submissions */}
          <div className="bg-white rounded-xl border border-gray-200 p-6">
            <h2 className="text-xl font-bold text-gray-900 mb-6">Pending Submissions</h2>
            {submissions.filter((s: any) => s.status === 'pending').length > 0 ? (
              <div className="space-y-4">
                {submissions
                  .filter((s: any) => s.status === 'pending')
                  .slice(0, 5)
                  .map((submission: any) => (
                    <div
                      key={submission.id}
                      className="flex items-center justify-between p-4 border border-gray-100 rounded-lg hover:bg-gray-50 transition"
                    >
                      <div className="flex-1">
                        <p className="font-medium text-gray-900">Submission {submission.id.slice(0, 8)}</p>
                        <p className="text-sm text-gray-600">
                          {new Date(submission.submittedAt).toLocaleDateString()}
                        </p>
                      </div>
                      <button className="text-purple-600 hover:text-purple-700 font-medium text-sm">
                        Review →
                      </button>
                    </div>
                  ))}
              </div>
            ) : (
              <p className="text-gray-500 text-center py-8">No pending submissions</p>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
