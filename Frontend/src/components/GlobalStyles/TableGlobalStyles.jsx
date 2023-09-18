import { createGlobalStyle } from "styled-components";

const TableGlobalStyle = createGlobalStyle`
   #marketplace th { 
      border: 0.5px solid ${(props) => props.theme.tableBorder} !important;
      background: ${(props) => props.theme.background} !important;
      font-size: 16px !important;
   }

   #marketplace th span {
	   color: ${(props) => props.theme.textPrimary} !important;
   }

   #marketplace th,  #marketplace td {
      font-family: 'Montserrat', sans-serif !important;
      line-height: 16px !important;
      color: ${(props) => props.theme.textPrimary} !important;
   }
   
   // This is the element in the background when no items show on search.
   [data-viewport-type="element"] {
   	background: ${(props) => props.theme.background} !important;
   }

   #marketplace .sort_cell:hover {
     cursor: pointer;
   }

   #marketplace td {
     font-size: 12px !important;
     line-height: 16px !important;
     color: ${(props) => props.theme.textTertiary} !important;
   }

   #marketplace tr {
   	background: ${(props) => props.theme.background} !important;
      padding:10px 0px
   }

   #marketplace tr:nth-child(2n) {
   	background: ${(props) => props.theme.tableSecondElement} !important;
   }

   #marketplace th span svg {
      fill: ${(props) => props.theme.textSecondary};
   }

`;

export default TableGlobalStyle;
