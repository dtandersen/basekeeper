import { type Readable, writable, type Writable } from "svelte/store"

type LineItem = { item: string, quantity: number }
type OrderItem = { item: string, quantity: number, components: LineItem[] }

// type State = { username: string scores: number }

const initialState: OrderItem[] = []

export type MyStore = Writable<OrderItem[]>

function createStore(): MyStore {
    const { subscribe, set, update } = writable<OrderItem[]>(initialState)

    return { subscribe, set, update }
}

export const inventoryStore = createStore()