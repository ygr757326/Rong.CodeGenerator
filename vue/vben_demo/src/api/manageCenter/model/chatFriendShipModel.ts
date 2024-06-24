export interface ChatFriendShipModel {
  serverTime: string;
  friends: Array<Friend>;
}

export interface Friend {
  friendUserId: string;
  friendTenantId: string;
  friendUserName: string;
  friendTenancyName: string;
  friendProfilePictureId: string;
  unreadMessageCount: number;
  isOnline: boolean;
  state: number;
  avatar?: string;
  message?: any[];
}
