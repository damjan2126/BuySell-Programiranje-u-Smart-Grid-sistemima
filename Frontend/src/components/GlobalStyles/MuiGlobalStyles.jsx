import { createGlobalStyle } from "styled-components";

const MuiGlobalStyles = createGlobalStyle`
	.MuiCheckbox-root svg {
		fill: ${(props) => props.theme.tabActive};
	}
	.css-5ryogn-MuiButtonBase-root-MuiSwitch-switchBase.Mui-checked {
		color: ${(props) => props.theme.tabActive} !important;
	}

	.css-5ryogn-MuiButtonBase-root-MuiSwitch-switchBase.Mui-checked+.MuiSwitch-track {
		background-color: ${(props) => props.theme.tabActive} !important;
	}
	
	.MuiPaper-root.MuiPaper-elevation {
		background-color:transparent !important;
	}

	.css-12wnr2w-MuiButtonBase-root-MuiCheckbox-root.Mui-checked {
		color: ${(props) => props.theme.tabActive} !important;
	}
`;

export default MuiGlobalStyles;
