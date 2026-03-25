<script lang="ts">
  import { Chart, Axis, Area } from "layerchart";
  import {
    MagnifyingGlass,
    Wallet,
    ChartPie,
    Lightbulb,
  } from "phosphor-svelte";

  let searchTerm = $state("");

  let profile = $state({
    firstName: "User",
    lastName: "Name",
    email: "user@example.com",
  });

  const spendingData = [
    { date: new Date(2024, 0, 1), amount: 1200 },
    { date: new Date(2024, 1, 1), amount: 900 },
    { date: new Date(2024, 2, 1), amount: 1500 },
    { date: new Date(2024, 3, 1), amount: 1100 },
  ];

  const transactions = $derived(
    [
      { id: 1, merchant: "Amazon", cat: "Shopping", amount: -45.99, date: "2024-05-01" },
      { id: 2, merchant: "Apple", cat: "Tech", amount: -9.99, date: "2024-05-02" },
      { id: 3, merchant: "Employer Inc", cat: "Salary", amount: 3200.0, date: "2024-05-03" },
      { id: 4, merchant: "Starbucks", cat: "Food", amount: -6.5, date: "2024-05-04" },
    ].filter((t) =>
      t.merchant.toLowerCase().includes(searchTerm.toLowerCase()),
    ),
  );
</script>

<div class="min-h-screen min-w-screen bg-slate-950 text-gray-50 font-sans selection:bg-fuchsia-500/30">
  <nav class="fixed left-0 top-0 h-full w-24 flex flex-col items-center py-8 bg-slate-900 border-r border-violet-900/30 z-10">
    <div class="w-12 h-12 bg-linear-to-br from-indigo-500 to-fuchsia-500 rounded-xl mb-12 flex items-center justify-center text-2xl font-bold text-white shadow-lg shadow-indigo-500/20">
      M
    </div>
    <div class="space-y-8 text-slate-500 flex flex-col items-center">
      <button class="text-violet-400 transition-colors"><Wallet size={28} weight="duotone" /></button>
      <button class="hover:text-fuchsia-400 transition-colors"><ChartPie size={28} /></button>
      <button class="hover:text-fuchsia-400 transition-colors"><Lightbulb size={28} /></button>
    </div>
  </nav>

  <main class="pl-32 pr-8 py-8 max-w-7xl mx-auto space-y-8">
    
    <header class="flex flex-col md:flex-row justify-between items-start md:items-end gap-4">
      <div>
        <h1 class="text-4xl font-bold tracking-tight">Dashboard</h1>
        <p class="text-violet-300 opacity-75 mt-1">Welcome back, {profile.firstName}!</p>
      </div>
      <div class="md:text-right space-y-1">
        <span class="text-xs font-bold uppercase tracking-widest text-slate-500">Total Liquidity</span>
        <p class="text-4xl font-mono text-gray-50 font-light">$12,450<span class="text-violet-400 text-2xl">.80</span></p>
      </div>
    </header>

    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
      
      <div class="lg:col-span-2 bg-slate-900/40 rounded-3xl p-6 border border-violet-900/20 shadow-sm h-80 flex flex-col">
        <h3 class="text-sm font-semibold mb-6 text-slate-400 uppercase tracking-wider">
          Spending Trend (Last 4 Months)
        </h3>
        <div class="grow">
          <Chart data={spendingData} x="date" y="amount" let:width let:height>
            <Area
              line={{ class: "stroke-fuchsia-500 stroke-2" }}
              fill="rgba(217, 70, 239, 0.1)"
            />
            <Axis placement="bottom" grid={{ class: "stroke-white/5" }} />
          </Chart>
        </div>
      </div>

      <div class="bg-linear-to-br from-indigo-600 to-fuchsia-600 rounded-3xl p-6 shadow-xl shadow-indigo-900/20 text-white flex flex-col">
        <div class="flex items-center gap-2 mb-6 opacity-90">
          <Wallet weight="fill" size={20} />
          <h3 class="font-bold tracking-wide">Upcoming Bills</h3>
        </div>
        
        <div class="space-y-4 grow">
          <div class="flex justify-between items-center border-b border-white/20 pb-3">
            <span class="text-sm font-medium">Netflix</span>
            <span class="font-mono font-bold">$15.99</span>
          </div>
          <div class="flex justify-between items-center border-b border-white/20 pb-3">
            <span class="text-sm font-medium">Adobe CC</span>
            <span class="font-mono font-bold">$54.99</span>
          </div>
          <div class="flex justify-between items-center pt-2">
            <span class="text-sm italic opacity-75">Due in 3 days</span>
            <span class="bg-white/20 px-2.5 py-1 rounded-md text-[10px] uppercase font-bold tracking-wider">Auto-Pay</span>
          </div>
        </div>

        <button class="mt-6 w-full py-3 bg-white/10 hover:bg-white/20 rounded-xl text-sm font-semibold transition-all">
          Manage Subscriptions
        </button>
      </div>

      <section class="lg:col-span-3 bg-slate-900/40 rounded-3xl border border-violet-900/20 overflow-hidden shadow-sm">
        <div class="p-6 border-b border-white/5 flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 bg-slate-900/50">
          <h3 class="text-lg font-bold">Recent Transactions</h3>

          <div class="relative w-full sm:max-w-xs text-slate-300">
            <span class="absolute left-3 top-1/2 -translate-y-1/2 opacity-50">
              <MagnifyingGlass size={18} />
            </span>
            <input
              bind:value={searchTerm}
              placeholder="Search merchants..."
              class="w-full bg-slate-950 border border-white/5 rounded-xl pl-10 pr-4 py-2.5 text-sm focus:ring-2 focus:ring-fuchsia-500 focus:border-transparent transition-all outline-none placeholder:text-slate-600"
            />
          </div>
        </div>

        <div class="overflow-x-auto">
          <table class="w-full text-left whitespace-nowrap">
            <thead class="bg-slate-950/50 text-slate-500 text-xs uppercase tracking-widest font-semibold">
              <tr>
                <th class="px-6 py-4">Merchant</th>
                <th class="px-6 py-4">Category</th>
                <th class="px-6 py-4">Date</th>
                <th class="px-6 py-4 text-right">Amount</th>
              </tr>
            </thead>
            <tbody class="divide-y divide-white/5">
              {#if transactions.length === 0}
                <tr>
                  <td colspan="4" class="px-6 py-12 text-center text-slate-500 italic">
                    No transactions found for "{searchTerm}"
                  </td>
                </tr>
              {:else}
                {#each transactions as tx (tx.id)}
                  <tr class="hover:bg-slate-800/50 transition-colors group">
                    <td class="px-6 py-4 font-medium text-gray-200 group-hover:text-fuchsia-400 transition-colors">
                      {tx.merchant}
                    </td>
                    <td class="px-6 py-4">
                      <span class="px-2.5 py-1 rounded-md bg-slate-950 border border-white/5 text-[10px] font-bold uppercase tracking-wider text-violet-300">
                        {tx.cat}
                      </span>
                    </td>
                    <td class="px-6 py-4 text-sm text-slate-500">{tx.date}</td>
                    <td class="px-6 py-4 text-right font-mono font-medium {tx.amount < 0 ? 'text-gray-300' : 'text-emerald-400'}">
                      {tx.amount < 0 ? "" : "+"}{tx.amount.toFixed(2)}
                    </td>
                  </tr>
                {/each}
              {/if}
            </tbody>
          </table>
        </div>
      </section>
      
    </div>
  </main>
</div>