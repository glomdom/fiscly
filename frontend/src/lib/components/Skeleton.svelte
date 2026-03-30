<script lang="ts">
  let {
    class: className = "",
    type = "block",
  }: {
    class?: string;
    type?: "block" | "circle" | "text";
  } = $props();

  const shapes = {
    block: "rounded-xl",
    text: "rounded",
    circle: "rounded-full",
  };
</script>

<div class="animate-pulse bg-slate-800/50 {shapes[type]} {className}"></div>
