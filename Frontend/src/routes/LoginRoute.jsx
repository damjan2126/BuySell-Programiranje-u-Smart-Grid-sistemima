import React from "react";
import { Navigate } from "react-router-dom";
import paths from "./paths";
import { useSelector } from "react-redux";
import { userSliceState } from "../store/features/userSlice/userSlice";

const LoginRoute = ({ children }) => {
   const user = useSelector(userSliceState);
   // console.log("Login route hook triggered");

   if (user) {
      return <Navigate to={paths.home} />;
   }
   return children;
};

export default LoginRoute;
