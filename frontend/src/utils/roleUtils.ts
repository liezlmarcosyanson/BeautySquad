/**
 * Role and Permission Utility Functions
 * Helper functions for checking access and permissions
 */

import { UserRole, ROLE_PERMISSIONS, DATA_ACCESS, ACCESSIBLE_DASHBOARDS, FEATURE_VISIBILITY, Permission } from '../types/roles';

/**
 * Check if a user has a specific permission
 */
export function hasPermission(userRole: UserRole, permission: Permission): boolean {
  const rolePermissions = ROLE_PERMISSIONS[userRole] || [];
  return rolePermissions.some(
    (p) => p.resource === permission.resource && p.action === permission.action
  );
}

/**
 * Check if a user has any of the given permissions
 */
export function hasAnyPermission(userRole: UserRole, permissions: Permission[]): boolean {
  return permissions.some((permission) => hasPermission(userRole, permission));
}

/**
 * Check if a user has all of the given permissions
 */
export function hasAllPermissions(userRole: UserRole, permissions: Permission[]): boolean {
  return permissions.every((permission) => hasPermission(userRole, permission));
}

/**
 * Get all permissions for a role
 */
export function getRolePermissions(userRole: UserRole): Permission[] {
  return ROLE_PERMISSIONS[userRole] || [];
}

/**
 * Check if user can access a resource
 */
export function canAccessResource(
  userRole: UserRole,
  resource: string,
  scope: 'own' | 'assigned' | 'all' = 'all'
): boolean {
  const accessRules = DATA_ACCESS[userRole];
  if (!accessRules) return false;

  const resourceKey = resource as keyof typeof accessRules;
  const userAccess = accessRules[resourceKey];

  if (userAccess === 'all') return true;
  if (userAccess === 'own' && (scope === 'own' || scope === 'all')) return true;
  if (userAccess === 'assigned' && (scope === 'assigned' || scope === 'all')) return true;

  return false;
}

/**
 * Get the data access scope for a resource
 */
export function getDataAccessScope(userRole: UserRole, resource: string): string {
  const accessRules = DATA_ACCESS[userRole];
  const resourceKey = resource as keyof typeof accessRules;
  return accessRules?.[resourceKey] || 'none';
}

/**
 * Check if user can access a dashboard
 */
export function canAccessDashboard(userRole: UserRole, dashboardPath: string): boolean {
  const accessibleDashboards = ACCESSIBLE_DASHBOARDS[userRole] || [];
  return accessibleDashboards.includes(dashboardPath);
}

/**
 * Get all accessible dashboards for a role
 */
export function getAccessibleDashboards(userRole: UserRole): string[] {
  return ACCESSIBLE_DASHBOARDS[userRole] || [];
}

/**
 * Check if a feature is visible to a role
 */
export function isFeatureVisible(userRole: UserRole, feature: keyof typeof FEATURE_VISIBILITY[UserRole]): boolean {
  const visibility = FEATURE_VISIBILITY[userRole];
  return visibility?.[feature] || false;
}

/**
 * Get all visible features for a role
 */
export function getVisibleFeatures(userRole: UserRole): Partial<typeof FEATURE_VISIBILITY[UserRole]> {
  return FEATURE_VISIBILITY[userRole] || {};
}

/**
 * Check if a user can perform an action
 * Combines role and resource checks
 */
export function canPerformAction(
  userRole: UserRole,
  action: string,
  resource?: string
): boolean {
  // First check if role has permission for action
  const permissions = getRolePermissions(userRole);
  const hasActionPermission = permissions.some(
    (p) => p.action === action && (!resource || p.resource === resource)
  );

  if (!hasActionPermission) return false;

  // Additional resource-based checks
  if (resource) {
    return canAccessResource(userRole, resource);
  }

  return true;
}

/**
 * Authorization middleware
 * Returns a function to check if user can perform an action
 */
export function requirePermission(permission: Permission) {
  return (userRole: UserRole | undefined): boolean => {
    if (!userRole) return false;
    return hasPermission(userRole, permission);
  };
}

/**
 * Get user's primary dashboard based on role
 */
export function getPrimaryDashboard(userRole: UserRole): string {
  const dashboardMap: Record<UserRole, string> = {
    [UserRole.ADMIN]: '/admin',
    [UserRole.BRAND]: '/brand',
    [UserRole.INFLUENCER]: '/influencer',
  };

  return dashboardMap[userRole] || '/';
}

/**
 * Check if action requires approval
 */
export function requiresApproval(action: string): boolean {
  const actionsRequiringApproval = [
    'SUBMIT_CONTENT',
    'PUBLISH_CAMPAIGN',
    'MODIFY_DELIVERABLES',
  ];

  return actionsRequiringApproval.includes(action);
}

/**
 * Get approval authority for an action
 */
export function getApprovalAuthority(action: string): UserRole | null {
  const approvalMap: Record<string, UserRole> = {
    SUBMIT_CONTENT: UserRole.BRAND,
    PUBLISH_CAMPAIGN: UserRole.ADMIN,
    MODIFY_DELIVERABLES: UserRole.BRAND,
  };

  return approvalMap[action] || null;
}

/**
 * Get role color for UI display
 */
export function getRoleColor(userRole: UserRole): string {
  const colorMap: Record<UserRole, string> = {
    [UserRole.ADMIN]: 'red',
    [UserRole.BRAND]: 'blue',
    [UserRole.INFLUENCER]: 'purple',
  };

  return colorMap[userRole] || 'gray';
}

/**
 * Get role icon for UI display
 */
export function getRoleIcon(userRole: UserRole): string {
  const iconMap: Record<UserRole, string> = {
    [UserRole.ADMIN]: 'shield',
    [UserRole.BRAND]: 'briefcase',
    [UserRole.INFLUENCER]: 'sparkles',
  };

  return iconMap[userRole] || 'user';
}

/**
 * Validate role assignment permissions
 * Check if source role can assign target role
 */
export function canAssignRole(sourceRole: UserRole, targetRole: UserRole): boolean {
  // Only admin can assign roles
  if (sourceRole !== UserRole.ADMIN) return false;

  // Admin can assign any role
  return true;
}

/**
 * Get action description for audit trail
 */
export function getActionDescription(action: string, details?: Record<string, any>): string {
  const actionMap: Record<string, string> = {
    CREATE_USER: `Created user ${details?.userName || ''}`,
    DELETE_USER: `Deleted user ${details?.userName || ''}`,
    ASSIGN_ROLE: `Assigned ${details?.role || ''} role to ${details?.userName || ''}`,
    CREATE_CAMPAIGN: `Created campaign "${details?.campaignName || ''}"`,
    EDIT_CAMPAIGN: `Edited campaign "${details?.campaignName || ''}"`,
    DELETE_CAMPAIGN: `Deleted campaign "${details?.campaignName || ''}"`,
    SUBMIT_CONTENT: `Submitted content for "${details?.campaignName || ''}"`,
    APPROVE_CONTENT: `Approved content from ${details?.influencerName || ''}`,
    REJECT_CONTENT: `Rejected content from ${details?.influencerName || ''}`,
    UPLOAD_VERSION: `Uploaded revised content version`,
  };

  return actionMap[action] || action;
}
