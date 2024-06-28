import { defineStore } from 'pinia';

import { getEnumGetValueList } from '@/api/manageCenter/systemManage';

import { Storage } from '@/utils/storage';

export const ENUM_KEY = 'ENUM_KEY';

export interface Dict {
  code: string | number;
  value: string | number;
  label: string;
  name: string;
}

export const useEnumStore = defineStore('enum', {
  state: function () {
    return {
      enums: [] as Array<Dict>,
    };
  },
  getters: {
    findName() {
      return (code: string | number, value: string | number) => {
        const dict: Array<Dict> = this.enums.filter(
          (ele: Dict) => ele?.code.toString().toLowerCase() === code.toString().toLowerCase(),
        );
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
        const dict: Array<Dict> = this.enums.filter(
          (ele: Dict) => ele?.code.toString().toLowerCase() === code.toString().toLowerCase(),
        );
        return dict;
      };
    },
    // 处理成select需要的格式
    findCodeSelect() {
      return (code: string | number) => {
        const dict: Array<Dict> = this.enums.filter(
          (ele: Dict) => ele?.code.toString().toLowerCase() === code.toString().toLowerCase(),
        );
        let data = dict.map((v) => {
          v.label = v.name;
          return v;
        });
        return data;
      };
    },
  },
  actions: {
    setEnums() {
      getEnumGetValueList().then((r) => {
        const { items } = r;
        this.enums = items as Array<Dict>;
        Storage.setStorage(ENUM_KEY, this.enums);
      });
    },
  },
});
