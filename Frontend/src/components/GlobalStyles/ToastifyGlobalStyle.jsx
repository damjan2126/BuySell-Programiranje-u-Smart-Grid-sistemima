import { createGlobalStyle } from "styled-components";

const ToastifyGlobalStyle = createGlobalStyle`
   .Toastify__toast-container {
		z-index:9999999999;
		width:400px;
		max-width:100%;
	}

	.Toastify__toast-container .Toastify__toast {
		color: ${(props) => props.theme.textPrimary};
      background-color: ${(props) => props.theme.background};
		border: 2px solid ${(props) => props.theme.body};
		border-radius:7px;
		margin:10px;
	}

	.Toastify__toast-container .Toastify__toast .Toastify__close-button svg {
		fill: ${(props) => props.theme.textPrimary} !important;
	}

	@media only screen and (max-width:480px) {
		.Toastify__toast-container {
			width:100%;
		}
	}
`;

export default ToastifyGlobalStyle;
