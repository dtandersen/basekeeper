<script lang="ts">
	import '../app.postcss';
	import { AppBar } from '@skeletonlabs/skeleton';
</script>

<AppBar>
	<svelte:fragment slot="lead"></svelte:fragment>
	BaseKeeper
	<svelte:fragment slot="trail">
		<a href="/inventory">Inventory</a>
		<a href="/orders">Orders</a>
	</svelte:fragment>
</AppBar>

<slot />
