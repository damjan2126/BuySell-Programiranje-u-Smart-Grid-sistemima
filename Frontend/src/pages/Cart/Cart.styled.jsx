import styled from "styled-components";

export const CartContainer = styled.div`
   margin-top: 20px;
   display: flex;
   flex-wrap: wrap;
`;

export const CartSingleItem = styled.div``;

export const SingleItemHeader = styled.div`
   text-align: center;
   font-size: 22px;
   color: ${(props) => props.theme.textPrimary};
   margin-bottom: 15px;

   overflow: hidden;
   white-space: nowrap;
   text-overflow: ellipsis;
`;

export const SingleItemImage = styled.div`
   aspect-ratio: 1/0.6;
   border: 1px solid ${(props) => props.theme.textTertiary};
   border-radius: 5px;
   padding: 10px;
`;
export const SingleItemDescription = styled.div`
   font-size: 13px;
   color: ${(props) => props.theme.textTertiary};
   padding: 10px;

   overflow: hidden;
   white-space: nowrap;
   text-overflow: ellipsis;
`;

export const SingleItemQ = styled.div`
   font-size: 13px;
   color: ${(props) => props.theme.buttonTertiary};
   padding: 10px;

   overflow: hidden;
   white-space: nowrap;
   text-overflow: ellipsis;
`;

export const SingleItemT = styled.div`
   font-size: 13px;
   color: ${(props) => props.theme.textPrimary};

   overflow: hidden;
   white-space: nowrap;
   text-overflow: ellipsis;

   text-align: center;
`;

export const ButtonZMP = styled.div`
   font-size: 13px;
   color: ${(props) => props.theme.textPrimary};
   background-color: ${(props) => props.theme.buttonTertiary};

   overflow: hidden;
   white-space: nowrap;
   text-overflow: ellipsis;
   display: flex;
   align-items: center;
   justify-content: center;
   align-self: stretch;

   flex-grow: 1;
   margin: 5px;
   max-width: 100%;
   border-radius: 5px;
   font-weight: bold;
   cursor: pointer;

   &:active {
      scale: 0.98;
      opacity: 0.5;
   }
`;

export const SingleItempPrice = styled.div`
   text-align: center;
   padding: 10px 30px;
   font-size: 16px;
   background-color: ${(props) => props.theme.buttonTertiary};
   width: 180px;
   max-width: 100%;
   border-radius: 5px;
   font-weight: bold;
   cursor: pointer;

   &:active {
      scale: 0.98;
      opacity: 0.5;
   }
`;

export const SingleItem = styled.div`
   width: calc(20% - 20px);
   margin: 10px;
   padding: 10px;
   border-radius: 5px;
   background-color: ${(props) => props.theme.background};
   transition: 200ms;

   &:hover {
      opacity: 0.9;
      scale: 0.98;
   }

   @media only screen and (max-width: 1400px) {
      width: calc(25% - 20px);
   }

   @media only screen and (max-width: 1200px) {
      width: calc(33.3333% - 20px);
   }

   @media only screen and (max-width: 800px) {
      width: calc(50% - 20px);
   }

   @media only screen and (max-width: 500px) {
      width: calc(100%);
      margin: 10px 0;
   }
`;

export const TotalPrice = styled.div`
   text-align: center;
   padding: 20px;
   color: ${(props) => props.theme.buttonTertiary};
`;

export const TPT = styled.span`
   padding: 0px 5px 0px 0px;
   color: ${(props) => props.theme.textPrimary};
`;
