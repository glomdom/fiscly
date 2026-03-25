<script lang="ts">
  import { MagnifyingGlassIcon, WalletIcon, ArrowUpRightIcon, ArrowDownRightIcon, PiggyBankIcon, TrendUpIcon } from "phosphor-svelte";

  let { data } = $props();

  let profile = $derived(data.user)!;

  let searchTerm = $state("");
</script>

<div class="min-h-screen bg-slate-950 text-gray-50 font-sans selection:bg-fuchsia-500/30">
  <main class="max-w-7xl mx-auto px-6 lg:px-12 py-12 space-y-10">
    <header class="flex flex-col md:flex-row justify-between items-start md:items-end gap-6 pb-6 border-b border-white/10">
      <div class="flex items-center gap-5">
        <div
          class="w-14 h-14 bg-linear-to-br from-indigo-500 to-fuchsia-500 rounded-2xl flex items-center justify-center text-3xl font-bold text-white shadow-lg shadow-indigo-500/20"
        >
          F
        </div>
        <div>
          <h1 class="text-4xl font-bold tracking-tight">Dashboard</h1>
          <p class="text-violet-300 opacity-75 mt-1 text-lg">Welcome back, {profile.firstName}.</p>
        </div>
      </div>

      <div class="md:text-right space-y-1 flex flex-col items-end">
        <span class="text-xs font-bold uppercase tracking-widest text-slate-500">Total Liquidity</span>

        {#await data.streamed.summary}
          <div class="h-10 w-40 bg-slate-800/50 rounded-lg animate-pulse mt-1"></div>
        {:then summary}
          <p class="text-4xl font-mono text-gray-50 font-light tracking-tight">
            ${Math.floor(summary.metrics.totalLiquidity).toLocaleString()}<span class="text-violet-400 text-2xl"
              >.{(summary.metrics.totalLiquidity % 1).toFixed(2).substring(2)}</span
            >
          </p>
        {:catch}
          <p class="text-red-400 text-xl font-mono mt-2">Error</p>
        {/await}
      </div>
    </header>

    <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
      {#await data.streamed.summary}
        <div class="col-span-1 sm:col-span-2 lg:col-span-4 bg-slate-900/40 rounded-3xl border border-white/5 shadow-sm h-32 flex flex-col items-center justify-center">
          <div class="w-8 h-8 rounded-full border-2 border-fuchsia-500/30 border-t-fuchsia-500 animate-spin mb-3"></div>
          <p class="text-sm text-slate-500 font-medium animate-pulse tracking-wide">Syncing secure data...</p>
        </div>
      {:then summary}
        <div
          class="bg-slate-900/60 rounded-3xl p-6 border border-white/5 shadow-sm flex flex-col justify-center relative overflow-hidden group hover:border-emerald-500/30 transition-colors"
        >
          <div class="absolute -right-4 -top-4 w-20 h-20 bg-emerald-500/10 rounded-full blur-2xl group-hover:bg-emerald-500/20 transition-colors"></div>
          <div class="flex items-center gap-2 mb-2">
            <ArrowDownRightIcon class="text-emerald-400" size={20} />
            <p class="text-sm font-medium text-slate-400">Monthly Income</p>
          </div>
          <p class="text-2xl font-mono text-gray-200">${summary.metrics.monthlyIncome.toLocaleString("en-US", { minimumFractionDigits: 2 })}</p>
        </div>

        <div
          class="bg-slate-900/60 rounded-3xl p-6 border border-white/5 shadow-sm flex flex-col justify-center relative overflow-hidden group hover:border-red-500/30 transition-colors"
        >
          <div class="absolute -right-4 -top-4 w-20 h-20 bg-red-500/10 rounded-full blur-2xl group-hover:bg-red-500/20 transition-colors"></div>
          <div class="flex items-center gap-2 mb-2">
            <ArrowUpRightIcon class="text-red-400" size={20} />
            <p class="text-sm font-medium text-slate-400">Monthly Expenses</p>
          </div>
          <p class="text-2xl font-mono text-gray-200">${summary.metrics.monthlyExpenses.toLocaleString("en-US", { minimumFractionDigits: 2 })}</p>
        </div>

        <div
          class="bg-slate-900/60 rounded-3xl p-6 border border-white/5 shadow-sm flex flex-col justify-center relative overflow-hidden group hover:border-indigo-500/30 transition-colors"
        >
          <div class="absolute -right-4 -top-4 w-20 h-20 bg-indigo-500/10 rounded-full blur-2xl group-hover:bg-indigo-500/20 transition-colors"></div>
          <div class="flex items-center gap-2 mb-2">
            <PiggyBankIcon class="text-indigo-400" size={20} />
            <p class="text-sm font-medium text-slate-400">Total Savings</p>
          </div>
          <p class="text-2xl font-mono text-gray-200">${summary.metrics.totalSavings.toLocaleString("en-US", { minimumFractionDigits: 2 })}</p>
        </div>

        <div
          class="bg-slate-900/60 rounded-3xl p-6 border border-white/5 shadow-sm flex flex-col justify-center relative overflow-hidden group hover:border-fuchsia-500/30 transition-colors"
        >
          <div class="absolute -right-4 -top-4 w-20 h-20 bg-fuchsia-500/10 rounded-full blur-2xl group-hover:bg-fuchsia-500/20 transition-colors"></div>
          <div class="flex items-center gap-2 mb-2">
            <TrendUpIcon class="text-fuchsia-400" size={20} />
            <p class="text-sm font-medium text-slate-400">Investments</p>
          </div>
          <p class="text-2xl font-mono text-gray-200">${summary.metrics.investments.toLocaleString("en-US", { minimumFractionDigits: 2 })}</p>
        </div>
      {:catch error}
        <div class="col-span-1 sm:col-span-2 lg:col-span-4 bg-red-950/30 rounded-3xl p-6 border border-red-500/30 text-center">
          <p class="text-red-400 font-medium">Unable to load metrics. Please refresh the page.</p>
        </div>
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

      <div class="bg-linear-to-br from-indigo-600 to-fuchsia-600 rounded-3xl p-6 shadow-xl shadow-indigo-900/20 text-white flex flex-col">
        <div class="flex items-center gap-2 mb-8 opacity-90">
          <WalletIcon weight="fill" size={20} />
          <h3 class="font-bold tracking-wide">Upcoming Bills</h3>
        </div>

        <div class="space-y-5 grow">
          <div class="flex justify-between items-center pb-4 border-b border-white/20">
            <div>
              <p class="font-semibold text-white">Netflix Premium</p>
              <p class="text-xs text-white/70 mt-1">Due in 3 days</p>
            </div>
            <span class="font-mono font-bold text-lg">$19.99</span>
          </div>
          <div class="flex justify-between items-center pb-4 border-b border-white/20">
            <div>
              <p class="font-semibold text-white">AWS Hosting</p>
              <p class="text-xs text-white/70 mt-1">Due in 5 days</p>
            </div>
            <span class="font-mono font-bold text-lg">$45.50</span>
          </div>
        </div>

        <button class="mt-8 w-full py-4 bg-white/10 hover:bg-white/20 rounded-xl text-sm font-bold tracking-wide transition-all backdrop-blur-sm shadow-inner">
          View All Subscriptions
        </button>
      </div>

      <section class="lg:col-span-3 bg-slate-900/60 rounded-3xl border border-white/5 overflow-hidden shadow-sm mt-4">
        <div class="p-6 border-b border-white/5 flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 bg-slate-950/30">
          <h3 class="text-lg font-bold">Recent Transactions</h3>

          <div class="relative w-full sm:max-w-xs text-slate-300">
            <span class="absolute left-4 top-1/2 -translate-y-1/2 opacity-50">
              <MagnifyingGlassIcon size={18} />
            </span>
            <input
              bind:value={searchTerm}
              placeholder="Search history..."
              class="w-full bg-slate-950 border border-white/10 rounded-xl pl-11 pr-4 py-3 text-sm focus:ring-2 focus:ring-fuchsia-500 focus:border-transparent transition-all outline-none placeholder:text-slate-600"
            />
          </div>
        </div>

        <div class="overflow-x-auto min-h-50">
          <table class="w-full text-left whitespace-nowrap">
            <thead class="bg-slate-950/80 text-slate-400 text-xs uppercase tracking-widest font-semibold border-b border-white/5">
              <tr>
                <th class="px-6 py-5">Merchant</th>
                <th class="px-6 py-5">Category</th>
                <th class="px-6 py-5">Date</th>
                <th class="px-6 py-5 text-right">Amount</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-white/5">
              <tr>
                <td colspan="4" class="px-6 py-16 text-center">
                  <div class="flex flex-col items-center justify-center opacity-50">
                    <WalletIcon size={48} weight="duotone" class="mb-4 text-slate-500" />
                    <p class="text-slate-400 font-medium">No recent transactions to display.</p>
                    <p class="text-slate-500 text-sm mt-1">Your activity will appear here.</p>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>
    </div>
  </main>
</div>
