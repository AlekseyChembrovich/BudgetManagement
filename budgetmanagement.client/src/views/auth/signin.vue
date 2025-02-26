<script setup lang="ts">

import { ref } from 'vue';
import { useField, useForm } from 'vee-validate';
import { AuthService, type ISignInCommand } from '@/services/auth-service';
import router from '@/router/index';

const service = new AuthService();

const { handleSubmit, handleReset } = useForm({
  validationSchema: {
    login(value: string) {
      if (/^[a-z.-]+@[a-z.-]+\.[a-z]+$/i.test(value))
        return true;

      return 'Must be a valid e-mail.';
    },
    password(value: string) {
      if (value?.length < 4) {
        return 'At least 5 symbols.';
      }

      return true;
    }
  }
})

const login = useField('login');
const password = useField('password');
const errors = ref(Array.of<string>());

const submit = handleSubmit(values => {
  const command: ISignInCommand = { login: values.login, password: values.password };
  service.signIn(command)
    .then(result => {
      if (!result.success) {
        errors.value = result.errors;
      }
      else {
        router.push("/categories");
      }
    });
});

</script>

<template>

  <v-sheet class="mx-auto" max-width="700">
    <v-form
      @submit.prevent="submit"
      class="mt-5">

      <v-text-field
        v-model="login.value.value"
        :error-messages="login.errorMessage.value"
        label="E-mail"
        type="email"
        required
        clearable
        hint="Enter your login"
        class="mb-2"
        prepend-icon="mdi-email">
      </v-text-field>

      <v-text-field
        v-model="password.value.value"
        :error-messages="password.errorMessage.value"
        label="Password"
        type="password"
        required
        clearable
        hint="Enter your password"
        prepend-icon="mdi-file-word-box">
      </v-text-field>

      <v-alert
        v-if="errors && errors.length > 0"
        title="Error"
        type="error"
        closable
        @click:close="errors = []">
        <span v-for="error in errors">{{error}}</span>
      </v-alert>

      <v-sheet class="mt-2 d-flex justify-center">
        <v-btn
          class="w-25 text-none mx-3"
          color="blue-darken-3"
          rounded="8"
          type="submit"
          text="Submit"
          variant="flat"
          append-icon="mdi-send-outline">
        </v-btn>

        <v-btn
          class="w-25 text-none mx-3"
          color="red-lighten-1"
          rounded="8"
          text="Clear"
          variant="flat"
          @click="handleReset"
          append-icon="mdi-trash-can-outline">
        </v-btn>
      </v-sheet>

    </v-form>
  </v-sheet>

</template>
