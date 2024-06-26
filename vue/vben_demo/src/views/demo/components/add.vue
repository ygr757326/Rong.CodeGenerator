<template>
  <a-drawer v-model:visible="visible" :title="title" width="800px">
    <template #extra>
      <a-button @click="visible = false">取消</a-button>
      <a-button @click="handleSubmit" type="primary" style="margin-left: 20px">提交</a-button>
    </template>
    <div>
      <BasicForm @register="Register"> </BasicForm>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
  import { nextTick, ref } from 'vue';
  import { postNoticeCreate } from '@/api/manageCenter/notice';
  import { GzUploadFile } from '@/components/GzUploadFile';
  import { BasicForm, FormSchema, useForm } from '@/components/Form/index';

  const visible = ref<Boolean>(false);
  const title = ref<string>('新增');
  const $emit = defineEmits(['success']);

  function open() {
    visible.value = true;
    nextTick(() => {
      resetFields();
    });
  }
  /**
   * 提交表单
   */
  async function handleSubmit() {
    await validateFields();
    const data: any = getFieldsValue();
    await postNoticeCreate(data);
    visible.value = false;
    $emit('success');
  }

  // 表单配置
  const schemas: FormSchema[] = [
    {
      field: 'title',
      component: 'Input',
      label: '标题',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'logo',
      component: 'ImageUpload',
      label: '封面图片',
      colProps: {
        span: 24,
      },
    },
    {
      field: 'content',
      component: 'Input',
      label: '内容',
      colProps: {
        span: 24,
      },
      required: true,
    },
  ];
  //   微信表单配置
  const [Register, { validateFields, getFieldsValue, resetFields }] = useForm({
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
