<template>
  <PageWrapper contentFullHeight>
    <!-- 审计日志配置 -->
    <BasicTable @register="register">
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              label: '查看',
              onClick: handleDetail.bind(null, record),
            },
          ]"
        />
      </template>
      <template #form-ExecutionTimeStart="{ model, field }">
        <a-date-picker
          format="YYYY-MM-DD HH:mm:ss"
          show-time
          placeholder="请选择"
          v-model:value="model[field]"
        />
      </template>
      <template #form-ExecutionTimeEnd="{ model, field }">
        <a-date-picker
          format="YYYY-MM-DD HH:mm:ss"
          show-time
          placeholder="请选择"
          v-model:value="model[field]"
        />
      </template>
    </BasicTable>
    <!-- 查看审计日志 -->
    <LogDetail ref="logDetailRef" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '@/components/Page';
  import { BasicColumn, BasicTable, useTable, TableAction } from '@/components/Table';
  import {
    getAuditLogGetList,
    getHttpMethodDropDown,
    getHttpStatusCodeDropDown,
  } from '@/api/manageCenter/auditLog';
  import { ref } from 'vue';
  import LogDetail from './components/logDetail.vue';
  import { AbsViewModal } from '#/common';
  import { formatToDateTime } from '@/utils/dateUtil';
  import { useGrid } from '@/hooks/manageCenter/useGrid';

  // 获取到详情节点
  const logDetailRef = ref<AbsViewModal | null>(null);

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '应用名称', dataIndex: 'applicationName' },
    { title: '用户账号', dataIndex: 'userName' },
    { title: '请求方式', dataIndex: 'httpMethod' },
    { title: '请求url', dataIndex: 'url' },
    { title: 'http状态码', dataIndex: 'httpStatusCode', sorter: true },
    { title: '执行时间', dataIndex: 'executionTime', sorter: true },
    { title: '执行时长(毫秒)', dataIndex: 'executionDuration', sorter: true },
  ];
  const [register] = useTable({
    columns,
    tableSetting: {
      redo: true,
      size: true,
      setting: true,
      fullScreen: true,
    },
    useSearchForm: true,
    // 处理搜索区域参数
    handleSearchInfoFn: (values) => {
      if (values.ExecutionTimeStart) {
        values.ExecutionTimeStart = formatToDateTime(values.ExecutionTimeStart);
      }
      if (values.ExecutionTimeEnd) {
        values.ExecutionTimeEnd = formatToDateTime(values.ExecutionTimeEnd);
      }
    },
    api: getAuditLogGetList,
    actionColumn: {
      width: 160,
      title: '操作',
      dataIndex: 'action',
      slots: { customRender: 'action' },
      fixed: 'right',
    },
    showTableSetting: true,
    formConfig: {
      labelWidth: 140,
      ...useGrid(),

      schemas: [
        {
          label: '应用名称',
          component: 'Input',
          field: 'ApplicationName',
        },
        // {
        //   label: '用户',
        //   component: 'Input',
        //   field: 'UserId',
        // },
        {
          label: '账号名称',
          component: 'Input',
          field: 'UserName',
        },
        // {
        //   label: '模拟用户id',
        //   component: 'Input',
        //   field: 'ImpersonatorUserId',
        // },
        {
          label: '执行开始时间',
          component: 'DatePicker',
          field: 'ExecutionTimeStart',
          slot: 'ExecutionTimeStart',
        },
        {
          label: '执行结束时间',
          component: 'Input',
          field: 'ExecutionTimeEnd',
          slot: 'ExecutionTimeEnd',
        },
        {
          label: '执行最小时长(毫秒)',
          component: 'InputNumber',
          field: 'ExecutionDurationMin',
        },
        {
          label: '执行最大时长(毫秒)',
          component: 'InputNumber',
          field: 'ExecutionDurationMax',
        },
        {
          label: '客户端ip地址',
          component: 'Input',
          field: 'ClientIpAddress',
        },
        {
          label: '客户端名称',
          component: 'Input',
          field: 'ClientName',
        },
        // {
        //   label: '客户端id',
        //   component: 'Input',
        //   field: 'ClientId',
        // },
        {
          label: '请求方式',
          component: 'ApiSelect',
          field: 'HttpMethod',
          componentProps: {
            api: getHttpMethodDropDown,
            labelField: 'name',
            valueField: 'value',
            resultField: 'items',
            showSearch: true,
            optionFilterProp: 'label',
          },
        },
        {
          label: '请求地址',
          component: 'Input',
          field: 'Url',
        },
        {
          label: 'http状态码',
          component: 'ApiSelect',
          field: 'HttpStatusCode',
          componentProps: {
            api: getHttpStatusCodeDropDown,
            labelField: 'name',
            valueField: 'value',
            resultField: 'items',
            showSearch: true,
            optionFilterProp: 'label',
          },
        },
        {
          label: '是否有异常',
          component: 'Select',
          field: 'HasException',
          componentProps: {
            options: [
              {
                label: '是',
                value: true,
                key: '1',
              },
              {
                label: '否',
                value: false,
                key: '2',
              },
            ],
          },
        },
        {
          label: '是否模拟用户',
          component: 'Select',
          field: 'IsImpersonator',
          componentProps: {
            options: [
              {
                label: '是',
                value: true,
                key: '1',
              },
              {
                label: '否',
                value: false,
                key: '2',
              },
            ],
          },
        },
      ],
    },
  });
  /**
   * 点击查看详情
   */
  function handleDetail(record: Recordable) {
    logDetailRef.value?.open(record.id);
  }
</script>

<style lang="less" scoped></style>
