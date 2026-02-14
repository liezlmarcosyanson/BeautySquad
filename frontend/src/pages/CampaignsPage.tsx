import React, { useState, useEffect } from 'react';
import { campaignService } from '../services/api';
import { Campaign } from '../types';
import { Calendar, DollarSign, Target, Loader } from 'lucide-react';

export function CampaignsPage() {
  const [campaigns, setCampaigns] = useState<Campaign[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [filter, setFilter] = useState('all');

  useEffect(() => {
    loadCampaigns();
  }, []);

  const loadCampaigns = async () => {
    try {
      const data = await campaignService.getAll();
      setCampaigns(data);
    } catch (err) {
      console.error('Failed to load campaigns', err);
    } finally {
      setIsLoading(false);
    }
  };

  const filteredCampaigns = campaigns.filter(
    (c) => filter === 'all' || c.status === filter
  );

  const getStatusColor = (status: string) => {
    const colors: { [key: string]: string } = {
      active: 'bg-green-50 text-green-700 border-green-200',
      completed: 'bg-blue-50 text-blue-700 border-blue-200',
      pending: 'bg-yellow-50 text-yellow-700 border-yellow-200',
    };
    return colors[status] || 'bg-gray-50 text-gray-700 border-gray-200';
  };

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <Loader className="animate-spin" size={32} />
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50 py-12 px-4">
      <div className="max-w-7xl mx-auto">
        <div className="mb-10">
          <h1 className="text-4xl font-bold text-gray-900 mb-2">Campaigns</h1>
          <p className="text-gray-600">Explore active campaigns and opportunities</p>
        </div>

        <div className="flex flex-wrap gap-3 mb-8">
          {['all', 'active', 'pending', 'completed'].map((status) => (
            <button
              key={status}
              onClick={() => setFilter(status)}
              className={`px-4 py-2 rounded-lg font-medium transition capitalize ${
                filter === status
                  ? 'bg-purple-600 text-white'
                  : 'bg-white text-gray-700 border border-gray-300 hover:border-purple-600'
              }`}
            >
              {status}
            </button>
          ))}
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filteredCampaigns.length > 0 ? (
            filteredCampaigns.map((campaign) => (
              <div
                key={campaign.id}
                className="bg-white rounded-xl shadow-sm hover:shadow-lg transition border border-gray-200 overflow-hidden group"
              >
                <div className="bg-gradient-to-br from-purple-500 to-purple-600 h-32 flex items-center justify-center">
                  <div className="text-white text-center">
                    <h3 className="font-bold text-xl">{campaign.title}</h3>
                  </div>
                </div>

                <div className="p-6">
                  <p className="text-gray-600 text-sm mb-4 line-clamp-2">{campaign.description}</p>

                  <div className="space-y-3 mb-4">
                    <div className="flex items-center space-x-2 text-gray-700">
                      <DollarSign size={18} className="text-purple-600" />
                      <span className="text-sm font-medium">${campaign.budget?.toLocaleString()}</span>
                    </div>

                    <div className="flex items-center space-x-2 text-gray-700">
                      <Target size={18} className="text-purple-600" />
                      <span className="text-sm">{campaign.objectives}</span>
                    </div>

                    <div className="flex items-center space-x-2 text-gray-700">
                      <Calendar size={18} className="text-purple-600" />
                      <span className="text-sm">
                        {new Date(campaign.start).toLocaleDateString()} -{' '}
                        {new Date(campaign.end).toLocaleDateString()}
                      </span>
                    </div>
                  </div>

                  <div className="flex items-center justify-between pt-4 border-t border-gray-100">
                    <span
                      className={`px-3 py-1 rounded-full text-xs font-medium border capitalize ${getStatusColor(
                        campaign.status
                      )}`}
                    >
                      {campaign.status}
                    </span>
                    <button className="text-purple-600 hover:text-purple-700 font-medium text-sm">
                      View →
                    </button>
                  </div>
                </div>
              </div>
            ))
          ) : (
            <div className="col-span-full text-center py-12">
              <p className="text-gray-500 text-lg">No campaigns found</p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
