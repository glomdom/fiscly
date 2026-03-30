<script lang="ts">
  import { cubicOut } from "svelte/easing";
  import { Tween } from "svelte/motion";

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

  const tweenCache = new Map<string, Tween<number>>();
  const displayData = $derived.by(() => {
    const rawCategories = monthlyBreakdowns[0]?.categories ?? [];
    const sorted = rawCategories
      .filter((item) => item.totalAmount < 0)
      .map((item) => ({
        category: item.category,
        amount: Math.abs(item.totalAmount),
      }))
      .sort((a, b) => b.amount - a.amount);

    return sorted.map((item, index) => {
      let tween = tweenCache.get(item.category);
      if (!tween) {
        tween = new Tween(0, {
          duration: 1500,
          delay: index * 100,
          easing: cubicOut,
        });

        tweenCache.set(item.category, tween);
      }

      tween.target = item.amount;

      return { ...item, tween };
    });
  });

  const grandTotal = $derived(displayData.reduce((sum, item) => sum + item.amount, 0));
  const formatCurrency = (val: number) => new Intl.NumberFormat("en-US", { style: "currency", currency: "USD" }).format(val);
  const colors = [
    { bg: "from-indigo-500 to-indigo-400", rgb: "99, 102, 241" },
    { bg: "from-fuchsia-500 to-fuchsia-400", rgb: "217, 70, 239" },
    { bg: "from-emerald-500 to-emerald-400", rgb: "16, 185, 129" },
    { bg: "from-violet-500 to-violet-400", rgb: "139, 92, 246" },
    { bg: "from-amber-500 to-amber-400", rgb: "245, 158, 11" },
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
      {@const animatedValue = item.tween.current}

      {@const percentage = grandTotal > 0 ? (animatedValue / grandTotal) * 100 : 0}
      {@const color = colors[i % colors.length]}

      <div>
        <div class="flex justify-between text-sm mb-2.5">
          <span class="font-medium text-gray-200">{item.category}</span>
          <span class="font-mono text-slate-400 tabular-nums">
            {formatCurrency(animatedValue)}
          </span>
        </div>

        <div class="w-full bg-slate-950/50 rounded-full h-3 border border-white/5 relative">
          <div
            class="bg-linear-to-r {color.bg} h-full rounded-full relative"
            style="
              width: {percentage}%; 
              box-shadow: {animatedValue > 0 ? `0 0 15px rgba(${color.rgb}, 0.4)` : 'none'};
            "
          ></div>
        </div>
      </div>
    {/each}
  </div>
</div>
