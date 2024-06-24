<template>
  <BasicDrawer v-bind="$attrs" @register="registerDetailDrawer" title="详情" width="500px">
    <!--    详情页面 -->
    <div class="detail" ref="smsDetail">
      <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
        <a-descriptions-item label="短信平台">{{ detailData?.platformText }}</a-descriptions-item>
        <a-descriptions-item label="短信类型">{{ detailData?.typeText }}</a-descriptions-item>
        <a-descriptions-item label="模板编号">{{ detailData?.templateCode }}</a-descriptions-item>
        <a-descriptions-item label="模板">{{ detailData?.template }}</a-descriptions-item>
        <a-descriptions-item label="有效时长(分钟)">{{
          detailData?.validDuration
        }}</a-descriptions-item>
        <a-descriptions-item label="签名名称">{{ detailData?.signName }}</a-descriptions-item>
        <a-descriptions-item label="状态">
          <Tag :color="detailData?.isActive ? 'green' : 'red'">
            {{ detailData?.isActive ? t('common.enabled') : t('common.disEnabled') }}
          </Tag>
        </a-descriptions-item>
        <a-descriptions-item label="备注">{{ detailData?.remark }}</a-descriptions-item>
      </a-descriptions>
    </div>
  </BasicDrawer>
</template>

<script setup lang="ts">
  import { getSmsGet } from '@/api/manageCenter/systemManage';
  import { ref } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { useI18n } from '@/hooks/web/useI18n';
  import { BasicDrawer, useDrawerInner } from '@/components/Drawer';
  import { useLoading } from '@/components/Loading';

  const smsDetail = ref(null);
  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: smsDetail,
    props: {
      absolute: true,
    },
  });
  const { t } = useI18n();
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
    width: '140px',
    'justify-content': 'flex-end',
  };
  let detailData: any = ref({});
  function getData(data) {
    getSmsGet(data)
      .then((res) => {
        detailData.value = res;
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
</script>

<style lang="less" scoped></style>
