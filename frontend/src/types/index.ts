export const API_BASE_URL = 'http://localhost:9000/api';

export interface LoginRequest {
  email: string;
  password: string;
}

export interface AuthResponse {
  token: string;
  userId: string;
  email: string;
  role: string;
}

export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: string;
}

export interface Campaign {
  id: string;
  title: string;
  description: string;
  budget: number;
  objectives: string;
  start: string;
  end: string;
  status: string;
  brandId: string;
}

export interface Influencer {
  id: string;
  name: string;
  bio: string;
  followerCount: number;
  engagementRate: number;
  category: string;
  tags: string[];
}

export interface ContentSubmission {
  id: string;
  campaignId: string;
  influencerId: string;
  status: string;
  content: string;
  submittedAt: string;
}

export interface Approval {
  id: string;
  contentSubmissionId: string;
  status: string;
  feedback: string;
  reviewedAt: string;
}

export interface PerformanceMetrics {
  id: string;
  campaignId: string;
  impressions: number;
  clicks: number;
  conversions: number;
  engagement: number;
}
