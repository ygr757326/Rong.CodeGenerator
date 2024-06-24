<template>
  <!--详情页面 -->
  <a-modal v-model:visible="visible" title="详情" width="60vw" :footer="null">
    <div class="detail" ref="logDetail">
      <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
        <a-descriptions-item label="短信平台">{{ detailData?.platformText }}</a-descriptions-item>
        <a-descriptions-item label="短信类型">{{ detailData?.typeText }}</a-descriptions-item>
        <a-descriptions-item label="电话号码">{{ detailData?.phone }}</a-descriptions-item>
        <a-descriptions-item label="是否模拟发送">
          <Tag :color="detailData.isMock ? 'green' : 'red'">
            {{ detailData.isMock ? '是' : '否' }}
          </Tag>
        </a-descriptions-item>
        <a-descriptions-item label="模板编号">{{ detailData?.templateCode }}</a-descriptions-item>
        <a-descriptions-item label="签名名称">{{ detailData?.signName }}</a-descriptions-item>
        <a-descriptions-item label="是否发送成功">
          <Tag :color="detailData.isSuccess ? 'green' : 'red'">
            {{ detailData.isSuccess ? '是' : '否' }}
          </Tag>
        </a-descriptions-item>
        <a-descriptions-item label="模板">{{ detailData?.template }}</a-descriptions-item>
        <a-descriptions-item label="模板参数">{{ detailData?.templateParams }}</a-descriptions-item>
        <a-descriptions-item label="短信内容">{{ detailData?.smsContent }}</a-descriptions-item>
        <a-descriptions-item label="发送返回结果">{{
          detailData?.returnResult
        }}</a-descriptions-item>
        <a-descriptions-item v-if="detailData?.exception" label="异常信息">{{
          detailData?.exception
        }}</a-descriptions-item>
      </a-descriptions>
    </div>
  </a-modal>
</template>

<script setup lang="ts">
  import { nextTick, ref } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { getSmsSendRecordGet } from '@/api/manageCenter/auditLog';
  import { useLoading } from '@/components/Loading';

  const logDetail = ref(null);

  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: logDetail,
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
    width: '140px',
    'justify-content': 'flex-end',
  };
  const visible = ref(false);
  function open(id: string | number) {
    detailData.value = {};
    nextTick(() => {
      openWrapLoading();
    });
    visible.value = true;
    getData({ id });
  }

  let detailData: any = ref({});
  function getData(data) {
    getSmsSendRecordGet(data)
      .then((res) => {
        detailData.value = res;
      })
      .finally(() => {
        closeWrapLoading();
      });
  }

  defineExpose({ open });
</script>

<style lang="less" scoped>
  .detail {
    padding: 20px;
  }
</style>
