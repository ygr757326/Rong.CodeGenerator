<template>
  <a-drawer v-model:visible="visible" :title="`权限设置 - ${roleEntity}`" width="600px">
    <template #extra>
      <a-button @click="visible = false">取消</a-button>
      <a-button @click="handleSubmit" type="primary" style="margin-left: 20px">提交</a-button>
    </template>
    <div ref="roleEmpower">
      <a-spin :spinning="formLoading">
        <a-form :model="formState">
          <a-form-item>
            <a-checkbox
              :indeterminate="
                !!getAllChecked.length && getAllChecked.length < totalPermissionsCount
              "
              :checked="getAllChecked.length === totalPermissionsCount"
              @change="onChangeAll($event)"
            >
              授予所有权限 ({{ getAllChecked.length }}/{{ totalPermissionsCount }})
            </a-checkbox>
          </a-form-item>
          <a-form-item>
            <a-tabs default-active-key="0" tab-position="left" :style="{ height: 'auto' }">
              <a-tab-pane
                v-for="(item, index) in permissionData"
                :key="index"
                :tab="`${item.displayName}(${item.checkedKeys.checked.length}/${item.permissions.length})`"
              >
                <a-form-item>
                  <a-checkbox
                    :indeterminate="
                      !!item.checkedKeys.checked.length &&
                      item.checkedKeys.checked.length < item.permissions.length
                    "
                    :checked="item.checkedKeys.checked.length === item.permissions.length"
                    @change="onChangeGroup($event, item)"
                  >
                    全部
                  </a-checkbox>
                  <a-tree
                    v-if="item.permissionsTree.length > 0"
                    checkable
                    checkStrictly
                    :tree-data="item.permissionsTree"
                    :replace-fields="replaceFields"
                    defaultExpandAll
                    v-model:checkedKeys="item.checkedKeys"
                    @check="onCheckPermissions"
                  />
                </a-form-item>
              </a-tab-pane>
            </a-tabs>
          </a-form-item>
        </a-form>
      </a-spin>
    </div>
  </a-drawer>
</template>

<script setup lang="ts">
  import { message } from 'ant-design-vue';
  import arrayToTree from 'array-to-tree';
  import { computed, nextTick, reactive, ref } from 'vue';
  import { getPermissionsAll, putModifyPermissions } from '@/api/manageCenter/identity';
  import { useLoading } from '@/components/Loading';

  const roleEmpower = ref(null);
  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: roleEmpower,
    props: {
      absolute: true,
    },
  });
  const formState = reactive({
    pass: '',
    checkPass: '',
    age: undefined,
  });
  let totalPermissionsCount = ref<number>(0);
  let permissionData: any = ref([]);
  let permissionNamesOld: any = ref([]);
  let roleEntity = ref<string>('');
  let replaceFields = reactive({
    title: 'displayName',
    key: 'name',
  });
  let provider = reactive({
    providerName: 'R',
    providerKey: null,
  });
  let indeterminate = ref<boolean>(true);
  let checkAll = ref<boolean>(false);
  let formLoading = ref<boolean>(false);
  // let form = ref();
  let visible = ref<boolean>(false);

  /**
   * 获取所有选中项
   */
  const getAllChecked = computed(() => {
    const checkedKeys: any = [];
    permissionData.value.forEach((group: any) => {
      checkedKeys.push(...group.checkedKeys.checked);
    });
    return checkedKeys;
  });
  /**
   * 和原权限相比改变的权限（保存时提交这个）
   */
  const changedPermissionNames = computed(() => {
    //差集
    let data = permissionNamesOld.value
      .concat(getAllChecked.value)
      .filter((a) => !permissionNamesOld.value.includes(a) || !getAllChecked.value.includes(a))
      .map((a) => ({
        name: a,
        isGranted: !permissionNamesOld.value.includes(a),
      }));
    return data;
  });
  // 初始化方法
  // function rolePermissions(record) {
  //   formLoading.value = true;
  //   roleEntity = record;
  //   provider.key = record.name;
  //   visible.value = true;
  //   init();
  // }
  function init() {
    totalPermissionsCount.value = 0;
    permissionNamesOld.value = [];
    permissionData.value = [];
    getPermissions()
      .then((res: any) => {
        permissionData.value = res;
        permissionData.value.forEach((it: any) => {
          totalPermissionsCount.value = totalPermissionsCount.value + it.permissions.length;
          permissionNamesOld.value.push(...it.checkedKeys.checked);
        });
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
  /**
   * 获取菜单列表
   */
  function getPermissions() {
    return new Promise((resolve) => {
      getPermissionsAll(provider).then((res) => {
        formLoading.value = false;

        // 默认展开目录级
        const groups = res.groups;
        groups.forEach((item) => {
          item.checkedKeys = { checked: [], halfChecked: [] };

          item.permissions.forEach((it) => {
            it.permissionCount = item.permissions.length;
            if (it.isGranted) {
              item.checkedKeys.checked.push(it.name);
            }
          });
          //转成树
          item.permissionsTree = toTree(item.permissions);
        });
        resolve(groups);
      });
    });
  }
  function toTree(array) {
    return arrayToTree(array, {
      parentProperty: 'parentName',
      customID: 'name',
      childrenProperty: 'children',
    });
  }
  function handleSubmit() {
    // const {
    //   form: { validateFields },
    // } = this;
    // validateFields((errors, values) => {
    //   if (!errors) {
    //     if (changedPermissionNames.value.length == 0) {
    //       message.success('权限未改变');
    //       handleCancel();
    //       return;
    //     }
    //     // confirmLoading.value = true;
    //     putModifyPermissions(
    //       provider.providerName,
    //       provider.providerKey,
    //       changedPermissionNames.value,
    //     )
    //       .then(() => {
    //         message.success('授权成功');
    //         // confirmLoading.value = false;
    //         // $emit("ok", values);
    //         handleCancel();
    //       })
    //       .finally(() => {
    //         // confirmLoading.value = false;
    //       });
    //   } else {
    //     // confirmLoading.value = false;
    //   }
    // });
    if (changedPermissionNames.value.length == 0) {
      message.success('权限未改变');
      handleCancel();
      return;
    }
    putModifyPermissions(provider.providerName, provider.providerKey, {
      permissions: changedPermissionNames.value,
    }).then(() => {
      message.success('授权成功');
      handleCancel();
    });
  }
  function handleCancel() {
    visible.value = false;
  }
  /**
   * 选中某个权限
   */
  function onCheckPermissions(checkKeys, event) {
    const checkedKeys = checkKeys.checked;
    const dataRef = event.node.dataRef;
    if (!event.checked) {
      var names = getChildrenCheckedByRef(dataRef);
      names.forEach((it) => {
        const index = checkedKeys.indexOf(it);
        if (index !== -1) {
          checkedKeys.splice(index, 1);
        }
      });
    } else {
      var pNames = getParentCheckedByNode(event.node);
      pNames.forEach((it) => {
        if (checkedKeys.indexOf(it) == -1) {
          checkedKeys.push(it);
        }
      });
    }
  }
  /**
   * 选择/取消 某组权限
   */
  function onChangeGroup(e, group) {
    const checkAll = e.target.checked;
    setCheckedByGroup(group, checkAll);
  }
  /**
   * 选择/取消 所有权限
   */
  function onChangeAll(e) {
    checkAll.value = e.target.checked;
    indeterminate.value = false;
    permissionData.value.forEach((group) => {
      setCheckedByGroup(group, checkAll.value);
    });
  }
  /**
   * 设置 选择/取消 某组权限
   */
  function setCheckedByGroup(group, checkAll) {
    const checked = group.checkedKeys.checked;
    checked.splice(0, checked.length);
    if (checkAll) {
      checked.push(...group.permissions.map((a) => a.name));
    }
  }

  /**
   * 获取父级权限名称
   */
  function getParentCheckedByNode(node) {
    const checkedKeys: any = ref([]);
    check(node.parent);
    return checkedKeys.value;
    function check(parentNode) {
      const parent = parentNode?.node;
      if (!parent) {
        return;
      }
      checkedKeys.value.push(parent.name);
      check(parentNode.parent);
    }
  }

  /**
   * 获取子级权限名称
   */
  function getChildrenCheckedByRef(dataRef) {
    const checkedKeys: any = ref([]);
    check(dataRef.children);
    return checkedKeys.value;
    function check(children) {
      if (!children) {
        return;
      }
      children.forEach((it) => {
        checkedKeys.value.push(it.name);
        check(it.children);
      });
    }
  }
  async function open(data) {
    nextTick(() => {
      openWrapLoading();
    });
    roleEntity.value = data.displayName;
    provider.providerKey = data.name;
    init();
    visible.value = true;
  }
  defineExpose({ open });
</script>

<style lang="less" scoped></style>
