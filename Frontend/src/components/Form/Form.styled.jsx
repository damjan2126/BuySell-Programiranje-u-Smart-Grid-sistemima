import styled from "styled-components";

export const LoginForm = styled.form`
   /* outline: 1px solid red; */
   width: 100%;
   max-width: 100%;
`;

export const LoginFormHeader = styled.div`
   color: ${(props) => props.theme.textPrimary};

   max-width: 100%;
   text-align: center;
   font-size: 32px;
   font-style: normal;
   font-weight: 700;
   line-height: 32.5px;
   margin-bottom: 24px;
`;

export const LoginFormText = styled.div`
   color: ${(props) => props.theme.textQuaternary};
   text-align: center;
   font-size: 18px;
   font-style: normal;
   font-weight: 400;
   line-height: 32.5px;
   margin-bottom: 48px;
`;

export const LoginButtonsContainer = styled.div`
   display: flex;
   justify-content: center;
`;
