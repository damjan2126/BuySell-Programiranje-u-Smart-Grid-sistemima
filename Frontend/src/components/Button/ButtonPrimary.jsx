import React from "react";
import { ButtonPrimary as ButtonP } from "./Button.styled";
import Loader from "../Loader/Loader";

const ButtonPrimary = ({ onClick = () => void 0, hasAuth, loading, canShrink, children, colorDif, ...rest }) => {
   const auth = false;

   return (
      <ButtonP
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
      </ButtonP>
   );
};

export default ButtonPrimary;
