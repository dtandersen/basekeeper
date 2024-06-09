<script>
	/** @type {import('./$types').PageData} */
	export let data;
</script>

<h1>Inventory</h1>

<table class="table table-zebra table-md">
	<thead>
		<tr>
			<th>Item</th>
			<th>On Hand</th>
			<th>Available</th>
		</tr>
	</thead>
	<tbody>
		{#each data.items as item}
			<tr>
				<td>{item.item}</td>
				<td>{item.quantity}</td>
				<td>{item.available}</td>
			</tr>
		{/each}
	</tbody>
</table>
