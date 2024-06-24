<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    title="编辑"
    width="540px"
    @ok="handleSubmit"
  >
    <div class="modify" ref="areaModify">
      <BasicForm @register="Register">
        <template #feeRateAmountMax="{ model, field }">
          <a-input type="number" v-model:value="model[field]" placeholder="请输入" allow-clear />
        </template>
      </BasicForm>
    </div>
  </BasicDrawer>
</template>

<script lang="ts" setup>
  import { ref } from 'vue';
  import { getAreaGet, putAreaUpdate } from '@/api/manageCenter/area';
  import { BasicDrawer, useDrawerInner } from '@/components/Drawer';
  import { BasicForm, type FormSchema, useForm } from '@/components/Form';
  import { useLoading } from '@/components/Loading';

  const areaModify = ref(null);
  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: areaModify,
    props: {
      absolute: true,
    },
  });
  // 选中的地区列表
  // const areaList: any = ref([]);
  const $emit = defineEmits(['success']);
  const [registerDrawer, { closeDrawer }] = useDrawerInner(async (data) => {
    resetFields();
    openWrapLoading();
    getData({ id: data.id });
  });
  // // 递归处理数据
  // function handleData(data) {
  //   data.forEach((v) => {
  //     v.label = v.name;
  //     v.value = v.code;
  //     if (v.children) {
  //       handleData(v.children);
  //     } else {
  //       return;
  //     }
  //   });
  // }
  function getData(data) {
    getAreaGet(data)
      .then((res) => {
        setFieldsValue(res);
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
  /**
   * 提交表单
   */
  async function handleSubmit() {
    await validateFields();
    const data: any = getFieldsValue();
    // data.platformCode = data.platformCode[data.platformCode.length - 1];
    // console.log(data, 'sumbit');
    await putAreaUpdate(data.id, data);
    closeDrawer();
    $emit('success');
  }

  // 表单配置
  const schemas: FormSchema[] = [
    {
      show: false,
      field: 'id',
      component: 'Input',
      label: 'id',
      colProps: {
        span: 24,
      },
      required: true,
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
  ];
  //   微信表单配置
  const [Register, { validateFields, getFieldsValue, setFieldsValue, resetFields }] = useForm({
    labelWidth: 160,
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
