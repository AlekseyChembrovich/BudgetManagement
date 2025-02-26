import { createApp } from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import router from './router';
import { createPinia } from 'pinia';

const pinia = createPinia();

createApp(App)
  .use(vuetify)
  .use(router)
  .use(pinia)
  .mount('#app')
