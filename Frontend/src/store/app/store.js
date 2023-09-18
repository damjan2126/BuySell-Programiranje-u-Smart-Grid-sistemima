import { configureStore } from "@reduxjs/toolkit";
import userSliceReducer from "../features/userSlice/userSlice";
import themeSliceReducer from "../features/themeSlice/themeSlice";
import modalSliceReducer from "../features/modalSlice/modalSlice";
import cartSliceReducer from "../features/cartSlice/cartSlice";
import tokenSliceReducer from "../features/tokenSlice/tokenSlice";

const store = configureStore({
   reducer: {
      user: userSliceReducer,
      theme: themeSliceReducer,
      modal: modalSliceReducer,
      cart: cartSliceReducer,
      token: tokenSliceReducer,
   },
   middleware: (getDefaultMiddleware) =>
      getDefaultMiddleware({
         serializableCheck: {
            ignoredActions: ["cart/toggleItem"],

            ignoredPaths: ["cart.*.item.refetch"],
         },
      }),
});

export default store;
