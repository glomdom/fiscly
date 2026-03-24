<script lang="ts">
  import { createQuery } from '@tanstack/svelte-query';
  import { Chart, Axis, Area, Tooltip } from 'layerchart';
  import { MagnifyingGlass, Wallet, ChartPie, Lightbulb } from 'phosphor-svelte'; // Example icons
  
  // Svelte 5 state for search functionality 
  let searchTerm = $state("");

  // Mock Profile Data (based on your existing 'me' endpoint) [cite: 10]
  let profile = $state({
    firstName: "User",
    lastName: "Name",
    email: "user@example.com"
  });

  // Mock Data for LayerChart 
  const spendingData = [
    { date: new Date(2024, 0, 1), amount: 1200 },
    { date: new Date(2024, 1, 1), amount: 900 },
    { date: new Date(2024, 2, 1), amount: 1500 },
    { date: new Date(2024, 3, 1), amount: 1100 },
  ];

  // Mock Transactions for the Table 
  const transactions = $derived([
    { id: 1, merchant: "Amazon", cat: "Shopping", amount: -45.99, date: "2024-05-01" },
    { id: 2, merchant: "Apple", cat: "Tech", amount: -9.99, date: "2024-05-02" },
    { id: 3, merchant: "Employer Inc", cat: "Salary", amount: 3200.00, date: "2024-05-03" },
    { id: 4, merchant: "Starbucks", cat: "Food", amount: -6.50, date: "2024-05-04" },
  ].filter(t => t.merchant.toLowerCase().includes(searchTerm.toLowerCase())));
</script>

<div class="min-h-screen min-w-screen bg-slate-950 text-slate-200 font-sans selection:bg-violet-500/30">
  
  <nav class="fixed left-0 top-0 h-full w-20 flex flex-col items-center py-8 bg-slate-900 border-r border-white/5">
    <div class="w-12 h-12 bg-violet-600 rounded-xl mb-12 flex items-center justify-center font-bold text-white">F</div>
    <div class="space-y-8 text-slate-500">
      <Wallet size={24} class="text-violet-400" />
      <ChartPie size={24} />
      <Lightbulb size={24} />
    </div>
  </nav>

  <main class="pl-24 p-8 max-w-7xl mx-auto space-y-8">
    
    <header class="flex justify-between items-end">
      <div>
        <h1 class="text-4xl font-bold tracking-tight">Dashboard</h1>
        <p class="text-slate-500">Welcome back, {profile.firstName}!</p>
      </div>
      <div class="text-right space-y-1">
        <span class="text-xs font-bold uppercase tracking-widest text-slate-500">Total Liquidity</span>
        <p class="text-3xl font-mono text-emerald-400 font-bold">$12,450.80</p>
      </div>
    </header>

    <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
      
      <div class="lg:col-span-2 bg-slate-900/50 rounded-3xl p-6 border border-white/5 h-80 relative">
        <h3 class="text-sm font-semibold mb-6 text-slate-400">Spending Trend (Last 4 Months)</h3>
        <div class="h-56">
          <Chart data={spendingData} x="date" y="amount" let:width let:height>
            <Area line={{ class: 'stroke-violet-500 stroke-2' }} fill="rgba(139, 92, 246, 0.1)" />
            <Axis placement="bottom" grid={{ class: 'stroke-white/5' }} />
          </Chart>
        </div>
      </div>

      <div class="bg-gradient-to-br from-violet-600 to-indigo-700 rounded-3xl p-6 shadow-xl shadow-violet-900/20 text-white">
        <div class="flex items-center gap-2 mb-4">
            <Wallet weight="fill" />
            <h3 class="font-bold italic">Upcoming Bills</h3>
        </div>
        <div class="space-y-4">
            <div class="flex justify-between items-center border-b border-white/10 pb-2">
            <span class="text-sm text-violet-100">Netflix</span>
            <span class="font-mono font-bold text-xs">$15.99</span>
            </div>
            <div class="flex justify-between items-center border-b border-white/10 pb-2">
            <span class="text-sm text-violet-100">Adobe CC</span>
            <span class="font-mono font-bold text-xs">$54.99</span>
            </div>
            <div class="flex justify-between items-center">
            <span class="text-sm text-violet-100 italic">Due in 3 days</span>
            <span class="bg-white/20 px-2 py-0.5 rounded text-[10px] uppercase font-bold">Auto-Pay</span>
            </div>
        </div>
        <button class="mt-6 w-full py-3 bg-white/10 hover:bg-white/20 rounded-xl text-sm font-semibold transition-all">
            Manage Subscriptions
        </button>
    </div>

    <section class="w-6xl wrap-anywhere object-fill bg-slate-900/50 rounded-3xl border border-white/5 overflow-visible">
      <div class="p-6 border-b border-white/5 flex flex-wrap justify-between items-center gap-4">
        <h3 class="text-lg font-bold">Recent Transactions</h3>
        
        <div class="relative w-full max-w-xs">
          <span class="absolute left-3 top-1/2 -translate-y-1/2 text-slate-500">
            <MagnifyingGlass size={18} />
          </span>
          <input 
            bind:value={searchTerm}
            placeholder="Search merchants..." 
            class="w-full bg-slate-800 border-none rounded-xl pl-10 pr-4 py-2 text-sm focus:ring-2 focus:ring-violet-500 transition-all outline-none"
          />
        </div>
      </div>

      <div class="overflow-x-auto">
        <table class="w-full text-left">
          <thead class="bg-white/5 text-slate-500 text-xs uppercase tracking-tighter">
            <tr>
              <th class="px-6 py-4">Merchant</th>
              <th class="px-6 py-4">Category</th>
              <th class="px-6 py-4">Date</th>
              <th class="px-6 py-4 text-right">Amount</th>
            </tr>
          </thead>
          <tbody class="divide-y divide-white/5">
            {#each transactions as tx}
              <tr class="hover:bg-white/5 transition-colors group">
                <td class="px-6 py-4 font-medium group-hover:text-violet-400 transition-colors">{tx.merchant}</td>
                <td class="px-6 py-4">
                  <span class="px-2 py-1 rounded-md bg-slate-800 text-[10px] font-bold uppercase">{tx.cat}</span>
                </td>
                <td class="px-6 py-4 text-sm text-slate-500">{tx.date}</td>
                <td class="px-6 py-4 text-right font-mono font-bold {tx.amount < 0 ? 'text-slate-300' : 'text-emerald-400'}">
                  {tx.amount < 0 ? '' : '+'}{tx.amount.toFixed(2)}
                </td>
              </tr>
            {/each}
          </tbody>
        </table>
      </div>
    </section>

  </main>
</div>