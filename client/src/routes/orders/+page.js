import { inventoryStore } from '$lib/store/orders';


/** @type {import('./$types').PageLoad} */
export async function load({ fetch, params }) {
    const res = await fetch(`http://localhost:5069/api/orders`);
    const items = await res.json();
    inventoryStore.set(items);
    // return { items };
}