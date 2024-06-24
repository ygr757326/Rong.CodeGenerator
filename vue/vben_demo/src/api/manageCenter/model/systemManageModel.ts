// 获取字典列表参数
export interface DictionaryGetListParams {
  IsActive?: boolean;
  Code?: string;
  Value?: string;
  Name?: string;
  DisplayName?: string;
  MerchantChannel?: number;
  MerchantType?: string;
}
export interface DictionaryGetListItem {
  concurrencyStamp: string;
  sort: number;
  isActive: boolean;
  remark: null | string;
  tuoke: null | string;
  code: string;
  value: string;
  name: string;
  displayName: string;
  id: string;
  merchantType?: string;
  merchantChannel?: number;
  merchantChannelName?: string;
}
// 获取码牌列表返回结果
export interface DictionaryGetListResult {
  totalCount: number;
  items: Array<DictionaryGetListItem>;
}
// 添加字典参数
export interface DictionaryCreateParams {
  code: string;
  value: string;
  name: string;
  displayName: string;
  sort: number;
  isActive: boolean;
  remark: string;
  tuoke: string;
  merchantType?: string;
}

// 获取系统配置列表返回数据
export interface settingItem {
  name: string;
  groups: Array<settingGroup>;
}
export interface settingGroup {
  name: string;
  settings: Array<Setting>;
}
export interface Setting {
  displayName: string;
  tips: null | string;
  valueType: number;
  valueTypeText: string;
  name: string;
  value: string;
}
// 获取枚举参数
export interface EnumGetValueListParams {
  EnumName: string;
}
export interface EnumGetValueListItem {
  code: string;
  name: string;
  value: number;
}
// 获取枚举返回数据
export interface EnumGetValueListResult {
  items: Array<EnumGetValueListItem>;
}
// 短信创建参数
export interface SmsCreateParams {
  platform: number;
  type: number;
  template: string;
  remark: string;
  isActive: boolean;
  smsType: number;
  templateCode: string;
  validDuration: number;
  signName: string;
}
export interface SmsCreateResult {
  id: string;
  platform: number;
  type: number;
  platformText: string;
  typeText: string;
  concurrencyStamp: string;
  template: string;
  isActive: boolean;
  remark?: string;
  smsType: number;
  templateCode: string;
  validDuration: number;
  signName: string;
}
export interface SmsGetListResult {
  totalCount: number;
  items: Array<SmsCreateResult>;
}
export interface SmsUpdateParams {
  platform: number;
  type: number;
  template: string;
  remark: string;
  isActive: boolean;
  concurrencyStamp: string;
  id: string;
  smsType: number;
  templateCode: string;
  validDuration: number;
  signName: string;
}
