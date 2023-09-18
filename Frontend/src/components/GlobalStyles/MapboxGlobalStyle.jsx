import { createGlobalStyle } from "styled-components";

const MapboxGlobalStyle = createGlobalStyle`
   .mapboxgl-ctrl-logo {
		display:none !important
	}

   @media only screen and (max-width:1300px) {
      .mapboxgl-ctrl-bottom-right {
	      position:absolute;
         bottom:40px;
      }
	}

	@media only screen and (max-width:800px) {
		.mapboxgl-ctrl-bottom-right {
			display:none;
		}
	}
`;

export default MapboxGlobalStyle;
