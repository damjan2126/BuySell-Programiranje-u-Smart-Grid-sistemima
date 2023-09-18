import styled from "styled-components";

export const LandingPageHolder = styled.div`
   min-height: 100vh;
   width: 100%;
   display: flex;
`;

export const LandingPageLeft = styled.div`
   flex: 1;
   display: flex;
   justify-content: center;
   text-align: center;
   align-items: center;
   background-color: ${(props) => props.theme.buttonPrimary};
   font-size: 50px;
   color: ${(props) => props.theme.textPrimary};

   @media only screen and (max-width: 1100px) {
      display: none;
   }
`;

export const LandingPageRight = styled.div`
   flex: 1;
   display: flex;
   justify-content: center;
   align-items: center;
   max-width: 100%;
   background-color: ${(props) => props.theme.background};
`;

export const LandingPageFormHolder = styled.div`
   width: 355px;
   max-width: 100%;
   margin: 10px;
`;
