import { OrgGetListItem, OrgGetListParams, OrgGetListResult } from './model/orgModel';
import { commonParams } from './model/identityModel';
import { defHttp } from '@/utils/http/axios';

enum Api {
  //获取组织机构列表
  orgGetList = '/api/Asset/org/getList',
  // 获取组织机构详情
  orgGet = '/api/Asset/org/get',
  // 修改组织机构
  orgUpdate = '/api/Asset/org/update?id=',
  // 删除组织机构
  orgDelete = '/api/Asset/org/delete?id=',
  //创建组织机构
  orgCreate = '/api/Asset/org/create',
  // 获取下拉树
  orgGetDropDownList = '/api/Asset/org/getDropDownList',
  // 获取可用组织机构树
  orgGetAvailableOrganizationUnitsTree = '/api/Asset/org/getAvailableOrganizationUnitsTree',
  // 同步组织机构
  orgSync = '/api/Asset/org/syncOrg',
}

// 获取组织机构列表
export const getOrgGetList = (params?: OrgGetListParams) =>
  defHttp.get<OrgGetListResult>({
    url: Api.orgGetList,
    params,
  });
// 获取组织机构详情
export const getOrgGet = (params: commonParams) =>
  defHttp.get<OrgGetListItem>({
    url: Api.orgGet,
    params,
  });
// 修改组织机构
export const putOrgUpdate = (id, data: OrgGetListItem) =>
  defHttp.put<OrgGetListItem>({
    url: Api.orgUpdate + id,
    data,
  });
// 删除组织机构
export const deleteOrgDelete = (id: string) =>
  defHttp.delete({
    url: Api.orgDelete + id,
  });
// 创建组织机构
export const postOrgCreate = (params: OrgGetListItem) =>
  defHttp.post({
    url: Api.orgCreate,
    params,
  });
//获取下拉树
export const getOrgGetDropDownList = (params?) =>
  defHttp.get({
    url: Api.orgGetDropDownList,
    params,
  });
//获取可用组织机构树
export const getOrgGetAvailableOrganizationUnitsTree = (params?) =>
  defHttp.get({
    url: Api.orgGetAvailableOrganizationUnitsTree,
    params,
  });
// 同步组织机构数据
export const postOrgSync = (params?) =>
  defHttp.post({
    url: Api.orgSync,
    params,
  });
