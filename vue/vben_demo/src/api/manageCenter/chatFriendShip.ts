import { defHttp } from '@/utils/http/axios';

export function getUserChatFriendsWithSettings() {
  return defHttp.get({
    url: '/api/Shop/chatFriendship/getUserChatFriendsWithSettings',
  });
}

export function readAll(data) {
  return defHttp.post({
    url: '/api/Shop/chatMessage/markAllUnreadMessagesOfUserAsRead',
    data,
  });
}

export function historyMessage(params) {
  return defHttp.get({
    url: '/api/Shop/chatMessage/getUserChatMessages',
    params,
  });
}
