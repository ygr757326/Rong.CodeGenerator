import {
  DictionaryCreateParams,
  DictionaryGetListItem,
  DictionaryGetListParams,
  DictionaryGetListResult,
  EnumGetValueListParams,
  EnumGetValueListResult,
  settingItem,
  SmsCreateParams,
  SmsCreateResult,
  SmsGetListResult,
  SmsUpdateParams,
} from './model/systemManageModel';
import { defHttp } from '@/utils/http/axios';

enum Api {
  // 获取系统配置列表
  settingGetAllSettings = '/api/Asset/setting/getAllSettings',
  // 修改系统配置
  settingUpdateSetting = '/api/Asset/setting/updateSetting',
  // 添加字典
  dictionaryCreate = '/api/Asset/dictionary/create',
  // 获取字典详情
  dictionaryGetList = '/api/Asset/dictionary/getList',
  // 修改字典
  dictionaryUpdate = '/api/Asset/dictionary/update?id=',
  // 删除字典
  dictionaryDelete = '/api/Asset/dictionary/delete?id=',
  //获取字典详情
  dictionaryGet = '/api/Asset/dictionary/get',
  // 获取字典下拉
  dictionaryGetDropDownList = '/api/Asset/dictionary/getDropDownList',
  // 启用字典
  dictionaryEnable = '/api/Asset/dictionary/enable',
  // 禁用字典
  dictionaryDisable = '/api/Asset/dictionary/disable',
  // 移除字典缓存
  dictionaryRemoveCache = '/api/Asset/dictionary/removeCache',
  // 获取所有枚举
  enumGetValueList = '/api/Asset/enum/getValueList',
  // 创建短信
  smsCreate = '/api/Asset/smsTemplate/create',
  // 获取短信列表
  smsGetList = '/api/Asset/smsTemplate/getList',
  // 获取短信详情
  smsGet = '/api/Asset/smsTemplate/get',
  // 修改短信
  smsUpdate = '/api/Asset/smsTemplate/update?id=',
  // 删除短信
  smsDelete = '/api/Asset/smsTemplate/delete?id=',
  // 移除缓存
  smsTemplateRemoveCache = '/api/Asset/smsTemplate/removeCache',
}

export const postDictionaryCreate = (data: DictionaryCreateParams) =>
  defHttp.post<DictionaryGetListItem>({
    url: Api.dictionaryCreate,
    data,
  });
export const getDictionaryGetList = (params: DictionaryGetListParams) =>
  defHttp.get<DictionaryGetListResult>({
    url: Api.dictionaryGetList,
    params,
  });
export const putDictionaryUpdate = (id, data: DictionaryGetListItem) =>
  defHttp.put<DictionaryGetListItem>({
    url: Api.dictionaryUpdate + id,
    data,
  });
export const deleteDictionaryDelete = (id: string) =>
  defHttp.delete({
    url: Api.dictionaryDelete + id,
  });
export const getDictionaryGet = (params) =>
  defHttp.get<DictionaryGetListItem>({
    url: Api.dictionaryGet,
    params,
  });
export const getDictionaryGetDropDownList = (params?) =>
  defHttp.get({
    url: Api.dictionaryGetDropDownList,
    params,
  });
export const postDictionaryEnable = (data) =>
  defHttp.post({
    url: Api.dictionaryEnable,
    data,
  });
export const postDictionaryDisable = (data) =>
  defHttp.post({
    url: Api.dictionaryDisable,
    data,
  });
// 移除字典缓存
export const deleteDictionaryRemoveCache = (params?) =>
  defHttp.delete({
    url: Api.dictionaryRemoveCache,
  });
export const getSettingGetAllSettings = (params?) =>
  defHttp.get<Array<settingItem>>({
    url: Api.settingGetAllSettings,
    params,
  });
export const putSettingUpdateSetting = (data: any) =>
  defHttp.put({
    url: Api.settingUpdateSetting,
    data,
  });
export const getEnumGetValueList = (params?: EnumGetValueListParams) =>
  defHttp.get<EnumGetValueListResult>({
    url: Api.enumGetValueList,
    params,
  });
// 创建短信
export const postSmsCreate = (data: SmsCreateParams) =>
  defHttp.post<SmsCreateResult>({
    url: Api.smsCreate,
    data,
  });
//获取短信列表
export const getSmsGetList = (params?) =>
  defHttp.get<SmsGetListResult>({
    url: Api.smsGetList,
    params,
  });
export const getSmsGet = (params) =>
  defHttp.get<SmsCreateResult>({
    url: Api.smsGet,
    params,
  });
export const putSmsUpdate = (id, data: SmsUpdateParams) =>
  defHttp.put<SmsCreateResult>({
    url: Api.smsUpdate + id,
    data,
  });
// 删除短信
export const deleteSmsDelete = (id: string) =>
  defHttp.delete({
    url: Api.smsDelete + id,
  });
// 移除短信缓存
export const deleteSmsTemplateRemoveCache = (params?) =>
  defHttp.delete({
    url: Api.smsTemplateRemoveCache,
  });
