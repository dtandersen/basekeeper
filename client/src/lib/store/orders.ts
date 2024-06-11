import { type Readable, writable, type Writable } from "svelte/store"

const initialState: OrderItem[] = []

export type MyStore = Writable<OrderItem[]>

function createStore(): MyStore {
    const { subscribe, set, update } = writable<OrderItem[]>(initialState)

    return { subscribe, set, update }
}

export const inventoryStore = createStore()