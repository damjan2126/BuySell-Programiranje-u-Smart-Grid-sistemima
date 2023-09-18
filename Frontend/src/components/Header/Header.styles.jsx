import styled from "styled-components";

export const Header = styled.header`
   background-color: ${(props) => props.theme.header};
   color: ${(props) => props.theme.textPrimary};

   display: flex;
   justify-content: space-between;
   align-items: center;

   flex-wrap: wrap;

   padding: 0px 32px;
   height: 68px;

   @media only screen and (max-width: 500px) {
      padding: 0px 10px;
   }
`;

export const HeaderLeft = styled.header`
   display: flex;
   justify-content: space-between;
   align-items: center;
   white-space: nowrap;
   flex-shrink: 1;
   font-weight: bold;
`;

export const HeaderRight = styled.header`
   display: flex;
   align-items: center;
`;

export const UserText = styled.div`
   @media only screen and (max-width: 500px) {
      display: none;
   }
`;

export const UserIconHolder = styled.div`
   display: flex;
   align-items: center;
   justify-content: center;

   margin: 0px 20px;
   @media only screen and (max-width: 500px) {
      margin: 0px;
   }
`;

export const UserIcon = styled.div`
   display: flex;
   align-items: center;
   justify-content: center;
   padding: 18px 14px;
   background-color: #232323;
   height: 20px;
   border-radius: 7px;
   margin-left: 10px;

   @media only screen and (min-width: 500px) {
      display: none;
   }
`;

export const DesktopLogo = styled.div`
   display: none;
   align-items: center;
   justify-content: center;
   @media only screen and (min-width: 500px) {
      display: flex;
   }
`;

export const MobileLogo = styled.div`
   display: inline-block;
   display: flex;
   align-items: center;
   justify-content: center;
   @media only screen and (min-width: 500px) {
      display: none;
   }
`;
