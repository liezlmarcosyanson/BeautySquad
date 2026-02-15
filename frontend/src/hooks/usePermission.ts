/**
 * Custom hooks for role-based access control
 */

import { useAuth } from '../context/AuthContext';
import { UserRole, Permission } from '../types/roles';
import {
  hasPermission as checkPermission,
  canAccessResource as checkResourceAccess,
  canAccessDashboard as checkDashboardAccess,
  isFeatureVisible as checkFeatureVisible,
  getPrimaryDashboard,
} from '../utils/roleUtils';

/**
 * usePermission Hook
 * Check if current user has specific permission(s)
 * 
 * Usage:
 * const { hasPermission, hasAllPermissions } = usePermission();
 * if (hasPermission(PERMISSIONS.APPROVE_CONTENT)) { ... }
 */
export function usePermission() {
  const { role } = useAuth();

  return {
    hasPermission: (permission: Permission) => {
      if (!role) return false;
      return checkPermission(role, permission);
    },
    hasMultiple: (permissions: Permission[]) => {
      if (!role) return false;
      return permissions.some((p) => checkPermission(role, p));
    },
    hasAllPermissions: (permissions: Permission[]) => {
      if (!role) return false;
      return permissions.every((p) => checkPermission(role, p));
    },
  };
}

/**
 * useResourceAccess Hook
 * Check resource access levels
 * 
 * Usage:
 * const { canAccess } = useResourceAccess();
 * if (canAccess('campaigns', 'own')) { ... }
 */
export function useResourceAccess() {
  const { role } = useAuth();

  return {
    canAccess: (resource: string, scope: 'own' | 'assigned' | 'all' = 'all') => {
      if (!role) return false;
      return checkResourceAccess(role, resource, scope);
    },
    canAccessOwn: (resource: string) => {
      if (!role) return false;
      return checkResourceAccess(role, resource, 'own');
    },
    canAccessAssigned: (resource: string) => {
      if (!role) return false;
      return checkResourceAccess(role, resource, 'assigned');
    },
    canAccessAll: (resource: string) => {
      if (!role) return false;
      return checkResourceAccess(role, resource, 'all');
    },
  };
}

/**
 * useRoleInfo Hook
 * Get information about current user's role
 * 
 * Usage:
 * const { isAdmin, isBrand, isInfluencer, primaryDashboard } = useRoleInfo();
 */
export function useRoleInfo() {
  const { role } = useAuth();

  return {
    role,
    isAdmin: role === UserRole.ADMIN,
    isBrand: role === UserRole.BRAND,
    isInfluencer: role === UserRole.INFLUENCER,
    primaryDashboard: role ? getPrimaryDashboard(role) : '/',
  };
}

/**
 * useDashboardAccess Hook
 * Check dashboard access and permissions
 * 
 * Usage:
 * const { canAccessDashboard } = useDashboardAccess();
 * if (canAccessDashboard('/admin')) { ... }
 */
export function useDashboardAccess() {
  const { role } = useAuth();

  return {
    canAccessDashboard: (path: string) => {
      if (!role) return false;
      return checkDashboardAccess(role, path);
    },
  };
}

/**
 * useFeatureAccess Hook
 * Check if features should be visible
 * 
 * Usage:
 * const { isVisible } = useFeatureAccess();
 * if (isVisible('contentApproval')) { ... }
 */
export function useFeatureAccess() {
  const { role } = useAuth();

  return {
    isVisible: (feature: string) => {
      if (!role) return false;
      return checkFeatureVisible(role, feature as any);
    },
  };
}

/**
 * useCanPerform Hook
 * Higher-level hook to check if user can perform specific actions
 * 
 * Usage:
 * const { canApproveContent, canCreateCampaign } = useCanPerform();
 */
export function useCanPerform() {
  const { role } = useAuth();

  return {
    canApproveContent: role === UserRole.BRAND,
    canCreateCampaign: role === UserRole.BRAND,
    canSubmitContent: role === UserRole.INFLUENCER,
    canManageUsers: role === UserRole.ADMIN,
    canManageRoles: role === UserRole.ADMIN,
    canViewAuditLogs: role === UserRole.ADMIN,
    canConfigurePlatform: role === UserRole.ADMIN,
  };
}
