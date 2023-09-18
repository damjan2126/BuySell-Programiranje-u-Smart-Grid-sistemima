import React, { useState } from "react";
import Button from "../Button/Button";
import { AiOutlineCloseSquare } from "react-icons/ai";
import {
   SidebarBakcdrop,
   SidebarS,
   Header,
   SidebarLeft,
   SidebarRight,
   SidebarContent,
   CloseButton,
} from "./Sidebar.styled";

const Sidebar = ({ header, openButton, closeButton, children }) => {
   const [active, setActive] = useState(false);

   const show = () => {
      document.body.style.overflow = "hidden";
      setActive(true);
   };
   const hide = () => {
      setActive(false);
      document.body.style.overflow = "auto";
   };
   const stopPropagation = (e) => e.stopPropagation();

   return (
      <>
         <span onClick={show}>{openButton ? openButton : <Button>Open</Button>}</span>
         {active && (
            <SidebarBakcdrop onClick={hide}>
               <SidebarS onClick={stopPropagation}>
                  <Header>
                     <SidebarLeft>{header || "Default header"}</SidebarLeft>
                     <SidebarRight>
                        <CloseButton onClick={hide}>
                           {closeButton ? closeButton : <AiOutlineCloseSquare>Close</AiOutlineCloseSquare>}
                        </CloseButton>
                     </SidebarRight>
                  </Header>
                  <SidebarContent>{children}</SidebarContent>
               </SidebarS>
            </SidebarBakcdrop>
         )}
      </>
   );
};

export default Sidebar;
