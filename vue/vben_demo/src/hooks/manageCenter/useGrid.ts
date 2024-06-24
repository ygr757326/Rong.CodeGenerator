/**
 *
 * @param params 获取枚举的参数
 * @returns
 */
export const useGrid = () => {
  return {
    rowProps: {
      gutter: 4,
    },
    baseColProps: {
      span: 8,
      xs: 12, //<576px
      sm: 12, //≥576px
      md: 8, //≥768px
      lg: 8, //≥992px
      xl: 6, //≥1200px
      xxl: 4, //≥1600px};}
    },
  };
};
