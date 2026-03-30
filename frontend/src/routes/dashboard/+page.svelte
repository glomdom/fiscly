<script lang="ts">
  import { Tween } from "svelte/motion";
  import { cubicOut } from "svelte/easing";
  import ArrowUpRightIcon from "phosphor-svelte/lib/ArrowUpRightIcon";
  import ArrowDownRightIcon from "phosphor-svelte/lib/ArrowDownRightIcon";
  import PiggyBankIcon from "phosphor-svelte/lib/PiggyBankIcon";
  import TrendUpIcon from "phosphor-svelte/lib/TrendUpIcon";
  import MetricCard from "$lib/components/MetricCard.svelte";
  import RecentTransactions from "$lib/components/RecentTransactions.svelte";
  import MetricCardSkeleton from "$lib/components/MetricCardSkeleton.svelte";
  import RecentTransactionsSkeleton from "$lib/components/RecentTransactionsSkeleton.svelte";
  import SpendingBreakdown from "$lib/components/SpendingBreakdown.svelte";
  import SpendingBreakdownSkeleton from "$lib/components/SpendingBreakdownSkeleton.svelte";

  let { data } = $props();

  let profile = $derived(data.user)!;

  const liquidityTween = new Tween(0, { duration: 1500, easing: cubicOut });

  $effect(() => {
    data.streamed.summary
      .then((summary: any) => {
        liquidityTween.target = summary.metrics.totalLiquidity;
      })
      .catch(() => {});
  });
</script>

<div class="min-h-screen bg-slate-950 text-gray-50 font-sans selection:bg-fuchsia-500/30">
  <main class="max-w-7xl mx-auto px-6 lg:px-12 py-12 space-y-10">
    <header class="flex flex-col md:flex-row justify-between items-start md:items-end gap-6 pb-6 border-b border-white/10">
      <div class="flex items-center gap-5">
        <div>
          <h1 class="text-4xl font-bold tracking-tight">Dashboard</h1>
          <p class="text-violet-300 opacity-75 mt-1 text-lg">Welcome back, {profile.firstName}.</p>
        </div>
      </div>

      <div class="md:text-right space-y-1 flex flex-col items-end">
        <span class="text-xs font-bold uppercase tracking-widest text-slate-500">Total Liquidity</span>

        {#await data.streamed.summary}
          <div class="flex items-center justify-end gap-3 h-10 w-full">
            <div class="w-5 h-5 rounded-full border-3 border-slate-800 border-t-slate-500 animate-spin"></div>
            <div class="h-9 w-36 bg-slate-800/50 rounded-lg animate-pulse"></div>
          </div>
        {:then summary}
          {@const val = liquidityTween.current}
          {@const isNegative = val < 0}
          {@const absVal = Math.abs(val)}

          <p class="text-4xl font-mono font-light tracking-tight leading-none h-10 flex items-baseline transition-colors {isNegative ? 'text-red-400' : 'text-gray-50'}">
            <span>{isNegative ? "-$" : "$"}</span>

            {Math.trunc(absVal).toLocaleString("en-US")}

            <span class="{isNegative ? 'text-red-500/80' : 'text-violet-400'} text-2xl">
              .{(absVal % 1).toFixed(2).split(".")[1]}
            </span>
          </p>
        {:catch}
          <p class="text-red-400 text-xl font-mono mt-2 h-10 flex items-center">Error</p>
        {/await}
      </div>
    </header>

    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
      {#await data.streamed.summary}
        {#each Array(4) as _}
          <MetricCardSkeleton />
        {/each}
      {:then summary}
        <MetricCard title="Monthly Income" amount={summary.metrics.monthlyIncome} theme="emerald" Icon={ArrowDownRightIcon} />
        <MetricCard title="Monthly Expenses" amount={summary.metrics.monthlyExpenses} theme="red" Icon={ArrowUpRightIcon} />
        <MetricCard title="Total Savings" amount={summary.metrics.totalSavings} theme="indigo" Icon={PiggyBankIcon} />
        <MetricCard title="Investments" amount={summary.metrics.investments} theme="fuchsia" Icon={TrendUpIcon} />
      {/await}
    </div>

    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
      {#await data.streamed.breakdown}
        <SpendingBreakdownSkeleton />
      {:then breakdown}
        <SpendingBreakdown monthlyBreakdowns={breakdown} />
      {:catch}
        <div class="bg-red-950/30 rounded-3xl p-6 border border-red-500/30 text-center mt-4">
          <p class="text-red-400">Failed to load breakdown.</p>
        </div>
      {/await}

      <div class="lg:col-span-3">
        {#await data.streamed.transactions}
          <RecentTransactionsSkeleton />
        {:then transactions}
          <RecentTransactions {transactions} />
        {:catch}
          <div class="bg-red-950/30 rounded-3xl p-6 border border-red-500/30 text-center mt-4">
            <p class="text-red-400">Failed to load transactions.</p>
          </div>
        {/await}
      </div>
    </div>
  </main>
</div>
