// 获取订单商品分页参数
export interface OrgGetListParams {
  ParentId?: string;
  DisplayName?: string;
}

export interface OrgGetListItem {
  id: string;
  parentId: string;
  displayName: string;
  concurrencyStamp: string;
}
export interface OrgGetListResult {
  totalCount: number;
  items: Array<OrgGetListItem>;
}
