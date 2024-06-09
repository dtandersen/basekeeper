/** @type {import('./$types').PageLoad} */
export async function load({ fetch, params }) {
    const res = await fetch(`http://localhost:5069/api/inventory`);
    const items = await res.json();

    return { items };
}