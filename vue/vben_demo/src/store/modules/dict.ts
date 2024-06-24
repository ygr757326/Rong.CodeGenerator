import { defineStore } from 'pinia';
import { getDictionaryGetDropDownList } from '@/api/manageCenter/systemManage';
import { Storage } from '@/utils/storage';

export const DICT_KEY = 'DICT_KEY';

export interface Dict {
  code: string | number;
  value: string | number;
  id: string;
  name: string;
  label?: string;
}

export const useDictStore = defineStore('dict', {
  state: function () {
    return {
      dicts: [] as Array<Dict>,
    };
  },
  getters: {
    findName() {
      return (code: string | number, value: string | number) => {
        const dict: Array<Dict> = this.dicts.filter((ele: Dict) => ele?.code === code);
        if (dict.length > 0) {
          const curDict = dict.find((ele: Dict) => ele.value === value);
          if (curDict !== undefined) {
            return curDict?.name;
          }
        }
      };
    },
    findByCode() {
      return (code: string | number) => {
        const dict: Array<Dict> = this.dicts.filter((ele: Dict) => ele?.code === code);
        return dict;
      };
    },
    // 处理成select需要的格式
    findCodeSelect() {
      return (code: string | number) => {
        const dict: Array<Dict> = this.dicts.filter((ele: Dict) => ele?.code === code);
        let data = dict.map((v) => {
          v.label = v.name;
          return v;
        });
        return data;
      };
    },
  },
  actions: {
    setDicts() {
      getDictionaryGetDropDownList().then((r) => {
        const { items } = r;
        this.dicts = items;
        Storage.setStorage(DICT_KEY, this.dicts);
      });
    },
  },
});
