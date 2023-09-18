import React from "react";
import { LoginForm, LoginFormHeader, LoginButtonsContainer } from "./Form.styled";
import { useTheme } from "styled-components";

const Form = ({ children, header, text, nav, button, onSubmit, ...rest }) => {
   const theme = useTheme();
   return (
      <LoginForm onSubmit={onSubmit} {...rest}>
         <LoginFormHeader>{header}</LoginFormHeader>
         {children}
         <LoginButtonsContainer>{button}</LoginButtonsContainer>
         {nav && (
            <div style={{ textAlign: "center", marginTop: "10px", fontSize: "10px", color: theme.textPrimary }}>or</div>
         )}
         <div
            style={{
               textAlign: "center",
               textDecoration: "underline",
               marginTop: "10px",
               fontSize: "10px",
               color: theme.buttonTertiary,
               cursor: "pointer",
            }}
         >
            {nav}
         </div>
      </LoginForm>
   );
};

export default Form;
