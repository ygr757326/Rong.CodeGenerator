<template>
  <!--  详情页面 -->
  <div class="detail" ref="detailContentRef">
    <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
      <a-descriptions-item label="标题">{{ detailData?.title }}</a-descriptions-item>
      <a-descriptions-item label="封面图片">
        <GzImagePreview :width="50" :height="50" :fileId="detailData.logo"> </GzImagePreview>
      </a-descriptions-item>
      <a-descriptions-item label="发布时间"> {{ detailData?.publicTime }}</a-descriptions-item>
      <a-descriptions-item label="浏览次数"> {{ detailData?.viewsNumber }} </a-descriptions-item>
      <a-descriptions-item label="是否发布">
        <Tag :color="detailData.isActive ? 'green' : 'red'">
          {{ detailData.isActive ? '已发布' : '未发布' }}
        </Tag>
      </a-descriptions-item>
    </a-descriptions>
  </div>
</template>

<script setup lang="ts">
  import { getNoticeGet } from '@/api/manageCenter/notice';
  import { ref, nextTick } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { useI18n } from '@/hooks/web/useI18n';
  import { useLoading } from '@/components/Loading';
  import { GzImagePreview } from '@/components/GzImage';

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
  function getData(data) {
    getNoticeGet(data)
      .then((res) => {
        detailData.value = res;
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
  function init(data) {
    nextTick(() => {
      openWrapLoading();
    });
    detailData.value = {};
    getData({ id: data.id });
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
