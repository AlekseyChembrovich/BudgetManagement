<script setup lang="ts">

import { onBeforeMount, ref } from "vue";
import type { Ref } from "@vue/reactivity";
import { VTreeview } from 'vuetify/labs/VTreeview'
import { CategoryService } from '@/services/category-service';
import { type ICategoryTreeNode } from '@/services/models/category-tree-node';
import CreateCategoryForm from "@/components/category/create-category-form.vue";
import UpdateCategoryForm from "@/components/category/update-category-dialog.vue";
import type {IExpenseCategory} from "@/services/models/expense-category.ts";

const service = new CategoryService();

const tree: Ref<ICategoryTreeNode[]> = ref([]);

const loadList = () => {
  service.getTree()
    .then(result => {
      if (result.success) {
        tree.value = result.data;
      }
    });
}

onBeforeMount(() => {
  loadList();
});

const openUpdateDialog = ref(false);
const categoryToUpdate: Ref<ICategoryTreeNode | null> = ref(null);

const updateCategory = (id: string) => {
  categoryToUpdate.value = tree.value.find(x => x.id === id) ?? null;
  openUpdateDialog.value = true;
};

const updatedCategoryEvent = (updatedCategory: ICategoryTreeNode) => {
  const index = tree.value.findIndex(x => x.id === updatedCategory.id);
  tree.value[index] = updatedCategory;
  openUpdateDialog.value = false;
  categoryToUpdate.value = null;
};

const updateDialogClosedEvent = () => {
  openUpdateDialog.value = false;
  categoryToUpdate.value = null;
};

const deleteCategory = (categoryId: string) => {
  service.delete(categoryId)
    .then(result => {
      if (result.success) {
        loadList();
      }
    });
}

const createdRecordEvent = (createdCategory: IExpenseCategory) => {
  let newNode: ICategoryTreeNode = {
    id: createdCategory.id,
    name: createdCategory.name,
    children: null,
    deletable: true
  };

  tree.value.push(newNode);
};

</script>

<template>
  <v-sheet
    class="pa-3 w-75 mx-auto"
    elevation="5"
    rounded="lg">

    <CreateCategoryForm @createdCategory="createdRecordEvent" />

    <v-treeview :items="tree">
      <template v-slot:title="{ item }">
        <div class="tree-item">
          <p class="font-weight-regular">
            {{ item.name }}
          </p>
        </div>
      </template>

      <template v-slot:append="{ item }">
        <div class="tree-item">
          <v-btn
            color="light-blue-accent-4"
            icon="mdi-pen"
            variant="text"
            @click="updateCategory(item.id)">
          </v-btn>

          <v-btn
            v-if="item.deletable"
            color="red-darken-1"
            icon="mdi-delete"
            variant="text"
            @click="deleteCategory(item.id)">
          </v-btn>

          <v-btn
            color="orange-lighten-1"
            icon="mdi-chart-pie"
            variant="text"
            :to="`/report/${item.id}`">
          </v-btn>
        </div>
      </template>
    </v-treeview>

    <div class="pa-4 text-center"
         v-if="openUpdateDialog && categoryToUpdate">
      <UpdateCategoryForm
        :category="categoryToUpdate"
        @updatedCategory="updatedCategoryEvent"
        @closed="updateDialogClosedEvent" />
    </div>

  </v-sheet>

</template>
