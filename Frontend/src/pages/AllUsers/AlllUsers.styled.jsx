import styled from "styled-components";

export const AllUsersContainer = styled.div`
   width: 800px;
   max-width: 100%;
   margin: 0 auto;
   padding: 10px;
`;

export const SingleUsersContainer = styled.div`
   padding: 20px 10px;
   margin: 20px;
   background-color: ${(props) => props.theme.background};
   transition: 200ms;
   border: 1px solid transparent;
   border-radius: 5px;

   .id-user {
      transform: 200ms;
   }

   &:hover .id-user {
      color: ${(props) => props.theme.buttonTertiary};
   }

   &:hover {
      border: 1px solid ${(props) => props.theme.buttonTertiary};
      scale: 1.01;
      cursor: pointer;
   }
`;
