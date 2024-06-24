<template>
  <PageWrapper contentFullHeight>
    <div class="system-config" ref="systemConfig">
      <a-tabs v-model:activeKey="activeKey" type="card">
        <a-tab-pane :key="item.name" :tab="item.name" v-for="item in settingList">
          <a-tabs v-model:activeKey="groupKey" tabPosition="left" v-if="item.groups.length > 1">
            <a-tab-pane :key="i" :tab="v.name" v-for="(v, i) in item.groups">
              <ConfigForm
                :settings="v.settings"
                :visible="activeKey === item.name && groupKey === i ? true : false"
                @finish="finish"
              />
            </a-tab-pane>
          </a-tabs>
          <ConfigForm
            :settings="item.groups[0].settings"
            :visible="activeKey === item.name ? true : false"
            @finish="finish"
            v-else
          />
        </a-tab-pane>
      </a-tabs>
    </div>
  </PageWrapper>
</template>

<script lang="ts" setup>
  import { PageWrapper } from '@/components/Page';
  import { getSettingGetAllSettings } from '@/api/manageCenter/systemManage';
  import { ref } from 'vue';
  import { settingItem } from '@/api/manageCenter/model/systemManageModel';
  import ConfigForm from './components/configForm.vue';
  import { useLoading } from '@/components/Loading';

  const systemConfig = ref(null);

  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: systemConfig,
    props: {
      absolute: true,
    },
  });
  // 系统配置列表
  let settingList = ref<Array<settingItem> | null>([]);
  const activeKey = ref('常规配置');
  const groupKey = ref(0);
  /**
   * 获取系统配置列表
   */
  async function getSettingList() {
    openWrapLoading();
    getSettingGetAllSettings()
      .then((res) => {
        settingList.value = res;
        activeKey.value = res[0].name;
      })
      .finally(() => {
        closeWrapLoading();
      });
  }
  getSettingList();
  // 修改配置后刷新
  function finish() {
    getSettingList();
  }
</script>

<style lang="less" scoped>
  .system-config {
    padding: 20px;
    background: #fff;
  }
</style>
