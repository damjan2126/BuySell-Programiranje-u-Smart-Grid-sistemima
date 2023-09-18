import { createSlice } from "@reduxjs/toolkit";

const storageUser = localStorage.app_user;
const initialState = storageUser ? storageUser : null;

const userSlice = createSlice({
   name: "user",
   initialState,
   reducers: {
      addUser: (state, action) => {
         return (state = action.payload);
      },
      removeUser: () => {
         localStorage.removeItem("token");
         localStorage.removeItem("refreshToken");
         return null;
      },
   },
});

export const userSliceState = (state) => state.user;
export const { addUser, removeUser } = userSlice.actions;

export default userSlice.reducer;
