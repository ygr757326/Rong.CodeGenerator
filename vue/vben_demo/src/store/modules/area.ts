import { defineStore } from 'pinia';

import { getAreaGetDropDownList } from '@/api/manageCenter/area';

import { Storage } from '@/utils/storage';

export const AREA_KEY = 'AREA_KEY';

// export interface Dict {
//   code: string | number;
//   value: string | number;
//   id: string;
//   name: string;
// }

export const useAreaStore = defineStore('area', {
  state: function () {
    return {
      areaTreeList: Array,
    };
  },
  getters: {},
  actions: {
    setArea(subLevel) {
      getAreaGetDropDownList({ SubLevel: subLevel }).then((r) => {
        const { items } = r;
        this.areaTreeList = items;
        Storage.setStorage(AREA_KEY, this.areaTreeList);
      });
    },
  },
});
