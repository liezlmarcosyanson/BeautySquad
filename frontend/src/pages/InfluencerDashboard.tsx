import React, { useState, useEffect } from 'react';
import { useAuth } from '../context/AuthContext';
import { contentSubmissionService, metricsService } from '../services/api';
import { BarChart3, Send, CheckCircle, AlertCircle, TrendingUp, Plus } from 'lucide-react';

export function InfluencerDashboard() {
  const { user } = useAuth();
  const [submissions, setSubmissions] = useState([]);
  const [metrics, setMetrics] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [showNewSubmission, setShowNewSubmission] = useState(false);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [submissionsData, metricsData] = await Promise.all([
        contentSubmissionService.getAll(),
        metricsService.getAll(),
      ]);
      setSubmissions(submissionsData?.filter((s: any) => s.influencerId === user?.id) || []);
      setMetrics(metricsData || []);
    } catch (err) {
      console.error('Failed to load data', err);
    } finally {
      setIsLoading(false);
    }
  };

  const pendingCount = submissions.filter((s: any) => s.status === 'pending').length;
  const approvedCount = submissions.filter((s: any) => s.status === 'approved').length;

  return (
    <div className="min-h-screen bg-gray-50 py-8 px-4">
      <div className="max-w-7xl mx-auto">
        {/* Header */}
        <div className="mb-8">
          <h1 className="text-4xl font-bold text-gray-900 mb-2">
            Welcome back, {user?.firstName}!
          </h1>
          <p className="text-gray-600">Manage your campaigns and submissions</p>
        </div>

        {/* Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6 mb-8">
          {[
            {
              label: 'Total Submissions',
              value: submissions.length,
              icon: Send,
              color: 'purple',
            },
            {
              label: 'Pending Review',
              value: pendingCount,
              icon: AlertCircle,
              color: 'yellow',
            },
            {
              label: 'Approved',
              value: approvedCount,
              icon: CheckCircle,
              color: 'green',
            },
            {
              label: 'Total Engagement',
              value: `${(Math.random() * 100).toFixed(1)}%`,
              icon: TrendingUp,
              color: 'blue',
            },
          ].map((stat, idx) => {
            const Icon = stat.icon;
            const bgColor = {
              purple: 'bg-purple-50',
              yellow: 'bg-yellow-50',
              green: 'bg-green-50',
              blue: 'bg-blue-50',
            }[stat.color];
            const iconColor = {
              purple: 'text-purple-600',
              yellow: 'text-yellow-600',
              green: 'text-green-600',
              blue: 'text-blue-600',
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
            onClick={() => setShowNewSubmission(true)}
            className="inline-flex items-center space-x-2 bg-purple-600 text-white px-6 py-3 rounded-lg font-medium hover:bg-purple-700 transition"
          >
            <Plus size={20} />
            <span>New Submission</span>
          </button>
        </div>

        {/* Recent Submissions */}
        <div className="bg-white rounded-xl border border-gray-200 p-6">
          <h2 className="text-xl font-bold text-gray-900 mb-6">Recent Submissions</h2>

          {submissions.length > 0 ? (
            <div className="space-y-4">
              {submissions.slice(0, 5).map((submission: any) => (
                <div
                  key={submission.id}
                  className="flex items-center justify-between p-4 border border-gray-100 rounded-lg hover:bg-gray-50 transition"
                >
                  <div className="flex-1">
                    <p className="font-medium text-gray-900">Campaign {submission.campaignId.slice(0, 8)}</p>
                    <p className="text-sm text-gray-600">
                      {new Date(submission.submittedAt).toLocaleDateString()}
                    </p>
                  </div>
                  <span
                    className={`px-3 py-1 rounded-full text-sm font-medium capitalize ${
                      submission.status === 'approved'
                        ? 'bg-green-50 text-green-700'
                        : submission.status === 'pending'
                        ? 'bg-yellow-50 text-yellow-700'
                        : 'bg-red-50 text-red-700'
                    }`}
                  >
                    {submission.status}
                  </span>
                </div>
              ))}
            </div>
          ) : (
            <p className="text-gray-500 text-center py-8">No submissions yet</p>
          )}
        </div>
      </div>

      {/* New Submission Modal */}
      {showNewSubmission && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-xl max-w-md w-full p-6">
            <h3 className="text-xl font-bold text-gray-900 mb-4">New Submission</h3>
            <div className="space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Campaign</label>
                <select className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500">
                  <option>Select a campaign</option>
                </select>
              </div>
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-2">Content</label>
                <textarea
                  className="w-full px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-purple-500"
                  rows={4}
                  placeholder="Describe your content..."
                />
              </div>
              <div className="flex space-x-4">
                <button
                  onClick={() => setShowNewSubmission(false)}
                  className="flex-1 px-4 py-2 border border-gray-300 text-gray-700 rounded-lg font-medium hover:bg-gray-50"
                >
                  Cancel
                </button>
                <button className="flex-1 px-4 py-2 bg-purple-600 text-white rounded-lg font-medium hover:bg-purple-700">
                  Submit
                </button>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
