<template>
  <PageWrapper contentFullHeight>
    <BasicTable @register="register">
      <template #toolbar>
        <a-button type="primary" style="margin-right: 20px" @click="handleCreate">添加</a-button>
        <a-button type="primary" style="margin-right: 20px" @click="removeCache">移除缓存</a-button>
      </template>
      <template #isActive="{ record }">
        <Tag :color="record.isActive ? 'green' : 'red'">
          {{ record.isActive ? t('common.enabled') : t('common.disEnabled') }}
        </Tag>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              label: '编辑',
              onClick: handleEdit.bind(null, record),
            },
            {
              label: '查看',
              onClick: handleDetail.bind(null, record),
            },
          ]"
          :dropDownActions="[
            {
              label: '删除',
              popConfirm: {
                title: '是否删除？',
                confirm: handleDelete.bind(null, record),
              },
            },
          ]"
        />
      </template>
    </BasicTable>
    <!-- 编辑短信 -->
    <smsModify @register="registerDrawer" @success="handleSuccess" />
    <!-- 查看短信 -->
    <smsDetail @register="registerDetailDrawer" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '@/components/Page';
  import { BasicColumn, BasicTable, useTable, TableAction } from '@/components/Table';
  import {
    getSmsGetList,
    deleteSmsDelete,
    getEnumGetValueList,
    deleteSmsTemplateRemoveCache,
  } from '@/api/manageCenter/systemManage';
  import smsModify from './components/smsModify.vue';
  import smsDetail from './components/smsDetail.vue';
  import { Tag, message } from 'ant-design-vue';
  import { useI18n } from '@/hooks/web/useI18n';
  import { useDrawer } from '@/components/Drawer';
  import { useGrid } from '@/hooks/manageCenter/useGrid';
  import { useLoading } from '@/components/Loading';

  const [openFullLoading, closeFullLoading] = useLoading({
    tip: '加载中...',
  });
  const [registerDrawer, { openDrawer: openModifyDrawer }] = useDrawer();
  const [registerDetailDrawer, { openDrawer: openDetailDrawer }] = useDrawer();

  const { t } = useI18n();

  // 编辑或新增成功
  function handleSuccess() {
    reload();
  }
  /**
   *移除缓存
   * @param record
   */
  function removeCache() {
    openFullLoading();
    deleteSmsTemplateRemoveCache()
      .then(() => {
        message.success('操作成功');
        reload();
      })
      .finally(() => {
        closeFullLoading();
      });
  }
  /**
   *删除
   * @param record
   */
  async function handleDelete(record: Recordable) {
    await deleteSmsDelete(record.id);
    reload();
  }

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '短信平台', dataIndex: 'platformText' },
    { title: '短信类型', dataIndex: 'typeText' },
    { title: '模板', dataIndex: 'template' },
    { title: '模板编号', dataIndex: 'templateCode' },
    { title: '有效时长', dataIndex: 'validDuration', sorter: true },
    { title: '签名名称', dataIndex: 'signName' },
    { title: '状态', dataIndex: 'isActive', slots: { customRender: 'isActive' } },
  ];
  const [register, { reload }] = useTable({
    columns,
    tableSetting: {
      redo: true,
      size: true,
      setting: true,
      fullScreen: true,
    },
    useSearchForm: true,
    api: getSmsGetList,
    beforeFetch: (params) => {
      return Object.assign(params, { IsActive: true });
    },
    actionColumn: {
      width: 160,
      title: '操作',
      dataIndex: 'action',
      slots: { customRender: 'action' },
      fixed: 'right',
    },
    showTableSetting: true,
    formConfig: {
      labelWidth: 120,
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
          label: '模板编号',
          component: 'Input',
          field: 'TemplateCode',
        },
        {
          label: '签名名称',
          component: 'Input',
          field: 'SignName',
        },
        {
          label: '状态',
          component: 'Select',
          field: 'IsActive',
          defaultValue: true,
          componentProps: {
            options: [
              {
                label: '启用',
                value: true,
              },
              {
                label: '禁用',
                value: false,
              },
            ],
          },
        },
        {
          label: '模板',
          component: 'Input',
          field: 'Template',
        },
      ],
    },
  });

  /**
   * 点击编辑
   */
  function handleEdit(record: Recordable) {
    openModifyDrawer(true, {
      id: record.id,
      isUpdate: true,
    });
  }
  /**
   * 点击添加
   */
  function handleCreate() {
    openModifyDrawer(true, {
      isUpdate: false,
    });
  }

  /**
   * 点击查看详情
   */
  function handleDetail(record: Recordable) {
    openDetailDrawer(true, {
      id: record.id,
    });
  }
</script>

<style lang="less" scoped></style>
