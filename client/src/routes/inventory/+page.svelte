<script>
	/** @type {import('./$types').PageData} */
	// export let data;
	import items from '../../inventory.js';
	let count = 0;
	/**
	 * @param {any} item
	 */
	function incrementCount(item) {
		items.removeItem(item);
	}
</script>

<h1>Inventory</h1>

<div class="table-container">
	<table class="table">
		<thead>
			<tr>
				<th>Item</th>
				<th>On Hand</th>
				<th>Available</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			{#each $items as item}
				<tr>
					<td>{item.item}</td>
					<td>{item.quantity}</td>
					<td>{item.available}</td>
					<td>
						<button
							on:click={() => incrementCount(item)}
							type="button"
							class="btn btn-sm variant-filled">Delete</button
						>
					</td>
				</tr>
			{/each}
		</tbody>
	</table>
</div>
