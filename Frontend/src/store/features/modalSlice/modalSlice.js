import { createSlice } from "@reduxjs/toolkit";

const initialState = false;

const modalSlice = createSlice({
	name: "modal",
	initialState,
	reducers: {
		openModal: (state) => (state = true),
		closeModal: (state) => (state = false),
	},
});

export const modalSliceState = (state) => state.modal;
export const { openModal, closeModal } = modalSlice.actions;

export default modalSlice.reducer;
