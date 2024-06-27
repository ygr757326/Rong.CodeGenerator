<template>
  <PageWrapper contentFullHeight>
      <BasicTable @register="register">
      <template #toolbar>
        <a-button auth="App.Create" type="primary" style="margin-right: 20px" @click="handleCreate">添加</a-button>
      </template>
       <template #action="{ record }">
        <TableAction
          :actions="[
            {
              auth:'App.Update',
              label: '编辑',
              onClick: handleEdit.bind(null, record),
            },
            { 
              auth:'App',
              label: '查看',
              onClick: handleDetail.bind(null, record),
            },
          ]"
          :dropDownActions="[
            {
              auth:'App.Delete',
              label: '删除',
              popConfirm: {
                title: '是否删除？',
                confirm: handleDelete.bind(null, record),
              },
            },
          ]"
        />
       </template>                    

        <template #clientDict3="{ value }">
          <Tag color="">
           {{ dictStore?.findName('MyDictCode', value) }}
          </Tag>
        </template>
        <template #isNowEffect="{ value }">
          <Tag :color="value ? 'green' : 'red'">
           {{ value ? '是' : '否' }}
          </Tag>
        </template>
        <template #isForceUpdate="{ value }">
          <Tag :color="value ? 'green' : 'red'">
           {{ value ? '是' : '否' }}
          </Tag>
        </template>
        <template #myEnum2="{ value }">
          <Tag color="">
           {{ enumStore?.findName('TestEnum', value) }}
          </Tag>
        </template>

    </BasicTable>
    <!-- 新增 -->
    <Add ref="addRef" @success="handleSuccess"></Add>
    <!-- 编辑 -->
    <Modify ref="modifyRef" @success="handleSuccess" />
    <!-- 查看 -->
    <Drawer ref="drawerRef" />
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { Tag } from 'ant-design-vue';
  import { ref } from 'vue';
  import Add from './components/add.vue';
  import Modify from './components/modify.vue';
  import Drawer from './components/drawer.vue';
  import { AbsViewModal } from '#/common';
  import { AppDelete, AppGetList } from './api';
  import { PageWrapper } from '@/components/Page';
  import { BasicColumn, BasicTable, TableAction, useTable } from '@/components/Table';
  import { useGrid } from '@/hooks/manageCenter/useGrid';
  import { formatToDate } from '@/utils/dateUtil';
  import { useDictStore } from '@/store/modules/dict';
  import { useEnumStore } from '@/store/modules/enum';

  const dictStore = useDictStore();
  const enumStore = useEnumStore();
  const addRef = ref<AbsViewModal | null>(null);
  const modifyRef = ref<AbsViewModal | null>(null);
  const drawerRef = ref<AbsViewModal | null>(null);

  // 编辑或新增成功
  function handleSuccess() {
    reload();
  }

  /**
   *删除
   */
  async function handleDelete(record: Recordable) {
    await AppDelete(record.id);
    reload();
  }

  /**
    * 表头
   */
  const columns: BasicColumn[] = [
    {
      title: 'Test',
      dataIndex: 'test',
    },
    {
      title: '头像',
      dataIndex: 'logo',
    },
    {
      title: '头像1',
      dataIndex: 'logos',
    },
    {
      title: '客户端',
      dataIndex: 'client',
    },
    {
      title: '客户端2',
      dataIndex: 'clientDict',
      customRender: ({ value }) => { 
        return dictStore?.findName('MyDictCode', value);
      },
      sorter: true,
    },
    {
      title: '客户端3',
      dataIndex: 'clientDict3',
      slots: { customRender: 'clientDict3' },
      sorter: true,
    },
    {
      title: 'App版本数字号',
      dataIndex: 'versionNumber',
    },
    {
      title: 'App版本数字号',
      dataIndex: 'versionNumber1',
    },
    {
      title: '是否马上生效',
      dataIndex: 'isNowEffect',
      slots: { customRender: 'isNowEffect' },
    },
    {
      title: '开始日期',
      dataIndex: 'startDate',
      customRender: ({ value }) => { 
        return formatToDate(value);
      },
    },
    {
      title: '结束日期',
      dataIndex: 'effectTime',
      customRender: ({ value }) => { 
        return formatToDate(value);
      },
    },
    {
      title: '是否强制性更新',
      dataIndex: 'isForceUpdate',
      slots: { customRender: 'isForceUpdate' },
    },
    {
      title: '金额',
      dataIndex: 'amount',
    },
    {
      title: '枚举',
      dataIndex: 'myEnum',
      customRender: ({ value }) => { 
        return enumStore?.findName('TestEnum', value);
      },
      sorter: true,
    },
    {
      title: '枚举1',
      dataIndex: 'myEnum1',
      customRender: ({ value }) => { 
        return enumStore?.findName('TestEnum', value);
      },
      sorter: true,
    },
    {
      title: '枚举2',
      dataIndex: 'myEnum2',
      slots: { customRender: 'myEnum2' },
      sorter: true,
    },

  ];

  /**
   * 表头配置
   */
  const [register, { reload }] = useTable({
    api: AppGetList,
    columns,
    tableSetting: {
      redo: true,
      size: true,
      setting: true,
      fullScreen: true,
    },
    useSearchForm: true,
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
          label: '版本号',
          field: 'version',
          component: 'Input',
          required: true,
        },
        {
          label: 'App版本数字号',
          field: 'versionNumber',
          component: 'InputNumber',
          componentProps: { precision: 0 },
        },
        {
          label: '是否马上生效',
          field: 'isNowEffect',
          component: 'Select',
          componentProps: {
            options: [
              {label: '是', value: true },
              {label: '否', value: false }
            ],
          },
        },
        {
          label: '指定生效时间',
          field: 'effectTimeStart',
          component: 'DatePicker',
          componentProps: {
            valueFormat: 'YYYY-MM-DD'
          },
        },

      ],
    },
  });

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
