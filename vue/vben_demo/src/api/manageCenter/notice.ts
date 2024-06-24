import {
  NoticeGetListItem,
  NoticeGetListParams,
  NoticeGetListResult,
  NoticeSetActiveItem,
} from './model/userNotificationModel';
import { commonParams } from './model/identityModel';
import { defHttp } from '/@/utils/http/axios';
enum Api {
  //获取公告列表
  noticeGetList = '/api/Shop/notice/getList',
  // 获取公告详情
  noticeGet = '/api/Shop/notice/get',
  // 修改公告
  noticeUpdate = '/api/Shop/notice/update?id=',
  // 删除公告
  noticeDelete = '/api/Shop/notice/delete?id=',
  //创建公告
  noticeCreate = '/api/Shop/notice/create',
  // 是否发布广告
  noticeSetIsActive = '/api/Shop/notice/setIsActive',
}

// 获取公告列表
export const getNoticeGetList = (params?: NoticeGetListParams) =>
  defHttp.get<NoticeGetListResult>({
    url: Api.noticeGetList,
    params,
  });
// 获取公告详情
export const getNoticeGet = (params: commonParams) =>
  defHttp.get<NoticeGetListItem>({
    url: Api.noticeGet,
    params,
  });
// 修改公告
export const putNoticeUpdate = (id, data: NoticeGetListItem) =>
  defHttp.put<NoticeGetListItem>({
    url: Api.noticeUpdate + id,
    data,
  });
// 删除公告
export const deleteNoticeDelete = (id: string) =>
  defHttp.delete({
    url: Api.noticeDelete + id,
  });
// 创建公告
export const postNoticeCreate = (params: NoticeGetListItem) =>
  defHttp.post({
    url: Api.noticeCreate,
    params,
  });
// 是否发布广告
export const postNoticeSetIsActive = (params: NoticeSetActiveItem) =>
  defHttp.post({
    url: Api.noticeSetIsActive,
    params,
  });
