<script setup lang="ts">

import { onBeforeMount, ref } from "vue";
import type { Ref } from "@vue/reactivity";
import { RecordService } from '@/services/record-service';
import { type IExpenseRecord } from '@/services/models/expense-record';
import { formatDate } from '@/services/basics/format';
import CreateRecordForm from "@/components/record/create-record-form.vue";
import UpdateRecordForm from "@/components/record/update-record-dialog.vue";

const service = new RecordService();

const records: Ref<IExpenseRecord[]> = ref([]);

const loadList = () => {
  service.getList()
    .then(result => {
      if (result.success) {
        records.value = result.data;
      }
    });
}

onBeforeMount(() => {
  loadList();
});

const openUpdateDialog = ref(false);
const recordToUpdate: Ref<IExpenseRecord | null> = ref(null);

const updateRecord = (id: string) => {
  recordToUpdate.value = records.value.find(x => x.id === id) ?? null;
  openUpdateDialog.value = true;
};

const updatedRecordEvent = (updatedRecord: IExpenseRecord) => {
  const index = records.value.findIndex(x => x.id === updatedRecord.id);
  records.value[index] = updatedRecord;
  openUpdateDialog.value = false;
  recordToUpdate.value = null;
};

const updateDialogClosedEvent = () => {
  openUpdateDialog.value = false;
  recordToUpdate.value = null;
};

const deleteRecord = (recordId: string) => {
  service.delete(recordId)
    .then(result => {
      if (result.success) {
        records.value = records.value.filter(item => item.id !== recordId);
      }
    });
}

const createdRecordEvent = (createdRecord: IExpenseRecord) => {
  records.value.push(createdRecord);
};

</script>

<template>
  <v-sheet
    class="pa-3 w-75 mx-auto"
    elevation="5"
    rounded="lg">

    <CreateRecordForm @createdRecord="createdRecordEvent" />

    <v-list v-if="records && records.length > 0" lines="one">
      <v-list-item
        v-for="(item, index) in records"
        :key="index">

        <template v-slot:prepend>
          <v-icon icon="mdi-notification-clear-all" size="small"></v-icon>
        </template>

        <v-list-item-content>
          <v-list-item-title>
            Expense amount: {{ item.amount }}
          </v-list-item-title>
          <v-list-item-subtitle>
            Created at: {{ formatDate(item.createdAt.toString()) }}
          </v-list-item-subtitle>
          <v-list-item-subtitle>
            Category name: {{ item.categoryName }}
          </v-list-item-subtitle>
        </v-list-item-content>

        <template v-slot:append>
          <v-btn
            color="light-blue-accent-4"
            icon="mdi-pen"
            variant="text"
            @click="updateRecord(item.id)">
          </v-btn>

          <v-btn
            color="red-darken-1"
            type="button"
            icon="mdi-delete"
            variant="text"
            @click="deleteRecord(item.id)">
          </v-btn>
        </template>
      </v-list-item>
    </v-list>

    <p v-if="records && records.length <= 0"
       class="font-weight-medium text-medium-emphasis my-2">
      There is no any expense record.
    </p>

    <div class="pa-4 text-center"
         v-if="openUpdateDialog && recordToUpdate">
      <UpdateRecordForm
        :record="recordToUpdate"
        @updatedRecord="updatedRecordEvent"
        @closed="updateDialogClosedEvent" />
    </div>

  </v-sheet>
</template>
