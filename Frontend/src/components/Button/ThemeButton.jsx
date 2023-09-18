import React from "react";
import { ThemeButton as TButton } from "./Button.styled";
import Loader from "../Loader/Loader";

const ThemeButton = ({ onClick = () => void 0, hasAuth, loading, canShrink, children, colorDif, ...rest }) => {
   const auth = false;

   return (
      <TButton
         $canShrink={canShrink}
         onClick={(e) => {
            if (!hasAuth) return onClick(e);
            if (!auth) return console.log("Error: Not Authenticated");
            onClick(e);
         }}
         {...rest}
      >
         {loading ? <Loader colorDif={colorDif} /> : children}
      </TButton>
   );
};

export default ThemeButton;
