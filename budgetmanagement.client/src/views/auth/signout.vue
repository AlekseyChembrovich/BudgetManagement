<script setup lang="ts">

import userStateStore from '@/services/stores/user-store';
import router from '@/router/index';
import { onBeforeMount } from "vue";

onBeforeMount(() => {
  userStateStore().clear();
  router.push("/signin");
});

</script>

<template>
  <div>
    <!-- Sign-out template -->
  </div>
</template>
