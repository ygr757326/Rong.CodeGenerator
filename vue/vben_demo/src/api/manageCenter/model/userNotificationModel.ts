// 获取用户通知分页参数
export interface UserNotificationGetListParams {
  StartDate?: string;
  EndDate?: string;
  State?: boolean;
  SkipCount?: number;
  MaxResultCount?: number;
}

export interface UserNotificationGetListItem {
  id: string;
  tenantId: string;
  userId: string;
  state: number;
  notification: any;
  targetNotifiers: string;
  targetNotifiersList: Array<string>;
  concurrencyStamp?: string;
}
export interface UserNotificationSetStateItem {
  id: string;
  state: number;
}
export interface UserNotificationGetListResult {
  totalCount: number;
  items: Array<UserNotificationGetListItem>;
}
