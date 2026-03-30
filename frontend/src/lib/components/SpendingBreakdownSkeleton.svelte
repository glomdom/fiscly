<div class="lg:col-span-3 bg-slate-900/60 rounded-3xl p-8 border border-white/5 shadow-sm flex flex-col min-h-90">
  <h3 class="flex items-center gap-2 text-sm font-semibold mb-8 text-slate-400 uppercase tracking-wider">
    <span>Spending Breakdown —</span>
    <div class="h-3 w-24 bg-slate-800 rounded-md animate-pulse"></div>
  </h3>

  <div class="space-y-6 grow flex flex-col justify-center">
    {#each [{ name: "w-32", bar: "w-[75%]" }, { name: "w-40", bar: "w-[50%]" }, { name: "w-24", bar: "w-[30%]" }, { name: "w-28", bar: "w-[15%]" }] as widths}
      <div>
        <div class="flex justify-between items-center text-sm mb-2.5">
          <div class="h-4 {widths.name} bg-slate-800/80 rounded-md animate-pulse"></div>
          <div class="h-4 w-16 bg-slate-800/80 rounded-md animate-pulse"></div>
        </div>

        <div class="w-full bg-slate-950/50 rounded-full h-3 border border-white/5 relative">
          <div class="h-full rounded-full bg-slate-800 animate-pulse {widths.bar}"></div>
        </div>
      </div>
    {/each}
  </div>
</div>
