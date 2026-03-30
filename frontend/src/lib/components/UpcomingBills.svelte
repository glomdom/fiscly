<script lang="ts">
  import WalletIcon from "phosphor-svelte/lib/WalletIcon";
</script>

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
