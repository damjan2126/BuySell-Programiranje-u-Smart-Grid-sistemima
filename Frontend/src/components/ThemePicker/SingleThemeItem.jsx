import React, { memo, useState } from "react";
import { ThemeHeader, ThemeText, SideColor } from "./ThemePicker.styled";
import ThemeButton from "../Button/ThemeButton";

const SingleThemeItem = (props) => {
   const [loading, setLoading] = useState(false);
   const toggleLoading = () => setLoading(!loading);

   return (
      <>
         <ThemeHeader $styles={props.value}>Header</ThemeHeader>
         <ThemeText $styles={props.value}>
            Example text
            <SideColor $styles={props.value} />
         </ThemeText>
         <ThemeButton
            colorDif
            loading={loading}
            onClick={(e) => {
               e.stopPropagation();
               toggleLoading();
            }}
            $styles={props.value}
         >
            Button
         </ThemeButton>
      </>
   );
};

export default memo(SingleThemeItem);
