import { createRouter, createWebHashHistory } from 'vue-router';
import type { RouteRecordRaw, RouteRecordName } from 'vue-router';
import userStateStore from '@/services/stores/user-store';
import CategoryList from '@/views/category/category-list.vue';
import RecordList from '@/views/record/record-list.vue';
import ChartReport from '@/views/record/chart-report.vue';
import SignInView from '@/views/auth/signin.vue';
import SignUpView from '@/views/auth/signup.vue';
import SignOutView from '@/views/auth/signout.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    redirect: '/categories',
  },
  {
    path: '/categories',
    name: 'categories',
    component: CategoryList
  },
  {
    path: '/records',
    name: 'records',
    component: RecordList
  },
  {
    path: '/report/:id?',
    name: 'report',
    component: ChartReport,
    props: true
  },
  {
    path: '/signin',
    name: 'signin',
    component: SignInView
  },
  {
    path: '/signup',
    name: 'signup',
    component: SignUpView
  },
  {
    path: '/signout',
    name: 'signout',
    component: SignOutView
  }
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

router.beforeEach((to, _) => {
  const store = userStateStore();

  let matched = onlyAuthenticated(to.name);
  if (matched && !store.userState.isAuthenticated) {
    return { name: 'signin' };
  }
});

export default router;

function onlyAuthenticated(routeName: RouteRecordName | undefined | null): boolean {
  if (routeName === undefined) {
    return false;
  }
  else if (routeName === null) {
    return false;
  }

  const result: boolean =
    routeName === 'categories'
    || routeName === 'records'
    || routeName === 'report';

  return result;
}
