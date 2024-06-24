/**
 * 验证手机号
 * @param phone
 * @returns
 */
export function useValidatePhone(data: string) {
  const reg = /^1([38][0-9]|4[014-9]|[59][0-35-9]|6[2567]|7[0-8])\d{8}$/;
  return reg.test(data);
}
/**
 * 验证邮箱
 * @param phone
 * @returns
 */
export function useValidateEmail(data: string) {
  const reg = /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+(.[a-zA-Z0-9_-])+/;
  return reg.test(data);
}
/**
 * 验证身份证号
 * @param phone
 * @returns
 */
export function useValidateIdCard(data: string) {
  const reg = /(^\d{15}$)|(^\d{17}([0-9]|X)$)/;
  return reg.test(data);
}
