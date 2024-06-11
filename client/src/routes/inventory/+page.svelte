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

	const handleSubmit = (e) => {
		// getting the action url
		console.log('submutting form');
		const ACTION_URL = e.target.action;

		// get the form fields data and convert it to URLSearchParams
		const formData = new FormData(e.target);
		let item = { item: formData.get('item'), quantity: formData.get('quantity') };
		items.addItem(item);
		e.target.reset();
		setTimeout(items.reload, 1000);
	};
</script>

<h1>Inventory</h1>

<form on:submit|preventDefault={handleSubmit}>
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
				<tr>
					<td><input class="input" type="text" name="item" value="" /></td>
					<td><input class="input" type="text" name="quantity" value="" /></td>
					<td></td>
					<td>
						<button type="submit" class="btn btn-sm variant-filled">Add</button>
					</td></tr
				>
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
</form>
