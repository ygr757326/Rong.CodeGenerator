import { defHttp } from '@/utils/http/axios';
import {
  AuditLogGetListParams,
  AuditLogGetListResult,
  AuditLogGetResult,
  SmsSendRecordResult,
  ExternalApiRecordResult,
  ExternalApiRecordItem,
  SmsSendRecordItem,
} from './model/auditLogModel';
import { commonParams } from './model/identityModel';

enum Api {
  // 获取审计日志列表
  auditLogGetList = '/api/Shop/auditLog/getList',
  // 查看审计日志详情
  auditLogGet = '/api/Shop/auditLog/get',
  // 获取http请求方式下拉
  getHttpMethodDropDown = '/api/Shop/auditLog/getHttpMethodDropDown',
  // 获取http状态码下拉
  getHttpStatusCodeDropDown = '/api/Shop/auditLog/getHttpStatusCodeDropDown',
  // 获取外部api请求
  getExternalApiRecordList = '/api/Shop/externalApiRecord/getList',
  // 获取外部api请求详情
  getExternalApiRecordGet = '/api/Shop/externalApiRecord/get',
  // 获取短信发送列表
  getSmsSendRecordList = '/api/Shop/smsSendRecord/getList',
  // 获取短信发送详情
  getSmsSendRecordGet = '/api/Shop/smsSendRecord/get',
}

export const getAuditLogGetList = (params?: AuditLogGetListParams) =>
  defHttp.get<AuditLogGetListResult>({
    url: Api.auditLogGetList,
    params,
  });
export const getAuditLogGet = (params?: commonParams) =>
  defHttp.get<AuditLogGetResult>({
    url: Api.auditLogGet,
    params,
  });
export const getHttpMethodDropDown = (params?) =>
  defHttp.get({
    url: Api.getHttpMethodDropDown,
    params,
  });
export const getHttpStatusCodeDropDown = (params?) =>
  defHttp.get({
    url: Api.getHttpStatusCodeDropDown,
    params,
  });
export const getExternalApiRecordList = (params?) =>
  defHttp.get<ExternalApiRecordResult>({
    url: Api.getExternalApiRecordList,
    params,
  });
export const getExternalApiRecordGet = (params?) =>
  defHttp.get<ExternalApiRecordItem>({
    url: Api.getExternalApiRecordGet,
    params,
  });
export const getSmsSendRecordList = (params?) =>
  defHttp.get<SmsSendRecordResult>({
    url: Api.getSmsSendRecordList,
    params,
  });
export const getSmsSendRecordGet = (params?) =>
  defHttp.get<SmsSendRecordItem>({
    url: Api.getSmsSendRecordGet,
    params,
  });
