
import { defHttp } from '@/utils/http/axios/index';

export enum AppApi {

    /**
    * 获取 App应用 列表 - 分页
    */
    GetList = '/api/CodeGenerator/app/getList',

    /**
    * 获取 App应用 详情 - 通过id
    */
    Get = '/api/CodeGenerator/app/get?id=',

    /**
    * 修改 App应用 - 
    */
    Update ='/api/CodeGenerator/app/update?id=',

    /**
    * 删除 App应用 - 
    */
    Delete = '/api/CodeGenerator/app/delete?id=',

    /**
    * 创建 App应用 - 
    */
    Create = '/api/CodeGenerator/app/create'
}

/**
 * 获取 App应用 列表 - 分页
 * @param id
 * @returns
 */
export const AppGetList = (params) =>
  defHttp.get({
    url: AppApi.GetList,
    params,
});

/**
* 获取 App应用 详情 - 通过id
 * @param id
 * @returns
*/
export const AppGet = (id) =>
  defHttp.get({
    url: AppApi.Get + id,
});

/**
 * 修改 App应用 - 
 * @param id
 * @param data
 * @returns
 */
export const AppUpdate = (id, data) =>
  defHttp.put({
    url: AppApi.Update + id,
    data,
});

/**
 * 删除 App应用 - 
 * @param id
 * @returns
 */
export const AppDelete = (id) =>
  defHttp.delete({
    url: AppApi.Delete + id,
});

/**
* 创建 App应用 - 
* @param data
* @returns
*/
export const AppCreate = (data) =>
  defHttp.post({
    url: AppApi.Create,
    data,
  }); 