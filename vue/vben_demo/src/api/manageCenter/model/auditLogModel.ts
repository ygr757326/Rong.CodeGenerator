// 获取审计日志列表参数
export interface AuditLogGetListParams {
  ApplicationName?: string;
  UserId?: string;
  UserName?: string;
  ImpersonatorUserId?: string;
  ExecutionTimeStart?: string;
  ExecutionTimeEnd?: string;
  ExecutionDurationMin?: string;
  ExecutionDurationMax?: string;
  ClientIpAddress?: string;
  ClientName?: string;
  ClientId?: string;
  HttpMethod?: string;
  Url?: string;
  HttpStatusCode?: string;
  HasException?: string;
  IsImpersonator?: string;
}
export interface AuditLogGetListItem {
  impersonatorUserId: null | string;
  impersonatorTenantId: null | string;
  executionTime: string;
  executionDuration: boolean;
  clientIpAddress: string;
  correlationId: string;
  httpMethod: string;
  url: string;
  comments: string;
  httpStatusCode: boolean;
  applicationName: string;
  userId: null | string;
  userName: null | string;
  clientName: null | string;
  clientId: null | string;
  id: string;
}
// 获取审计日志列表返回数据
export interface AuditLogGetListResult {
  totalCount: number;
  items: Array<AuditLogGetListItem>;
}
export interface AuditLogGetActionItem {
  id: string;
  auditLogId: string;
  serviceName: string;
  methodName: string;
  executionTime: string;
  executionDuration: boolean;
  parameters: string;
  extraProperties: AuditLogGetActionExtraProperties;
}
export interface AuditLogGetActionExtraProperties {
  additionalProp1?: string;
  additionalProp2?: string;
  additionalProp3?: string;
}
// 获取审计日志详情返回数据
export interface AuditLogGetResult {
  id: string;
  applicationName: string;
  userId: string;
  userName: string;
  clientName: string;
  clientId: string;
  impersonatorUserId: string;
  impersonatorTenantId: string;
  executionTime: string;
  executionDuration: boolean;
  clientIpAddress: string;
  correlationId: string;
  httpMethod: string;
  url: string;
  comments: string;
  httpStatusCode: boolean;
  browserInfo: string;
  exceptions: string;
  actions: Array<AuditLogGetActionItem>;
}
// 短信发送记录
export interface SmsSendRecordItem {
  id: string;
  platform: number;
  smsType: number;
  phone: string;
  platformText: string;
  typeText: string;
  isMock: boolean;
  templateCode: string;
  signName: string;
  isSuccess: boolean;
  template?: string;
  templateParams?: string;
  smsContent?: string;
  returnResult?: string;
  exception?: string;
}
export interface SmsSendRecordResult {
  totalCount: number;
  items: Array<SmsSendRecordItem>;
}
// 外部api请求
export interface ExternalApiRecordItem {
  id: string;
  correlationId: string;
  clientName: string;
  name: string;
  url: string;
  concurrencyStamp: string;
  method: string;
  isSuccess: boolean;
  executionTime: string;
  executionDuration: number;
  parameters?: string;
  encryptParameters?: string;
  returnValue?: string;
  exception?: string;
}
export interface ExternalApiRecordResult {
  totalCount: number;
  items: Array<ExternalApiRecordItem>;
}
