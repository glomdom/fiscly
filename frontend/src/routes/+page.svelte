<script>
    import { onMount } from 'svelte';
    import { fade, fly, slide } from "svelte/transition";

    let ready = false;
     onMount(() => ready = true);
</script>
<style>
    :global(body){
        background-color: white;
    }
</style>

<div class="flex flex-auto flex-wrap h-180 bg-linear-to-bl from-slate-950 to-violet-500 pb-8">
    {#if ready}

        <div class="flex flex-col self-center w-500 text-center text-gray-50 text-9xl font-bold p-30 gap-y-10 place-items-center"
        transition:fly={{y:100, duration:600}}
        >MONEYTRACKER
            <hr class="w-300 border-2 border-violet-300">

            <div class="text-center text-gray-50 text-5xl font-light opacity-75">
                Take control of your finances today with our amazing services.<br>
                Track your earnings and costs with one click!
            </div>

            <button class="w-100 h-30 opacity-75 rounded-4xl bg-indigo-500 hover:bg-fuchsia-500 hover:underline text-5xl font-medium" transition:slide>
                <a href="/login">Start here →</a>
            </button>
        </div>
    {/if}
</div>

<div class="flex flex-auto flex-wrap h-500 order-last mx-auto"></div>

<div class="flex flex-auto flex-wrap w-full h-20 bg-gray-950 text-gray-500 align-text-bottom order-last relative bottom-0">Copyright © 2026</div>
