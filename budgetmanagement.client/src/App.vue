<script setup lang="ts">

import AppNavigation from "@/components/app-navigation.vue";

</script>

<template>
  <v-app>
    <AppNavigation />

    <v-main>
      <v-container>
        <router-view/>
      </v-container>
    </v-main>
  </v-app>
</template>

<style scoped>

</style>
