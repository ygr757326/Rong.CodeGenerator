<script setup lang="ts">
  import Icon from '@/components/Icon/Icon.vue';
  import { Tooltip } from 'ant-design-vue';
  import { useRouter } from 'vue-router';

  const router = useRouter();

  function navToScreen() {
    router.push('/dataScreen');
  }
</script>

<template>
  <div class="data-screen-guide">
    <Tooltip placement="bottom" trigger="hover">
      <template #title>
        <span>数据大屏</span>
      </template>
      <div class="icon" @click="navToScreen">
        <icon size="18px" icon="ant-design:fund-projection-screen-outlined" />
      </div>
    </Tooltip>
  </div>
</template>

<style scoped lang="less">
  .data-screen-guide {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 48px;
    padding: 0 2px;
    color: var(--text-color);
    font-size: 1.2em;
    cursor: pointer;

    .icon {
      padding: 8px;
    }

    &:hover {
      background: rgb(0 0 0 / 8%);
    }
  }
</style>
