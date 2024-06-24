// 用户获取列表参数
export interface IdentityUsersParams {
  Filter?: string;
  IsActive?: boolean;
  OrganizationUnitId?: string;
  OnlyOwnCorp?: boolean;
}
export interface IdentityUsersItem {
  tenantId: null | string;
  userName: string;
  name: string;
  surname: null | string;
  email: string;
  emailConfirmed: boolean;
  phoneNumber: null | string;
  phoneNumberConfirmed: boolean;
  isActive: boolean;
  lockoutEnabled: boolean;
  lockoutEnd: null | string;
  concurrencyStamp: string;
  entityVersion: number;
  isDeleted: boolean;
  deleterId: null | string;
  deletionTime: null | string;
  lastModificationTime: string;
  lastModifierId: null | string;
  creationTime: string;
  creatorId: null | string;
  id: string;
  extraProperties?: ExtraProperties;
}
export interface ExtraProperties {
  Avatar: string;
  Birthday: string;
  Sex: number;
}
// 用户获取列表返回数据
export interface IdentityUsersResult {
  totalCount: number;
  items: Array<IdentityUsersItem>;
}

// 角色获取列表参数
export interface IdentityRoleParams {
  Filter?: string;
}
// 角色获取
export interface IdentityRoleItem {
  name: string;
  isDefault: boolean;
  isStatic: boolean;
  isPublic: boolean;
  concurrencyStamp: string;
  id: string;
  extraProperties?: RoleExtraProperties;
}
export interface RoleExtraProperties {
  DisplayName: string;
}
// 角色获取获取列表返回数据
export interface IdentityRoleResult {
  totalCount?: number;
  items: Array<IdentityRoleItem>;
}
//修改角色参数
export interface IdentityRoleUpdateParams {
  name: string;
  isDefault: boolean;
  isPublic: boolean;
  concurrencyStamp: string;
}
export interface commonParams {
  id: string;
}
// 修改密码参数
export interface SysUserChangePasswordParams {
  // id: string;
  oldPassword: string;
  password: string;
  confirmPassword: string;
}
// 获取授权参数
export interface PermissionsParams {
  providerName?: string;
  providerKey?: string | null;
}
interface PermissionsItems {
  name: string;
  isGranted: boolean;
}
export interface ModifyPermissionsParams {
  permissions: Array<PermissionsItems>;
}
