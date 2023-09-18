import React from "react";
import { Navigate } from "react-router-dom";
import paths from "./paths";
import { useSelector } from "react-redux";
import { userSliceState } from "../store/features/userSlice/userSlice";

const ProtectedRoute = ({ children }) => {
   const user = useSelector(userSliceState);
   // console.log("Protected route hook triggered");

   if (user) {
      return children;
   }
   return <Navigate to={paths.login} />;
};

export default ProtectedRoute;
