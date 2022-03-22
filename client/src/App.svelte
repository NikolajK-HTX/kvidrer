<script>
	let inputTitle;
	let inputText;
	let loading = false;
	const API_URL = "https://localhost:7160/";

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
		});
	}
</script>

<main>
	<h1 class="title has-text-centered mt-5 is-1">Kvidrer 🐦</h1>

	<form action="" class="my-form is-expanded">
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
				class="button is-primary"
				on:click|preventDefault={submitForm}
				class:is-loading={loading}>Kvidr 🐦</button
			>
		</div>
	</form>
</main>

<style>
	.my-form {
		width: 50%;
		margin: 0 auto;
	}
</style>
