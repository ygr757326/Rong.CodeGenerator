<template>
  <a-drawer v-model:visible="visible" title="详情" width="500px">
    <!--  详情页面 -->
    <div class="detail" ref="dictionaryDetail">
      <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
        <a-descriptions-item label="排序">{{ detailData?.sort }}</a-descriptions-item>
        <a-descriptions-item label="类别">{{ detailData?.code }}</a-descriptions-item>
        <a-descriptions-item label="值">{{ detailData?.value }}</a-descriptions-item>
        <a-descriptions-item label="名称">{{ detailData?.name }}</a-descriptions-item>
        <a-descriptions-item label="显示名称">{{ detailData?.displayName }}</a-descriptions-item>
        <a-descriptions-item label="状态">
          <Tag :color="detailData?.isActive ? 'green' : 'red'">
            {{ detailData?.isActive ? t('common.enabled') : t('common.disEnabled') }}
          </Tag>
        </a-descriptions-item>
        <a-descriptions-item label="备注">{{ detailData?.remark }}</a-descriptions-item>
      </a-descriptions>
    </div>
  </a-drawer>
</template>

<script setup lang="ts">
  import { getDictionaryGet } from '@/api/manageCenter/systemManage';
  import { ref, nextTick } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { useI18n } from '@/hooks/web/useI18n';
  import { useLoading } from '@/components/Loading';

  const dictionaryDetail = ref(null);
  const { t } = useI18n();
  const visible = ref<boolean>(false);
  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: dictionaryDetail,
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
    getDictionaryGet(data)
      .then((res) => {
        detailData.value = res;
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
  function open(data) {
    nextTick(() => {
      openWrapLoading();
    });
    visible.value = true;
    detailData.value = {};
    getData({ id: data.id });
  }
  defineExpose({ open });
</script>

<style lang="less" scoped></style>
