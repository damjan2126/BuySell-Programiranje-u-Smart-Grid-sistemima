import { createGlobalStyle } from "styled-components";

const ScrollGlobalStyle = createGlobalStyle`
   ::-webkit-scrollbar {
      width: 4px;
      height: 4px;          
   }

   ::-webkit-scrollbar-track {
      background: ${(props) => props.theme.background} !important;
   }

   ::-webkit-scrollbar-thumb {
      background: #888;
      border-radius:8px;
   }

   ::-webkit-scrollbar-thumb:hover {
      background: #555;
   }
`;

export default ScrollGlobalStyle;
