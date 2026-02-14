import axios, { AxiosInstance } from 'axios';
import { API_BASE_URL, LoginRequest, AuthResponse } from '../types';

const client: AxiosInstance = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

const getToken = () => localStorage.getItem('auth_token');

client.interceptors.request.use((config) => {
  const token = getToken();
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const authService = {
  login: async (email: string, password: string): Promise<AuthResponse> => {
    const response = await client.post('/auth/login', { email, password });
    return response.data;
  },

  logout: () => {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('user');
  },

  register: async (data: any): Promise<AuthResponse> => {
    const response = await client.post('/auth/register', data);
    return response.data;
  },
};

export const campaignService = {
  getAll: async () => {
    const response = await client.get('/campaigns');
    return response.data;
  },

  getById: async (id: string) => {
    const response = await client.get(`/campaigns/${id}`);
    return response.data;
  },

  create: async (data: any) => {
    const response = await client.post('/campaigns', data);
    return response.data;
  },

  update: async (id: string, data: any) => {
    const response = await client.put(`/campaigns/${id}`, data);
    return response.data;
  },

  delete: async (id: string) => {
    await client.delete(`/campaigns/${id}`);
  },
};

export const influencerService = {
  getAll: async () => {
    const response = await client.get('/influencers');
    return response.data;
  },

  getById: async (id: string) => {
    const response = await client.get(`/influencers/${id}`);
    return response.data;
  },

  create: async (data: any) => {
    const response = await client.post('/influencers', data);
    return response.data;
  },

  update: async (id: string, data: any) => {
    const response = await client.put(`/influencers/${id}`, data);
    return response.data;
  },

  delete: async (id: string) => {
    await client.delete(`/influencers/${id}`);
  },
};

export const contentSubmissionService = {
  getAll: async () => {
    const response = await client.get('/content-submissions');
    return response.data;
  },

  getById: async (id: string) => {
    const response = await client.get(`/content-submissions/${id}`);
    return response.data;
  },

  create: async (data: any) => {
    const response = await client.post('/content-submissions', data);
    return response.data;
  },

  update: async (id: string, data: any) => {
    const response = await client.put(`/content-submissions/${id}`, data);
    return response.data;
  },
};

export const approvalService = {
  getAll: async () => {
    const response = await client.get('/approvals');
    return response.data;
  },

  create: async (data: any) => {
    const response = await client.post('/approvals', data);
    return response.data;
  },
};

export const metricsService = {
  getAll: async () => {
    const response = await client.get('/performance-metrics');
    return response.data;
  },

  create: async (data: any) => {
    const response = await client.post('/performance-metrics', data);
    return response.data;
  },
};

export default client;
