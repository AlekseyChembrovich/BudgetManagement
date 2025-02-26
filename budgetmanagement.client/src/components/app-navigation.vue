<script setup lang="ts">

import userStateStore from "@/services/stores/user-store";

const store = userStateStore();

</script>

<template>
  <v-app-bar>
    <v-container>

      <div class="d-flex justify-space-between">
        <v-sheet class="d-flex justify-center align-center">
          <v-icon class="mr-3" icon="mdi-cash-100" size="x-large"></v-icon>
          <p class="font-weight-medium">My Budget</p>
        </v-sheet>

        <v-sheet class="d-flex justify-center align-center">

          <div v-if="store.userState.isAuthenticated" class="d-flex justify-end align-center">
            <v-btn
                value="categories"
                prepend-icon="mdi-file-tree-outline"
                to="/categories"
                class="text-none text-white mx-2"
                color="deep-purple-darken-3"
                rounded="8"
                variant="flat"
                text="Categories">
            </v-btn>

            <v-btn
              value="records"
              prepend-icon="mdi-menu"
              to="/records"
              class="text-none text-white mx-2"
              color="indigo-darken-3"
              rounded="8"
              variant="flat"
              text="Records">
            </v-btn>

            <v-btn
              value="report"
              prepend-icon="mdi-chart-pie"
              to="/report"
              class="text-none text-white mx-2"
              color="blue-darken-4"
              rounded="8"
              variant="flat"
              text="Report">
            </v-btn>

            <v-btn
                value="signout"
                prepend-icon="mdi-login-variant"
                to="/signout"
                class="text-none text-white mx-2"
                color="green-darken-3"
                rounded="8"
                variant="outlined"
                text="Sign out">
            </v-btn>

            <p class="font-weight-medium mx-2">
              <v-icon icon="mdi-account"></v-icon>
              {{store.userState.login}}
            </p>
          </div>

          <div v-else-if="!store.userState.isAuthenticated" class="d-flex justify-end align-center">
            <v-btn
                value="signin"
                prepend-icon="mdi-login-variant"
                to="/signin"
                class="text-none text-white mx-2"
                color="blue-darken-4"
                rounded="8"
                variant="flat"
                text="Sign in">
            </v-btn>

            <v-btn
                value="signup"
                prepend-icon="mdi-account"
                to="/signup"
                class="text-none mx-2"
                color="blue-darken-4"
                rounded="8"
                variant="outlined"
                text="Sign up">
            </v-btn>
          </div>

        </v-sheet>
      </div>

    </v-container>

  </v-app-bar>
</template>
