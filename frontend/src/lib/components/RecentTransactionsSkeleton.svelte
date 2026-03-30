<section class="lg:col-span-3 bg-slate-900/60 rounded-3xl border border-white/5 overflow-hidden shadow-sm mt-4">
  <div class="p-6 border-b border-white/5 flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 bg-slate-950/30">
    <h3 class="text-lg font-bold">Recent Transactions</h3>
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
        {#each Array(4) as _}
          <tr>
            <td class="px-6 py-4">
              <div class="h-4 bg-white/10 rounded-md w-3/4 animate-pulse"></div>
            </td>
            <td class="px-6 py-4">
              <div class="h-4 bg-white/10 rounded-md w-1/2 animate-pulse"></div>
            </td>
            <td class="px-6 py-4">
              <div class="h-4 bg-white/10 rounded-md w-24 animate-pulse"></div>
            </td>
            <td class="px-6 py-4">
              <div class="h-4 bg-white/10 rounded-md w-16 ml-auto animate-pulse"></div>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
</section>
