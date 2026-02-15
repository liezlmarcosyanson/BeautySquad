# Role-Based Access Control (RBAC) System

## Overview

The BeautySquad platform implements a comprehensive role-based access control system with three distinct roles, each with specific permissions, data access rules, and UI features.

## Roles

### 1. **Admin** 🛡️ (Red Badge)
**Purpose**: Platform governance, security, and tenant-level configuration.

**One-Line Summary**: *Runs the platform*

#### Responsibilities
- Own overall platform configuration and governance
- Manage users, roles, and access control
- Ensure data security, auditability, and compliance readiness
- Maintain system-level settings and environments

#### Permissions
- ✅ Create, edit, deactivate users
- ✅ Assign and manage roles (Admin, Brand, Influencer)
- ✅ View system-wide audit logs
- ✅ Configure platform settings (data retention, feature toggles)
- ✅ Restore or permanently archive soft-deleted records

#### Data Access
- **Campaigns**: All (unrestricted across all brands)
- **Influencers**: All (unrestricted)
- **Content Submissions**: All (unrestricted)
- **Users**: All (unrestricted)
- **Audit Logs**: All (full visibility)
- **Performance Metrics**: All (unrestricted)

#### Dashboard Access
- Home (`/`)
- Admin Console (`/admin`)
- Campaigns (`/campaigns`)
- Influencers (`/influencers`)

#### Visible Features
- User Management
- Platform Settings
- Audit Logs
- System Metrics

---

### 2. **Brand** 💼 (Blue Badge)
**Purpose**: Own brand strategy, campaign execution, and content governance.

**One-Line Summary**: *Runs campaigns and approvals*

#### Responsibilities
- Define brand advocacy strategy and guidelines
- Create and manage influencer campaigns
- Approve or reject influencer content
- Ensure brand safety and message consistency
- Monitor campaign and influencer performance

#### Permissions
- ✅ Create, edit, and archive campaigns
- ✅ Define campaign briefs, deliverables, and timelines
- ✅ Assign influencers to campaigns
- ✅ Review, approve, or reject submitted content
- ✅ Create and edit influencer profiles (CRM)
- ✅ View analytics across brand campaigns

#### Data Access
- **Campaigns**: Own (limited to brand's campaigns)
- **Influencers**: Own (brand's CRM contacts)
- **Content Submissions**: Own (submissions for brand's campaigns)
- **Users**: None (cannot manage users)
- **Audit Logs**: Own (only their actions)
- **Performance Metrics**: Own (brand-level only)

#### Dashboard Access
- Home (`/`)
- Brand Dashboard (`/brand`)
- Campaigns (`/campaigns`)
- Influencers (`/influencers`)

#### Visible Features
- Campaign Creation
- Content Approval Workflows
- Influencer Management (CRM)
- Brand Analytics

---

### 3. **Influencer** ✨ (Purple Badge)
**Purpose**: Create authentic content and track personal performance.

**One-Line Summary**: *Creates content and drives advocacy*

#### Responsibilities
- Participate in assigned campaigns
- Create and submit content for review
- Update content versions based on feedback
- Monitor own performance and engagement metrics

#### Permissions
- ✅ View assigned campaigns and deliverables
- ✅ Create and edit own content drafts
- ✅ Submit content for approval
- ✅ Upload revised content versions
- ✅ View own performance analytics

#### Data Access
- **Campaigns**: Assigned (only campaigns they're assigned to)
- **Influencers**: Own (their profile only)
- **Content Submissions**: Own (their submissions only)
- **Users**: None (no user management)
- **Audit Logs**: None (no audit visibility)
- **Performance Metrics**: Own (personal metrics only)

#### Dashboard Access
- Home (`/`)
- Influencer Dashboard (`/influencer`)
- Campaigns (`/campaigns`)
- Influencers (`/influencers`)

#### Visible Features
- Content Submission
- Content Drafting & Versioning
- Personal Analytics

---

## Approval & Workflow Summary

| Action | Initiated By | Approved By | Who Can See |
|--------|-------------|-------------|------------|
| **Campaign creation** | Brand | — | Brand, Admin |
| **Content draft creation** | Influencer | — | Influencer, Assigned Brand, Admin |
| **Content submission** | Influencer | — | Influencer, Campaign Brand, Admin |
| **Content approval/rejection** | — | Brand | Influencer, Brand, Admin |
| **Platform user setup** | Admin | — | Admin |

---

## Key Governance Principles

### 1. **Least Privilege**
- Influencers access **only their own data** and assigned campaigns
- Brands access **only their own campaigns and content**
- Admin has unrestricted access for governance

### 2. **Single Approval Authority**
- **Brand role owns all content approvals**
- Influencers cannot self-approve
- Only brands can publish approved content

### 3. **Auditability**
- All approvals, edits, and deletions are logged
- Admin can view complete audit trail
- Soft deletes retain historical data

### 4. **Soft Deletes**
- Records are archived, not permanently removed
- Only Admin can permanently delete
- Historical data remains accessible

---

## Implementation Guide

### Using Permission Guards in Components

```tsx
import { RoleGuard, PermissionGuard } from '../components/PermissionGuard';
import { PERMISSIONS } from '../types/roles';

// Guard by role
<RoleGuard allowedRoles={[UserRole.BRAND]}>
  <CreateCampaignButton />
</RoleGuard>

// Guard by permission
<PermissionGuard permission={PERMISSIONS.APPROVE_CONTENT}>
  <ApprovalPanel />
</PermissionGuard>

// Guard by resource access
<ResourceAccessGuard resource="campaigns" scope="own">
  <BrandCampaigns />
</ResourceAccessGuard>
```

### Using Permission Hooks

```tsx
import { usePermission, useRoleInfo, useCanPerform } from '../hooks/usePermission';
import { PERMISSIONS } from '../types/roles';

function MyComponent() {
  const { hasPermission } = usePermission();
  const { isAdmin, isBrand, isInfluencer } = useRoleInfo();
  const { canApproveContent, canCreateCampaign } = useCanPerform();

  return (
    <>
      {canCreateCampaign && <CreateCampaignButton />}
      {canApproveContent && <ApprovalQueue />}
    </>
  );
}
```

### Using Role Utils

```tsx
import { 
  hasPermission,
  canAccessResource,
  getPrimaryDashboard,
  getRoleColor,
  getRoleIcon
} from '../utils/roleUtils';
import { UserRole, PERMISSIONS } from '../types/roles';

// Check permissions
if (hasPermission(UserRole.BRAND, PERMISSIONS.APPROVE_CONTENT)) {
  // Show approval UI
}

// Check resource access
if (canAccessResource(UserRole.INFLUENCER, 'campaigns', 'assigned')) {
  // Show assigned campaigns
}

// Get role info for UI
const dashboard = getPrimaryDashboard(UserRole.BRAND); // '/brand'
const color = getRoleColor(UserRole.ADMIN); // 'red'
```

---

## Data Isolation

### Campaign Access

| Role | Access |
|------|--------|
| **Admin** | All campaigns, all brands |
| **Brand** | Only own campaigns |
| **Influencer** | Only assigned campaigns |

### Content Access

| Role | Access |
|------|--------|
| **Admin** | All submissions, all brands |
| **Brand** | Submissions for own campaigns |
| **Influencer** | Only own submissions |

### User Access

| Role | Can See | Can Edit |
|------|---------|----------|
| **Admin** | All users | Yes |
| **Brand** | Own team only | No |
| **Influencer** | Own profile | Limited |

---

## Feature Visibility by Role

### Admin Dashboard Features
- ✅ User Management
- ✅ Platform Settings
- ✅ Audit Logs
- ✅ System Metrics
- ❌ Campaign Creation
- ❌ Content Approval
- ❌ Content Submission

### Brand Dashboard Features
- ❌ User Management
- ❌ Platform Settings
- ❌ Audit Logs
- ✅ Campaign Creation
- ✅ Content Approval
- ✅ Influencer Management
- ❌ Content Submission

### Influencer Dashboard Features
- ❌ User Management
- ❌ Platform Settings
- ❌ Audit Logs
- ❌ Campaign Creation
- ❌ Content Approval
- ✅ Content Submission
- ✅ Draft Management
- ✅ Performance Tracking

---

## Components & Hooks Reference

### Guard Components
- `<PermissionGuard>` - Guard by permission
- `<RoleGuard>` - Guard by role
- `<ResourceAccessGuard>` - Guard by resource access
- `<ProtectedRoute>` - Route-level protection

### Display Components
- `<RoleBadge>` - Shows role badge with styling
- `<RoleInfoCard>` - Shows detailed role information
- `<RoleList>` - Lists all roles for selection
- `<PermissionList>` - Lists permissions for a role

### Hooks
- `usePermission()` - Check individual permissions
- `useResourceAccess()` - Check resource access
- `useRoleInfo()` - Get role information
- `useDashboardAccess()` - Check dashboard access
- `useFeatureAccess()` - Check feature visibility
- `useCanPerform()` - Check specific action capability

### Utilities (roleUtils.ts)
- `hasPermission()` - Check single permission
- `hasAllPermissions()` - Check multiple permissions
- `canAccessResource()` - Check resource access
- `canPerformAction()` - Check action capability
- `getPrimaryDashboard()` - Get role's main dashboard
- `getRoleColor()` - Get role color for UI
- `getRoleIcon()` - Get role icon

---

## API Integration

All API requests include role-level authorization:

```ts
// Backend enforces permissions
GET /api/campaigns - Admin: all, Brand: own, Influencer: assigned
GET /api/content-submissions - Admin: all, Brand: own, Influencer: own
POST /api/campaigns - Brand only
PUT /api/approvals - Brand only
```

---

## Audit Trail

All actions are logged with:
- User ID
- Action type
- Resource ID
- Timestamp
- IP Address (backend)
- Status (success/failure)

Accessible to: **Admin only**

---

## Security Best Practices

1. **Always verify permissions on backend** - Frontend guards are for UX only
2. **Use role-based queries** - Filter API responses by role
3. **Log all sensitive actions** - Maintain complete audit trail
4. **Soft delete for compliance** - Never permanently remove data without approval
5. **Token expiration** - Tokens expire, requiring re-authentication
6. **Data isolation** - Each brand's data is completely isolated

---

## Testing RBAC

### Test Scenarios

**Admin User**
- ✅ Can access `/admin` dashboard
- ✅ Can view all users
- ✅ Can view all campaigns
- ✅ Can view audit logs
- ✅ Cannot create campaigns
- ✅ Cannot submit content

**Brand User**
- ✅ Can access `/brand` dashboard
- ✅ Can create campaigns
- ✅ Can approve content
- ✅ Can view own campaigns
- ❌ Cannot access `/admin`
- ❌ Cannot view other brands' campaigns

**Influencer User**
- ✅ Can access `/influencer` dashboard
- ✅ Can submit content
- ✅ Can see assigned campaigns
- ✅ Can view own performance
- ❌ Cannot access `/admin` or `/brand`
- ❌ Cannot approve content
- ❌ Cannot create campaigns

---

## File Structure

```
src/
├── types/
│   ├── roles.ts              # Role definitions & permissions
│   └── index.ts              # User & auth types
├── utils/
│   └── roleUtils.ts          # Permission checking utilities
├── hooks/
│   └── usePermission.ts      # Custom permission hooks
├── components/
│   ├── PermissionGuard.tsx   # Guard components
│   ├── RoleBadge.tsx         # Role display components
│   ├── ProtectedRoute.tsx    # Route protection (updated)
│   └── Navbar.tsx            # (uses role for display)
├── pages/
│   ├── AdminDashboard.tsx    # Admin only
│   ├── BrandDashboard.tsx    # Brand only
│   ├── InfluencerDashboard.tsx # Influencer only
│   └── ...                   # Public pages
└── context/
    └── AuthContext.tsx       # Auth + role context
```

---

## Frequently Asked Questions

### Q: Can an influencer see other influencers' metrics?
**A**: No. Influencers can only see their own profile and performance metrics.

### Q: Can a brand see other brands' campaigns?
**A**: No. Brands are completely isolated; each brand sees only their own data.

### Q: Who can delete campaigns?
**A**: Brands can archive (soft delete) their own campaigns. Only Admin can permanently delete.

### Q: What happens if a brand is deleted?
**A**: The brand record is soft-deleted. Campaigns and content remain but are archived. Admin can restore if needed.

### Q: Can influencers change their role?
**A**: No. Only Admin can assign or change roles.

### Q: Are all actions logged?
**A**: Yes. All approvals, edits, and deletions are logged in the audit trail (Admin-only visibility).

---

**Last Updated**: February 14, 2026
**Version**: 1.0
**Status**: Production Ready
