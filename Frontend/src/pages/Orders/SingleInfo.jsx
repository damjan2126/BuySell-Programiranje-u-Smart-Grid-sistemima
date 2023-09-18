import React from "react";
import { useTheme } from "styled-components";

const SingleInfo = ({ text, children }) => {
   const theme = useTheme();
   return (
      <div
         style={{
            fontSize: "13px",
            display: "flex",
            alignItems: "center",
            gap: "20px",
            flexWrap: "wrap",
            margin: "10px 0",
         }}
      >
         <div style={{ padding: "5px 20px", borderRadius: "6px", background: theme.body, width: "150px" }}>{text}</div>
         <div style={{ flexGrow: 1 }}>{children}</div>
      </div>
   );
};

export default SingleInfo;
