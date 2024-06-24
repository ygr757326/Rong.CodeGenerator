<template>
  <PageWrapper dense contentFullHeight fixedHeight contentClass="flex">
    <DeptTree class="w-1/4 xl:w-1/5" @select="handleSelect" />
    <BasicTable @register="registerTable" class="w-3/4 xl:w-4/5" :searchInfo="searchInfo">
      <template #toolbar>
        <a-button type="primary" style="margin-right: 20px" @click="handleCreate">添加</a-button>
        <a-button type="primary" style="margin-right: 20px" @click="handleSync">同步数据</a-button>
      </template>
      <template #isActive="{ record }">
        <Tag :color="record.isActive ? 'green' : 'red'">
          {{ record.isActive ? t('common.enabled') : t('common.disEnabled') }}
        </Tag>
      </template>
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
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
      </template>
    </BasicTable>
    <!-- 编辑用户 -->
    <UserModify @register="registerModifyModal" @success="handleSuccess"></UserModify>
    <!-- 查看用户详情 -->
    <UserDetail @register="registerDetailDrawer"></UserDetail>
  </PageWrapper>
</template>
<script lang="ts" setup>
  import { PageWrapper } from '@/components/Page';
  import { ref, reactive } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { BasicTable, useTable, TableAction } from '@/components/Table';
  import DeptTree from './DeptTree.vue';
  import { useModal } from '@/components/Modal';
  import { columns, searchFormSchema } from './account.data';
  import { message } from 'ant-design-vue';
  import { useI18n } from '@/hooks/web/useI18n';
  import UserModify from './components/userModify.vue';
  import UserDetail from './components/userDetail.vue';
  import { useDrawer } from '@/components/Drawer';
  import { useLoading } from '@/components/Loading';
  import {
    getIdentityUsers,
    deleteIdentityUsers,
    postSysUserResetPassword,
    postUserSync,
  } from '@/api/manageCenter/identity';
  defineOptions({ name: 'AccountManagement' });
  const [registerModifyModal, { openModal: modifyModal }] = useModal();
  const [registerDetailDrawer, { openDrawer: openDetailDrawer }] = useDrawer();
  const { t } = useI18n();

  const searchInfo = reactive<Recordable>({});
  // 选择的组织机构
  const organizationUnitId = ref('');
  const [openFullLoading, closeFullLoading] = useLoading({
    tip: '加载中...',
  });
  const [registerTable, { reload }] = useTable({
    title: '账号列表',
    beforeFetch: (params) => {
      return Object.assign(params, { OrganizationUnitId: organizationUnitId.value });
    },
    api: getIdentityUsers,
    rowKey: 'id',
    columns,
    formConfig: {
      labelWidth: 70,
      schemas: searchFormSchema,
      autoSubmitOnEnter: true,
    },
    useSearchForm: true,
    showTableSetting: true,
    bordered: true,
    handleSearchInfoFn(info) {
      console.log('handleSearchInfoFn', info);
      return info;
    },
    actionColumn: {
      width: 140,
      title: '操作',
      dataIndex: 'action',
      // slots: { customRender: 'action' },
    },
  });
  // 删除账号
  async function handleDelete(record: Recordable) {
    if (record.userName === 'admin') {
      message.error('admin为主账号，禁止删除');
    } else {
      await deleteIdentityUsers(record.id);
      reload();
    }
  }
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

  /**
   * 点击编辑
   */
  function handleEdit(record: Recordable) {
    modifyModal(true, {
      id: record.id,
      isUpdate: true,
      organizationUnitId: organizationUnitId.value,
    });
  }
  /**
   * 点击创建
   */
  function handleCreate() {
    if (!organizationUnitId.value) {
      message.error('请先选择组织机构');
      return;
    }
    modifyModal(true, {
      isUpdate: false,
      organizationUnitId: organizationUnitId.value,
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

  // 重置密码
  function handleResetPwd(record: Recordable) {
    postSysUserResetPassword(record.id);
  }

  function handleSuccess() {
    reload();
  }
  function handleSelect(data) {
    organizationUnitId.value = data;
    reload();
  }
</script>
