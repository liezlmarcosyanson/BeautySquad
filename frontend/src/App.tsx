import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { Navbar } from './components/Navbar';
import { Footer } from './components/Footer';
import { ProtectedRoute } from './components/ProtectedRoute';
import { UserRole } from './types/roles';
import { HomePage } from './pages/HomePage';
import { LoginPage } from './pages/LoginPage';
import { SignupPage } from './pages/SignupPage';
import { CampaignsPage } from './pages/CampaignsPage';
import { InfluencersPage } from './pages/InfluencersPage';
import { InfluencerDashboard } from './pages/InfluencerDashboard';
import { BrandDashboard } from './pages/BrandDashboard';
import { AdminDashboard } from './pages/AdminDashboard';

function App() {
  return (
    <Router>
      <AuthProvider>
        <div className="flex flex-col min-h-screen">
          <Navbar />
          <main className="flex-grow">
            <Routes>
              {/* Public Routes */}
              <Route path="/" element={<HomePage />} />
              <Route path="/login" element={<LoginPage />} />
              <Route path="/signup" element={<SignupPage />} />
              <Route path="/campaigns" element={<CampaignsPage />} />
              <Route path="/influencers" element={<InfluencersPage />} />

              {/* Protected Routes - Influencer Dashboard */}
              <Route
                path="/influencer"
                element={
                  <ProtectedRoute requiredRole={UserRole.INFLUENCER}>
                    <InfluencerDashboard />
                  </ProtectedRoute>
                }
              />

              {/* Protected Routes - Brand Dashboard */}
              <Route
                path="/brand"
                element={
                  <ProtectedRoute requiredRole={UserRole.BRAND}>
                    <BrandDashboard />
                  </ProtectedRoute>
                }
              />

              {/* Protected Routes - Admin Dashboard */}
              <Route
                path="/admin"
                element={
                  <ProtectedRoute requiredRole={UserRole.ADMIN}>
                    <AdminDashboard />
                  </ProtectedRoute>
                }
              />

              {/* 404 Page */}
              <Route
                path="*"
                element={
                  <div className="flex items-center justify-center min-h-screen">
                    <p className="text-2xl font-bold text-gray-900">Page not found</p>
                  </div>
                }
              />
            </Routes>
          </main>
          <Footer />
        </div>
      </AuthProvider>
    </Router>
  );
}

export default App;
