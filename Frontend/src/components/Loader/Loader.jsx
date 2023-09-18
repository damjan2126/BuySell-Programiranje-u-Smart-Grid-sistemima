import React from "react";
import { useTheme } from "styled-components";
import { BeatLoader } from "react-spinners";

const Loader = ({ size = 10 }) => {
   const { textPrimary } = useTheme();
   return (
      <div style={{ height: "100%", width: "100%", display: "flex", alignItems: "center", justifyContent: "center" }}>
         <BeatLoader size={size} color={textPrimary} style={{ display: "flex" }} />
      </div>
   );
};

export default Loader;
