<div class="lg:col-span-3 bg-slate-900/60 rounded-3xl p-8 border border-white/5 shadow-sm flex flex-col min-h-90">
  <h3 class="text-sm font-semibold mb-8 text-slate-400 uppercase tracking-wider">Spending Breakdown</h3>

  <div class="space-y-6 grow flex flex-col justify-center">
    {#each Array(4) as _}
      <div>
        <div class="flex justify-between text-sm mb-3">
          <div class="h-4 bg-white/10 rounded-md w-1/8 animate-pulse"></div>
          <div class="h-4 bg-white/10 rounded-md w-18 animate-pulse"></div>
        </div>
        <div class="h-3 w-full bg-white/10 rounded-md animate-pulse"></div>
        <!-- <div class="w-full bg-slate-950/50 rounded-full h-3 border border-white/5 overflow-hidden">
          <div class="bg-linear-to-r from-indigo-500 to-indigo-400 h-full rounded-full" style="width: 65%"></div>
        </div> -->
      </div>
    {/each}
  </div>
</div>
