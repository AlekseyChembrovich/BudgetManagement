import { createRouter, createWebHashHistory } from 'vue-router';
import type { RouteRecordRaw, RouteRecordName } from 'vue-router';

const routes: Array<RouteRecordRaw> = [
  /*{
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/workspace',
    name: 'workspace',
    component: WorkspaceView
  },
  {
    path: '/admin-panel',
    name: 'admin-panel',
    component: AdminPanelView
  },
  {
    path: '/feed',
    name: 'feed',
    component: PublicationFeedView
  },
  {
    path: '/item/:id',
    name: 'item',
    component: ContentItemView,
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
  }*/
];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

/*router.beforeEach((to, _) => {
  const store = userStateStore();

  let matched = onlyAuthenticated(to.name);
  if (matched && !store.userState.isAuthenticated) {
    return { name: 'signin' };
  }

  matched = onlyAdmin(to.name);
  if (
    matched
    && (!store.userState.isAuthenticated || !userStateStore.isAdmin)
  ) {
    return { name: 'signin' };
  }
});*/

export default router;

function onlyAuthenticated(routeName: RouteRecordName | undefined | null): boolean {
  if (routeName === undefined) {
    return false;
  }
  else if (routeName === null) {
    return false;
  }

  const result: boolean = routeName === 'catalog';

  return result;
}

function onlyAdmin(routeName: RouteRecordName | undefined | null): boolean {
  if (routeName === undefined) {
    return false;
  }
  else if (routeName === null) {
    return false;
  }

  const result: boolean =
    routeName === 'exhibits-table'
    || routeName === 'categories-list';

  return result;
}
