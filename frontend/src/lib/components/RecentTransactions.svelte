<script lang="ts">
  import WalletIcon from "phosphor-svelte/lib/WalletIcon";

  let { transactions = [] } = $props();

  let searchTerm = $state("");

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString("en-US", {
      month: "short",
      day: "numeric",
      year: "numeric",
    });
  };
</script>

<section class="lg:col-span-3 bg-slate-900/60 rounded-3xl border border-white/5 overflow-hidden shadow-sm mt-4">
  <div class="p-6 border-b border-white/5 flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 bg-slate-950/30">
    <h3 class="text-lg font-bold text-slate-100">Recent Transactions</h3>
  </div>

  <div class="overflow-x-auto min-h-50">
    <table class="w-full text-left whitespace-nowrap table-fixed">
      <thead class="bg-slate-950/80 text-slate-400 text-xs uppercase tracking-widest font-semibold border-b border-white/5">
        <tr>
          <th class="px-6 py-5 w-[20%]">Merchant</th>
          <th class="px-6 py-5 w-[20%]">Category</th>
          <th class="px-6 py-5 w-[20%]">Date</th>
          <th class="px-6 py-5 w-[10%] text-right">Amount</th>
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
            <tr class="hover:bg-white/2 transition-colors group">
              <td class="px-6 py-4 font-medium text-slate-200">{tx.merchant}</td>
              <td class="px-6 py-4 text-slate-400">{tx.category}</td>
              <td class="px-6 py-4 text-slate-500 text-sm">{formatDate(tx.date)}</td>
              <td class="px-6 py-4 text-right">
                <span
                  class="inline-flex items-center justify-center px-3 py-1 rounded-full text-sm font-mono font-medium border transition-colors {tx.amount < 0
                    ? 'bg-red-500/10 border-red-500/20 text-red-400 group-hover:bg-red-500/20 group-hover:border-red-500/40'
                    : 'bg-emerald-500/10 border-emerald-500/20 text-emerald-400 group-hover:bg-emerald-500/20 group-hover:border-emerald-500/40'}"
                >
                  {tx.amount < 0 ? "-" : "+"}${Math.abs(tx.amount).toFixed(2)}
                </span>
              </td>
            </tr>
          {/each}
        {/if}
      </tbody>
    </table>
  </div>
</section>
