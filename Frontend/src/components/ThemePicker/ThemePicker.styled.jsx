import styled from "styled-components";

export const ThemePickerContainer = styled.div`
   background-color: ${(props) => props.theme.backgroundSections};
   padding: 2px;
   display: flex;
   border-radius: 8px;
   flex-wrap: wrap;
   width: 400px;

   @media only screen and (max-width: 600px) {
      width: 100%;
   }
`;

export const ThemePickerItem = styled.div`
   background-color: ${(props) => props.$styles.background};
   overflow: hidden;
   border-radius: 8px;
   font-size: 12px;
   margin: 5px;
   cursor: pointer;
   border: ${(props) => `2px solid ${props.$styles.header}`};

   position: relative;

   width: calc(50% - 2 * 5px);
   aspect-ratio: 1/1;
`;

export const ThemeHeader = styled.div`
   padding: 5%;
   height: 20%;

   display: flex;
   align-items: center;

   color: white;
   background-color: ${(props) => props.$styles.header};
`;

export const ThemeText = styled.div`
   padding: 5%;
   margin-top: 10px;
   color: ${(props) => props.$styles.textQuaternary};
   display: flex;
   align-items: center;
   justify-content: space-between;
`;

export const PickedText = styled.div`
   width: 100%;
   text-align: center;
   position: absolute;
   left: 50%;
   transform: translate(-50%);
   color: ${(props) => props.$styles.buttonTertiary};
   background-color: ${(props) => props.$styles.backgroundSections};
   padding: 5px 20px;
   border-radius: 10px;
   bottom: 0px;
   font-size: 10px;
`;

export const SideColor = styled.span`
   height: 15px;
   aspect-ratio: 1/1;
   background-color: ${(props) => props.$styles.tabActive};
   border-radius: 50%;
`;
