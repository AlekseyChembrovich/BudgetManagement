<script setup lang="ts">

import { useField, useForm } from "vee-validate";
import { onBeforeMount, ref } from "vue";
import type { Ref } from "@vue/reactivity";
import { RecordService } from "@/services/record-service";
import { CategoryService } from "@/services/category-service";
import type { IExpenseRecord } from "@/services/models/expense-record";
import type { IExpenseCategory } from "@/services/models/expense-category";

const emit = defineEmits<{ (e: 'createdRecord', model: IExpenseRecord): void }>();

const service = new RecordService();
const categoryService = new CategoryService();

const { handleSubmit, handleReset } = useForm({
  validationSchema: {
    amount(value: number | null) {
      if (!value) return "Amount is required.";
      return true;
    },
    categoryId(value: string | null) {
      if (!value) return "Category is required.";
      return true;
    }
  }
});

const amount = useField('amount');
const categoryId = useField('categoryId');
const errors = ref(Array.of<string>());

const submit = handleSubmit(values => {
  const amount: number = values.amount;
  const categoryId: string = values.categoryId;
  service.create({ amount: amount, categoryId: categoryId })
    .then(result => {
      if (result.success) {
        handleReset();
        emit('createdRecord', result.data);
      }
      else {
        errors.value = result.errors;
      }
    });
});

const categories: Ref<IExpenseCategory[]> = ref([]);

const loadCategories = () => {
  categoryService.getList()
    .then(result => {
      if (result.success) {
        categories.value = result.data;
      }
    });
};

onBeforeMount(() => {
  loadCategories();
});

const itemProps = (category: IExpenseCategory) => {
  return { id: category.id, title: category.name }
};

</script>

<template>
  <v-form
    class="pa-5 d-flex align-center justify-center"
    @submit.prevent="submit">

    <v-text-field
      v-model="amount.value.value"
      :error-messages="amount.errorMessage.value"
      label="Record amount"
      required
      clearable
      hint="Enter record amount"
      prepend-icon="mdi-tag-plus-outline">
    </v-text-field>

    <v-select
      v-model="categoryId.value.value"
      :error-messages="amount.errorMessage.value"
      class="pl-5"
      :items="categories"
      item-value="id"
      label="Select category"
      :item-props="itemProps"
      clearable
      prepend-icon="mdi-format-list-bulleted-type">
    </v-select>

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
