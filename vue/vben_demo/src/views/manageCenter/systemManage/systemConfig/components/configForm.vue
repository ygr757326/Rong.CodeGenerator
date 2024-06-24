<template>
  <div>
    <BasicForm @register="Register" @submit="handleSubmit" />
  </div>
</template>

<script lang="ts" setup>
  import { Setting } from '@/api/manageCenter/model/systemManageModel';
  import { BasicForm, FormSchema, useForm } from '@/components/Form/index';
  import { nextTick, watch } from 'vue';
  import { putSettingUpdateSetting } from '@/api/manageCenter/systemManage';

  const $emit = defineEmits(['finish']);
  const props = defineProps({
    settings: Array<Setting>,
    visible: Boolean,
  });
  watch(
    () => props.visible,
    (newValue) => {
      if (newValue) {
        clearValidate();
        setFieldsValue(initData);
      }
    },
  );
  // 处理接收到的数据，初始化表单数据
  const initData = {};
  // 表单配置
  const schemas: FormSchema[] = [];
  props.settings?.forEach((v) => {
    v.name = v.name.replace(/\./g, ':');

    let obj: any = {};
    obj.label = v.displayName;
    obj.field = v.name;
    obj.required = true;
    obj.colProps = {
      span: 14,
    };
    if (v.tips) {
      obj.helpMessage = [v.tips];
    }
    if (v.valueType === 1) {
      obj.component = 'Input';
    } else if (v.valueType === 2) {
      obj.component = 'InputNumber';
    } else if (v.valueType === 3) {
      obj.component = 'Switch';
    } else if (v.valueType === 4) {
      obj.component = 'InputNumber';
    }
    schemas.push(obj);
    initData[v.name] = v.value;
  });

  //   微信表单配置
  const [Register, { validateFields, clearValidate, getFieldsValue, setFieldsValue }] = useForm({
    labelWidth: 300,
    schemas,
    actionColOptions: {
      span: 12,
    },
    submitButtonOptions: {
      text: '保存',
    },
  });
  nextTick(() => {
    setFieldsValue(initData);
  });

  //  保存
  async function handleSubmit() {
    await validateFields();
    const data = getFieldsValue();
    let newData = {};
    for (let key in data) {
      newData[key.replace(/\:/g, '.')] = data[key];
    }
    putSettingUpdateSetting(newData);
    $emit('finish');
  }
</script>

<style lang="less" scoped></style>
