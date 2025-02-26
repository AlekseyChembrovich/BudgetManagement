<script setup lang="ts">
import { useField, useForm } from "vee-validate";
import { ref } from "vue";
import { CategoryService } from "@/services/category-service";
import type { IExpenseCategory } from "@/services/models/expense-category";

const emit = defineEmits<{ (e: 'createdCategory', model: IExpenseCategory): void }>();

const service = new CategoryService();

const { handleSubmit, handleReset } = useForm({
  validationSchema: {
    name(value: string | null) {
      if (!value) return "Name is required.";
      return true;
    }
  }
});

const name = useField('name');
const errors = ref(Array.of<string>());

const submit = handleSubmit(values => {
  const name: string = values.name;
  service.create({ name: name, rootId: null })
    .then(result => {
      if (result.success) {
        handleReset();
        emit('createdCategory', result.data);
      }
      else {
        errors.value = result.errors;
      }
    });
});
</script>

<template>
  <v-form
    class="pa-5 d-flex align-center justify-center"
    @submit.prevent="submit">

    <v-text-field
      v-model="name.value.value"
      :error-messages="name.errorMessage.value"
      label="Category name"
      required
      clearable
      hint="Enter category name"
      prepend-icon="mdi-subtitles-outline">
    </v-text-field>

    <v-btn
      class="mx-2 text-none"
      color="teal-darken-1"
      type="submit"
      text="Add new"
      append-icon="mdi-plus"
      variant="outlined">
    </v-btn>

    <v-alert
      v-if="errors && errors.length > 0"
      title="Error"
      type="error"
      closable
      @click:close="errors = []"
      class="mt-4">
      <span v-for="error in errors">{{ error }}</span>
    </v-alert>
  </v-form>
</template>
