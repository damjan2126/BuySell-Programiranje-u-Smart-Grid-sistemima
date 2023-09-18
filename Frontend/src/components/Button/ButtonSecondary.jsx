import React from "react";
import { ButtonSecondary as ButtonSec } from "./Button.styled";
import Loader from "../Loader/Loader";

const ButtonSecondary = ({
   onClick = () => void 0,
   hasAuth,
   loading,
   fontSize = "16px",
   canShrink,
   children,
   colorDif,
   ...rest
}) => {
   const auth = false;

   return (
      <ButtonSec
         $canShrink={canShrink}
         $fontSize={fontSize}
         disabled={loading}
         onClick={() => {
            if (!hasAuth) return onClick();
            if (!auth) return console.log("Error: Not Authenticated");
            onClick();
         }}
         {...rest}
      >
         {loading ? <Loader size={10} color={colorDif} /> : children}
      </ButtonSec>
   );
};
export default ButtonSecondary;
