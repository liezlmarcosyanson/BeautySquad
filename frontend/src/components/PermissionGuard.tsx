/**
 * Permission Guard Component
 * Conditionally renders content based on user permissions
 */

import React from 'react';
import { useAuth } from '../context/AuthContext';
import { Permission, UserRole } from '../types/roles';
import { hasPermission, isFeatureVisible, canPerformAction } from '../utils/roleUtils';

interface PermissionGuardProps {
  permission?: Permission;
  resource?: string;
  action?: string;
  feature?: string;
  role?: UserRole;
  fallback?: React.ReactNode;
  children: React.ReactNode;
}

/**
 * PermissionGuard Component
 * Shows children only if user has the required permission
 * 
 * Usage:
 * <PermissionGuard permission={PERMISSIONS.APPROVE_CONTENT}>
 *   <ApprovalButton />
 * </PermissionGuard>
 */
export function PermissionGuard({
  permission,
  resource,
  action,
  feature,
  role: requiredRole,
  fallback,
  children,
}: PermissionGuardProps) {
  const { role } = useAuth();

  if (!role) return fallback || null;

  // Check by explicit role
  if (requiredRole && role !== requiredRole) {
    return fallback || null;
  }

  // Check by permission
  if (permission && !hasPermission(role, permission)) {
    return fallback || null;
  }

  // Check by action and resource
  if (action && !canPerformAction(role, action, resource)) {
    return fallback || null;
  }

  // Check by feature visibility
  if (feature && !isFeatureVisible(role, feature as any)) {
    return fallback || null;
  }

  return <>{children}</>;
}

interface RoleGuardProps {
  allowedRoles: UserRole[];
  fallback?: React.ReactNode;
  children: React.ReactNode;
}

/**
 * RoleGuard Component
 * Shows children only if user has one of the specified roles
 * 
 * Usage:
 * <RoleGuard allowedRoles={[UserRole.BRAND, UserRole.ADMIN]}>
 *   <CampaignDashboard />
 * </RoleGuard>
 */
export function RoleGuard({ allowedRoles, fallback, children }: RoleGuardProps) {
  const { role } = useAuth();

  if (!role || !allowedRoles.includes(role)) {
    return fallback || null;
  }

  return <>{children}</>;
}

interface ResourceAccessGuardProps {
  resource: string;
  scope?: 'own' | 'assigned' | 'all';
  fallback?: React.ReactNode;
  children: React.ReactNode;
}

/**
 * ResourceAccessGuard Component
 * Shows children only if user can access the specified resource and scope
 * 
 * Usage:
 * <ResourceAccessGuard resource="campaigns" scope="own">
 *   <BrandCampaigns />
 * </ResourceAccessGuard>
 */
export function ResourceAccessGuard({
  resource,
  scope = 'all',
  fallback,
  children,
}: ResourceAccessGuardProps) {
  const { canAccessResource } = useAuth();

  if (!canAccessResource(resource, scope)) {
    return fallback || null;
  }

  return <>{children}</>;
}
