import React from "react";
import { LandingPageHolder, LandingPageLeft, LandingPageRight, LandingPageFormHolder } from "./LandingPage.styled";

const LandingPage = ({ children }) => {
   return (
      <LandingPageHolder>
         <LandingPageLeft>DAJMAN APP</LandingPageLeft>
         <LandingPageRight>
            <LandingPageFormHolder>{children}</LandingPageFormHolder>
         </LandingPageRight>
      </LandingPageHolder>
   );
};

export default LandingPage;
