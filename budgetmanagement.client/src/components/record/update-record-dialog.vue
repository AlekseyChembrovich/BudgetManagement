<script setup lang="ts">

import { ref, watch, onBeforeMount } from "vue";
import type { Ref } from "@vue/reactivity";
import { useField, useForm } from "vee-validate";
import { RecordService, type IUpdateRecordCommand } from "@/services/record-service";
import { CategoryService } from "@/services/category-service";
import type { IExpenseRecord } from "@/services/models/expense-record";
import type { IExpenseCategory } from "@/services/models/expense-category.ts";

const props = defineProps<{
  record: IExpenseRecord
}>();

const emit = defineEmits<{
  (e: 'updatedRecord', model: IExpenseRecord): void
  (e: 'closed'): void
}>();

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
amount.value.value = props.record.amount;
categoryId.value.value = props.record.categoryId;
const errors = ref(Array.of<string>());

const dialogOpened = ref(true);

watch(
    () => props.record,
    (newVal, _) => {
      amount.value.value = newVal.amount;
      categoryId.value.value = newVal.categoryId;
      dialogOpened.value = true;
    }
);

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

const submit = handleSubmit(values => {
  const command: IUpdateRecordCommand = {
    id: props.record.id,
    amount: values.amount,
    categoryId: values.categoryId
  };

  service.update(command)
      .then(result => {
        if (result.success) {
          handleReset();
          dialogOpened.value = false;
          emit('updatedRecord', result.data);
        }
        else {
          errors.value = result.errors;
        }
      });
});

const close = () => {
  dialogOpened.value = false;
  emit('closed');
};

const itemProps = (category: IExpenseCategory) => {
  return { id: category.id, title: category.name }
};

</script>

<template>
  <v-dialog
      v-model="dialogOpened"
      max-width="500">

    <v-card title="Update record">
      <template v-slot:text>
        <v-form
            class="d-flex align-center justify-center"
            @submit.prevent="submit">

          <v-text-field
              v-model="amount.value.value"
              :error-messages="amount.errorMessage.value"
              label="Record amount"
              required
              clearable
              hint="Enter record amount">
          </v-text-field>

          <v-select
            v-model="categoryId.value.value"
            :error-messages="amount.errorMessage.value"
            :items="categories"
            item-value="id"
            label="Select category"
            :item-props="itemProps"
            clearable>
          </v-select>

          <v-btn
              class="mx-2 text-none"
              color="teal-darken-1"
              type="submit"
              text="Update"
              append-icon="mdi-content-save"
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

      <v-card-actions>
        <v-spacer></v-spacer>

        <v-btn
            text="Close"
            variant="text"
            @click="close">
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
