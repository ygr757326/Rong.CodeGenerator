<template>
  <PageWrapper contentFullHeight>
    <!-- 区域管理 -->
    <BasicTable @register="register">
      <template #toolbar>
        <a-button type="primary" style="margin-right: 20px" @click="removeCache">移除缓存</a-button>
      </template>
      <template #parent="{ record }">
        <span>{{ record.parent?.name }}</span>
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
    <!-- 查看区域 -->
    <AreaDetail @register="registerDetailDrawer" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { Tag, message } from 'ant-design-vue';
  import AreaDetail from './components/areaDetail.vue';
  import {
    deleteAreaDelete,
    getAreaGetChildrenDropDownList,
    getAreaGetList,
    postAreaDisable,
    postAreaEnable,
    deleteAreaRemoveCache,
  } from '@/api/manageCenter/area';
  import { getEnumGetValueList } from '@/api/manageCenter/systemManage';
  import { useDrawer } from '@/components/Drawer';
  import { PageWrapper } from '@/components/Page';
  import { BasicColumn, BasicTable, TableAction, useTable } from '@/components/Table';
  import { useI18n } from '@/hooks/web/useI18n';
  import { useGrid } from '@/hooks/manageCenter/useGrid';
  import { useLoading } from '@/components/Loading';

  const [openFullLoading, closeFullLoading] = useLoading({
    tip: '加载中...',
  });

  const { t } = useI18n();
  const [registerDetailDrawer, { openDrawer: openDetailDrawer }] = useDrawer();
  /**
   *移除缓存
   * @param record
   */
  function removeCache() {
    openFullLoading();
    deleteAreaRemoveCache()
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
    await deleteAreaDelete(record.id);
    reload();
  }
  /**
   *禁用
   * @param record
   */
  async function handleDisable(record: Recordable) {
    await postAreaDisable({ id: record.id });
    reload();
  }
  /**
   *启用
   * @param record
   */
  async function handleEnable(record: Recordable) {
    await postAreaEnable({ id: record.id });
    reload();
  }

  // 表格表头
  const columns: BasicColumn[] = [
    { title: '地区名称', dataIndex: 'name' },
    { title: '地区简称', dataIndex: 'shortName' },
    { title: '地区编码', dataIndex: 'code', sorter: true },
    { title: '级别', dataIndex: 'levelText' },
    {
      title: '父级名称',
      dataIndex: 'parent',
      slots: { customRender: 'parent' },
    },
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
    api: getAreaGetList,
    beforeFetch: (params) => {
      const areaCode = params.ParentCode;
      if (areaCode && areaCode.length) {
        params = Object.assign(params, { ParentCode: areaCode[areaCode.length - 1] });
      }
      return params;
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
          label: '地区名称',
          component: 'Input',
          field: 'Name',
        },
        {
          label: '地区简称',
          component: 'Input',
          field: 'ShortName',
        },
        {
          label: '拼音',
          component: 'Input',
          field: 'Spell',
        },
        {
          label: '父级地区',
          component: 'ApiCascader',
          field: 'ParentCode',
          componentProps: {
            api: (e) => {
              return new Promise((res, rec) => {
                getAreaGetChildrenDropDownList({ Code: e.parentCode })
                  .then((r) => {
                    if (r.level > 2) {
                      res([]);
                      return;
                    }
                    res(r);
                  })
                  .catch((e) => rec(e));
              });
            },
            resultField: 'children',
            labelField: 'name',
            valueField: 'value',
          },
        },
        {
          label: '城市编码/区号',
          component: 'Input',
          field: 'CityCode',
        },
        {
          label: '级别',
          component: 'ApiSelect',
          field: 'Level',
          componentProps: {
            api: getEnumGetValueList,
            params: { EnumName: 'AreaLevelEnum' },
            labelField: 'name',
            valueField: 'value',
            resultField: 'items',
            showSearch: true,
            optionFilterProp: 'label',
          },
        },
        {
          label: '状态',
          component: 'Select',
          field: 'isActive',
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
   * 点击查看详情
   */
  function handleDetail(record: Recordable) {
    openDetailDrawer(true, {
      id: record.id,
    });
  }
</script>

<style lang="less" scoped></style>
