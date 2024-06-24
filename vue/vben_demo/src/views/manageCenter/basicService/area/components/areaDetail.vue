<template>
  <BasicDrawer v-bind="$attrs" @register="registerDetailDrawer" title="详情" width="540px">
    <!--    详情页面 -->
    <div class="detail" ref="areaDetail">
      <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
        <a-descriptions-item label="地区编码">{{ detailData?.code }}</a-descriptions-item>
        <a-descriptions-item label="地区名称">{{ detailData?.name }}</a-descriptions-item>
        <a-descriptions-item label="地区简称">{{ detailData?.shortName }}</a-descriptions-item>
        <a-descriptions-item label="级别">{{ detailData?.levelText }}</a-descriptions-item>
        <a-descriptions-item label="拼音">{{ detailData?.spell }}</a-descriptions-item>
        <a-descriptions-item label="城市编码/区号">{{ detailData?.cityCode }}</a-descriptions-item>
        <a-descriptions-item label="中心点经度">{{ detailData?.longitude }}</a-descriptions-item>
        <a-descriptions-item label="中心点纬度">{{ detailData?.latitude }}</a-descriptions-item>
        <a-descriptions-item label="父级地区编码集合">{{ detailData?.codes }}</a-descriptions-item>
        <a-descriptions-item v-if="detailData.parent" label="父级地区编码">{{
          detailData.parent?.code
        }}</a-descriptions-item>
        <a-descriptions-item v-if="detailData.parent" label="父级地区名称">{{
          detailData.parent?.name
        }}</a-descriptions-item>
        <a-descriptions-item v-if="detailData.parent" label="父级地区简称">{{
          detailData.parent?.shortName
        }}</a-descriptions-item>
        <a-descriptions-item v-if="detailData.parent" label="父级级别">{{
          detailData.parent?.levelText
        }}</a-descriptions-item>
        <a-descriptions-item label="状态">
          <Tag :color="detailData?.isActive ? 'green' : 'red'">
            {{ detailData?.isActive ? t('common.enabled') : t('common.disEnabled') }}
          </Tag>
        </a-descriptions-item>
      </a-descriptions>
    </div>
  </BasicDrawer>
</template>

<script setup lang="ts">
  import { Tag } from 'ant-design-vue';
  import { ref } from 'vue';
  import { getAreaGet } from '@/api/manageCenter/area';
  import { BasicDrawer, useDrawerInner } from '@/components/Drawer';
  import { useLoading } from '@/components/Loading';
  import { useI18n } from '@/hooks/web/useI18n';

  const areaDetail = ref(null);
  const { t } = useI18n();
  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: areaDetail,
    props: {
      absolute: true,
    },
  });
  const [registerDetailDrawer] = useDrawerInner(async (data) => {
    detailData.value = {};
    openWrapLoading();
    getData({ id: data.id });
  });
  const props = defineProps({
    id: { type: String, default: '' },
    column: { type: Object, default: { xxl: 1, xl: 1, lg: 1, md: 1, sm: 1, xs: 1 } },
  });
  // 宽度css属性
  const width = {
    width: '160px',
    'justify-content': 'flex-end',
  };

  let detailData: any = ref({});
  function getData(data) {
    getAreaGet(data)
      .then((res) => {
        detailData.value = res;
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
</script>

<style lang="less" scoped></style>
