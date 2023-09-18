import { createSlice } from "@reduxjs/toolkit";
import WriteToLocalStorage from "../../../utils/WriteToLocalStorage";

const lsCart = localStorage.app_cart;
const initialState = lsCart ? JSON.parse(lsCart) : [];

const cartSlice = createSlice({
   name: "cart",
   initialState,
   reducers: {
      toggleItem: (state, action) => {
         const itemExists = state.find((el) => el.item.id === action.payload.id);
         if (itemExists) {
            const newItems = state.map((el) => {
               if (el.item.id === action.payload.id) {
                  return { ...el, quantity: el.quantity + 1 };
               } else {
                  return el;
               }
            });
            WriteToLocalStorage("app_cart", JSON.stringify(newItems));
            return newItems;
         }
         WriteToLocalStorage("app_cart", JSON.stringify([...state, { quantity: 1, item: action.payload }]));
         return [...state, { quantity: 1, item: action.payload }];
      },
      removeCartItems: () => {
         localStorage.removeItem("app_cart");
         return [];
      },
      quantityPlus: (state, action) => {
         const newItems = state.map((el) => {
            if (el.item.id === action.payload.item.id) {
               return { ...el, quantity: el.quantity + 1 };
            } else {
               return el;
            }
         });
         WriteToLocalStorage("app_cart", JSON.stringify(newItems));
         return newItems;
      },
      quantityMinus: (state, action) => {
         if (action.payload.quantity === 1) {
            const updatedItems = state.filter((el) => el.item.id !== action.payload.item.id);
            WriteToLocalStorage("app_cart", JSON.stringify(updatedItems));
            return updatedItems;
         }
         const newItems = state.map((el) => {
            if (el.item.id === action.payload.item.id) {
               return { ...el, quantity: el.quantity - 1 };
            } else {
               return el;
            }
         });
         WriteToLocalStorage("app_cart", JSON.stringify(newItems));
         return newItems;
      },
   },
});

export const cartSliceState = (state) => state.cart;
export const { toggleItem, quantityPlus, quantityMinus, removeCartItems } = cartSlice.actions;

export default cartSlice.reducer;
