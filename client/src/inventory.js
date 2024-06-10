import { writable } from "svelte/store";

const { subscribe, set, update } = writable([]);

const removeItem = (/** @type {any} */ itemtoremove) =>
    update((items) => {
        let url = `http://localhost:5069/api/inventory/${itemtoremove.item}`;
        console.log("deleting " + url);
        fetch(url, {
            method: "DELETE"
        });
        return items.filter((item) => item !== itemtoremove);
    });

export default {
    subscribe,
    removeItem,
    set
};