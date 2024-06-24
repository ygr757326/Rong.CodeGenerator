import store from 'store';

export class Storage {
  static setStorage(key: string, value: any) {
    store.set(key, value);
  }

  static getStorage(key: string) {
    return store.get(key);
  }

  static eachStorage(cb: (key: string, value: any) => void) {
    store.each(cb);
  }
}
