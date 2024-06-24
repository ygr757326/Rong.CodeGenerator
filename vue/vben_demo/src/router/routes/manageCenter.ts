import { LAYOUT } from '@/router/constant';
import type { AppRouteModule } from '@/router/types';
import { t } from '@/hooks/web/useI18n';

export const manageCenter: Array<AppRouteModule> = [
  {
    path: '/staff-manage',
    name: 'staffManage',
    component: LAYOUT,
    redirect: '/staff-manage/user',
    meta: {
      title: t('routes.manageCenter.staffManage'),
    },
    children: [
      // {
      //   path: 'user',
      //   name: 'user',
      //   component: () => import('@/views/manageCenter/staffManage/user/user.vue'),
      //   meta: {
      //     title: t('routes.manageCenter.user'),
      //   },
      // },
      {
        path: 'user',
        name: 'user',
        component: () => import('@/views/manageCenter/staffManage/account/index.vue'),
        meta: {
          title: t('routes.manageCenter.user'),
        },
      },
      {
        path: 'role',
        name: 'role',
        component: () => import('@/views/manageCenter/staffManage/role/role.vue'),
        meta: {
          title: t('routes.manageCenter.role'),
        },
      },
    ],
  },
  // 基本信息管理
  {
    path: '/basic-service',
    name: 'basicService',
    component: LAYOUT,
    redirect: '/basic-service/dictionaryManage',
    meta: {
      title: t('routes.manageCenter.basicService'),
    },
    children: [
      {
        path: 'dictionaryManage',
        name: 'dictionaryManage',
        component: () =>
          import('@/views/manageCenter/basicService/dictionaryManage/dictionaryManage.vue'),
        meta: {
          title: t('routes.manageCenter.dictionaryManage'),
        },
      },
      {
        path: 'area',
        name: 'area',
        component: () => import('@/views/manageCenter/basicService/area/area.vue'),
        meta: {
          title: t('routes.manageCenter.area'),
        },
      },
    ],
  },
  {
    path: '/system-manage',
    name: 'systemManage',
    component: LAYOUT,
    redirect: '/system-manage/menuManage',
    meta: {
      title: t('routes.manageCenter.systemManage'),
    },
    children: [
      {
        path: 'sms',
        name: 'sms',
        component: () => import('@/views/manageCenter/systemManage/sms/sms.vue'),
        meta: {
          title: t('routes.manageCenter.sms'),
        },
      },
      {
        path: 'systemConfig',
        name: 'systemConfig',
        component: () => import('@/views/manageCenter/systemManage/systemConfig/systemConfig.vue'),
        meta: {
          title: t('routes.manageCenter.systemConfig'),
        },
      },
    ],
  },
  {
    path: '/log-manage',
    name: 'logManage',
    component: LAYOUT,
    redirect: '/log-manage/log',
    meta: {
      title: t('routes.manageCenter.logManage'),
    },
    children: [
      {
        path: 'log',
        name: 'log',
        component: () => import('@/views/manageCenter/logManage/log/log.vue'),
        meta: {
          title: t('routes.manageCenter.systemLog'),
        },
      },
      {
        path: 'externalApi',
        name: 'externalApi',
        component: () => import('@/views/manageCenter/logManage/externalApi/externalApi.vue'),
        meta: {
          title: t('routes.manageCenter.externalApiLog'),
        },
      },
      {
        path: 'smsSend',
        name: 'smsSend',
        component: () => import('@/views/manageCenter/logManage/smsSend/smsSend.vue'),
        meta: {
          title: t('routes.manageCenter.smsLog'),
        },
      },
    ],
  },
];
