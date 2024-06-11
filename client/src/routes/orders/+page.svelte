<script lang="ts">
	import { inventoryStore } from '$lib/store/orders';

	const reload = async () => {
		console.log('reload orders');
		let response = await fetch('http://localhost:5069/api/orders');
		let itemsjson = (await response.json()) as OrderItem[];
		inventoryStore.set(itemsjson);
	};

	const handleSubmit = async (e) => {
		// getting the action url
		console.log('submutting form');
		const ACTION_URL = e.target.action;

		// get the form fields data and convert it to URLSearchParams
		const formData = new FormData(e.target);
		let item = { item: formData.get('item'), quantity: formData.get('quantity') };
		await fetch('http://localhost:5069/api/orders', {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(item)
		});
		// let itemsjson = (await response.json()) as OrderItem[];
		// inventoryStore.set(itemsjson);
		// items.addItem(item);
		e.target.reset();
		setTimeout(reload, 1000);
	};

	const deleteOrder = async (item: string) => {
		// getting the action url
		console.log('deleteOrder form');
		// const ACTION_URL = e.target.action;

		// // get the form fields data and convert it to URLSearchParams
		// const formData = new FormData(e.target);
		// let item = { item: formData.get('item'), quantity: formData.get('quantity') };
		let response = await fetch('http://localhost:5069/api/orders/' + item, {
			method: 'DELETE',
			headers: {
				// 'Content-Type': 'application/json'
			}
		});
		// let itemsjson = (await response.json()) as OrderItem[];
		// inventoryStore.set(itemsjson);
		// items.addItem(item);
		// e.target.reset();
		setTimeout(reload, 1000);
	};
</script>

<h1>Orders</h1>

<form on:submit|preventDefault={handleSubmit}>
	<div class="table-container">
		<table class="table">
			<thead>
				<tr>
					<th>Item</th>
					<th>On Hand</th>
					<th></th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td><input class="input" type="text" name="item" value="" /></td>
					<td><input class="input" type="text" name="quantity" value="" /></td>
					<td>
						<button type="submit" class="btn btn-sm variant-filled">Add</button>
					</td></tr
				>
				{#each $inventoryStore as item}
					<tr class="bg-slate-500 x">
						<td>{item.item}</td>
						<td>{item.quantity}</td>
						<td
							><button
								type="button"
								class="btn btn-sm variant-filled"
								on:click={() => deleteOrder(item.item)}>Delete</button
							></td
						>
					</tr>
					{#each item.components as component}
						<tr>
							<td>{component.item}</td>
							<td>{component.quantity}</td>
							<td></td>
						</tr>
					{/each}
				{/each}
			</tbody>
		</table>
	</div>
</form>
