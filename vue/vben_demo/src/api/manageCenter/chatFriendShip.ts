import { defHttp } from '@/utils/http/axios';

export function getUserChatFriendsWithSettings() {
  return defHttp.get({
    url: '/api/Asset/chatFriendship/getUserChatFriendsWithSettings',
  });
}

export function readAll(data) {
  return defHttp.post({
    url: '/api/Asset/chatMessage/markAllUnreadMessagesOfUserAsRead',
    data,
  });
}

export function historyMessage(params) {
  return defHttp.get({
    url: '/api/Asset/chatMessage/getUserChatMessages',
    params,
  });
}
