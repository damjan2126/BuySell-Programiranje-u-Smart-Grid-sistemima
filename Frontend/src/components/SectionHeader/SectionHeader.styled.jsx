import styled from "styled-components";

export const SectionHeaderContainer = styled.div`
   display: flex;
   align-items: center;
   justify-content: space-between;
   padding: 0px 16px;
   background: ${(props) => props.theme.background};
   box-shadow: 0px 0.5px 10px 0px rgba(0, 0, 0, 0.08);
   min-height: 50px;
`;

export const SectionHeaderLeft = styled.div`
   display: flex;
   align-items: center;
   height: 50px;
   font-size: 16px;
   font-style: normal;
   font-weight: 600;
   line-height: 24px; /* 150% */
   letter-spacing: 0.4px;
   text-transform: uppercase;
   color: ${(props) => props.theme.textTertiary};
   display: ${(props) => (props.$leftExists ? "none" : "flex")};
`;

export const SectionHeaderRight = styled.div`
   @media only screen and (max-width: 1350px) {
      display: ${(props) => (props.$willPersist ? "block" : "none")};
   }
`;
