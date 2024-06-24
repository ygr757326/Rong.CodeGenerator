<template>
  <!--详情页面 -->
  <a-modal v-model:visible="visible" title="详情" width="60vw" :footer="null">
    <div class="detail" ref="logDetail">
      <a-tabs v-model:activeKey="activeKey">
        <a-tab-pane key="base" tab="基本信息">
          <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
            <a-descriptions-item label="名称">{{ detailData?.name }}</a-descriptions-item>
            <a-descriptions-item label="客户端名称">{{
              detailData?.clientName
            }}</a-descriptions-item>
            <a-descriptions-item label="请求方式">{{ detailData?.method }}</a-descriptions-item>
            <a-descriptions-item label="请求url">{{ detailData?.url }}</a-descriptions-item>
            <a-descriptions-item label="是否成功">
              <Tag :color="detailData.isSuccess ? 'green' : 'red'">
                {{ detailData.isSuccess ? '是' : '否' }}
              </Tag></a-descriptions-item
            >
            <a-descriptions-item label="开始执行时间">{{
              detailData?.executionTime
            }}</a-descriptions-item>
            <a-descriptions-item label="执行时长(毫秒)">{{
              detailData?.executionDuration
            }}</a-descriptions-item>
            <!-- <a-descriptions-item label="请求参数">{{ detailData?.parameters }}</a-descriptions-item>
            <a-descriptions-item label="请求加密参数">{{
              detailData?.encryptParameters
            }}</a-descriptions-item> -->
            <a-descriptions-item label="返回值">{{ detailData?.returnValue }}</a-descriptions-item>
          </a-descriptions>
        </a-tab-pane>
        <a-tab-pane key="error" tab="异常信息" v-if="detailData?.exception">{{
          detailData?.exception
        }}</a-tab-pane>
      </a-tabs>
    </div>
  </a-modal>
</template>

<script setup lang="ts">
  import { nextTick, ref } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { getExternalApiRecordGet } from '@/api/manageCenter/auditLog';
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
  const activeKey = ref<string>('base');
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
    getExternalApiRecordGet(data)
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
