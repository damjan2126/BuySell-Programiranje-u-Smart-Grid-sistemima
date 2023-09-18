import { createGlobalStyle } from "styled-components";

const GlobalStyles = createGlobalStyle`
   *,
	*::before,
	*::after {
      margin:0;
      padding:0;
      box-sizing:border-box;
		font-family: 'Montserrat', sans-serif;
		/* user-select:none; */
		/* outline: 1px solid red; */
   }

   body {
      color: ${(props) => props.theme.textPrimary};
      background-color: ${(props) => props.theme.body};
   }

	// These are tied to My Profile Menu in Header
	[class$="ValueContainer"] {
	  	padding:0px !important;
	}	

	@media only screen and (max-width:500px) {
		[class$="IndicatorsContainer"] {
		  	display:none !important;
		}	
		[class$="menu"] {
	  		width:150px !important;
	  		right:0;
		}	
	}
	//
.user-info > * {
	width:calc(33.3333% - 10px);
	padding:10px;
	margin:5px;
	border-radius:5px;
	background-color: ${(props) => props.theme.body};

	@media only screen and (max-width: 700px) {
		width:calc(50% - 10px);
	}
	@media only screen and (max-width: 500px) {
		width:calc(100%);
	}
}

.user-info-2 > * {
	width:calc(50% - 10px);
	padding:10px;
	margin:5px;
	border-radius:5px;
	background-color: ${(props) => props.theme.body};

	@media only screen and (max-width: 700px) {
		width:calc(50% - 10px);
	}
	@media only screen and (max-width: 500px) {
		width:calc(100%);
	}
}
	
   @keyframes hopInFromBottom {
		0% {
			transform: translateY(200px);
			opacity: 0;
		}
		50% {
			transform: translateY(-20px);
		}
		100% {
			transform: translateY(0px);
			opacity: 1;
		}
	}

	@keyframes fadeIn {
		from {
			opacity: 0;
		}
		to {
			opacity: 1;
		}
	}
	
	@keyframes open {
   	from {
      	transform: translate(-100%);
   	}
   	to {
     	 transform: translate(0%);
   	}
	}
`;

export default GlobalStyles;
