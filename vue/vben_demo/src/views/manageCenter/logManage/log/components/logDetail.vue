<template>
  <!--详情页面 -->
  <a-modal v-model:visible="visible" title="详情" width="60vw" :footer="null">
    <div class="detail" ref="logDetail">
      <a-tabs v-model:activeKey="activeKey">
        <a-tab-pane key="base" tab="基本信息">
          <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
            <a-descriptions-item label="应用名称">{{
              detailData?.applicationName
            }}</a-descriptions-item>
            <a-descriptions-item label="用户账号">{{ detailData?.userName }}</a-descriptions-item>

            <a-descriptions-item label="请求方式">{{ detailData?.httpMethod }}</a-descriptions-item>
            <a-descriptions-item label="请求url">{{ detailData?.url }}</a-descriptions-item>
            <a-descriptions-item label="http状态码">{{
              detailData?.httpStatusCode
            }}</a-descriptions-item>
            <a-descriptions-item label="请求方法"
              ><a-button type="primary" @click="checkActions" size="small"
                >查看</a-button
              ></a-descriptions-item
            >
            <a-descriptions-item label="客户端名称">{{
              detailData?.clientName
            }}</a-descriptions-item>
            <a-descriptions-item label="执行时间">{{
              detailData?.executionTime
            }}</a-descriptions-item>
            <a-descriptions-item label="执行时长(毫秒)">{{
              detailData?.executionDuration
            }}</a-descriptions-item>
            <a-descriptions-item label="客户端ip地址">{{
              detailData?.clientIpAddress
            }}</a-descriptions-item>
            <a-descriptions-item label="浏览器信息">{{
              detailData?.browserInfo
            }}</a-descriptions-item>
          </a-descriptions>
        </a-tab-pane>
        <a-tab-pane key="error" tab="错误信息" v-if="detailData?.exceptions">{{
          detailData?.exceptions
        }}</a-tab-pane>
      </a-tabs>
    </div>
  </a-modal>
  <a-modal v-model:visible="actionVisible" title="请求方法" width="80vw" :footer="null">
    <BasicTable @register="register">
      <template #parameters="{ record }">
        <a-button
          @click="checkParams(record.parameters)"
          size="small"
          type="primary"
          v-if="record.parameters.length > 2"
          >查看</a-button
        >
        <div v-else>无</div>
      </template>
    </BasicTable></a-modal
  >
  <a-modal v-model:visible="paramsVisible" title="查看参数" width="80vw" :footer="null">
    <div style="padding: 20px"> {{ paramsData }}</div>
  </a-modal>
</template>

<script setup lang="ts">
  import { nextTick, ref } from 'vue';
  import { getAuditLogGet } from '@/api/manageCenter/auditLog';
  import { useLoading } from '@/components/Loading';
  import { BasicColumn, BasicTable, useTable } from '@/components/Table';

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
  const actionVisible = ref(false);
  const paramsVisible = ref(false);
  const paramsData = ref('');
  function open(id: string | number) {
    detailData.value = {};
    nextTick(() => {
      openWrapLoading();
    });
    visible.value = true;
    getData({ id });
  }
  //  查看请求方法
  function checkActions() {
    actionVisible.value = true;
    nextTick(() => {
      setTableData(detailData.value.actions);
    });
  }
  let detailData: any = ref({});
  function getData(data) {
    getAuditLogGet(data)
      .then((res) => {
        detailData.value = res;
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
  // 表格表头
  const columns: BasicColumn[] = [
    { title: '服务名称', dataIndex: 'serviceName' },
    { title: '方式名称', dataIndex: 'methodName' },
    { title: '执行时间', dataIndex: 'executionTime' },
    { title: '执行时长', dataIndex: 'executionDuration' },
    { title: '参数', dataIndex: 'parameters', slots: { customRender: 'parameters' } },
    { title: '扩展属性', dataIndex: 'extraProperties' },
  ];
  const [register, { setTableData }] = useTable({
    columns,
    useSearchForm: false,
    showTableSetting: false,
    dataSource: detailData.value.actions,
  });
  // 点击查看参数
  function checkParams(data) {
    paramsVisible.value = true;
    paramsData.value = data;
  }
  defineExpose({ open });
</script>

<style lang="less" scoped>
  .detail {
    padding: 20px;
  }
</style>
