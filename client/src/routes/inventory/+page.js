import items from '../../inventory.js';

/** @type {import('./$types').PageLoad} */
export async function load({ fetch, params }) {
    const res = await fetch(`http://localhost:5069/api/inventory`);
    const items2 = await res.json();
    console.log("got " + JSON.stringify(items2));
    items.set(items2);

    // return { items };
}