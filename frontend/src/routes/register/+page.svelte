<script>
  import { useForm, validators, HintGroup, Hint, email, required } from "svelte-use-form";
  import { onMount } from 'svelte';
  import { fade, fly, slide } from "svelte/transition";
    import { goto } from "$app/navigation";

  const form = useForm();

  //for some reason the whole form bugs out when trying to do this for anims
  //im sorry kirile i am too retarded and lazy to fix ts

  let ready = true;
  onMount(() => ready = true);

  async function onRegister() {

    const formData = $form.values; 
    console.log($form.values);

    const response = await fetch('http://10.157.70.56:5096/api/auth/register', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(formData)
    });


    if (response.ok) {
      goto('/login')
    } else {
      alert("Error! Couldn't register.")
    }
  }
</script>

<form use:form on:submit={onRegister}
  <div class="flex flex-auto flex-wrap h-180 bg-linear-to-bl from-slate-950 to-violet-500 pb-8 gap-4 content-center place-content-end pr-100">
    {#if ready}

      <div class="text-left text-gray-50 text-6xl font-bold pt-50 mr-50" transition:fade={{duration:1000}}>
        Make your account <br> today, for free!
      </div>

      <div class="rounded-4xl flex-col flex flex-wrap w-100 h-160 bg-gray-50 pb-4 gap-2 content-center place-content-center"
      transition:fly={{y:100, duration:600}}>
        <div class="text-4xl pb-4 text-center">
          Register
        </div>

            <!--   REGISTER VALUES   -->

        <hr class="gray-200">
        <h1 class="text-2xl text-gray-500">First Name:</h1>
          
        <input type="firstName" name="firstName" use:validators={[required]}
        class="w-75 h-10 opacity-75 rounded-sm border border-black bg-gray-50 hover:bg-gray-200 text-1xl text-gray-950 font-medium pl-2"/>
        <Hint for="firstName" on="required">Name required</Hint>

        <h1 class="text-2xl text-gray-500">Last Name:</h1>
          
        <input type="lastName" name="lastName" use:validators={[required]}
        class="w-75 h-10 opacity-75 rounded-sm border border-black bg-gray-50 hover:bg-gray-200 text-1xl text-gray-950 font-medium pl-2"/>
        <Hint for="lastName" on="required">Name required</Hint>

        <h1 class="text-2xl text-gray-500">Email:</h1>

        <input type="email" name="email" use:validators={[required, email]}
        class="w-75 h-10 opacity-75 rounded-sm border border-black bg-gray-50 hover:bg-gray-200 text-1xl text-gray-950 font-medium pl-2"/>

        <HintGroup for="email">
          <Hint on="required">This is a mandatory field</Hint>
          <Hint on="email" hideWhenRequired>Email is not valid</Hint>
        </HintGroup>

        <h1 class="text-2xl text-gray-500">Password:</h1>

        <input type="password" name="password" use:validators={[required]}
        class="w-75 h-10 opacity-75 rounded-sm border border-black bg-gray-50 hover:bg-gray-200 text-1xl text-gray-950 font-medium pl-2"/>
        <Hint for="password" on="required">This is a mandatory field</Hint>

        
        <div class="pt-10 pl-13">
          <button disabled={!$form.valid}
          class="w-50 h-20 opacity-75 rounded-2xl bg-indigo-700 hover:bg-fuchsia-700 hover:underline text-3xl font-medium text-gray-50">Register</button>
        </div>
      </div>
    {/if}
</form>

<style>
	:global(.touched:invalid) {
		border-color: red;
		outline-color: red;
	}
</style>