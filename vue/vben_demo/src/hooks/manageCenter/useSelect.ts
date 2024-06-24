import { getEnumGetValueList } from '/@/api/manageCenter/systemManage';
import { EnumGetValueListParams } from '/@/api/manageCenter/model/systemManageModel';
import { ref } from 'vue';
interface optionsItem {
  label: string;
  value: number | string;
}
/**
 *
 * @param params 获取枚举的参数
 * @returns
 */
export const useSelectOptions = async (params: EnumGetValueListParams) => {
  let options = ref<Array<optionsItem>>([]);
  let res = await getEnumGetValueList(params);
  res.items.forEach((v) => {
    let obj: optionsItem = {
      label: '',
      value: '',
    };
    obj.label = v.name;
    obj.value = v.value;
    options.value.push(obj);
  });
  return options;
};
