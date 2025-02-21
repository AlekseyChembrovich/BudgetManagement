<script setup lang="ts">

/*import userStateStore from "@/services/stores/user-state";

const store = userStateStore();*/

</script>

<template>
  <v-app-bar>
    <v-container>

      <div class="d-flex justify-space-between">
        <v-sheet class="d-flex justify-center align-center">
          <v-icon class="mr-3" icon="mdi-file-image" size="x-large"></v-icon>
          <!--<v-app-bar-title>Media Store</v-app-bar-title>-->
          <p class="font-weight-medium">Media Store</p>
        </v-sheet>

        <v-sheet class="d-flex justify-center align-center">

          <div class="d-flex justify-end align-center">
            <v-btn
                value="workspace"
                prepend-icon="mdi-database-outline"
                to="/workspace"
                class="text-none text-white mx-2"
                color="indigo-darken-1"
                rounded="8"
                variant="flat"
                text="Workspace">
            </v-btn>

            <v-btn
                value="feed"
                prepend-icon="mdi-earth"
                to="/feed"
                class="text-none text-white mx-2"
                color="purple-lighten-1"
                rounded="8"
                variant="outlined"
                text="Feed">
            </v-btn>

            <v-btn
                value="signout"
                prepend-icon="mdi-login-variant"
                to="/signout"
                class="text-none text-white mx-2"
                color="blue-darken-4"
                rounded="8"
                variant="flat"
                text="Sign out">
            </v-btn>
          </div>

          <div class="d-flex justify-end align-center">
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
