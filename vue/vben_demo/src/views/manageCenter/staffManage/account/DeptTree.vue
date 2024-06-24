<template>
  <div class="m-4 mr-0 overflow-hidden bg-white" ref="treeAreaRef">
    <div style="display: flex; justify-content: flex-end; margin-bottom: 10px; padding: 10px">
      <a-button type="primary" @click="handleOrgSync">同步组织机构数据</a-button></div
    >
    <BasicTree
      title="部门列表"
      toolbar
      search
      treeWrapperClassName="h-[calc(100%-35px)] overflow-auto"
      :clickRowToExpand="false"
      :treeData="treeData"
      :fieldNames="{ children: 'children', title: 'name', key: 'value' }"
      @select="handleSelect"
    />
  </div>
</template>
<script lang="ts" setup>
  import { onMounted, ref } from 'vue';

  import { BasicTree, TreeItem } from '@/components/Tree';
  import { getOrgGetDropDownList, postOrgSync } from '@/api/manageCenter/org';
  import { useLoading } from '@/components/Loading';
  import { message } from 'ant-design-vue';

  defineOptions({ name: 'DeptTree' });
  const treeAreaRef = ref(null);
  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: treeAreaRef,
    props: {
      absolute: true,
    },
  });
  const emit = defineEmits(['select']);

  const treeData = ref<TreeItem[]>([]);

  async function fetch() {
    let data = await getOrgGetDropDownList();
    treeData.value = data.items;
  }
  // 同步组织机构数据
  function handleOrgSync() {
    openWrapLoading();
    postOrgSync()
      .then(async () => {
        message.success('操作成功');
        await fetch();
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
  function handleSelect(keys) {
    emit('select', keys[0]);
  }

  onMounted(() => {
    fetch();
  });
</script>
