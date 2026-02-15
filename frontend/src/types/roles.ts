/**
 * Role-Based Access Control (RBAC) System
 * Defines all roles, permissions, and access controls for BeautySquad
 */

export enum UserRole {
  ADMIN = 'admin',
  BRAND = 'brand',
  INFLUENCER = 'influencer',
}

/**
 * Permission definitions for each role
 * Follows the principle of least privilege
 */

export interface Permission {
  resource: string;
  action: string;
  description: string;
}

export const PERMISSIONS = {
  // Platform Management (Admin Only)
  MANAGE_USERS: { resource: 'users', action: 'manage', description: 'Create, edit, deactivate users' },
  MANAGE_ROLES: { resource: 'roles', action: 'manage', description: 'Assign and manage roles' },
  VIEW_AUDIT_LOGS: { resource: 'auditLogs', action: 'view', description: 'View system-wide audit logs' },
  CONFIGURE_PLATFORM: { resource: 'platform', action: 'configure', description: 'Configure platform settings' },
  RESTORE_DELETED: { resource: 'records', action: 'restore', description: 'Restore soft-deleted records' },

  // Campaign Management (Brand)
  CREATE_CAMPAIGN: { resource: 'campaigns', action: 'create', description: 'Create new campaigns' },
  EDIT_CAMPAIGN: { resource: 'campaigns', action: 'edit', description: 'Edit campaign details' },
  DELETE_CAMPAIGN: { resource: 'campaigns', action: 'delete', description: 'Archive/delete campaigns' },
  ASSIGN_INFLUENCERS: { resource: 'campaigns', action: 'assignInfluencers', description: 'Assign influencers to campaigns' },

  // Content Approval (Brand)
  APPROVE_CONTENT: { resource: 'content', action: 'approve', description: 'Approve influencer content' },
  REJECT_CONTENT: { resource: 'content', action: 'reject', description: 'Reject content submissions' },

  // Brand CRM
  CREATE_INFLUENCER_PROFILE: { resource: 'influencers', action: 'create', description: 'Create brand CRM influencer profiles' },
  EDIT_INFLUENCER_PROFILE: { resource: 'influencers', action: 'edit', description: 'Edit influencer profiles' },

  // Analytics (Brand)
  VIEW_BRAND_ANALYTICS: { resource: 'analytics', action: 'view', description: 'View brand-wide analytics' },
  VIEW_CAMPAIGN_PERFORMANCE: { resource: 'analytics', action: 'viewCampaigns', description: 'View campaign performance' },

  // Content Creation (Influencer)
  CREATE_DRAFT: { resource: 'content', action: 'createDraft', description: 'Create content drafts' },
  SUBMIT_CONTENT: { resource: 'content', action: 'submit', description: 'Submit content for approval' },
  UPLOAD_VERSION: { resource: 'content', action: 'uploadVersion', description: 'Upload revised content versions' },

  // Analytics (Influencer)
  VIEW_OWN_ANALYTICS: { resource: 'analytics', action: 'viewOwn', description: 'View own performance analytics' },
  VIEW_ASSIGNED_CAMPAIGNS: { resource: 'campaigns', action: 'viewAssigned', description: 'View assigned campaigns' },
};

/**
 * Role Definitions with Associated Permissions
 */

export const ROLE_PERMISSIONS: Record<UserRole, Permission[]> = {
  [UserRole.ADMIN]: [
    // Platform governance
    PERMISSIONS.MANAGE_USERS,
    PERMISSIONS.MANAGE_ROLES,
    PERMISSIONS.VIEW_AUDIT_LOGS,
    PERMISSIONS.CONFIGURE_PLATFORM,
    PERMISSIONS.RESTORE_DELETED,
  ],

  [UserRole.BRAND]: [
    // Campaign management
    PERMISSIONS.CREATE_CAMPAIGN,
    PERMISSIONS.EDIT_CAMPAIGN,
    PERMISSIONS.DELETE_CAMPAIGN,
    PERMISSIONS.ASSIGN_INFLUENCERS,

    // Content approval
    PERMISSIONS.APPROVE_CONTENT,
    PERMISSIONS.REJECT_CONTENT,

    // CRM
    PERMISSIONS.CREATE_INFLUENCER_PROFILE,
    PERMISSIONS.EDIT_INFLUENCER_PROFILE,

    // Analytics
    PERMISSIONS.VIEW_BRAND_ANALYTICS,
    PERMISSIONS.VIEW_CAMPAIGN_PERFORMANCE,
  ],

  [UserRole.INFLUENCER]: [
    // Content creation
    PERMISSIONS.CREATE_DRAFT,
    PERMISSIONS.SUBMIT_CONTENT,
    PERMISSIONS.UPLOAD_VERSION,

    // Analytics
    PERMISSIONS.VIEW_OWN_ANALYTICS,
    PERMISSIONS.VIEW_ASSIGNED_CAMPAIGNS,
  ],
};

/**
 * Role Information for UI Display
 */

export const ROLE_INFO: Record<UserRole, RoleInfo> = {
  [UserRole.ADMIN]: {
    displayName: 'Admin',
    description: 'Platform governance and security',
    color: 'red',
    shortSummary: 'Runs the platform',
    icon: 'shield',
  },
  [UserRole.BRAND]: {
    displayName: 'Brand',
    description: 'Campaign execution and content governance',
    color: 'blue',
    shortSummary: 'Runs campaigns and approvals',
    icon: 'briefcase',
  },
  [UserRole.INFLUENCER]: {
    displayName: 'Influencer',
    description: 'Content creation and advocacy',
    color: 'purple',
    shortSummary: 'Creates content and drives advocacy',
    icon: 'sparkles',
  },
};

export interface RoleInfo {
  displayName: string;
  description: string;
  color: 'red' | 'blue' | 'purple' | 'green' | 'yellow' | 'orange';
  shortSummary: string;
  icon: string;
}

/**
 * Data Access Rules
 * Define scope of data each role can access
 */

export const DATA_ACCESS: Record<UserRole, DataAccessScope> = {
  [UserRole.ADMIN]: {
    campaigns: 'all',
    influencers: 'all',
    contentSubmissions: 'all',
    users: 'all',
    auditLogs: 'all',
    performanceMetrics: 'all',
  },
  [UserRole.BRAND]: {
    campaigns: 'own',
    influencers: 'own',
    contentSubmissions: 'own',
    users: 'none',
    auditLogs: 'own',
    performanceMetrics: 'own',
  },
  [UserRole.INFLUENCER]: {
    campaigns: 'assigned',
    influencers: 'own',
    contentSubmissions: 'own',
    users: 'none',
    auditLogs: 'none',
    performanceMetrics: 'own',
  },
};

export interface DataAccessScope {
  campaigns: 'all' | 'own' | 'assigned' | 'none';
  influencers: 'all' | 'own' | 'none';
  contentSubmissions: 'all' | 'own' | 'none';
  users: 'all' | 'none';
  auditLogs: 'all' | 'own' | 'none';
  performanceMetrics: 'all' | 'own' | 'none';
}

/**
 * Dashboard Access Rules
 * Define which dashboards each role can access
 */

export const ACCESSIBLE_DASHBOARDS: Record<UserRole, string[]> = {
  [UserRole.ADMIN]: ['/', '/admin', '/campaigns', '/influencers'],
  [UserRole.BRAND]: ['/', '/brand', '/campaigns', '/influencers'],
  [UserRole.INFLUENCER]: ['/', '/influencer', '/campaigns', '/influencers'],
};

/**
 * Feature Visibility Rules
 * Define which UI features should be shown to each role
 */

export const FEATURE_VISIBILITY: Record<UserRole, FeatureVisibility> = {
  [UserRole.ADMIN]: {
    userManagement: true,
    platformSettings: true,
    auditLogs: true,
    systemMetrics: true,
    campaignCreation: false,
    contentApproval: false,
    contentSubmission: false,
  },
  [UserRole.BRAND]: {
    userManagement: false,
    platformSettings: false,
    auditLogs: false,
    systemMetrics: false,
    campaignCreation: true,
    contentApproval: true,
    contentSubmission: false,
  },
  [UserRole.INFLUENCER]: {
    userManagement: false,
    platformSettings: false,
    auditLogs: false,
    systemMetrics: false,
    campaignCreation: false,
    contentApproval: false,
    contentSubmission: true,
  },
};

export interface FeatureVisibility {
  userManagement: boolean;
  platformSettings: boolean;
  auditLogs: boolean;
  systemMetrics: boolean;
  campaignCreation: boolean;
  contentApproval: boolean;
  contentSubmission: boolean;
}

/**
 * Workflow Ownership Rules
 */

export const WORKFLOW_OWNERSHIP = {
  campaignCreation: UserRole.BRAND,
  contentSubmission: UserRole.INFLUENCER,
  contentApproval: UserRole.BRAND,
  userSetup: UserRole.ADMIN,
};

/**
 * Audit Trail Compliance
 */

export const AUDITABLE_ACTIONS = [
  'CREATE_USER',
  'DELETE_USER',
  'ASSIGN_ROLE',
  'CREATE_CAMPAIGN',
  'EDIT_CAMPAIGN',
  'DELETE_CAMPAIGN',
  'SUBMIT_CONTENT',
  'APPROVE_CONTENT',
  'REJECT_CONTENT',
  'UPLOAD_VERSION',
  'RESTORE_DELETED',
  'UPDATE_SETTINGS',
];
