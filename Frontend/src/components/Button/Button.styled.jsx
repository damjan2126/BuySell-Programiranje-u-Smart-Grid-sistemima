import styled from "styled-components";

export const Button = styled.button`
   background-color: ${(props) => props.theme.buttonTertiary};
   padding: 4px 8px;
   color: white;
   border-radius: 4px;
   border: none;
   font-size: 16px;
   display: flex;
   min-width: ${(props) => (props.$canShrink ? "auto" : "120px")};
   height: 35px;
   justify-content: center;
   align-items: center;
   gap: 8px;
   flex-shrink: 0;
   box-shadow: 0px 1px 10px 0px rgba(220, 87, 24, 0.5);

   position: relative;

   cursor: pointer;
   transition: 200ms;

   &:hover {
      scale: 0.98;
   }

   &:disabled {
      opacity: 0.8;
      box-shadow: none;
      cursor: not-allowed;
   }
`;

export const ButtonSecondary = styled(Button)`
   background-color: ${(props) => props.theme.buttonSecondary};
   box-shadow: 0px 1px 10px 0px ${(props) => props.theme.buttonSecondaryBoxShadow};
   color: ${(props) => props.theme.textPrimary};
   border: 1px solid ${(props) => props.theme.buttonSecondaryBorder};
   font-size: ${(props) => (props.$fontSize ? props.$fontSize : "inherit")};
`;

export const ButtonPrimary = styled(Button)`
   background-color: ${(props) => props.theme.buttonPrimary};
   box-shadow: none;
   border: 1px solid ${(props) => props.theme.textPrimary};
   color: white;
`;

export const ThemeButton = styled(Button)`
   margin: 10px;
   width: auto;
   min-width: auto;
   height: auto;
   font-size: 12px;
   box-shadow: none;
   background-color: ${(props) => props.$styles.buttonTertiary};

   color: white;
`;
