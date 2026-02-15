/**
 * Role Badge Component
 * Displays user role with appropriate styling and icon
 */

import React from 'react';
import { UserRole, ROLE_INFO } from '../types/roles';
import { getRoleIcon } from '../utils/roleUtils';
import { Shield, Briefcase, Sparkles } from 'lucide-react';

interface RoleBadgeProps {
  role: UserRole;
  size?: 'sm' | 'md' | 'lg';
  showIcon?: boolean;
  showText?: boolean;
}

const iconMap: Record<string, React.ReactNode> = {
  shield: <Shield size={20} />,
  briefcase: <Briefcase size={20} />,
  sparkles: <Sparkles size={20} />,
};

export function RoleBadge({ role, size = 'md', showIcon = true, showText = true }: RoleBadgeProps) {
  const info = ROLE_INFO[role];

  const sizeClasses = {
    sm: 'px-2 py-1 text-xs',
    md: 'px-3 py-1.5 text-sm',
    lg: 'px-4 py-2 text-base',
  };

  const colorClasses: Record<string, string> = {
    red: 'bg-red-50 text-red-700 border-red-200',
    blue: 'bg-blue-50 text-blue-700 border-blue-200',
    purple: 'bg-purple-50 text-purple-700 border-purple-200',
    green: 'bg-green-50 text-green-700 border-green-200',
    yellow: 'bg-yellow-50 text-yellow-700 border-yellow-200',
    orange: 'bg-orange-50 text-orange-700 border-orange-200',
  };

  const iconSize = {
    sm: 12,
    md: 16,
    lg: 20,
  }[size];

  const icon = getRoleIcon(role);
  const iconElement = iconMap[icon];

  return (
    <div className={`${sizeClasses[size]} ${colorClasses[info.color]} border rounded-full font-medium inline-flex items-center space-x-2`}>
      {showIcon && <span>{iconElement}</span>}
      {showText && <span>{info.displayName}</span>}
    </div>
  );
}

interface RoleInfoCardProps {
  role: UserRole;
  showDescription?: boolean;
}

export function RoleInfoCard({ role, showDescription = true }: RoleInfoCardProps) {
  const info = ROLE_INFO[role];

  const colorClasses: Record<string, string> = {
    red: 'bg-red-50 border-red-200 text-red-900',
    blue: 'bg-blue-50 border-blue-200 text-blue-900',
    purple: 'bg-purple-50 border-purple-200 text-purple-900',
    green: 'bg-green-50 border-green-200 text-green-900',
    yellow: 'bg-yellow-50 border-yellow-200 text-yellow-900',
    orange: 'bg-orange-50 border-orange-200 text-orange-900',
  };

  return (
    <div className={`${colorClasses[info.color]} border rounded-lg p-4`}>
      <div className="flex items-start space-x-3">
        <span className="text-2xl mt-1">{iconMap[getRoleIcon(role)]}</span>
        <div>
          <h3 className="font-bold text-lg">{info.displayName}</h3>
          {showDescription && <p className="text-sm mt-1 opacity-80">{info.description}</p>}
          <p className="text-xs mt-2 font-semibold uppercase tracking-wider">{info.shortSummary}</p>
        </div>
      </div>
    </div>
  );
}

interface RoleListProps {
  selectedRole?: UserRole;
  onSelectRole?: (role: UserRole) => void;
}

export function RoleList({ selectedRole, onSelectRole }: RoleListProps) {
  return (
    <div className="space-y-3">
      {Object.values(UserRole).map((role) => (
        <button
          key={role}
          onClick={() => onSelectRole?.(role)}
          className={`w-full text-left p-4 rounded-lg border-2 transition ${
            selectedRole === role
              ? 'border-purple-600 bg-purple-50'
              : 'border-gray-200 hover:border-purple-300'
          }`}
        >
          <RoleBadge role={role} size="sm" />
          <p className="mt-2 text-sm text-gray-600">{ROLE_INFO[role].description}</p>
        </button>
      ))}
    </div>
  );
}

interface PermissionListProps {
  role: UserRole;
  title?: string;
}

export function PermissionList({ role, title }: PermissionListProps) {
  const { ROLE_PERMISSIONS } = require('../types/roles');
  const permissions = ROLE_PERMISSIONS[role] || [];

  return (
    <div>
      {title && <h3 className="text-lg font-bold mb-4 text-gray-900">{title}</h3>}
      <div className="grid grid-cols-1 md:grid-cols-2 gap-3">
        {permissions.map((permission: any) => (
          <div key={`${permission.resource}-${permission.action}`} className="p-3 bg-gray-50 rounded-lg border border-gray-200">
            <div className="font-medium text-gray-900 text-sm">
              {permission.action.charAt(0).toUpperCase() + permission.action.slice(1)}
            </div>
            <div className="text-xs text-gray-600 mt-1">{permission.description}</div>
            <div className="text-xs text-gray-500 mt-2 uppercase tracking-wider font-semibold">{permission.resource}</div>
          </div>
        ))}
      </div>
    </div>
  );
}
