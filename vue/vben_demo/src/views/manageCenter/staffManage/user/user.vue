<template>
  <PageWrapper contentFullHeight>
    <!-- 用户配置 -->
    <BasicTable @register="register">
      <template #toolbar>
        <a-button type="primary" style="margin-right: 20px" @click="handleCreate">添加</a-button>
        <a-button type="primary" style="margin-right: 20px" @click="handleSync">同步数据</a-button>
      </template>
      <template #isEnableFeeRateAmountMax="{ record }">
        <span>{{ record.isEnableFeeRateAmountMax ? '是' : '否' }}</span>
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
            {
              label: '重置密码',
              popConfirm: {
                title: '是否重置密码？',
                confirm: handleResetPwd.bind(null, record),
              },
            },
          ]"
        />
      </template>
    </BasicTable>
    <!-- 编辑用户 -->
    <UserModify @register="registerModal" @success="handleSuccess" />
    <!-- 查看用户详情 -->
    <UserDetail @register="registerDetailDrawer" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '@/components/Page';
  import { BasicColumn, BasicTable, useTable, TableAction } from '@/components/Table';
  import {
    getIdentityUsers,
    deleteIdentityUsers,
    postSysUserResetPassword,
    postUserSync,
  } from '@/api/manageCenter/identity';

  import UserModify from './components/userModify.vue';
  import UserDetail from './components/userDetail.vue';

  import { message, Tag } from 'ant-design-vue';
  import { useI18n } from '@/hooks/web/useI18n';
  import { useDrawer } from '@/components/Drawer';
  import { useModal } from '@/components/Modal';
  import { useGrid } from '@/hooks/manageCenter/useGrid';
  import { useLoading } from '@/components/Loading';

  const [openFullLoading, closeFullLoading] = useLoading({
    tip: '加载中...',
  });
  const [registerModal, { openModal }] = useModal();
  const { t } = useI18n();

  const [registerDetailDrawer, { openDrawer: openDetailDrawer }] = useDrawer();
  // 同步数据
  function handleSync() {
    openFullLoading();
    postUserSync()
      .then(() => {
        message.success('操作成功');
        reload();
      })
      .finally(() => {
        closeFullLoading();
      });
  }
  // 删除账号
  async function handleDelete(record: Recordable) {
    if (record.userName === 'admin') {
      message.error('admin为主账号，禁止删除');
    } else {
      await deleteIdentityUsers(record.id);
      reload();
    }
  }

  function handleSuccess() {
    reload();
  }
  // 修改密码
  // function handleChangePwd(record: Recordable) {
  //   console.log(record);
  // }
  // 重置密码
  function handleResetPwd(record: Recordable) {
    postSysUserResetPassword(record.id);
  }

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '用户名', dataIndex: 'userName' },
    { title: '真实姓名', dataIndex: 'name' },
    { title: '邮箱', dataIndex: 'email' },
    { title: '手机号码', dataIndex: 'phoneNumber' },
    { title: '创建时间', dataIndex: 'creationTime', sorter: true },
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
    api: getIdentityUsers,
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
        //TODO待确认字段
        // {
        //   label: '组织',
        //   component: 'Input',
        //   field: 'OrganizationUnitId',
        // },
        // {
        //   label: '是否只有自己公司',
        //   component: 'Input',
        //   field: 'OnlyOwnCorp',
        // },
        {
          label: '用户名称',
          component: 'Input',
          field: 'filter',
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
    openModal(true, {
      id: record.id,
      isUpdate: true,
    });
  }
  /**
   * 点击创建
   */
  function handleCreate() {
    openModal(true, {
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
