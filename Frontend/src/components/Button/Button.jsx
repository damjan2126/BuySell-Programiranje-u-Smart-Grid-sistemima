import React from "react";
import { Button as ButtonStyle } from "./Button.styled";
import Loader from "../Loader/Loader";

const Button = ({ onClick = () => void 0, hasAuth, loading, canShrink, children, colorDif, ...rest }) => {
   const auth = false;

   return (
      <ButtonStyle
         $canShrink={canShrink}
         disabled={loading}
         onClick={() => {
            if (!hasAuth) return onClick();
            if (!auth) return console.log("Error: Not Authenticated");
            onClick();
         }}
         {...rest}
      >
         {loading ? <Loader size={10} color={colorDif} /> : children}
      </ButtonStyle>
   );
};

export default Button;
