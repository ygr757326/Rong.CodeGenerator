<template>
  <a-drawer v-model:visible="visible" :title="title" width="800px">
    <template #extra>
      <a-button @click="visible = false">取消</a-button>
      <a-button @click="handleSubmit" type="primary" style="margin-left: 20px">提交</a-button>
    </template>
    <div ref="modifyFormRef">
      <BasicForm @register="Register">
        <template #logo="{ model, field }">
          <GzUploadFile v-model="model[field]"></GzUploadFile>
        </template>
      </BasicForm>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
  import { nextTick, ref } from 'vue';
  import { getNoticeGet, putNoticeUpdate } from '@/api/manageCenter/notice';
  import { BasicForm, FormSchema, useForm } from '@/components/Form/index';
  import { useLoading } from '@/components/Loading';
  import { GzUploadFile } from '@/components/GzUploadFile';

  const modifyFormRef = ref(null);

  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: modifyFormRef,
    props: {
      absolute: true,
    },
  });

  const visible = ref<boolean>(false);
  const title = ref<string>('编辑');
  const $emit = defineEmits(['success']);
  function open(data) {
    visible.value = true;
    nextTick(() => {
      resetFields();
    });
    openWrapLoading();
    title.value = '编辑';
    getData({ id: data.id });
  }
  /**
   * 提交表单
   */
  async function handleSubmit() {
    await validateFields();
    const data: any = getFieldsValue();
    await putNoticeUpdate(data.id, data);
    visible.value = false;
    $emit('success');
  }

  async function getData(data) {
    getNoticeGet(data)
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
      show: false,
    },
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
      component: 'Input',
      label: '封面图片',
      colProps: {
        span: 24,
      },
      slot: 'logo',
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
