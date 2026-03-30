<script lang="ts">
  import ArrowUpRightIcon from "phosphor-svelte/lib/ArrowUpRightIcon";
  import ArrowDownRightIcon from "phosphor-svelte/lib/ArrowDownRightIcon";
  import PiggyBankIcon from "phosphor-svelte/lib/PiggyBankIcon";
  import TrendUpIcon from "phosphor-svelte/lib/TrendUpIcon";
  import MetricCard from "$lib/components/MetricCard.svelte";
  import UpcomingBills from "$lib/components/UpcomingBills.svelte";
  import RecentTransactions from "$lib/components/RecentTransactions.svelte";
  import MetricCardSkeleton from "$lib/components/MetricCardSkeleton.svelte";
  import RecentTransactionsSkeleton from "$lib/components/RecentTransactionsSkeleton.svelte";

  let { data } = $props();

  let profile = $derived(data.user)!;
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
            <div class="w-5 h-5 rounded-full border-2 border-fuchsia-500/30 border-t-fuchsia-500 animate-spin"></div>
            <div class="h-9 w-36 bg-slate-800/50 rounded-lg animate-pulse"></div>
          </div>
        {:then summary}
          <p class="text-4xl font-mono text-gray-50 font-light tracking-tight leading-none h-10 flex items-center">
            ${Math.floor(summary.metrics.totalLiquidity).toLocaleString()}<span class="text-violet-400 text-2xl"
              >.{(summary.metrics.totalLiquidity % 1).toFixed(2).substring(2)}</span
            >
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
      <div class="lg:col-span-2 bg-slate-900/60 rounded-3xl p-8 border border-white/5 shadow-sm flex flex-col min-h-85">
        <h3 class="text-sm font-semibold mb-8 text-slate-400 uppercase tracking-wider">Spending Breakdown</h3>

        <div class="space-y-6 grow flex flex-col justify-center">
          <div>
            <div class="flex justify-between text-sm mb-2.5">
              <span class="font-medium text-gray-200">Housing & Utilities</span>
              <span class="font-mono text-slate-400">$1,250.00</span>
            </div>
            <div class="w-full bg-slate-950/50 rounded-full h-3 border border-white/5 overflow-hidden">
              <div class="bg-linear-to-r from-indigo-500 to-indigo-400 h-full rounded-full" style="width: 65%"></div>
            </div>
          </div>

          <div>
            <div class="flex justify-between text-sm mb-2.5">
              <span class="font-medium text-gray-200">Food & Dining</span>
              <span class="font-mono text-slate-400">$420.50</span>
            </div>
            <div class="w-full bg-slate-950/50 rounded-full h-3 border border-white/5 overflow-hidden">
              <div class="bg-linear-to-r from-fuchsia-500 to-fuchsia-400 h-full rounded-full" style="width: 40%"></div>
            </div>
          </div>

          <div>
            <div class="flex justify-between text-sm mb-2.5">
              <span class="font-medium text-gray-200">Shopping & Tech</span>
              <span class="font-mono text-slate-400">$185.00</span>
            </div>
            <div class="w-full bg-slate-950/50 rounded-full h-3 border border-white/5 overflow-hidden">
              <div class="bg-linear-to-r from-emerald-500 to-emerald-400 h-full rounded-full" style="width: 25%"></div>
            </div>
          </div>

          <div>
            <div class="flex justify-between text-sm mb-2.5">
              <span class="font-medium text-gray-200">Entertainment</span>
              <span class="font-mono text-slate-400">$95.00</span>
            </div>
            <div class="w-full bg-slate-950/50 rounded-full h-3 border border-white/5 overflow-hidden">
              <div class="bg-linear-to-r from-violet-500 to-violet-400 h-full rounded-full" style="width: 15%"></div>
            </div>
          </div>
        </div>
      </div>

      <UpcomingBills />

      <div class="lg:col-span-3">
        {#await data.streamed.transactions}
          <RecentTransactionsSkeleton />
        {:then transactions}
          <RecentTransactions transactions={transactions} />
        {:catch}
          <div class="bg-red-950/30 rounded-3xl p-6 border border-red-500/30 text-center mt-4">
            <p class="text-red-400">Failed to load transactions.</p>
          </div>
        {/await}
      </div>
    </div>
  </main>
</div>
