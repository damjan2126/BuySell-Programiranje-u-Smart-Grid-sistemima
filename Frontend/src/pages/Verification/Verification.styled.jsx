import styled from "styled-components";

export const VerificationContainer = styled.div`
   max-width: 100%;
   width: 900px;

   margin: 20px auto;
`;

export const VerificationItem = styled.div`
   background-color: ${(props) => props.theme.background};
   color: ${(props) => props.theme.textPrimary};
   padding: 10px;
   margin: 10px 5px;

   display: flex;
   justify-content: space-between;
   align-items: center;
   border-radius: 5px;
`;

export const VerificationItemLeft = styled.div``;
export const VerificationItemRight = styled.div`
   display: flex;
`;
