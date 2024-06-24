<template>
  <BasicDrawer v-bind="$attrs" @register="registerDetailDrawer" title="详情" width="500px">
    <!--    详情页面 -->
    <div class="detail" ref="userDetail">
      <a-descriptions bordered :column="props.column" size="small" :labelStyle="width">
        <a-descriptions-item label="用户名">{{ detailData?.userName }}</a-descriptions-item>
        <a-descriptions-item label="真实名称">{{ detailData?.name }}</a-descriptions-item>
        <a-descriptions-item label="用户角色"
          ><span v-for="(v, i) in roleNames" :key="i"
            ><a-tag>{{ v }}</a-tag></span
          ></a-descriptions-item
        >
        <a-descriptions-item label="创建时间">{{ detailData?.creationTime }}</a-descriptions-item>
        <a-descriptions-item label="邮箱">{{ detailData?.email }}</a-descriptions-item>
        <a-descriptions-item label="手机号码">{{ detailData?.phoneNumber }}</a-descriptions-item>
        <a-descriptions-item label="用户头像">
          <GzImagePreview :width="50" :height="50" :fileId="detailData.extraProperties?.Avatar">
          </GzImagePreview
          >{{ detailData.extraProperties?.Avatar }}</a-descriptions-item
        >
        <a-descriptions-item label="用户生日">{{
          detailData.extraProperties?.Birthday
        }}</a-descriptions-item>
        <a-descriptions-item label="状态">
          <Tag :color="detailData?.isActive ? 'green' : 'red'">
            {{ detailData?.isActive ? t('common.enabled') : t('common.disEnabled') }}
          </Tag>
        </a-descriptions-item>
      </a-descriptions>
    </div>
  </BasicDrawer>
</template>

<script setup lang="ts">
  import { getIdentityUsersDetail, getSysUserGetRoles } from '/@/api/manageCenter/identity';
  import { ref } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { useI18n } from '/@/hooks/web/useI18n';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { GzImagePreview } from '/@/components/GzImage';
  import { useLoading } from '/@/components/Loading';

  const userDetail = ref(null);
  const [openWrapLoading, closeWrapLoading] = useLoading({
    target: userDetail,
    props: {
      absolute: true,
    },
  });
  const { t } = useI18n();
  const [registerDetailDrawer] = useDrawerInner(async (data) => {
    detailData.value = {};
    openWrapLoading();
    getData({ id: data.id });
  });
  const roleNames: any = ref([]);
  const props = defineProps({
    id: { type: String, default: '' },
    column: { type: Object, default: { xxl: 1, xl: 1, lg: 1, md: 1, sm: 1, xs: 1 } },
  });
  // 宽度css属性
  const width = {
    width: '180px',
    'justify-content': 'flex-end',
  };

  let detailData: any = ref({});
  async function getData(data) {
    let res = await getIdentityUsersDetail(data);
    detailData.value = res;
    const roleRes = await getSysUserGetRoles(data);
    roleNames.value = roleRes.items.map((v) => v.extraProperties?.DisplayName);
    closeWrapLoading();
  }
</script>

<style lang="less" scoped></style>
