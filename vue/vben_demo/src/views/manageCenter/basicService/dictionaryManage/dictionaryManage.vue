<template>
  <PageWrapper contentFullHeight>
    <BasicTable @register="register">
      <template #toolbar>
        <a-button type="primary" style="margin-right: 20px" @click="handleCreate">添加</a-button>
        <a-button type="primary" style="margin-right: 20px" @click="removeCache">移除缓存</a-button>
      </template>
      <template #isActive="{ value, record }">
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
            {
              label: '启用',
              ifShow: () => !record.isActive,
              popConfirm: {
                title: '是否启用？',
                confirm: handleEnable.bind(null, record),
              },
            },
            {
              label: '禁用',
              ifShow: () => record.isActive,
              popConfirm: {
                title: '是否禁用？',
                confirm: handleDisable.bind(null, record),
              },
            },
          ]"
        />
      </template>
    </BasicTable>
    <!-- 编辑字典 -->
    <DictionaryModify ref="dictionaryModifyRef" @success="handleSuccess" />
    <!-- 查看字典 -->
    <DictionaryDetail ref="dictionaryDetailRef" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { Tag, message } from 'ant-design-vue';
  import { ref } from 'vue';
  import DictionaryDetail from './components/dictionaryDetail.vue';
  import DictionaryModify from './components/dictionaryModify.vue';
  import { AbsViewModal } from '#/common';
  import {
    deleteDictionaryDelete,
    getDictionaryGetList,
    getEnumGetValueList,
    postDictionaryDisable,
    postDictionaryEnable,
    deleteDictionaryRemoveCache,
  } from '@/api/manageCenter/systemManage';
  import { PageWrapper } from '@/components/Page';
  import { BasicColumn, BasicTable, TableAction, useTable } from '@/components/Table';
  import { useI18n } from '@/hooks/web/useI18n';
  import { useGrid } from '@/hooks/manageCenter/useGrid';
  import { useLoading } from '@/components/Loading';

  const [openFullLoading, closeFullLoading] = useLoading({
    tip: '加载中...',
  });
  const dictionaryModifyRef = ref<AbsViewModal | null>(null);
  const dictionaryDetailRef = ref<AbsViewModal | null>(null);
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
    deleteDictionaryRemoveCache()
      .then(() => {
        message.success('操作成功');
        reload();
      })
      .finally(() => {
        closeFullLoading();
      });
  }
  /**
   *禁用
   * @param record
   */
  async function handleDisable(record: Recordable) {
    await postDictionaryDisable({ id: record.id });
    reload();
  }
  /**
   *启用
   * @param record
   */
  async function handleEnable(record: Recordable) {
    await postDictionaryEnable({ id: record.id });
    reload();
  }
  /**
   *删除
   * @param record
   */
  async function handleDelete(record: Recordable) {
    await deleteDictionaryDelete(record.id);
    reload();
  }

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '类别名称', dataIndex: 'codeName' },
    { title: '编码名称', dataIndex: 'name' },
    { title: '显示名称', dataIndex: 'displayName' },
    { title: '编码值', dataIndex: 'value', sorter: true },
    { title: '状态', dataIndex: 'isActive', slots: { customRender: 'isActive' } },
    { title: '备注', dataIndex: 'remark' },
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
    api: getDictionaryGetList,
    actionColumn: {
      width: 160,
      title: '操作',
      dataIndex: 'action',
      slots: { customRender: 'action' },
      fixed: 'right',
    },
    showTableSetting: true,
    formConfig: {
      labelWidth: 90,
      ...useGrid(),
      schemas: [
        {
          label: '类别',
          component: 'ApiSelect',
          field: 'Code',
          componentProps: {
            api: getEnumGetValueList,
            params: { EnumName: 'DictionaryTypeEnum' },
            labelField: 'name',
            valueField: 'valueCode',
            resultField: 'items',
            showSearch: true,
            optionFilterProp: 'label',
          },
        },
        {
          label: '名称',
          component: 'Input',
          field: 'Name',
        },
        {
          label: '显示名称',
          component: 'Input',
          field: 'DisplayName',
        },
        {
          label: '状态',
          component: 'Select',
          field: 'IsActive',
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
      ],
    },
  });

  /**
   * 点击编辑
   */
  function handleEdit(record: Recordable) {
    dictionaryModifyRef.value?.open({
      id: record.id,
      isUpdate: true,
    });
  }
  /**
   * 点击添加
   */
  function handleCreate() {
    dictionaryModifyRef.value?.open({
      isUpdate: false,
    });
  }

  /**
   * 点击查看详情
   */
  function handleDetail(record: Recordable) {
    dictionaryDetailRef.value?.open({
      id: record.id,
    });
  }
</script>

<style lang="less" scoped></style>
../../../../hooks/manageCenter/useGrid
