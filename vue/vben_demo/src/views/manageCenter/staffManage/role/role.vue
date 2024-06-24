<template>
  <PageWrapper contentFullHeight>
    <!-- 角色配置 -->
    <BasicTable @register="register">
      <template #toolbar>
        <a-button type="primary" style="margin-right: 20px" @click="handleCreate">添加</a-button>
      </template>
      <template #isDefault="{ record }">
        <Tag :color="record.isDefault ? 'red' : 'green'">
          {{ record.isDefault ? '是' : '否' }}
        </Tag>
      </template>
      <template #extraProperties="{ record }">
        {{ record.extraProperties.DisplayName }}
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              label: '授权',
              onClick: handleEmpower.bind(null, record),
            },
            {
              label: '编辑',
              onClick: handleEdit.bind(null, record),
            },
          ]"
          :dropDownActions="[
            {
              label: '删除',
              popConfirm: {
                title: '是否删除角色？',
                confirm: handleDelete.bind(null, record),
              },
            },
          ]"
        />
      </template>
    </BasicTable>
    <!-- 编辑角色 -->
    <RoleModify ref="roleModifyRef" @success="handleSuccess" />
    <!--角色授权-->
    <RoleEmpower ref="roleEmpowerRef" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '@/components/Page';
  import { BasicColumn, BasicTable, useTable, TableAction } from '@/components/Table';
  import { getIdentityRoles, deleteIdentityRoles } from '@/api/manageCenter/identity';
  import { ref } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { AbsViewModal } from '#/common';
  import RoleModify from './components/roleModify.vue';
  import RoleEmpower from './components/roleEmpower.vue';

  const roleModifyRef = ref<AbsViewModal | null>(null);
  const roleEmpowerRef = ref<AbsViewModal | null>(null);
  // 编辑或新增成功
  function handleSuccess() {
    reload();
  }
  // 删除账号
  async function handleDelete(record: Recordable) {
    await deleteIdentityRoles(record.id);
    reload();
  }

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '角色名称', dataIndex: 'extraProperties', slots: { customRender: 'extraProperties' } },
    { title: '默认', dataIndex: 'isDefault', slots: { customRender: 'isDefault' } },
  ];
  const [register, { reload }] = useTable({
    columns,
    tableSetting: {
      redo: true,
      size: true,
      setting: true,
      fullScreen: true,
    },
    useSearchForm: false,
    api: getIdentityRoles,
    actionColumn: {
      width: 160,
      title: '操作',
      dataIndex: 'action',
      slots: { customRender: 'action' },
      fixed: 'right',
    },
    showTableSetting: true,
    // formConfig: {
    //   labelWidth: 120,
    //   baseColProps: {
    //     span: 8,
    //   },
    //   schemas: [
    //     {
    //       label: '角色名称',
    //       component: 'Input',
    //       field: 'name',
    //     },

    //     {
    //       label: '是否为默认',
    //       component: 'Select',
    //       field: 'isDefault',
    //       componentProps: {
    //         options: [
    //           {
    //             label: '是',
    //             value: true,
    //             key: '1',
    //           },
    //           {
    //             label: '否',
    //             value: false,
    //             key: '2',
    //           },
    //         ],
    //       },
    //     },
    //   ],
    // },
  });
  /**
   * 点击添加
   */
  function handleCreate(record: Recordable) {
    roleModifyRef.value?.open({
      isUpdate: false,
    });
  }
  /**
   * 点击编辑
   */
  function handleEdit(record: Recordable) {
    roleModifyRef.value?.open({
      id: record.id,
      isUpdate: true,
    });
  }
  /**
   * 点击授权
   */
  function handleEmpower(record: Recordable) {
    roleEmpowerRef.value?.open({
      id: record.id,
      name: record.name,
      displayName: record.extraProperties.DisplayName,
    });
  }
</script>

<style lang="less" scoped></style>
