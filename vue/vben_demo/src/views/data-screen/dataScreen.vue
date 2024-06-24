<script setup lang="ts">
  import { nextTick, onMounted, ref } from 'vue';
  import { useLineChart, option, useLineMock } from './line';

  const lineOpts = ref(option);
  const userOpts = ref({
    ...option,
    title: {
      ...option.title,
      text: '驾驶员合规率排行榜',
    },
  });
  const docsOpts = ref({
    ...option,
    title: {
      ...option.title,
      text: '当月档案合规率排行榜',
    },
  });

  function lineMock(lineChart) {
    let data;
    setTimeout(() => {
      data = useLineMock(data);
      lineChart.setOption({
        series: {
          data,
        },
      });
      lineMock(lineChart);
    }, 3000);
  }

  function resize(chart) {
    window.addEventListener('resize', () => {
      chart.resize();
    });
  }

  function carCharts() {
    const lineChart = useLineChart('car-charts');
    lineChart.setOption(lineOpts.value);
    lineMock(lineChart);
    resize(lineChart);
  }

  function userCharts() {
    const lineChart = useLineChart('user-charts');
    lineChart.setOption(userOpts.value);
    lineMock(lineChart);
    resize(lineChart);
  }

  function docsCharts() {
    const lineChart = useLineChart('doc-charts');
    lineChart.setOption(docsOpts.value);
    lineMock(lineChart);
    resize(lineChart);
  }

  onMounted(() => {
    nextTick(() => {
      carCharts();
      userCharts();
      docsCharts();
    });
  });
</script>

<template>
  <div class="body">
    <div class="header">
      <div class="title">
        <span>昆明市网约车企业安全合规排行榜</span>
      </div>
    </div>
    <div class="data-screen">
      <a-row :gutter="24">
        <a-col :span="8">
          <div class="charts-wrapper">
            <div class="charts-inner">
              <div class="charts-title"></div>
              <div class="charts" id="car-charts"></div>
            </div>
          </div>
        </a-col>
        <a-col :span="8">
          <div class="charts-wrapper">
            <div class="charts-inner">
              <div class="charts-title"></div>
              <div class="charts" id="user-charts"></div>
            </div>
          </div>
        </a-col>
        <a-col :span="8">
          <div class="charts-wrapper">
            <div class="charts-inner">
              <div class="charts-title"></div>
              <div class="charts" id="doc-charts"></div
            ></div>
          </div>
        </a-col>
      </a-row>
    </div>
    <div class="footer"></div>
  </div>
</template>

<style scoped lang="less">
  .body {
    position: relative;
    width: 100vw;
    height: 100vh;
    background: #0a1631 url('@/assets/images/bg.png') 0 0 / 100% 100% no-repeat;

    .header {
      position: absolute;
      width: 100vw;
      height: 48px;
      background: url('@/assets/images/header.png') 0 0 / 100% 100% no-repeat;

      .title {
        display: flex;
        align-items: center;
        justify-content: center;
        height: 100%;
        color: #fff;
        font-size: 18px;
        line-height: 100%;
      }
    }

    .footer {
      position: absolute;
      right: 0;
      bottom: 0;
      left: 0;
      width: 100vw;
      height: 24px;
      background: url('@/assets/images/footer.png') 0 0 / 100% 100% no-repeat;
    }

    .data-screen {
      box-sizing: border-box;
      width: 100%;
      height: 100%;
      padding: 48px 0 24px;

      .ant-row {
        height: 100%;
      }

      .charts-wrapper {
        display: flex;
        position: relative;
        box-sizing: border-box;
        align-items: center;
        justify-content: center;
        width: 100%;
        height: 100%;
        padding: 24px 12px;

        .charts-title {
          position: absolute;
          top: 0;
          width: 100%;
          height: 48px;
          border-bottom: 1px solid #fff;
          background: linear-gradient(to right, rgba(#4362c9, 1), rgba(#101832, 0.12));
        }

        .charts-inner {
          position: relative;
          width: 100%;
          height: 98%;
        }

        .charts {
          box-sizing: border-box;
          width: 100%;
          height: 100%;
          padding: 12px;
          border: 1px solid #fff;
          // border-radius: 12px;
          background-color: rgb(255 255 255 / 16%);
        }
      }
    }
  }
</style>
