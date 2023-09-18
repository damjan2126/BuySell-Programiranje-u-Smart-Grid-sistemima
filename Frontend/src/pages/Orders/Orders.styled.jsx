import styled from "styled-components";

export const OrderContainer = styled.main`
   width: 900px;
   padding: 10px;
   margin: 20px auto;
   max-width: 100%;
`;

export const OrderContent = styled.main`
   background-color: ${(props) => props.theme.background};
   padding: 10px;
   margin-bottom: 20px;
   border-radius: 7px;
`;

export const DeleteTrash = styled.div`
   position: absolute;
   top: 20px;
   right: 20px;
   transition: 200ms;

   :hover {
      fill: red;
   }
`;
