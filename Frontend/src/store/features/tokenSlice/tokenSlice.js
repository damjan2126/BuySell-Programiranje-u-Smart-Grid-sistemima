import { createSlice } from "@reduxjs/toolkit";

const storageToken = localStorage.token;
const initialState = storageToken ? storageToken : null;

const tokenSlice = createSlice({
   name: "token",
   initialState,
   reducers: {
      addToken: (state, action) => {
         return action.payload;
      },
      removeToken: () => {
         return null;
      },
   },
});

export const tokenSliceState = (state) => state.token;
export const { addToken, removeToken } = tokenSlice.actions;

export default tokenSlice.reducer;
