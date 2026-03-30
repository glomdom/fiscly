<script lang="ts">
  import MagnifyingGlassIcon from "phosphor-svelte/lib/MagnifyingGlassIcon";
  import WalletIcon from "phosphor-svelte/lib/WalletIcon";

  let { transactions = [] } = $props();

  let searchTerm = $state("");
</script>

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
          <th class="px-6 py-5 w-[40%]">Merchant</th>
          <th class="px-6 py-5 w-[25%]">Category</th>
          <th class="px-6 py-5 w-[20%]">Date</th>
          <th class="px-6 py-5 w-[15%] text-right">Amount</th>
        </tr>
      </thead>
      <tbody class="divide-y divide-white/5">
        {#if transactions.length === 0}
          <tr>
            <td colspan="4" class="px-6 py-16 text-center">
              <div class="flex flex-col items-center justify-center opacity-50">
                <WalletIcon size={48} weight="duotone" class="mb-4 text-slate-500" />
                <p class="text-slate-400 font-medium">
                  {searchTerm ? "No results found." : "No recent transactions to display."}
                </p>
              </div>
            </td>
          </tr>
        {:else}
          {#each transactions as tx}
            <tr>
              <td class="px-6 py-4">{tx.merchant}</td>
              <td class="px-6 py-4">{tx.category}</td>
              <td class="px-6 py-4">{tx.date}</td>
              <td class="px-6 py-4 text-right">${tx.amount}</td>
            </tr>
          {/each}
        {/if}
      </tbody>
    </table>
  </div>
</section>
