import {
  BarChart,
  BarSeriesOption,
  CanvasRenderer,
  GridComponent,
  GridComponentOption,
  LegendComponent,
  LegendComponentOption,
  TitleComponent,
  TitleComponentOption,
  TooltipComponent,
  TooltipComponentOption,
  echarts,
} from './config';

echarts.use([
  TitleComponent,
  TooltipComponent,
  GridComponent,
  LegendComponent,
  BarChart,
  CanvasRenderer,
]);

type EChartsOption = echarts.ComposeOption<
  | TitleComponentOption
  | TooltipComponentOption
  | GridComponentOption
  | LegendComponentOption
  | BarSeriesOption
>;

const yAxisData = [
  '滇约出行',
  '途途乐',
  '曹操专车',
  '滴滴出行',
  '高德打车',
  '首约出行',
  '嘀嗒出行',
  '享道出行',
  '首汽约车',
  '万顺叫车',
  '神州专车',
  '花小猪打车',
  '美团打车',
  'T3出行',
  '星星打车',
];
const default_data = new Array(yAxisData.length).fill(Math.round(Math.random() * 100));

export const option: EChartsOption = {
  title: {
    text: '车辆合规率排行榜',
    left: 'center',
    textStyle: {
      color: '#FFFFFF',
    },
  },
  tooltip: {
    trigger: 'axis',
    axisPointer: {
      type: 'shadow',
    },
  },
  legend: {},
  grid: {
    left: '3%',
    right: '4%',
    bottom: '3%',
    containLabel: true,
    show: false,
    borderColor: '#000',
  },
  xAxis: {
    type: 'value',
    boundaryGap: [0, 0.01],
    nameTextStyle: { color: '#f5f5f5' },
    axisLine: { lineStyle: { color: '#f5f5f5' } },
    splitLine: {
      show: true,
      lineStyle: {
        color: '#666',
        type: 'solid',
      },
    },
  },
  yAxis: {
    type: 'category',
    data: yAxisData,
    nameTextStyle: { color: '#f5f5f5' },
    axisLine: { lineStyle: { color: '#f5f5f5' } },
    inverse: true,
    animationDuration: 300,
    animationDurationUpdate: 300,
    splitLine: {
      show: false,
      lineStyle: {
        color: '#666',
        type: 'solid',
      },
    },
  },
  series: [
    {
      type: 'bar',
      barWidth: 18,
      label: {
        show: true,
        position: 'right',
        valueAnimation: true,
      },
      // realtimeSort: true,
      data: default_data.map((ele) => {
        return {
          value: ele,
          itemStyle: {
            borderRadius: [0, 10, 10, 0],
            color: '#0099ff',
          },
        };
      }),
    },
  ],
  animationDuration: 0,
  animationDurationUpdate: 3000,
  animationEasing: 'linear',
  animationEasingUpdate: 'linear',
};

export function useLineChart(el) {
  const chartDom = document.getElementById(el)!;
  return echarts.init(chartDom);
}

export function useLineMock(cData?: Array<any>) {
  return cData
    ? cData.map((ele) => {
        return {
          value: (ele += Math.round(Math.random() * 10)),
          itemStyle: {
            borderRadius: [0, 10, 10, 0],
            color: '#0099ff',
          },
        };
      })
    : default_data.map((ele) => {
        return {
          value: (ele += Math.round(Math.random() * 10)),
          itemStyle: {
            borderRadius: [0, 10, 10, 0],
            color: '#0099ff',
          },
        };
      });
}
