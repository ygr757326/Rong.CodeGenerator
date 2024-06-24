import { defHttp } from '@/utils/http/axios';

export function evaluatedList(data) {
  return defHttp.get({
    url: '/api/Shop/orderComment/getList',
    params: data,
  });
}

export function evaluated(data) {
  return defHttp.get({
    url: '/api/Shop/orderComment/get',
    params: data,
  });
}
// 添加评论
export function postOrderCommentCreate(data) {
  return defHttp.post({
    url: '/api/Shop/orderComment/create',
    data,
  });
}
