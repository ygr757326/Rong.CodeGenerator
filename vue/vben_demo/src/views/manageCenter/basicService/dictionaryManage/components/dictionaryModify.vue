<template>
  <a-drawer v-model:visible="visible" :title="title" width="40vw">
    <template #extra>
      <a-button @click="visible = false">取消</a-button>
      <a-button @click="handleSubmit" type="primary" style="margin-left: 20px">提交</a-button>
    </template>
    <div ref="dictionaryModify">
      <BasicForm @register="Register" v-if="visible">
        <!-- <template #logo="{ model, field }">
          <ImageUpload v-model="model[field]"></ImageUpload>
        </template> -->
      </BasicForm>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
  import { nextTick, ref } from 'vue';
  import {
    getDictionaryGet,
    getEnumGetValueList,
    postDictionaryCreate,
    putDictionaryUpdate,
  } from '@/api/manageCenter/systemManage';
  import { BasicForm, FormSchema, useForm } from '@/components/Form/index';
  import { useLoading } from '@/components/Loading';

  import { postFileOssAppServiceUploadForm } from '@/api/manageCenter/fileManage';
  const dictionaryModify = ref(null);

  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: dictionaryModify,
    props: {
      absolute: true,
    },
  });
  const isUpdate = ref(false);
  const visible = ref<boolean>(false);
  const title = ref<string>('编辑');
  const $emit = defineEmits(['success']);
  function open(data) {
    visible.value = true;
    nextTick(() => {
      resetFields();
    });
    if (data.isUpdate) {
      openWrapLoading();
      isUpdate.value = true;
      title.value = '编辑';
      getData({ id: data.id });
    } else {
      isUpdate.value = false;
      title.value = '新增';
    }
  }
  /**
   * 提交表单
   */
  async function handleSubmit() {
    await validateFields();
    const data: any = getFieldsValue();
    console.log('---77', data);

    if (isUpdate.value) {
      await putDictionaryUpdate(data.id, data);
    } else {
      await postDictionaryCreate(data);
    }
    visible.value = false;
    $emit('success');
  }

  async function getData(data) {
    getDictionaryGet(data)
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
      field: 'logo',
      component: 'ImageUpload',
      label: '封面图片',
      colProps: {
        span: 24,
      },
      // componentProps: {
      //   api: postFileOssAppServiceUploadForm,
      //   valueField: 'fileList',
      // },
      // slot: 'logo',
    },
    {
      label: '类别',
      component: 'ApiSelect',
      field: 'code',
      colProps: {
        span: 24,
      },
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
      field: 'name',
      component: 'Input',
      label: '名称',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'value',
      component: 'Input',
      label: '值',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'displayName',
      component: 'Input',
      label: '显示名称',
      colProps: {
        span: 24,
      },
      required: true,
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
  defineExpose({ open });
</script>
