<script setup lang="ts">

import { ref, onMounted } from "vue";
import { Chart, type ChartData, registerables } from "chart.js";
import { PieChart } from "vue-chart-3";
import type { Ref } from "@vue/reactivity";
import { RecordService, type IGetReport } from "@/services/report-service";
import { useField, useForm } from 'vee-validate';

Chart.register(...registerables);

const chartData: Ref<ChartData<"pie"> | null> = ref(null);

const service = new RecordService();

const props = defineProps<{ id?: string }>();
const categoryId: string | null = props.id?.trim() || null;

onMounted(async () => {
  await loadData()
});

const loadData = async (from: Date | null = null, to: Date | null = null) => {
  let request: IGetReport = { categoryId: categoryId, from: from, to: to };
  let result = await service.get(request);
  if (result.success) {
    chartData.value = {
      labels: result.data.map(x => x.categoryName),
      datasets: [
        {
          label: "Expenses",
          data: result.data.map(x => x.totalSum),
          backgroundColor: result.data.map(() => getRandomColor())
        },
      ]
    };
  }
};

const getRandomColor = () => {
  const r = Math.floor(Math.random() * 256);
  const g = Math.floor(Math.random() * 256);
  const b = Math.floor(Math.random() * 256);
  return `rgba(${r}, ${g}, ${b}, 0.7)`;
};

const { handleSubmit, handleReset } = useForm({
  validationSchema: {
    from(value: Date | null) {
      if (!value) return "First date is required.";
      return true;
    },
    to(value: Date | null) {
      if (!value) return "Second date is required.";
      return true;
    }
  }
})

const from = useField('from');
const to = useField('to');
const errors = ref(Array.of<string>());

const submit = handleSubmit(async (values) => {
  await loadData(values.from, values.to);
});

</script>

<template>
  <v-sheet
    class="pa-5 w-75 mx-auto"
    elevation="5"
    rounded="lg">

    <div class="text-h5 mb-3">Chart report</div>

    <v-row>
      <v-col>
        <v-form
          @submit.prevent="submit"
          class="mt-5">

          <v-text-field
            v-model="from.value.value"
            :error-messages="from.errorMessage.value"
            label="From date"
            type="date"
            required
            hint="Enter first date"
            class="mb-2"
            prepend-icon="mdi-calendar-range">
          </v-text-field>

          <v-text-field
            v-model="to.value.value"
            :error-messages="to.errorMessage.value"
            label="To date"
            type="date"
            required
            hint="Enter second date"
            prepend-icon="mdi-calendar-range">
          </v-text-field>

          <v-alert
            v-if="errors && errors.length > 0"
            title="Error"
            type="error"
            closable
            @click:close="errors = []">
            <span v-for="error in errors">{{error}}</span>
          </v-alert>

          <v-sheet class="mt-2 d-flex justify-start">
            <v-btn
              class="w-25 text-none mx-3"
              color="blue-darken-3"
              rounded="8"
              type="submit"
              text="Apply"
              variant="flat"
              append-icon="mdi-filter-outline">
            </v-btn>

            <v-btn
              class="w-25 text-none mx-3"
              color="red-lighten-1"
              rounded="8"
              text="Clear"
              variant="flat"
              @click="handleReset"
              append-icon="mdi-filter-remove-outline">
            </v-btn>
          </v-sheet>

        </v-form>
      </v-col>
      <v-col>
        <div
          v-if="chartData"
          style="width: 100%; max-width: 500px; height: auto;">
          <PieChart :chart-data="chartData!" />
        </div>
        <v-progress-circular
          v-else
          :size="70"
          :width="7"
          color="purple"
          indeterminate>
        </v-progress-circular>
      </v-col>
    </v-row>

  </v-sheet>
</template>
