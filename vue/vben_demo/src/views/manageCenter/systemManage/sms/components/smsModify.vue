<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="title"
    width="500px"
    @ok="handleSubmit"
  >
    <div class="modify" ref="smsModify">
      <BasicForm @register="Register" />
    </div>
  </BasicDrawer>
</template>

<script lang="ts" setup>
  import { ref } from 'vue';
  import {
    getEnumGetValueList,
    getSmsGet,
    postSmsCreate,
    putSmsUpdate,
  } from '@/api/manageCenter/systemManage';
  import { BasicDrawer, useDrawerInner } from '@/components/Drawer';
  import { BasicForm, FormSchema, useForm } from '@/components/Form/index';
  import { useLoading } from '@/components/Loading';

  const smsModify = ref(null);

  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: smsModify,
    props: {
      absolute: true,
    },
  });
  const isUpdate = ref(false);
  const title = ref<string>('编辑');
  const [registerDrawer, { closeDrawer }] = useDrawerInner(async (data) => {
    // 重置表单的值
    resetFields();
    if (data.isUpdate) {
      openWrapLoading();
      isUpdate.value = true;
      title.value = '编辑';
      getData({ id: data.id });
    } else {
      isUpdate.value = false;
      title.value = '新增';
    }
  });
  const $emit = defineEmits(['success']);
  /**
   * 提交表单
   */
  async function handleSubmit() {
    await validateFields();
    const data: any = getFieldsValue();
    if (isUpdate.value) {
      await putSmsUpdate(data.id, data);
    } else {
      await postSmsCreate(data);
    }
    closeDrawer();
    $emit('success');
  }

  function getData(data) {
    getSmsGet(data)
      .then((res) => {
        setFieldsValue(res);
      })
      .finally(() => {
        closeWrapLoading();
      });
  }

  // 表单配置
  const schemas: FormSchema[] = [
    {
      field: 'id',
      component: 'Input',
      label: 'id',
      colProps: {
        span: 24,
      },
      required: true,
      ifShow: isUpdate.value,
      show: false,
    },
    {
      field: 'concurrencyStamp',
      component: 'Input',
      label: '并发标记',
      colProps: {
        span: 24,
      },
      required: true,
      ifShow: isUpdate.value,
      show: false,
    },
    {
      field: 'platform',
      component: 'ApiSelect',
      label: '短信平台',
      colProps: {
        span: 24,
      },
      required: true,
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
      field: 'smsType',
      component: 'ApiSelect',
      label: '短信类型',
      colProps: {
        span: 24,
      },
      required: true,
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
      field: 'templateCode',
      component: 'Input',
      label: '模板编号',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'template',
      component: 'InputTextArea',
      label: '模板',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'validDuration',
      component: 'InputNumber',
      label: '有效时长(分钟)',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'signName',
      component: 'InputTextArea',
      label: '签名名称',
      colProps: {
        span: 24,
      },
    },
    {
      field: 'isActive',
      component: 'Switch',
      label: '是否启用',
      colProps: {
        span: 24,
      },
      required: true,
      defaultValue: true,
    },
    {
      field: 'remark',
      component: 'InputTextArea',
      label: '备注',
      colProps: {
        span: 24,
      },
    },
  ];
  //   微信表单配置
  const [Register, { validateFields, getFieldsValue, setFieldsValue, resetFields }] = useForm({
    labelWidth: 120,
    schemas,
    actionColOptions: {
      span: 12,
    },
    // 隐藏操作按钮
    showActionButtonGroup: false,
  });
</script>

<style lang="less" scoped>
  .modify {
    // padding: 20px;
    .action {
      margin-top: 20px;
      text-align: center;
    }
  }
</style>
