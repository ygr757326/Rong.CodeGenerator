import { defineStore } from 'pinia';

import { getEnumGetValueList } from '@/api/manageCenter/systemManage';

import { Storage } from '@/utils/storage';

export const ENUM_KEY = 'ENUM_KEY';

export interface Dict {
  code: string | number;
  value: string | number;
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
        const dict: Array<Dict> = this.enums.filter((ele: Dict) => ele?.code === code);
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
        const dict: Array<Dict> = this.enums.filter((ele: Dict) => ele?.code === code);
        return dict;
      };
    },
  },
  actions: {
    setEnums() {
      getEnumGetValueList().then((r) => {
        const { items } = r;
        this.enums = items;
        Storage.setStorage(ENUM_KEY, this.enums);
      });
    },
  },
});
