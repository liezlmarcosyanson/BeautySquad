# RBAC Quick Reference for Developers

## 🚀 Quick Start

### Import What You Need

```tsx
// Types
import { UserRole, PERMISSIONS, ROLE_INFO } from '../types/roles';

// Components
import { PermissionGuard, RoleGuard, ResourceAccessGuard } from '../components/PermissionGuard';
import { RoleBadge, RoleInfoCard, PermissionList } from '../components/RoleBadge';
import { ProtectedRoute } from '../components/ProtectedRoute';

// Hooks
import { usePermission, useRoleInfo, useCanPerform, useResourceAccess } from '../hooks/usePermission';

// Utilities
import { 
  hasPermission, 
  canAccessResource, 
  getPrimaryDashboard,
  getRoleColor 
} from '../utils/roleUtils';
```

---

## 📋 Common Patterns

### Pattern 1: Show Content for Specific Role Only

```tsx
import { RoleGuard } from '../components/PermissionGuard';
import { UserRole } from '../types/roles';

function MyComponent() {
  return (
    <>
      <RoleGuard allowedRoles={[UserRole.BRAND]}>
        <CampaignCreationPanel />
      </RoleGuard>
    </>
  );
}
```

### Pattern 2: Show Content if User Has Permission

```tsx
import { PermissionGuard } from '../components/PermissionGuard';
import { PERMISSIONS } from '../types/roles';

function MyComponent() {
  return (
    <>
      <PermissionGuard permission={PERMISSIONS.APPROVE_CONTENT}>
        <ApprovalButton />
      </PermissionGuard>
    </>
  );
}
```

### Pattern 3: Hide Content for Unsupported Roles

```tsx
import { RoleGuard } from '../components/PermissionGuard';
import { UserRole } from '../types/roles';

function MyComponent() {
  return (
    <>
      <RoleGuard 
        allowedRoles={[UserRole.ADMIN, UserRole.BRAND]}
        fallback={<div>You don't have access to this feature</div>}
      >
        <AdvancedAnalytics />
      </RoleGuard>
    </>
  );
}
```

### Pattern 4: Check Permission in Code Logic

```tsx
import { usePermission } from '../hooks/usePermission';
import { PERMISSIONS } from '../types/roles';

function MyComponent() {
  const { hasPermission } = usePermission();

  if (!hasPermission(PERMISSIONS.CREATE_CAMPAIGN)) {
    return <div>You cannot create campaigns</div>;
  }

  return <CampaignForm />;
}
```

### Pattern 5: Get User's Role Info

```tsx
import { useRoleInfo } from '../hooks/usePermission';

function MyComponent() {
  const { role, isAdmin, isBrand, isInfluencer, primaryDashboard } = useRoleInfo();

  return (
    <>
      {isAdmin && <AdminControls />}
      {isBrand && <BrandDashboard />}
      {isInfluencer && <InfluencerDashboard />}
    </>
  );
}
```

### Pattern 6: Check Resource Access

```tsx
import { useResourceAccess } from '../hooks/usePermission';

function MyComponent() {
  const { canAccess, canAccessOwn } = useResourceAccess();

  if (!canAccessOwn('campaigns')) {
    return <div>No access to campaigns</div>;
  }

  return <CampaignList />;
}
```

### Pattern 7: Display Role Information

```tsx
import { RoleBadge, RoleInfoCard } from '../components/RoleBadge';
import { useRoleInfo } from '../hooks/usePermission';

function MyComponent() {
  const { role } = useRoleInfo();

  return (
    <>
      {/* Simple badge */}
      <RoleBadge role={role} size="md" />

      {/* Detailed card */}
      <RoleInfoCard role={role} showDescription={true} />
    </>
  );
}
```

### Pattern 8: Protect Routes

```tsx
// In App.tsx
import { ProtectedRoute } from './components/ProtectedRoute';
import { UserRole } from './types/roles';

<Route
  path="/admin"
  element={
    <ProtectedRoute requiredRole={UserRole.ADMIN}>
      <AdminDashboard />
    </ProtectedRoute>
  }
/>
```

### Pattern 9: Check Multiple Roles

```tsx
import { RoleGuard } from '../components/PermissionGuard';
import { UserRole } from '../types/roles';

function MyComponent() {
  return (
    <>
      <RoleGuard allowedRoles={[UserRole.ADMIN, UserRole.BRAND]}>
        <AdminAndBrandOnlyContent />
      </RoleGuard>
    </>
  );
}
```

### Pattern 10: Quick Permission Check

```tsx
import { useCanPerform } from '../hooks/usePermission';

function MyComponent() {
  const { canApproveContent, canCreateCampaign, canSubmitContent } = useCanPerform();

  return (
    <>
      {canCreateCampaign && <CreateCampaignBtn />}
      {canApproveContent && <ApprovalQueueBtn />}
      {canSubmitContent && <SubmitContentBtn />}
    </>
  );
}
```

---

## 🎯 Role Summary Card

### Admin 🛡️
```
Role: admin
Color: red
Motto: "Runs the platform"
Dashboard: /admin
Main Actions: 
  ✓ Manage users
  ✓ View audit logs
  ✓ Configure platform
  ✗ Cannot create campaigns
  ✗ Cannot approve content
```

### Brand 💼
```
Role: brand
Color: blue
Motto: "Runs campaigns and approvals"
Dashboard: /brand
Main Actions:
  ✓ Create campaigns
  ✓ Approve content
  ✓ Manage influencers
  ✗ Cannot manage users
  ✗ Cannot submit content
```

### Influencer ✨
```
Role: influencer
Color: purple
Motto: "Creates content and drives advocacy"
Dashboard: /influencer
Main Actions:
  ✓ Submit content
  ✓ View assigned campaigns
  ✓ Track metrics
  ✗ Cannot create campaigns
  ✗ Cannot approve content
```

---

## 📊 Permission Matrix

### Admin Permissions
```
✓ MANAGE_USERS
✓ MANAGE_ROLES
✓ VIEW_AUDIT_LOGS
✓ CONFIGURE_PLATFORM
✓ RESTORE_DELETED
```

### Brand Permissions
```
✓ CREATE_CAMPAIGN
✓ EDIT_CAMPAIGN
✓ DELETE_CAMPAIGN
✓ ASSIGN_INFLUENCERS
✓ APPROVE_CONTENT
✓ REJECT_CONTENT
✓ CREATE_INFLUENCER_PROFILE
✓ EDIT_INFLUENCER_PROFILE
✓ VIEW_BRAND_ANALYTICS
✓ VIEW_CAMPAIGN_PERFORMANCE
```

### Influencer Permissions
```
✓ CREATE_DRAFT
✓ SUBMIT_CONTENT
✓ UPLOAD_VERSION
✓ VIEW_OWN_ANALYTICS
✓ VIEW_ASSIGNED_CAMPAIGNS
```

---

## 🔐 Data Access Levels

### Campaign Access
```
Admin:      all
Brand:      own
Influencer: assigned
```

### Content Access
```
Admin:      all
Brand:      own
Influencer: own
```

### User Access
```
Admin:      all
Brand:      none
Influencer: limited (own only)
```

---

## 🎨 Role Colors & Icons

```typescript
Admin:      red, shield icon
Brand:      blue, briefcase icon
Influencer: purple, sparkles icon
```

---

## 🧪 Testing Roles Locally

```tsx
// Fake a login for testing
import { useAuth } from '../context/AuthContext';
import { User } from '../types';

const testUser: User = {
  id: '123',
  email: 'test@example.com',
  firstName: 'Test',
  lastName: 'Admin',
  role: 'admin', // Change to test different roles
};

const { login } = useAuth();
login(testUser, 'fake-token');
```

---

## 💡 Best Practices

### ✅ DO

- Use guards for UX-level access control
- Always validate on backend API
- Check permissions before showing features
- Use role-specific components
- Provide fallback UI for restricted content
- Log permission checks in development
- Test all role scenarios

### ❌ DON'T

- Rely on frontend-only security
- Store sensitive permissions in localStorage
- Modify permissions in browser DevTools
- Hardcode role names (use `UserRole` enum)
- Forget backend validation
- Show forbidden features, then error
- Trust client-side permission checks

---

## 🐛 Debugging

### Check Current Role
```tsx
const { role, permissions } = useAuth();
console.log('Current role:', role);
console.log('Permissions:', permissions);
```

### Check Permission
```tsx
const { hasPermission } = usePermission();
console.log('Can approve?', hasPermission(PERMISSIONS.APPROVE_CONTENT));
```

### Check Resource Access
```tsx
const { canAccessOwn } = useResourceAccess();
console.log('Can access campaigns?', canAccessOwn('campaigns'));
```

---

## 📱 Component Reference

### Guards
| Component | Use For |
|-----------|---------|
| `<RoleGuard>` | Restrict by role |
| `<PermissionGuard>` | Restrict by permission |
| `<ResourceAccessGuard>` | Restrict by resource + scope |
| `<ProtectedRoute>` | Protect entire routes |

### Display
| Component | Use For |
|-----------|---------|
| `<RoleBadge>` | Show role insignia |
| `<RoleInfoCard>` | Show role description |
| `<RoleList>` | Select/edit roles |
| `<PermissionList>` | Display role permissions |

### Hooks
| Hook | Returns |
|------|---------|
| `usePermission()` | `{ hasPermission, hasMultiple, hasAllPermissions }` |
| `useResourceAccess()` | `{ canAccess, canAccessOwn, canAccessAssigned, canAccessAll }` |
| `useRoleInfo()` | `{ role, isAdmin, isBrand, isInfluencer, primaryDashboard }` |
| `useDashboardAccess()` | `{ canAccessDashboard }` |
| `useFeatureAccess()` | `{ isVisible }` |
| `useCanPerform()` | `{ canApproveContent, canCreateCampaign, ... }` |

---

## 🔗 File Locations

```
Frontend Structure:
├── src/
│   ├── types/roles.ts              ← Role definitions
│   ├── utils/roleUtils.ts          ← Permission utilities
│   ├── hooks/usePermission.ts      ← Custom hooks
│   ├── components/
│   │   ├── PermissionGuard.tsx     ← Guard components
│   │   ├── RoleBadge.tsx           ← Display components
│   │   └── ProtectedRoute.tsx      ← Route protection
│   ├── context/AuthContext.tsx     ← Auth + roles
│   └── pages/
│       ├── AdminDashboard.tsx
│       ├── BrandDashboard.tsx
│       └── InfluencerDashboard.tsx
│
Documentation:
├── RBAC_GUIDE.md                   ← Comprehensive guide
├── RBAC_IMPLEMENTATION.md          ← Implementation details
└── RBAC_QUICK_REFERENCE.md         ← This file
```

---

## 📞 Need Help?

1. **Check the docs**: `RBAC_GUIDE.md` and `RBAC_IMPLEMENTATION.md`
2. **Look at types**: `src/types/roles.ts` has all definitions
3. **Check utilities**: `src/utils/roleUtils.ts` has helper functions
4. **Review examples**: Look for `<RoleGuard>` and `usePermission()` usage in pages
5. **Test locally**: Change role and check feature visibility

---

**Last Updated**: February 14, 2026  
**Quick Version**: 1.0
