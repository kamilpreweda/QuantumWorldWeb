import { createReducer, on } from "@ngrx/store";
import { decrement, increment, reset } from "./app.actions";
import { initialState } from "./app.state";

const _appReducer = createReducer(initialState, on(increment, (state) => {
    return {
        ...state,
        carbonFiberResource: state.carbonFiberResource + 1,
    };
}), on(decrement, (state) => {
    return {
        ...state,
        carbonFiberResource: state.carbonFiberResource - 1,
    };
}),
    on(reset, (state) => {
        return {
            ...state,
            carbonFiberResource: 0
        };
    }));

export function appReducer(state: any, action: any) {
    return _appReducer(state, action);
}