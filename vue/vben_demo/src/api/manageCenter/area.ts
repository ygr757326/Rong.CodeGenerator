import { Indexable } from './../../utils/types';
import {
  AreaGetChildrenDropDownListParams,
  AreaGetChildrenDropDownListResult,
  AreaGetListItem,
  AreaGetListParams,
  AreaGetListResult,
} from './model/areaModel';
import { commonParams } from './model/identityModel';
import { defHttp } from '@/utils/http/axios';

enum Api {
  // 获取区域分页
  areaGetList = '/api/Asset/area/getList',
  // 获取区域详情
  areaGet = '/api/Asset/area/get',
  // 删除区域
  areaDelete = '/api/Asset/area/delete',
  // 修改区域信息
  areaUpdate = '/api/Asset/area/update?id=',
  // 启用
  areaEnable = '/api/Asset/area/enable',
  // 禁用
  areaDisable = '/api/Asset/area/disable',
  // 获取区域子级
  areaGetChildrenDropDownList = '/api/Asset/area/getChildrenDropDownList',
  // 获取区域下拉
  areaGetDropDownList = '/api/Asset/area/getDropDownList',
  // 移除区域缓存
  areaRemoveCache = '/api/Asset/area/removeCache',
}

export const getAreaGetList = (params?: AreaGetListParams) =>
  defHttp.get<AreaGetListResult>({
    url: Api.areaGetList,
    params,
  });
export const getAreaGet = (params: commonParams) =>
  defHttp.get<AreaGetListItem>({
    url: Api.areaGet,
    params,
  });

export const deleteAreaDelete = (id: string) =>
  defHttp.delete({
    url: Api.areaDelete + id,
  });
export const postAreaEnable = (data: commonParams) =>
  defHttp.post({
    url: Api.areaEnable,
    data,
  });
export const putAreaUpdate = (id, data) =>
  defHttp.put({
    url: Api.areaUpdate + id,
    data,
  });
export const postAreaDisable = (data: commonParams) =>
  defHttp.post({
    url: Api.areaDisable,
    data,
  });
export const getAreaGetChildrenDropDownList = (params?: AreaGetChildrenDropDownListParams) =>
  defHttp.get<AreaGetChildrenDropDownListResult>({
    url: Api.areaGetChildrenDropDownList,
    params,
  });
export const getAreaGetDropDownList = (params?) =>
  defHttp.get({
    url: Api.areaGetDropDownList,
    params,
  });
// 移除缓存
export const deleteAreaRemoveCache = (params?) =>
  defHttp.delete({
    url: Api.areaRemoveCache,
  });
