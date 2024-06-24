// 登录参数
export interface ConnectTokenParams {
  grant_type: string;
  client_id: string;
  password: string | undefined;
  username: string | undefined;
  rememberMe?: boolean;
  scope: string;
}
