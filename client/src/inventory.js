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

const addItem = (/** @type {any} */ itemtoadd) => {
    let url = `http://localhost:5069/api/inventory`;
    console.log("adding " + JSON.stringify(itemtoadd));
    fetch(url, {
        body: JSON.stringify(itemtoadd),
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        }
    });
    update((items) => {

        // items.push(itemtoadd);
        return items;
    })
};

function reload2() {
    console.log("reloading");
    const res = fetch(`http://localhost:5069/api/inventory`)
        .then((res) => res.json())
        .then((res) => {
            // This is the JSON from our server.  It should contain an array of items.
            console.log("loading result=" + JSON.stringify(res));
            const items2 = res;//.json();
            update((items) => {
                items = items2
                return items;
            })
        });

}

const reload = () => {
    reload2();
};

export default {
    subscribe,
    removeItem,
    addItem,
    reload,
    set
};