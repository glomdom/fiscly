<script>
  import { useForm, validators, HintGroup, Hint, email, required } from "svelte-use-form";
  import { onMount } from "svelte";
  import { fade, fly } from "svelte/transition";
  import { goto } from "$app/navigation";

  const form = useForm();
  let ready = false;

  onMount(() => {
    ready = true;
  });

  async function onRegister() {
    const formData = $form.values;

    try {
      const response = await fetch("http://localhost:5096/api/auth/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      if (response.ok) {
        goto("/login");
      } else {
        alert("Error! Couldn't register. Please check your details.");
      }
    } catch (error) {
      console.error("Registration failed:", error);
      alert("Network error. Make sure the server is running.");
    }
  }
</script>

<div class="min-h-screen w-full bg-slate-950 flex items-center justify-center p-6 selection:bg-fuchsia-500/30 font-sans">
  {#if ready}
    <div class="w-full max-w-5xl flex flex-col md:flex-row items-center justify-between gap-12 lg:gap-24">
      <div class="text-center md:text-left flex-1" in:fade={{ duration: 800 }}>
        <div
          class="w-16 h-16 bg-linear-to-br from-indigo-500 to-fuchsia-500 rounded-2xl mb-8 mx-auto md:mx-0 flex items-center justify-center text-3xl font-bold text-white shadow-lg shadow-indigo-500/20"
        >
          F
        </div>

        <h1 class="text-5xl lg:text-6xl font-bold text-gray-50 tracking-tight mb-4 leading-tight">Create an account, for free.</h1>
        <p class="text-violet-300 opacity-75 text-lg"></p>
      </div>

      <div class="w-full max-w-md bg-slate-900/50 p-8 rounded-3xl border border-violet-900/30 shadow-2xl backdrop-blur-sm" in:fly={{ y: 40, duration: 800, delay: 150 }}>
        <h2 class="text-2xl font-bold text-gray-50 mb-6">Register</h2>

        <form use:form on:submit|preventDefault={onRegister} class="space-y-5">
          <div class="grid grid-cols-2 gap-4">
            <div class="space-y-1.5">
              <label for="firstName" class="block text-sm font-medium text-slate-400">First Name</label>
              <input
                type="text"
                name="firstName"
                id="firstName"
                use:validators={[required]}
                class="w-full bg-slate-950 border border-white/10 rounded-xl px-4 py-3 text-gray-50 focus:ring-2 focus:ring-fuchsia-500 focus:border-transparent transition-all outline-none placeholder:text-slate-600"
                placeholder="John"
              />
              <div class="text-red-400 text-xs mt-1 font-medium px-1">
                <Hint for="firstName" on="required">Name required</Hint>
              </div>
            </div>

            <div class="space-y-1.5">
              <label for="lastName" class="block text-sm font-medium text-slate-400">Last Name</label>
              <input
                type="text"
                name="lastName"
                id="lastName"
                use:validators={[required]}
                class="w-full bg-slate-950 border border-white/10 rounded-xl px-4 py-3 text-gray-50 focus:ring-2 focus:ring-fuchsia-500 focus:border-transparent transition-all outline-none placeholder:text-slate-600"
                placeholder="Doe"
              />
              <div class="text-red-400 text-xs mt-1 font-medium px-1">
                <Hint for="lastName" on="required">Name required</Hint>
              </div>
            </div>
          </div>

          <div class="space-y-1.5">
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
              <div class="text-red-400 text-xs mt-1 font-medium px-1">
                <Hint on="required">Email is required</Hint>
                <Hint on="email" hideWhenRequired>Please enter a valid email</Hint>
              </div>
            </HintGroup>
          </div>

          <div class="space-y-1.5">
            <label for="password" class="block text-sm font-medium text-slate-400">Password</label>
            <input
              type="password"
              name="password"
              id="password"
              use:validators={[required]}
              class="w-full bg-slate-950 border border-white/10 rounded-xl px-4 py-3 text-gray-50 focus:ring-2 focus:ring-fuchsia-500 focus:border-transparent transition-all outline-none placeholder:text-slate-600"
              placeholder="••••••••"
            />
            <div class="text-red-400 text-xs mt-1 font-medium px-1">
              <Hint for="password" on="required">Password is required</Hint>
            </div>
          </div>

          <button
            type="submit"
            disabled={!$form.valid}
            class="w-full py-3.5 bg-linear-to-r from-indigo-600 to-fuchsia-600 hover:from-indigo-500 hover:to-fuchsia-500 text-white rounded-xl font-semibold shadow-lg shadow-fuchsia-900/20 transition-all disabled:opacity-50 disabled:cursor-not-allowed mt-4"
          >
            Create Account
          </button>
        </form>

        <div class="mt-6 text-center">
          <a href="/login" class="text-sm text-slate-400 hover:text-fuchsia-400 transition-colors">
            Already have an account? <span class="font-semibold underline underline-offset-4">Sign in</span>
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
