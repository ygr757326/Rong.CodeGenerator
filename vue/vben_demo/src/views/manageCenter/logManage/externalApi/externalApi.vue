<template>
  <PageWrapper contentFullHeight>
    <!-- 审计日志配置 -->
    <BasicTable @register="register">
      <template #isSuccess="{ record }">
        <Tag :color="record.isSuccess ? 'green' : 'red'">
          {{ record.isSuccess ? '是' : '否' }}
        </Tag>
      </template>
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
    <ExternalApiDetail ref="externalApiDetailRef" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '@/components/Page';
  import { Tag } from 'ant-design-vue';
  import { BasicColumn, BasicTable, useTable, TableAction } from '@/components/Table';
  import { getExternalApiRecordList, getHttpMethodDropDown } from '@/api/manageCenter/auditLog';
  import { ref } from 'vue';
  import ExternalApiDetail from './components/externalApiDetail.vue';
  import { AbsViewModal } from '#/common';
  import { formatToDateTime } from '@/utils/dateUtil';
  import { useGrid } from '@/hooks/manageCenter/useGrid';

  // 获取到详情节点
  const externalApiDetailRef = ref<AbsViewModal | null>(null);

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '名称', dataIndex: 'name' },
    { title: '客户端名称', dataIndex: 'clientName' },
    { title: '请求方式', dataIndex: 'method' },
    { title: '请求url', dataIndex: 'url' },
    { title: '是否成功', dataIndex: 'isSuccess', slots: { customRender: 'isSuccess' } },
    { title: '开始执行时间', dataIndex: 'executionTime', sorter: true },
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
    api: getExternalApiRecordList,
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
          label: '客户端名称',
          component: 'Input',
          field: 'ClientName',
        },
        {
          label: '名称',
          component: 'Input',
          field: 'Name',
        },
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
          label: '请求方式',
          component: 'ApiSelect',
          field: 'Method',
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
          label: '是否成功',
          component: 'Select',
          field: 'IsSuccess',
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
                key: '0',
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
    externalApiDetailRef.value?.open(record.id);
  }
</script>

<style lang="less" scoped></style>
