// 获取区域分页参数
export interface AreaGetListParams {
  Code: string;
  Name: string;
  ShortName: string;
  Spell: string;
  ParentCode: string;
  CityCode: string;
  Level: number;
  ParentId: string;
  IsActive: boolean;
}
export interface AreaGetListParent {
  code: string;
  name: string;
  shortName: string;
  level: number;
  parentId: string;
  levelText: string;
  id: string;
}
export interface AreaGetListItem {
  concurrencyStamp: string;
  spell: null | string;
  parentCode: string;
  sort: number;
  parent: AreaGetListParent;
  isActive: boolean;
  code: string;
  name: string;
  shortName: string;
  level: number;
  parentId: string;
  levelText: string;
  id: string;
  codes?: string;
  cityCode?: string;
  longitude?: string;
  latitude?: string;
}
export interface AreaGetListResult {
  totalCount: number;
  items: Array<AreaGetListItem>;
}

// 获取区域子级参数
export interface AreaGetChildrenDropDownListParams {
  Code?: string;
  Name?: string;
  IsActive?: boolean;
  AreaChannel?: number | string;
}
// interface AreaGetChildrenDropDownListChild {
//   name: string;
//   value: string;
//   shortName: string;
//   code: string;
//   parentCode: string;
//   longitude: string;
//   latitude: string;
//   level: number;
//   levelText: string;
//   isActive: boolean;
//   children?: Array<AreaGetChildrenDropDownListChild>;
// }
// 获取区域子级返回数据
export interface AreaGetChildrenDropDownListResult {
  name: string;
  value: string;
  label?: string;
  shortName: string;
  code: string;
  parentCode: null | string;
  longitude: string;
  latitude: string;
  level: number;
  levelText: string;
  isActive: boolean;
  isLeaf?: boolean;
  children: Array<AreaGetChildrenDropDownListResult>;
}
