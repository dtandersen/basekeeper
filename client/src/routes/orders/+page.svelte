<script lang="ts">
	import { inventoryStore } from '$lib/store/orders';
</script>

<h1>Orders</h1>

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
			{#each $inventoryStore as item}
				<tr class="bg-slate-500 x">
					<td>{item.item}</td>
					<td>{item.quantity}</td>
					<td><button class="btn btn-sm variant-filled">Delete</button></td>
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
