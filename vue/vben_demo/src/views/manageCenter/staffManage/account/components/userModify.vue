<template>
  <BasicModal v-bind="$attrs" @register="registerModal" :title="title" @ok="handleOk" width="40vw">
    <div class="modify" ref="modifyRef">
      <a-tabs v-model:activeKey="activeKey">
        <a-tab-pane key="userInfo" tab="用户信息">
          <BasicForm @register="Register"></BasicForm>
        </a-tab-pane>
        <a-tab-pane key="roleInfo" tab="角色">
          <a-checkbox-group v-model:value="roleNames" style="width: 100%">
            <a-row :gutter="[16, 16]">
              <a-col :span="24" v-for="item in roleList" :key="item.name">
                <a-checkbox :value="item.name">{{ item.extraProperties.DisplayName }}</a-checkbox>
              </a-col>
            </a-row>
          </a-checkbox-group>
        </a-tab-pane>
        <!-- <a-tab-pane key="orgInfo" tab="组织机构">
          <a-tree
            :defaultExpandAll="true"
            v-model:selectedKeys="selectedKeys"
            v-model:checkedKeys="checkedKeys"
            checkable
            :checkStrictly="true"
            :fieldNames="{ children: 'children', title: 'displayName', key: 'id' }"
            :tree-data="treeData"
            @check="handleCheckKeys"
          >
            <template #title="{ displayName }">
              <span>{{ displayName }}</span>
            </template>
          </a-tree>
        </a-tab-pane> -->
      </a-tabs>
    </div>
  </BasicModal>
</template>

<script lang="ts" setup>
  import { message } from 'ant-design-vue';
  import { ref, watch } from 'vue';
  import {
    getIdentityUsersDetail,
    getSysUserGetAssignableRoles,
    getSysUserGetRoles,
    postSysUserCreate,
    putIdentityUsers,
    getAvailableOrganizationUnitsTree,
    getOrganizationUnits,
  } from '/@/api/manageCenter/identity';
  import { BasicForm, FormSchema, useForm } from '/@/components/Form/index';
  import { useLoading } from '/@/components/Loading';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { useValidatePhone } from '/@/hooks/manageCenter/useValidate';

  const modifyRef = ref(null);

  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: modifyRef,
    props: {
      absolute: true,
    },
  });
  const title = ref<string>('编辑');
  const isUpdate = ref<boolean>(false);
  // const selectedKeys = ref<string[]>([]);
  // const checkedKeys = ref<string[]>([]);
  // const treeData: any = ref([]);
  /**
   *   当前用户角色
   */
  const roleNames = ref<string[]>([]);
  // 所属组织机构
  const OrganizationUnitId = ref<string>('');
  // 可用角色列表
  const roleList: any = ref([]);
  const [registerModal, { closeModal }] = useModalInner(async (data) => {
    resetFields();
    activeKey.value = 'userInfo';
    OrganizationUnitId.value = data.organizationUnitId;
    // await getAvailableOrgTree();
    if (data.isUpdate) {
      openWrapLoading();
      isUpdate.value = true;
      title.value = '编辑';
      getData({ id: data.id });
    } else {
      isUpdate.value = false;
      title.value = '新增';
      roleNames.value=[]
    }
  });
  const $emit = defineEmits(['success']);
  // tab选中项
  const activeKey = ref('userInfo');

  // function handleCheckKeys(keys) {
  //   let data: Array<string> = [];
  //   data.push(keys.checked[keys.checked.length - 1]);
  //   selectedKeys.value = data;
  //   checkedKeys.value = data;
  //   OrganizationUnitId.value = data;
  //   console.log('keys', keys, data, checkedKeys);
  // }
  /**
   *   获取可用组织机构树
   */
  // function getAvailableOrgTree() {
  //   openWrapLoading();
  //   getAvailableOrganizationUnitsTree()
  //     .then((res) => {
  //       treeData.value = res.items;
  //     })
  //     .finally(() => {
  //       closeWrapLoading();
  //     });
  // }
  /**
   *   获取用户组织机构
   */
  // function getOrgUnits(data) {
  //   openWrapLoading();
  //   getOrganizationUnits(data)
  //     .then((res) => {
  //       selectedKeys.value = res.items.map((v) => v.id);
  //       checkedKeys.value = res.items.map((v) => v.id);
  //     })
  //     .finally(() => closeWrapLoading());
  // }
  /**
   *   当前用户角色
   */
  const handleOk = async () => {
    if (roleNames.value.length === 0) {
      message.error('请选择角色');
      return;
    }
    // if (OrganizationUnitId.value.length === 0) {
    //   message.error('请选择所属组织机构');
    //   return;
    // }
    await validateFields();
    const data = getFieldsValue();
    console.log('first', data);
    if (!isUpdate.value && data.password !== data.confirmPassword) {
      message.error('两次输入密码不一致');
    } else {
      if (isUpdate.value) {
        await putIdentityUsers(
          data.id,
          Object.assign(data, {
            roleNames: roleNames.value,
            extraProperties: { OrganizationUnitId: OrganizationUnitId.value },
          }),
        );
      } else {
        await postSysUserCreate(
          Object.assign(data, {
            roleNames: roleNames.value,
            extraProperties: { OrganizationUnitId: OrganizationUnitId.value },
          }),
        );
      }
      closeModal();
      $emit('success');
    }
  };
  watch(activeKey, async (newValue) => {
    if (newValue === 'roleInfo') {
      let res = await getSysUserGetAssignableRoles();
      roleList.value = res.items;
    }
  });
  async function getData(data) {
    const res = await getIdentityUsersDetail(data);
    setFieldsValue(res);
    const roleRes = await getSysUserGetRoles(data);
    roleNames.value = roleRes.items.map((v) => v.name);
    // await getOrgUnits({ id: data.id });
    closeWrapLoading();
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
      ifShow: () => isUpdate.value,
    },
    {
      field: 'userName',
      component: 'Input',
      label: '用户名',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'name',
      component: 'Input',
      label: '真实名称',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'email',
      component: 'Input',
      label: '邮箱',
      colProps: {
        span: 24,
      },
      required: true,
    },
    {
      field: 'phoneNumber',
      component: 'Input',
      label: '手机号码',
      colProps: {
        span: 24,
      },
      rules: [
        { required: true, message: '请输入手机号', trigger: 'blur' },
        {
          validator: (_, value) => {
            if (!value) {
              return;
            }
            if (!useValidatePhone(value)) {
              return Promise.reject('请输入正确的11位手机号码');
            }
            return Promise.resolve();
          },
          trigger: 'change',
        },
      ],
    },
    {
      field: 'password',
      component: 'InputPassword',
      label: '密码',
      colProps: {
        span: 24,
      },
      // ifShow: () => !isUpdate.value,
      // rules: [
      //   {
      //     required: true,
      //     message: '请输入密码',
      //     trigger: 'blur',
      //   },
      //   {
      //     min: 6,
      //     message: '密码至少6位',
      //     trigger: 'blur',
      //   },
      // ],
    },
    {
      field: 'confirmPassword',
      component: 'InputPassword',
      label: '确认密码',
      // ifShow: () => !isUpdate.value,
      // rules: [
      //   {
      //     required: true,
      //     message: '请输入确认密码',
      //     trigger: 'blur',
      //   },
      //   {
      //     min: 6,
      //     message: '密码至少6位',
      //     trigger: 'blur',
      //   },
      // ],
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
  ];
  //   微信表单配置
  const [Register, { validateFields, resetFields, getFieldsValue, setFieldsValue }] = useForm({
    labelWidth: 100,
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
    padding: 20px;
  }
</style>
