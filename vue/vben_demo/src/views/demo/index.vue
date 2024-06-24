<template>
  <PageWrapper contentFullHeight>
    <BasicTable @register="register">
      <template #toolbar>
        <a-button type="primary" style="margin-right: 20px" @click="handleCreate">添加</a-button>
      </template>
      <template #isActive="{ record }">
        <Tag :color="record.isActive ? 'green' : 'red'">
          {{ record.isActive ? '已发布' : '未发布' }}
        </Tag>
      </template>
      <template #logo="{ record }">
        <GzImagePreview :width="50" :height="50" :fileId="record.logo"> </GzImagePreview>
      </template>
      <template #setIsActive="{ record }">
        <a-switch v-model:checked="record.isActive" @change="isActiveChange(record)" />
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
    <!-- 新增公告申请 -->
    <Add ref="addRef" @success="handleSuccess"></Add>
    <!-- 编辑公告申请 -->
    <Modify ref="modifyRef" @success="handleSuccess" />
    <!-- 查看公告申请 -->
    <Drawer ref="drawerRef" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { Tag } from 'ant-design-vue';
  import { ref } from 'vue';
  import Add from './components/add.vue';
  import Modify from './components/modify.vue';
  import Drawer from './components/drawer.vue';
  import { GzImagePreview } from '@/components/GzImage';
  import { AbsViewModal } from '#/common';
  import {
    deleteNoticeDelete,
    getNoticeGetList,
    postNoticeSetIsActive,
  } from '@/api/manageCenter/notice';
  import { PageWrapper } from '@/components/Page';
  import { BasicColumn, BasicTable, TableAction, useTable } from '@/components/Table';
  import { useGrid } from '@/hooks/manageCenter/useGrid';

  const addRef = ref<AbsViewModal | null>(null);
  const modifyRef = ref<AbsViewModal | null>(null);
  const drawerRef = ref<AbsViewModal | null>(null);

  // 编辑或新增成功
  function handleSuccess() {
    reload();
  }
  // 是否已发布状态改变
  function isActiveChange(record) {
    setIsActive(record.id, record.isActive);
  }
  /**
   *删除
   * @param record
   */
  async function handleDelete(record: Recordable) {
    await deleteNoticeDelete(record.id);
    reload();
  }

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '标题', dataIndex: 'title' },
    { title: '封面图片', dataIndex: 'logo', slots: { customRender: 'logo' } },
    { title: '是否发布', dataIndex: 'isActive', slots: { customRender: 'isActive' } },
    { title: '设置发布', dataIndex: 'isActive', slots: { customRender: 'setIsActive' } },
    { title: '发布时间', dataIndex: 'publicTime' },
    { title: '浏览次数', dataIndex: 'viewsNumber' },
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
    api: getNoticeGetList,
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
          label: '标题',
          component: 'Input',
          field: 'Title',
        },
        {
          label: '是否启用',
          component: 'Select',
          field: 'IsActive',
          componentProps: {
            options: [
              {
                label: '是',
                value: true,
              },
              {
                label: '否',
                value: false,
              },
            ],
          },
        },
      ],
    },
  });
  /**
   * 设置是否发布
   */
  async function setIsActive(id, data) {
    await postNoticeSetIsActive({ id: id, isActive: data });
    reload();
  }

  /**
   * 点击编辑
   */
  function handleEdit(record: Recordable) {
    modifyRef.value?.open({
      id: record.id,
    });
  }
  /**
   * 点击添加
   */
  function handleCreate() {
    addRef.value?.open();
  }

  /**
   * 点击查看详情
   */
  function handleDetail(record: Recordable) {
    drawerRef.value?.open({
      id: record.id,
    });
  }
</script>

<style lang="less" scoped></style>
