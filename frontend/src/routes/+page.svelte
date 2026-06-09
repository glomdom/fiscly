<script lang="ts">
  import { onMount } from "svelte";
  import { fade, fly } from "svelte/transition";

  let ready = false;
  onMount(() => {
    ready = true;
  });
</script>

<div class="min-h-screen w-full bg-slate-950 flex flex-col justify-between font-sans selection:bg-fuchsia-500/30 text-gray-50 overflow-x-hidden relative">
  
  <div class="absolute top-0 left-1/4 w-96 h-96 bg-indigo-600/10 rounded-full blur-3xl pointer-events-none"></div>
  <div class="absolute bottom-1/4 right-1/4 w-96 h-96 bg-fuchsia-600/10 rounded-full blur-3xl pointer-events-none"></div>

  {#if ready}
    <div class="flex-1 flex items-center justify-center p-6 my-12 z-10">
      <div 
        class="w-full max-w-5xl bg-slate-900/40 p-8 md:p-16 rounded-3xl border border-violet-900/30 shadow-2xl backdrop-blur-md flex flex-col items-center text-center gap-8"
        in:fly={{ y: 40, duration: 800 }}
      >
        <div class="space-y-3">
          <h1 class="text-5xl md:text-7xl font-bold tracking-tight bg-clip-text text-transparent bg-gradient-to-r from-gray-50 via-violet-100 to-fuchsia-200">
            FISCLY
          </h1>
          <div class="w-24 h-1 bg-gradient-to-r from-indigo-500 to-fuchsia-500 mx-auto rounded-full mt-4"></div>
        </div>

        <p class="max-w-2xl text-violet-300/80 text-lg md:text-xl leading-relaxed font-light">
          Take control of your finances today with Fiscly. 
          Track your earnings, monitor your costs, and unlock your true financial potential with just one click.
        </p>

        <div class="flex flex-col sm:flex-row gap-4 w-full sm:w-auto pt-4" in:fade={{ delay: 300, duration: 600 }}>
          <a 
            href="/register" 
            class="px-8 py-4 bg-gradient-to-r from-indigo-600 to-fuchsia-600 hover:from-indigo-500 hover:to-fuchsia-500 text-white rounded-xl font-semibold shadow-lg shadow-fuchsia-900/20 transition-all transform hover:-translate-y-0.5"
          >
            Get Started
          </a>
          <a 
            href="/login" 
            class="px-8 py-4 bg-slate-950/60 hover:bg-slate-950 border border-white/10 hover:border-violet-500/50 text-slate-300 hover:text-white rounded-xl font-semibold transition-all backdrop-blur-sm"
          >
            Sign In
          </a>
        </div>

        <div class="grid grid-cols-2 md:grid-cols-3 gap-6 pt-10 border-t border-white/5 w-full mt-6 text-slate-400 text-sm">
          <div class="flex flex-col items-center gap-1">
            <span class="text-xl text-fuchsia-400 font-semibold">100% Free</span>
            <span class="text-xs opacity-70">No hidden fees</span>
          </div>
          <div class="flex flex-col items-center gap-1">
            <span class="text-xl text-indigo-400 font-semibold">Intuitive UI</span>
            <span class="text-xs opacity-70">Clean and simple tracking</span>
          </div>
          <div class="flex grid-cols-2 flex-col items-center gap-1 col-span-2 md:col-span-1">
            <span class="text-xl text-violet-400 font-semibold">Secure</span>
            <span class="text-xs opacity-70">Data is protected</span>
          </div>
        </div>

      </div>
    </div>
  {/if}

  <footer class="w-full bg-slate-950 border-t border-white/5 py-6 px-8 flex flex-col sm:flex-row items-center justify-between gap-4 text-sm text-slate-500 z-10">
    <div>
      Copyright 2026 Kiril Sadonkov, Teo Mitev, Rumena Popova. All Rights Reserved.
    </div>
  </footer>
</div>

<style>
  :global(body) {
    background-color: #020617;
    margin: 0;
  }
</style>