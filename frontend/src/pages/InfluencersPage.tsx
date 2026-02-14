import React, { useState, useEffect } from 'react';
import { influencerService } from '../services/api';
import { Influencer } from '../types';
import { Users, TrendingUp, Tag, Loader, ShoppingBag } from 'lucide-react';

export function InfluencersPage() {
  const [influencers, setInfluencers] = useState<Influencer[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [selectedCategory, setSelectedCategory] = useState('all');

  useEffect(() => {
    loadInfluencers();
  }, []);

  const loadInfluencers = async () => {
    try {
      const data = await influencerService.getAll();
      setInfluencers(data);
    } catch (err) {
      console.error('Failed to load influencers', err);
    } finally {
      setIsLoading(false);
    }
  };

  const categories = ['all', 'fashion', 'beauty', 'lifestyle', 'fitness', 'travel'];
  const filteredInfluencers = influencers.filter(
    (i) => selectedCategory === 'all' || i.category === selectedCategory
  );

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
          <h1 className="text-4xl font-bold text-gray-900 mb-2">Influencers</h1>
          <p className="text-gray-600">Discover talented creators to collaborate with</p>
        </div>

        <div className="flex flex-wrap gap-2 mb-8 overflow-x-auto">
          {categories.map((cat) => (
            <button
              key={cat}
              onClick={() => setSelectedCategory(cat)}
              className={`px-4 py-2 rounded-full font-medium transition whitespace-nowrap capitalize ${
                selectedCategory === cat
                  ? 'bg-purple-600 text-white'
                  : 'bg-white text-gray-700 border border-gray-300 hover:border-purple-600'
              }`}
            >
              {cat}
            </button>
          ))}
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {filteredInfluencers.length > 0 ? (
            filteredInfluencers.map((influencer) => (
              <div
                key={influencer.id}
                className="bg-white rounded-xl shadow-sm hover:shadow-lg transition border border-gray-200 overflow-hidden group"
              >
                <div className="bg-gradient-to-br from-purple-400 to-purple-600 h-32 flex items-end justify-center relative">
                  <div className="absolute inset-0 bg-gradient-to-t from-purple-900/20 to-transparent"></div>
                </div>

                <div className="p-6 text-center">
                  <h3 className="font-bold text-lg text-gray-900 mb-1">{influencer.name}</h3>
                  <p className="text-purple-600 text-sm font-medium capitalize mb-3">{influencer.category}</p>

                  <p className="text-gray-600 text-sm mb-4 h-10 line-clamp-2">{influencer.bio}</p>

                  <div className="space-y-2 mb-4 pt-4 border-t border-gray-100">
                    <div className="flex items-center justify-center space-x-2 text-gray-700">
                      <Users size={16} className="text-purple-600" />
                      <span className="text-sm font-medium">
                        {(influencer.followerCount / 1000000).toFixed(1)}M followers
                      </span>
                    </div>

                    <div className="flex items-center justify-center space-x-2 text-gray-700">
                      <TrendingUp size={16} className="text-purple-600" />
                      <span className="text-sm font-medium">{(influencer.engagementRate * 100).toFixed(1)}% engagement</span>
                    </div>
                  </div>

                  {influencer.tags && influencer.tags.length > 0 && (
                    <div className="flex flex-wrap gap-1 justify-center mb-4">
                      {influencer.tags.slice(0, 2).map((tag) => (
                        <span
                          key={tag}
                          className="inline-flex items-center space-x-1 bg-purple-50 text-purple-700 px-2 py-1 rounded-full text-xs"
                        >
                          <Tag size={12} />
                          <span>{tag}</span>
                        </span>
                      ))}
                    </div>
                  )}

                  <button className="w-full bg-purple-600 text-white py-2 rounded-lg font-medium hover:bg-purple-700 transition flex items-center justify-center space-x-2">
                    <ShoppingBag size={16} />
                    <span>Collaborate</span>
                  </button>
                </div>
              </div>
            ))
          ) : (
            <div className="col-span-full text-center py-12">
              <p className="text-gray-500 text-lg">No influencers found</p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
