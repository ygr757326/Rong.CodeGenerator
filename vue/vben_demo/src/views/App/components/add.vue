
<template>
  <a-drawer v-model:visible="visible" title="新增 - App应用" width="800px">
    <template #extra>
      <a-button @click="visible = false">取消</a-button>
      <a-button @click="handleSubmit" type="primary" style="margin-left: 20px">提交</a-button>
    </template>
    <div>
            <BasicForm @register="Register" v-if="visible">
            <!-- <template #healthAttachs="{ model, field }"></template> -->
        </BasicForm>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
  import { nextTick, ref } from 'vue';
  import { AppCreate } from '../api';
  import { BasicForm, FormSchema, useForm } from '@/components/Form/index';
  import { useDictStore } from '@/store/modules/dict';
  import { useEnumStore } from '@/store/modules/enum';

  const dictStore = useDictStore();
  const enumStore = useEnumStore();
  const visible = ref<Boolean>(false);
  const $emit = defineEmits(['success']);

  /**
   * 表单
   */
  const schemas: FormSchema[] = [
      {
      label: '头像',
      field: 'logo',
      component: 'ImageUpload',
      componentProps: {
        multiple: false,
      },
      required: true,
    },
    {
      label: '头像1',
      field: 'logos',
      component: 'ImageUpload',
      componentProps: {
        multiple: true,
      },
      required: true,
    },
    {
      label: '客户端',
      field: 'client',
      component: 'Input',
      required: true,
    },
    {
      label: '客户端2',
      field: 'clientDict',
      component: 'RadioGroup',
      componentProps: {
        options: dictStore?.findCodeSelect('MyDictCode'),
        showSearch: true,
        optionFilterProp: 'label'
      },
      required: true,
    },
    {
      label: '客户端3',
      field: 'clientDict3',
      component: 'Select',
      componentProps: {
        options: dictStore?.findCodeSelect('MyDictCode'),
        showSearch: true,
        optionFilterProp: 'label'
      },
      required: true,
    },
    {
      label: 'App版本数字号',
      field: 'versionNumber',
      component: 'InputNumber',
      componentProps: { precision: 0 },
      required: true,
    },
    {
      label: '是否马上生效',
      field: 'isNowEffect',
      component:'Switch',
      required: true,
    },
    {
      label: '开始日期',
      field: 'startDate',
      component: 'DatePicker',
      componentProps: {
        valueFormat: 'YYYY-MM-DD'
      },
      required: true,
    },
    {
      label: '结束日期',
      field: 'effectTime',
      component: 'DatePicker',
      componentProps: {
        valueFormat: 'YYYY-MM-DD'
      },
    },
    {
      label: '是否强制性更新',
      field: 'isForceUpdate',
      component:'Switch',
      required: true,
    },
    {
      label: '金额',
      field: 'amount',
      component:'InputNumber',
      componentProps: { precision: 2 },
      required: true,
    },
    {
      label: '枚举',
      field: 'myEnum',
      component: 'Select',
      componentProps: {
        options: enumStore?.findCodeSelect('TestEnum'),
        showSearch: true,
        optionFilterProp: 'label'
      },
      required: true,
    },
    {
      label: '枚举1',
      field: 'myEnum1',
      component: 'Select',
      componentProps: {
        options: enumStore?.findCodeSelect('TestEnum'),
        showSearch: true,
        optionFilterProp: 'label'
      },
      required: true,
    },
    {
      label: '枚举2',
      field: 'myEnum2',
      component: 'RadioGroup',
      componentProps: {
        options: enumStore?.findCodeSelect('TestEnum'),
        showSearch: true,
        optionFilterProp: 'label'
      },
      required: true,
    },

  ];

  /**
   * 表单配置
   */
  const [Register, { validateFields, getFieldsValue, resetFields }] = useForm({
    labelWidth: 120,
    schemas,
    actionColOptions: {
      span: 12,
    },
    baseColProps: {
      span: 24,
    },
    // 是否显示操作按钮
    showActionButtonGroup: false,
  });

  /**
  * 提交表单
  */
  async function handleSubmit() {
    //验证
    await validateFields();
    //获取
    const data: any = getFieldsValue();
    //创建
    await AppCreate(data);

    visible.value = false;
    $emit('success');
  }

  /**
  * 弹框
  */
  function open() {
    visible.value = true;
    nextTick(() => {
      resetFields();
    });
  }
  defineExpose({ open });

</script>