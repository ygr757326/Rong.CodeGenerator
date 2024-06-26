import { defHttp } from '@/utils/http/axios';
import {
  IdentityUsersParams,
  IdentityUsersResult,
  IdentityUsersItem,
  IdentityRoleResult,
  IdentityRoleParams,
  IdentityRoleUpdateParams,
  IdentityRoleItem,
  SysUserChangePasswordParams,
  PermissionsParams,
  ModifyPermissionsParams,
  commonParams,
} from './model/identityModel';

enum Api {
  // 获取用户列表
  identityUsers = '/api/Asset/user/getPaged',
  // 新增用户s
  sysUserCreate = '/api/Asset/user/create',
  // 修改用户2
  sysUserUpdate = '/api/Asset/user/update?id=',
  // 获取用户详情
  sysUserGet = '/api/Asset/user/get',
  // 删除用户
  sysUserDelete = '/api/Asset/user/delete?id=',
  // 同步用户数据
  userSync = '/api/Asset/user/sync',
  // 重置密码
  sysUserResetPassword = '/api/Asset/user/resetPassword?id=',
  // 修改密码,
  sysUserChangePassword = '/api/Asset/user/changePassword',
  // 获取可用角色
  sysUserGetAssignableRoles = '/api/Asset/user/getAssignableRoles',
  // 获取某用户角色
  sysUserGetRoles = '/api/Asset/user/getRoles',
  // 获取角色列表
  identityRoles = '/api/identity/roles',
  // 添加角色
  identityRolesAdd = '/api/identity/roles',
  // 获取角色详情
  identityRolesDetail = '/api/identity/roles/',
  // 操作角色
  identityRolesAction = '/api/identity/roles/',
  // 获取授权信息
  permissions = '/api/permission-management/permissions',
  // 修改授权
  modifyPermissions = '/api/permission-management/permissions?',
  // 获取可用组织机构信息
  availableOrganizationUnitsTree = '/api/Asset/user/getAvailableOrganizationUnitsTree',
  //获取用户组织机构信息
  organizationUnits = '/api/Asset/user/getOrganizationUnits',
}

export const getIdentityUsers = (params?: IdentityUsersParams) =>
  defHttp.get<IdentityUsersResult>({
    url: Api.identityUsers,
    params,
  });
// 获取用户详情
export const getIdentityUsersDetail = (params: string) =>
  defHttp.get<IdentityUsersItem>({
    url: Api.sysUserGet,
    params,
  });
// 新增用户
export const postSysUserCreate = (data: any) =>
  defHttp.post({
    url: Api.sysUserCreate,
    data,
  });
// 修改用户
export const putIdentityUsers = (id: string, data: any) =>
  defHttp.put<IdentityUsersItem>({
    url: Api.sysUserUpdate + id,
    data,
  });
// 删除用户
export const deleteIdentityUsers = (id: string) =>
  defHttp.delete({
    url: Api.sysUserDelete + id,
  });

export const postSysUserResetPassword = (id: string) =>
  defHttp.post({
    url: Api.sysUserResetPassword + id,
  });
export const postSysUserChangePassword = (data: SysUserChangePasswordParams) =>
  defHttp.post({
    url: Api.sysUserChangePassword,
    data,
  });
// 同步用户数据
export const postUserSync = (params?) =>
  defHttp.post({
    url: Api.userSync,
    params,
  });

// 获取可用角色
export const getSysUserGetAssignableRoles = (params?) =>
  defHttp.get<IdentityRoleResult>({
    url: Api.sysUserGetAssignableRoles,
    params,
  });
/**
 * 获取某用户角色
 * @param params
 * @returns
 */
export const getSysUserGetRoles = (params: commonParams) =>
  defHttp.get<IdentityRoleResult>({
    url: Api.sysUserGetRoles,
    params,
  });
// 获取角色详情
export const getIdentityRolesDetail = (params: commonParams) =>
  defHttp.get<IdentityRoleItem>({
    url: Api.identityRolesDetail + params,
  });
// 获取角色列表
export const getIdentityRoles = (params?: IdentityRoleParams) =>
  defHttp.get<IdentityRoleResult>({
    url: Api.identityRoles,
    params,
  });
// 新增角色
export const postIdentityRolesAdd = (data: SysUserChangePasswordParams) =>
  defHttp.post({
    url: Api.identityRolesAdd,
    data,
  });
// 修改角色
export const putIdentityRoles = (id: string, data: IdentityRoleUpdateParams) =>
  defHttp.put<IdentityRoleItem>({
    url: Api.identityRolesAction + id,
    data,
  });
// 删除角色
export const deleteIdentityRoles = (id: string) =>
  defHttp.delete({
    url: Api.identityRolesAction + id,
  });
// 获取授权信息
export const getPermissionsAll = (params?: PermissionsParams) =>
  defHttp.get({
    url: Api.permissions,
    params,
  });
// 修改授权
export const putModifyPermissions = (name, key, data: ModifyPermissionsParams) =>
  defHttp.put({
    url: Api.modifyPermissions + 'providerName=' + name + '&' + 'providerKey=' + key,
    data,
  });
// 获取可用组织机构信息
export const getAvailableOrganizationUnitsTree = (params?: PermissionsParams) =>
  defHttp.get({
    url: Api.availableOrganizationUnitsTree,
    params,
  });
//获取用户组织机构信息
export const getOrganizationUnits = (params?: PermissionsParams) =>
  defHttp.get({
    url: Api.organizationUnits,
    params,
  });
