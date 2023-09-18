import styled from "styled-components";

export const Input = styled.input`
   background-color: ${(props) => props.theme.inputColor};
   padding: 10px;
   color: ${(props) => props.theme.textPrimary};
   width: 100%;
   max-width: 100%;
   border-radius: 6px;
   font-size: 14px;
   border: 1px solid ${(props) => (props.$hasError ? props.theme.buttonTertiary : props.theme.inputBorder)};
   background: ${(props) => props.theme.inputBackground};

   &:hover {
      background-color: "red";
   }
`;

export const InputFieldContainer = styled.div`
   width: 100%;
`;

export const InputFieldLabel = styled.div`
   margin-bottom: 10px;
`;

export const InputFieldError = styled.div`
   max-width: 100%;
   font-size: 12px;
   color: ${(props) => props.theme.buttonTertiary};
   min-height: 18.5px;
   margin: 7px 0px 10px 0px;
`;
