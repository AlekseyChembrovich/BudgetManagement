<script setup lang="ts">

import { ref, watch } from "vue";
import { useField, useForm } from "vee-validate";
import {
  CategoryService,
  type ICreateCategoryCommand,
  type IUpdateCategoryCommand
} from "@/services/category-service";
import type { ICategoryTreeNode } from "@/services/models/category-tree-node.ts";

const props = defineProps<{
  category: ICategoryTreeNode
}>();

const emit = defineEmits<{
  (e: 'updatedCategory', model: ICategoryTreeNode): void
  (e: 'closed'): void
}>();

const service = new CategoryService();

const dialogOpened = ref(true);

watch(
  () => props.category,
  (newVal, _) => {
    name.value.value = newVal.name;
    dialogOpened.value = true;
  }
);

const updateFormContext = useForm({
  validationSchema: {
    name(value: string) {
      return true;
    }
  }
});

const name = useField('name');
name.value.value = props.category.name;
const errors = ref(Array.of<string>());

const updateFormSubmit = updateFormContext.handleSubmit(values => {
  const command: IUpdateCategoryCommand = {
    id: props.category.id,
    name: values.name
  }

  props.category.name = values.name;

  service.update(command)
      .then(result => {
        if (result.success) {
          updateFormContext.handleReset();
          dialogOpened.value = false;
          emit('updatedCategory', props.category);
        }
        else {
          errors.value = result.errors;
        }
      });
});











const addSubCategoryFormContext = useForm({
  validationSchema: {
    subCategoryName(value: string) {
      return true;
    }
  }
});

const subCategoryName = useField('subCategoryName');
subCategoryName.value.value = null as string | null;

const addSubCategoryFormSubmit = addSubCategoryFormContext.handleSubmit(values => {
  let command: ICreateCategoryCommand = {
    name: values.subCategoryName,
    rootId: props.category.id
  };

  service.create(command)
    .then(result => {
      if (result.success) {
        addSubCategoryFormContext.handleReset();
        props.category.children ??= [];
        let newSubNode: ICategoryTreeNode = {
          id: result.data.id,
          name: result.data.name,
          children: null,
          deletable: true
        };

        props.category.children!.push(newSubNode);
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

</script>

<template>
  <v-dialog
      v-model="dialogOpened"
      max-width="500">

    <v-card title="Update category">
      <template v-slot:text>
        <v-form
            class="d-flex align-center justify-center"
            @submit.prevent="updateFormSubmit">

          <v-text-field
              v-model="name.value.value"
              :error-messages="name.errorMessage.value"
              label="Category name"
              required
              clearable
              hint="Enter category name">
          </v-text-field>

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

      <v-card-title class="py-0">Sub-category list</v-card-title>
      <v-card-text class="pt-0">
        <v-list>
          <v-list-item
            v-for="(subCategory, index) in category.children"
            :key="index">
            {{ (index + 1) }}. {{ subCategory.name }}
          </v-list-item>
        </v-list>

        <v-form @submit.prevent="addSubCategoryFormSubmit">
          <v-text-field
            v-model="subCategoryName.value.value"
            :error-messages="subCategoryName.errorMessage.value"
            label="New sub-category"
            required
            clearable
            hint="Enter sub-category name">
          </v-text-field>

          <v-btn
            color="primary"
            type="submit">
            Add new
          </v-btn>
        </v-form>
      </v-card-text>

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
