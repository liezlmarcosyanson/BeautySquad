import React from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import { UserRole } from '../types/roles';

interface ProtectedRouteProps {
  children: React.ReactNode;
  requiredRole?: UserRole | UserRole[];
}

export function ProtectedRoute({ children, requiredRole }: ProtectedRouteProps) {
  const { isAuthenticated, user, isLoading, role } = useAuth();

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-purple-600"></div>
      </div>
    );
  }

  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  // Check role-based access
  if (requiredRole) {
    const allowedRoles = Array.isArray(requiredRole) ? requiredRole : [requiredRole];
    if (!role || !allowedRoles.includes(role)) {
      // Redirect to appropriate dashboard based on user's actual role
      const roleRedirects: Record<UserRole, string> = {
        [UserRole.ADMIN]: '/admin',
        [UserRole.BRAND]: '/brand',
        [UserRole.INFLUENCER]: '/influencer',
      };
      return <Navigate to={role ? roleRedirects[role] : '/'} replace />;
    }
  }

  return <>{children}</>;
}
