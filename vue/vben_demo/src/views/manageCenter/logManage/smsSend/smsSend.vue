<template>
  <PageWrapper contentFullHeight>
    <!-- 审计日志配置 -->
    <BasicTable @register="register">
      <template #isMock="{ record }">
        <Tag :color="record.isMock ? 'green' : 'red'">
          {{ record.isMock ? '是' : '否' }}
        </Tag>
      </template>
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
    </BasicTable>
    <!-- 查看审计日志 -->
    <SmsSendDetail ref="smsSendDetailRef" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '@/components/Page';
  import { Tag } from 'ant-design-vue';
  import { BasicColumn, BasicTable, useTable, TableAction } from '@/components/Table';
  import { getSmsSendRecordList } from '@/api/manageCenter/auditLog';
  import { getEnumGetValueList } from '@/api/manageCenter/systemManage';

  import { ref } from 'vue';
  import SmsSendDetail from './components/smsSendDetail.vue';
  import { AbsViewModal } from '#/common';
  import { useGrid } from '@/hooks/manageCenter/useGrid';

  // 获取到详情节点
  const smsSendDetailRef = ref<AbsViewModal | null>(null);

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '短信平台', dataIndex: 'platformText' },
    { title: '短信类型', dataIndex: 'typeText' },
    { title: '电话号码', dataIndex: 'phone' },
    { title: '是否模拟发送', dataIndex: 'isMock', slots: { customRender: 'isMock' } },
    { title: '是否发送成功', dataIndex: 'isSuccess', slots: { customRender: 'isSuccess' } },
    // { title: '模板编号', dataIndex: 'templateCode' },
    { title: '签名名称', dataIndex: 'signName' },
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
    api: getSmsSendRecordList,
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
          label: '短信平台',
          component: 'ApiSelect',
          field: 'Platform',
          componentProps: {
            api: getEnumGetValueList,
            params: { EnumName: 'SmsPlatformEnum' },
            labelField: 'name',
            valueField: 'value',
            resultField: 'items',
            showSearch: true,
            optionFilterProp: 'label',
          },
        },
        {
          label: '短信类型',
          component: 'ApiSelect',
          field: 'SmsType',
          componentProps: {
            api: getEnumGetValueList,
            params: { EnumName: 'SmsTypeEnum' },
            labelField: 'name',
            valueField: 'value',
            resultField: 'items',
            showSearch: true,
            optionFilterProp: 'label',
          },
        },
        {
          label: '电话号码',
          component: 'Input',
          field: 'Phone',
        },
        {
          label: '是否模拟发送',
          component: 'Select',
          field: 'IsMock',
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
          label: '是否发送成功',
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
    smsSendDetailRef.value?.open(record.id);
  }
</script>

<style lang="less" scoped></style>
