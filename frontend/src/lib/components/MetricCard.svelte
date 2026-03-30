<script lang="ts">
  import { Tween } from "svelte/motion";
  import { cubicOut } from "svelte/easing";

  const themes = {
    emerald: { hover: "hover:border-emerald-500/30", blob: "bg-emerald-500/10 group-hover:bg-emerald-500/20", icon: "text-emerald-400" },
    red: { hover: "hover:border-red-500/30", blob: "bg-red-500/10 group-hover:bg-red-500/20", icon: "text-red-400" },
    indigo: { hover: "hover:border-indigo-500/30", blob: "bg-indigo-500/10 group-hover:bg-indigo-500/20", icon: "text-indigo-400" },
    fuchsia: { hover: "hover:border-fuchsia-500/30", blob: "bg-fuchsia-500/10 group-hover:bg-fuchsia-500/20", icon: "text-fuchsia-400" },
  };

  type ThemeColor = keyof typeof themes;

  let {
    title,
    amount,
    Icon,
    theme = "emerald",
  }: {
    title: string;
    amount: number;
    Icon: any;
    theme?: ThemeColor;
  } = $props();

  let t = $derived(themes[theme]);
  const animatedAmount = new Tween(0, { duration: 1200, easing: cubicOut });

  $effect(() => {
    animatedAmount.target = amount;
  });
</script>

<div class="bg-slate-900/60 rounded-3xl p-6 border border-white/5 shadow-sm flex flex-col justify-center relative overflow-hidden group transition-colors {t.hover}">
  <div class="absolute -right-4 -top-4 w-20 h-20 rounded-full blur-2xl transition-colors {t.blob}"></div>
  <div class="flex items-center gap-2 mb-2">
    <Icon class={t.icon} size={20} />

    <p class="text-sm font-medium text-slate-400">{title}</p>
  </div>

  <p class="text-2xl font-mono text-gray-200">
    ${animatedAmount.current.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
  </p>
</div>
