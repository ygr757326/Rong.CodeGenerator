<template>
  <a-drawer v-model:visible="visible" title="编辑" width="40vw">
    <template #extra>
      <a-button @click="visible = false">取消</a-button>
      <a-button @click="handleSubmit" type="primary" style="margin-left: 20px">提交</a-button>
    </template>
    <div ref="roleModify">
      <BasicForm @register="Register" />
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
  import { nextTick, ref } from 'vue';
  import {
    getIdentityRolesDetail,
    putIdentityRoles,
    postIdentityRolesAdd,
  } from '@/api/manageCenter/identity';
  import { BasicForm, FormSchema, useForm } from '@/components/Form/index';
  import { useLoading } from '@/components/Loading';

  const roleModify = ref(null);

  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: roleModify,
    props: {
      absolute: true,
    },
  });
  const isUpdate = ref(false);
  const visible = ref<boolean>(false);
  const id = ref<string>('');

  const $emit = defineEmits(['success']);
  function open(data) {
    visible.value = true;
    nextTick(() => {
      resetFields();
      isUpdate.value = data.isUpdate;
      if (isUpdate.value) {
        openWrapLoading();
        getData(data.id);
        isUpdate.value = true;
        id.value = data.id;
      }
    });
  }
  /**
   * 提交表单
   */
  async function handleSubmit() {
    await validateFields();
    const data: any = getFieldsValue();
    data.extraProperties = {};
    data.extraProperties.DisplayName = data.DisplayName;
    if (isUpdate.value) {
      await putIdentityRoles(id.value, data);
    } else {
      await postIdentityRolesAdd(data);
    }
    visible.value = false;
    $emit('success');
  }

  function getData(data) {
    getIdentityRolesDetail(data)
      .then((res) => {
        let roleValue: any = res;
        roleValue.DisplayName = roleValue.extraProperties?.DisplayName;
        setFieldsValue(roleValue);
      })
      .finally(() => {
        closeWrapLoading();
      });
  }

  // 表单配置
  const schemas: FormSchema[] = [
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
      field: 'name',
      component: 'Input',
      label: '角色编码',
      colProps: {
        span: 24,
      },
      required: true,
      show: () => !isUpdate.value,
    },
    {
      field: 'DisplayName',
      component: 'Input',
      label: '角色名称',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'isDefault',
      component: 'Switch',
      label: '是否默认',
      defaultValue: true,
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'isPublic',
      component: 'Switch',
      label: '是否公开',
      defaultValue: true,
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
