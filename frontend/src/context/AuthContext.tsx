import React, { createContext, useContext, useState, useEffect } from 'react';
import { User, AuthResponse } from '../types';
import { UserRole, ROLE_PERMISSIONS } from '../types/roles';
import { hasPermission, canAccessResource } from '../utils/roleUtils';

interface AuthContextType {
  user: User | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  role: UserRole | null;
  permissions: ReturnType<typeof ROLE_PERMISSIONS[UserRole]> | [];
  login: (user: User, token: string) => void;
  logout: () => void;
  hasPermission: (permission: any) => boolean;
  canAccessResource: (resource: string, scope?: 'own' | 'assigned' | 'all') => boolean;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [user, setUser] = useState<User | null>(null);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    const token = localStorage.getItem('auth_token');

    if (storedUser && token) {
      setUser(JSON.parse(storedUser));
    }
    setIsLoading(false);
  }, []);

  const role = (user?.role as UserRole) || null;
  const permissions = role ? ROLE_PERMISSIONS[role] : [];

  const login = (userData: User, token: string) => {
    setUser(userData);
    localStorage.setItem('auth_token', token);
    localStorage.setItem('user', JSON.stringify(userData));
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem('auth_token');
    localStorage.removeItem('user');
  };

  const checkPermission = (permission: any) => {
    if (!role) return false;
    return hasPermission(role, permission);
  };

  const checkResourceAccess = (resource: string, scope: 'own' | 'assigned' | 'all' = 'all') => {
    if (!role) return false;
    return canAccessResource(role, resource, scope);
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        isAuthenticated: !!user,
        isLoading,
        role,
        permissions,
        login,
        logout,
        hasPermission: checkPermission,
        canAccessResource: checkResourceAccess,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within AuthProvider');
  }
  return context;
}
