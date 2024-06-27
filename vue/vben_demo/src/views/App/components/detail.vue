<template>
  <!--  详情页面 -->
  <div class="detail" ref="detailContentRef">
      <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
          
        <a-descriptions-item label="Test">
           {{ detailData?.test  }} 
        </a-descriptions-item>
        <a-descriptions-item label="头像">
          <ImagePreview :width="100" :height="100" :src="detailData?.logo" ></ImagePreview>
        </a-descriptions-item>
        <a-descriptions-item label="头像1">
          <ImagePreview :width="100" :height="100" v-for="(item, i) in detailData?.logos|| []" :key="i" :src="item" ></ImagePreview>
        </a-descriptions-item>
        <a-descriptions-item label="客户端">
           {{ detailData?.client  }} 
        </a-descriptions-item>
        <a-descriptions-item label="客户端2">
           {{ dictStore?.findName('MyDictCode', detailData?.clientDict)  }} 
        </a-descriptions-item>
        <a-descriptions-item label="客户端3">
          <Tag color="">
           {{ dictStore?.findName('MyDictCode', detailData?.clientDict3) }} 
          </Tag>
        </a-descriptions-item>
        <a-descriptions-item label="App版本数字号">
           {{ detailData?.versionNumber  }} 
        </a-descriptions-item>
        <a-descriptions-item label="App版本数字号">
           {{ detailData?.versionNumber1  }} 
        </a-descriptions-item>
        <a-descriptions-item label="是否马上生效">
          <Tag :color="detailData?.isNowEffect ? 'green' : 'red'">
           {{ detailData?.isNowEffect ? '是' : '否' }} 
          </Tag>
        </a-descriptions-item>
        <a-descriptions-item label="开始日期">
           {{ formatToDate(detailData?.startDate)  }} 
        </a-descriptions-item>
        <a-descriptions-item label="结束日期">
           {{ formatToDate(detailData?.effectTime)  }} 
        </a-descriptions-item>
        <a-descriptions-item label="是否强制性更新">
          <Tag :color="detailData?.isForceUpdate ? 'green' : 'red'">
           {{ detailData?.isForceUpdate ? '是' : '否' }} 
          </Tag>
        </a-descriptions-item>
        <a-descriptions-item label="金额">
           {{ detailData?.amount  }} 
        </a-descriptions-item>
        <a-descriptions-item label="枚举">
           {{ enumStore?.findName('TestEnum', detailData?.myEnum)  }} 
        </a-descriptions-item>
        <a-descriptions-item label="枚举1">
           {{ enumStore?.findName('TestEnum', detailData?.myEnum1)  }} 
        </a-descriptions-item>
        <a-descriptions-item label="枚举2">
          <Tag color="">
           {{ enumStore?.findName('TestEnum', detailData?.myEnum2) }} 
          </Tag>
        </a-descriptions-item>

      </a-descriptions>
  </div>
</template>

<script setup lang="ts">

  import { AppGet } from '../api';
  import { ref, nextTick } from 'vue';
  import { useI18n } from '@/hooks/web/useI18n';
  import { useLoading } from '@/components/Loading';
  import { formatToDate } from '@/utils/dateUtil';
  import { useDictStore } from '@/store/modules/dict';
  import { useEnumStore } from '@/store/modules/enum';

  const dictStore = useDictStore();
  const enumStore = useEnumStore();
  const detailContentRef = ref(null);
  const { t } = useI18n();
  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: detailContentRef,
    props: {
      absolute: true,
    },
  });
  const props = defineProps({
    id: { type: String, default: '' },
    column: { type: Object, default: { xxl: 1, xl: 1, lg: 1, md: 1, sm: 1, xs: 1 } },
  });
  // 宽度css属性
  const width = {
    width: '120px',
    'justify-content': 'flex-end',
  };

  let detailData: any = ref({});

  /**
   * 获取详情
   */
  function getData(data) {
AppGet(data.id).then((res) => {
        detailData.value = res;
      })
      .finally(() => {
        closeWrapLoading();
      });
  }

  /**
   * 初始化
   */
  function init(data) {
    nextTick(() => {
      openWrapLoading();
    });
    detailData.value = {};
    getData(data);
  }
  defineExpose({ init });

</script>

<style lang="less" scoped>
  .title {
    margin-top: 20px;
    font-size: 18px;
    font-weight: bold;
  }

  .contentBox {
    min-height: 500px;
    padding: 10px;
    border: 1px solid #ccc;
  }
</style>
