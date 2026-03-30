<script lang="ts">
  import { onMount } from "svelte";

  interface CategorySummary {
    category: string;
    totalAmount: number;
  }

  interface MonthlyBreakdown {
    year: number;
    month: number;
    categories: CategorySummary[];
    monthlyTotal: number;
  }

  interface Props {
    monthlyBreakdowns: MonthlyBreakdown[];
  }

  let { monthlyBreakdowns = [] }: Props = $props();

  let mounted = $state(false);
  onMount(() => {
    setTimeout(() => {
      mounted = true;
    }, 100);
  });

  const displayData = $derived(
    (monthlyBreakdowns[0]?.categories ?? [])
      .filter((item) => item.totalAmount < 0)
      .map((item) => ({
        category: item.category,
        amount: Math.abs(item.totalAmount),
      }))
      .sort((a, b) => b.amount - a.amount),
  );

  const grandTotal = $derived(displayData.reduce((sum, item) => sum + item.amount, 0));
  const formatCurrency = (val: number) => new Intl.NumberFormat("en-US", { style: "currency", currency: "USD" }).format(val);
  const colors = [
    "from-indigo-500 to-indigo-400",
    "from-fuchsia-500 to-fuchsia-400",
    "from-emerald-500 to-emerald-400",
    "from-violet-500 to-violet-400",
    "from-amber-500 to-amber-400",
  ];
</script>

<div class="lg:col-span-3 bg-slate-900/60 rounded-3xl p-8 border border-white/5 shadow-sm flex flex-col min-h-90">
  <h3 class="text-sm font-semibold mb-8 text-slate-400 uppercase tracking-wider">
    Spending Breakdown
    {#if monthlyBreakdowns[0]}
      — {new Date(monthlyBreakdowns[0].year, monthlyBreakdowns[0].month - 1).toLocaleString("default", { month: "long" })}
    {/if}
  </h3>

  <div class="space-y-6 grow flex flex-col justify-center">
    {#each displayData as item, i}
      {@const percentage = grandTotal > 0 ? (item.amount / grandTotal) * 100 : 0}

      <div>
        <div class="flex justify-between text-sm mb-2.5">
          <span class="font-medium text-gray-200">{item.category}</span>
          <span class="font-mono text-slate-400">{formatCurrency(item.amount)}</span>
        </div>

        <div class="w-full bg-slate-950/50 rounded-full h-3 border border-white/5 overflow-hidden">
          <div class="bg-linear-to-r {colors[i % colors.length]} h-full rounded-full transition-[width] duration-1000 ease-in-out" style="width: {mounted ? percentage : 0}%"></div>
        </div>
      </div>
    {/each}
  </div>
</div>
