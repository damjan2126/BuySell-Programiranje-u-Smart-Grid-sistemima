import { createSlice } from "@reduxjs/toolkit";
import WriteToLocalStorage from "../../../utils/WriteToLocalStorage";
import Theme from "../../../themes/Theme";

let allThemes = [];
for (let i in Theme) {
   allThemes.push(i);
}

const storageTheme = localStorage.PRTL_theme;
if (storageTheme && !allThemes.includes(localStorage.PRTL_theme)) {
   WriteToLocalStorage("PRTL_theme", "Black");
}
const initialState = storageTheme ? storageTheme : allThemes[1];

const themeSlice = createSlice({
   name: "theme",
   initialState,
   reducers: {
      changeTheme: (state, action) => {
         WriteToLocalStorage("PRTL_theme", action.payload);
         return (state = action.payload);
      },
   },
});

export const themeSliceState = (state) => state.theme;
export const { changeTheme } = themeSlice.actions;

export default themeSlice.reducer;
