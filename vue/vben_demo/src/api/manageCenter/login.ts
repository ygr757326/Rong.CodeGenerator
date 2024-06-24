import { defHttp } from '@/utils/http/axios';
import { ConnectTokenParams } from './model/loginModel';
import QueryString from 'qs';

enum Api {
  // 登录
  connectToken = '/connect/token',
  // 获取用户信息
  accountMyProfile = '/api/account/my-profile',
  // 退出登录
  logout = '/api/Account/Logout',
  // 退出登录
  logoutByToken = '/connect/revocation',
}

export const getConnectToken = (data: ConnectTokenParams) =>
  defHttp.post({
    url: Api.connectToken,
    data: QueryString.stringify(data),
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded',
    },
  });
export const getAccountMyProfile = (params?) =>
  defHttp.get({
    url: Api.accountMyProfile,
    params,
  });
/**
 *
 * @param params 退出登录
 * @returns
 */
export const LogoutByToken = (token?) => {
  const parameter = {
    token: token,
    token_type_hint: 'access_token',
    Client_Id: 'Shop_App',
  };
  return defHttp.post({
    url: Api.logoutByToken,
    data: QueryString.stringify(parameter),
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded',
    },
  });
};
