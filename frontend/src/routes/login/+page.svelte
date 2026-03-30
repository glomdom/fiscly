<script lang="ts">
  import { useForm, validators, HintGroup, Hint, email, required } from "svelte-use-form";
  import { enhance } from "$app/forms";
  import { onMount } from "svelte";
  import { fade, fly } from "svelte/transition";

  export let form: { error?: string };

  const clientForm = useForm();
  let ready = false;

  onMount(() => {
    ready = true;
  });
</script>

<div class="min-h-screen w-full bg-slate-950 flex items-center justify-center p-6 selection:bg-fuchsia-500/30 font-sans">
  {#if ready}
    <div class="w-full max-w-5xl flex flex-col md:flex-row items-center justify-between gap-12 lg:gap-24">
      <div class="text-center md:text-left flex-1" in:fade={{ duration: 800 }}>
        <h1 class="text-5xl lg:text-6xl font-bold text-gray-50 tracking-tight mb-4 leading-tight">Welcome back.</h1>
        <p class="text-violet-300 opacity-75 text-lg">Log in to view your finances.</p>
      </div>

      <div class="w-full max-w-md bg-slate-900/50 p-8 rounded-3xl border border-violet-900/30 shadow-2xl backdrop-blur-sm" in:fly={{ y: 40, duration: 800, delay: 150 }}>
        <h2 class="text-2xl font-bold text-gray-50 mb-6">Sign In</h2>

        <form method="POST" use:clientForm use:enhance class="space-y-6">
          <div class="space-y-2">
            <label for="email" class="block text-sm font-medium text-slate-400">Email Address</label>
            <input
              type="email"
              name="email"
              id="email"
              use:validators={[required, email]}
              class="w-full bg-slate-950 border border-white/10 rounded-xl px-4 py-3 text-gray-50 focus:ring-2 focus:ring-fuchsia-500 focus:border-transparent transition-all outline-none placeholder:text-slate-600"
              placeholder="you@example.com"
            />
            <HintGroup for="email">
              <div class="text-red-400 text-xs mt-1.5 font-medium px-1">
                <Hint on="required">Email is required</Hint>
                <Hint on="email" hideWhenRequired>Please enter a valid email</Hint>
              </div>
            </HintGroup>
          </div>

          <div class="space-y-2">
            <label for="password" class="block text-sm font-medium text-slate-400">Password</label>
            <input
              type="password"
              name="password"
              id="password"
              use:validators={[required]}
              class="w-full bg-slate-950 border border-white/10 rounded-xl px-4 py-3 text-gray-50 focus:ring-2 focus:ring-fuchsia-500 focus:border-transparent transition-all outline-none placeholder:text-slate-600"
              placeholder="••••••••"
            />
            <div class="text-red-400 text-xs mt-1.5 font-medium px-1">
              <Hint for="password" on="required">Password is required</Hint>
            </div>
          </div>

          {#if form?.error}
            <div class="flex items-center gap-2 text-red-400 text-sm font-medium px-1 mb-2" in:fly={{ y: -5, duration: 200 }}>
              <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" viewBox="0 0 256 256">
                <path
                  d="M128,24A104,104,0,1,0,232,128,104.11,104.11,0,0,0,128,24Zm0,192a88,88,0,1,1,88-88A88.1,88.1,0,0,1,128,216Zm-8-80V80a8,8,0,0,1,16,0v56a8,8,0,0,1-16,0Zm20,36a12,12,0,1,1-12-12A12,12,0,0,1,140,172Z"
                ></path>
              </svg>
              {form.error}
            </div>
          {/if}

          <button
            type="submit"
            disabled={!$clientForm.valid}
            class="w-full py-3.5 bg-linear-to-r from-indigo-600 to-fuchsia-600 hover:from-indigo-500 hover:to-fuchsia-500 text-white rounded-xl font-semibold shadow-lg shadow-fuchsia-900/20 transition-all disabled:opacity-50 disabled:cursor-not-allowed mt-4"
          >
            Login to Dashboard
          </button>
        </form>

        <div class="mt-8 text-center">
          <span class="text-sm text-slate-400">Don't have an account?</span>

          <a href="/register" class="text-sm font-semibold underline underline-offset-4 text-slate-400 hover:text-fuchsia-400 transition-colors">
            Register here
          </a>
        </div>
      </div>
    </div>
  {/if}
</div>

<style>
  :global(input.touched:invalid) {
    border-color: #f87171 !important;
  }

  :global(input.touched:invalid:focus) {
    --tw-ring-color: #f87171 !important;
  }
</style>
