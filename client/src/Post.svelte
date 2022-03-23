<script>
    export let timestamp;
    export let name;
    export let content;
</script>

<div class="post box mt-4">
    <h1 class="label">Post from {name}</h1>
    <p class="">{content}</p>
    <p class="help">{new Date(timestamp * 1000).toLocaleString()}</p>
</div>

<style>
    .post {
        width: 50%;
        margin: 0 auto;
    }
</style>
