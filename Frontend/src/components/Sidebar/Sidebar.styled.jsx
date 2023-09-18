import styled from "styled-components";

export const SidebarBakcdrop = styled.div`
   inset: 0;
   position: fixed;
   backdrop-filter: blur(3px);
   animation: fadeIn 300ms linear;
   display: flex;
   flex-direction: row;
   z-index: 999999;
`;

export const SidebarS = styled.div`
   background-color: ${(props) => props.theme.background};
   height: 100%;
   display: inline-block;
   max-width: 100%;
   box-shadow: 1px 20px 25px 0px black;
   animation: open 300ms linear backwards;
   position: relative;
   z-index: 999999;
`;

export const Header = styled.div`
   border-bottom: 1px solid ${(props) => props.theme.body};
   padding: 20px 10px;
   gap: 10px;
   display: flex;
   justify-content: space-between;
`;

export const SidebarLeft = styled.div`
   color: ${(props) => props.theme.textQuaternary};
   max-width: 100%;
   text-overflow: ellipsis;
   overflow: hidden;
`;

export const SidebarRight = styled.div`
   display: flex;
   align-items: center;
   justify-content: center;
   max-width: 100%;
   text-overflow: ellipsis;
   overflow: hidden;
`;
export const SidebarContent = styled.div`
   max-width: 100%;
   height: calc(100% - 60px);
   overflow: auto;
   padding: 10px;
`;

export const OpenButton = styled.span``;

export const CloseButton = styled.span`
   display: flex;
   align-items: center;
   justify-content: center;

   & svg {
      fill: ${(props) => props.theme.textSecondary};
      transition: 300ms;
      scale: 1.2;
   }

   & svg:hover {
      scale: 1.1;
      fill: ${(props) => props.theme.buttonTertiary};
   }
`;
