<!--
<script setup lang="ts">

import { ref, onMounted } from "vue";
import type { Ref } from "@vue/reactivity";
import { useRoute } from "vue-router";
import { CategoryService, type ICreateCategoryCommand } from "@/services/category-service.ts";
import type { ICategoryTreeNode } from "@/services/models/category-tree-node.ts";

const service = new CategoryService();

const route = useRoute();
const category: Ref<ICategoryTreeNode | null> = ref<ICategoryTreeNode | null>(null);

onMounted(async () => {
  const id = route.params.id as string;
  let result = await service.getById(id);
  if (result.success) {
    category.value = result.data;
  }
});

const newCategoryName = ref<string | null>(null);

function addSubCategory() {
  let command: ICreateCategoryCommand = {
    name: newCategoryName.value!,
    rootId: category.value!.id
  };

  service.create(command)
    .then(result => {
      if (result.success) {
        category.value!.children ??= [];
        category.value!.children!.push({ id: result.data.id, name: result.data.name, children: null });
      }
    });
}

</script>

<template>
  <v-container>
    <v-card v-if="category" class="pa-4">
      <v-card-title>{{ category.name }}</v-card-title>
      <v-card-subtitle>ID: {{ category.id }}</v-card-subtitle>
      <v-btn to="/home" variant="outlined" class="mt-3">Back</v-btn>

      <v-card-title>Sub-category list</v-card-title>
      <v-card-text>
        <v-list>
          <v-list-item v-for="(subCategory, index) in category.children" :key="index">
            {{ subCategory.name }}
          </v-list-item>
        </v-list>

        <v-text-field
          v-model="newCategoryName"
          label="New category"
          outlined
          dense
          @keyup.enter="addSubCategory">
        </v-text-field>

        <v-btn color="primary" @click="addSubCategory">Add new</v-btn>
      </v-card-text>
    </v-card>

    <v-alert v-else type="error">The category is not found.</v-alert>
  </v-container>
</template>
-->
