<script>
	/** @type {import('./$types').PageData} */
	export let data;
</script>

<h1>Inventory</h1>
<div class="table-container">
	<table class="table">
		<thead>
			<tr>
				<th>Item</th>
				<th>On Hand</th>
			</tr>
		</thead>
		<tbody>
			{#each data.items as item}
				<tr class="bg-slate-400">
					<td>{item.item}</td>
					<td>{item.quantity}</td>
				</tr>
				{#each item.components as component}
					<tr>
						<td>{component.item}</td>
						<td>{component.quantity}</td>
					</tr>{/each}
			{/each}
		</tbody>
	</table>
</div>
