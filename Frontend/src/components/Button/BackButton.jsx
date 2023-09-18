import React from "react";
import { Button as ButtonStyle } from "./Button.styled";
import { useLocation, useNavigate } from "react-router-dom";

const BackButton = ({ hasAuth, children, ...rest }) => {
   const auth = false;

   const location = useLocation();
   const navigate = useNavigate();
   if (location.pathname === "/") return;
   return (
      <ButtonStyle
         $canShrink={true}
         style={{ marginRight: "10px" }}
         onClick={() => {
            if (!hasAuth) return navigate("/");
            if (!auth) return console.log("Error: Not Authenticated");
            navigate("/");
         }}
         {...rest}
      >
         {children}
      </ButtonStyle>
   );
};

export default BackButton;
