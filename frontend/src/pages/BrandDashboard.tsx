import React, { useState, useEffect } from 'react';
import { useAuth } from '../context/AuthContext';
import { campaignService, metricsService } from '../services/api';
import { BarChart, BarChart3, Users, TrendingUp, Plus, Activity } from 'lucide-react';

export function BrandDashboard() {
  const { user } = useAuth();
  const [campaigns, setCampaigns] = useState([]);
  const [metrics, setMetrics] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [showNewCampaign, setShowNewCampaign] = useState(false);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [campaignsData, metricsData] = await Promise.all([
        campaignService.getAll(),
        metricsService.getAll(),
      ]);
      setCampaigns(campaignsData?.filter((c: any) => c.brandId === user?.id) || []);
      setMetrics(metricsData || []);
    } catch (err) {
      console.error('Failed to load data', err);
    } finally {
      setIsLoading(false);
    }
  };

  const activeCampaigns = campaigns.filter((c: any) => c.status === 'active').length;
  const totalBudget = campaigns.reduce((sum: number, c: any) => sum + (c.budget || 0), 0);

  return (
    <div className="min-h-screen bg-gray-50 py-8 px-4">
      <div className="max-w-7xl mx-auto">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-4xl font-bold text-gray-900 mb-2">
            Brand Dashboard
          </h1>
          <p className="text-gray-600">Manage and monitor your campaigns</p>
        </div>

        {/* Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
          {[
            {
              label: 'Total Campaigns',
              value: campaigns.length,
              icon: BarChart,
              color: 'purple',
            },
            {
              label: 'Active Campaigns',
              value: activeCampaigns,
              icon: Activity,
              color: 'green',
            },
            {
              label: 'Total Budget',
              value: `$${(totalBudget / 1000).toFixed(0)}K`,
              icon: BarChart3,
              color: 'blue',
            },
            {
              label: 'Performance',
              value: `${(Math.random() * 100).toFixed(0)}%`,
              icon: TrendingUp,
              color: 'orange',
            },
          ].map((stat, idx) => {
            const Icon = stat.icon;
            const bgColor = {
              purple: 'bg-purple-50',
              green: 'bg-green-50',
              blue: 'bg-blue-50',
              orange: 'bg-orange-50',
            }[stat.color];
            const iconColor = {
              purple: 'text-purple-600',
              green: 'text-green-600',
              blue: 'text-blue-600',
              orange: 'text-orange-600',
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

        {/* Actions */}
        <div className="mb-8">
          <button
            onClick={() => setShowNewCampaign(true)}
            className="inline-flex items-center space-x-2 bg-purple-600 text-white px-6 py-3 rounded-lg font-medium hover:bg-purple-700 transition"
          >
            <Plus size={20} />
            <span>Create Campaign</span>
          </button>
        </div>

        {/* Campaigns List */}
        <div className="bg-white rounded-xl border border-gray-200 p-6">
          <h2 className="text-xl font-bold text-gray-900 mb-6">Your Campaigns</h2>

          {campaigns.length > 0 ? (
            <div className="space-y-4">
              {campaigns.map((campaign: any) => (
                <div
                  key={campaign.id}
                  className="flex items-center justify-between p-4 border border-gray-100 rounded-lg hover:bg-gray-50 transition"
                >
                  <div className="flex-1">
                    <p className="font-medium text-gray-900">{campaign.title}</p>
                    <div className="flex items-center space-x-4 text-sm text-gray-600 mt-1">
                      <span>Budget: ${campaign.budget?.toLocaleString()}</span>
                      <span>
                        {new Date(campaign.start).toLocaleDateString()} -{' '}
                        {new Date(campaign.end).toLocaleDateString()}
                      </span>
                    </div>
                  </div>
                  <div className="text-right">
                    <span
                      className={`inline-block px-3 py-1 rounded-full text-sm font-medium capitalize ${
                        campaign.status === 'active'
                          ? 'bg-green-50 text-green-700'
                          : campaign.status === 'pending'
                          ? 'bg-yellow-50 text-yellow-700'
                          : 'bg-gray-50 text-gray-700'
                      }`}
                    >
                      {campaign.status}
                    </span>
                    <button className="text-purple-600 hover:text-purple-700 font-medium text-sm block mt-2">
                      View →
                    </button>
                  </div>
                </div>
              ))}
            </div>
          ) : (
            <p className="text-gray-500 text-center py-8">No campaigns yet</p>
          )}
        </div>
      </div>

      {/* New Campaign Modal */}
      {showNewCampaign && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-xl max-w-2xl w-full p-6">
            <h3 className="text-xl font-bold text-gray-900 mb-4">Create Campaign</h3>
            <div className="space-y-4 max-h-96 overflow-y-auto">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Title</label>
                <input
                  type="text"
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
                  placeholder="Campaign title"
                />
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Description</label>
                <textarea
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
                  rows={3}
                  placeholder="Describe your campaign..."
                />
              </div>
              <div className="grid grid-cols-2 gap-4">
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">Budget</label>
                  <input
                    type="number"
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
                    placeholder="0"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 mb-2">Start Date</label>
                  <input
                    type="date"
                    className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
                  />
                </div>
              </div>
              <div className="flex space-x-4">
                <button
                  onClick={() => setShowNewCampaign(false)}
                  className="flex-1 px-4 py-2 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50"
                >
                  Cancel
                </button>
                <button className="flex-1 px-4 py-2 bg-purple-600 text-white rounded-lg font-medium hover:bg-purple-700">
                  Create
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
