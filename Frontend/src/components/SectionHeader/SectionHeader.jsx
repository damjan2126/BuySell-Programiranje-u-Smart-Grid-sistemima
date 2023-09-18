import React from "react";
import { SectionHeaderContainer, SectionHeaderLeft, SectionHeaderRight } from "./SectionHeader.styled";

const SectionHeader = ({ title, willPersist, leftExists, children }) => {
   return (
      <SectionHeaderContainer>
         <SectionHeaderLeft $leftExists={leftExists}>{title}</SectionHeaderLeft>
         <SectionHeaderRight $willPersist={willPersist} $leftExists={leftExists}>
            {children}
         </SectionHeaderRight>
      </SectionHeaderContainer>
   );
};

export default SectionHeader;
