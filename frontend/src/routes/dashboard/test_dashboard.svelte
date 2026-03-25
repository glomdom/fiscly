<script lang="ts">
  import { onMount } from "svelte";
  let profile: any = {};
  onMount(async () => {
    const res = await fetch("http://10.157.70.56:5096/api/auth/me", {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    });
    profile = await res.json();
  });
</script>

<div
  class="flex flex-auto flex-wrap place-self-center w-lvw h-lvh bg-linear-to-bl from-slate-950 to-violet-500 p-8 content-center place-content-center text-4xl text-gray-950"
>
  <div class=" rounded-4xl bg-gray-50 w-200 h-100 content-center p-10">
    <p class="text-5xl font-medium">{profile.firstName} {profile.lastName}</p>
    <br />
    Email: {profile.email} <br />
    ID: {profile.userId}
  </div>
</div>
