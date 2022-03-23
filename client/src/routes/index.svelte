<script>
	import Post from "../lib/post.svelte";
	let inputTitle;
	let inputText;
	let loading = false;
	const API_URL = "https://localhost:7160/";
	let form;
	let posts = [];
	function submitForm() {
		loading = true;
		console.log(inputTitle);
		console.log(inputText);
		const payload = {
			Name: inputTitle,
			Content: inputText,
		};
		fetch(API_URL, {
			method: "POST",
			mode: "cors",
			body: JSON.stringify(payload),
			headers: {
				"content-type": "application/json",
			},
		}).then(() => {
			loading = false;
			form.reset();
			getPosts();
		});
	}
	function getPosts() {
		fetch(API_URL + "posts")
			.then((res) => res.json())
			.then((res) => {
				console.log(res);
				posts = res;
			});
	}
	getPosts();
</script>

<h1 class="title has-text-centered mt-5 is-1">Kvidrer 🐦</h1>

<form action="" class="my-form is-expanded" bind:this={form}>
	<div class="field">
		<label class="label" for="name">Name</label>
		<input
			class="input"
			type="text"
			name="name"
			id="name"
			bind:value={inputTitle}
		/>
	</div>
	<div class="field">
		<label class="label" for="content">Content</label>
		<textarea
			class="textarea"
			name="content"
			id="content"
			bind:value={inputText}
		/>
	</div>
	<div class="field">
		<button
			class="button is-primary is-medium"
			on:click|preventDefault={submitForm}
			class:is-loading={loading}>Kvidr 🐦</button
		>
	</div>
</form>

{#each posts as item, i}
	<Post {...item} />
{/each}

<style>
	.my-form {
		width: 50%;
		margin: 0 auto;
	}
</style>