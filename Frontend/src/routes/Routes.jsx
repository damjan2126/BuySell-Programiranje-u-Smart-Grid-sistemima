import React from "react";
import { Navigate, useRoutes } from "react-router-dom";
import paths from "./paths";
import ProtectedRoute from "./ProtectedRoute";
import LoginRoute from "./LoginRoute";
import Home from "../pages/Home/Home";
import Login from "../pages/Login/Login";
import Signup from "../pages/Signup/Signup";
import AllUsers from "../pages/AllUsers/AllUsers";
import SingleUser from "../pages/SingleUser/SingleUser";
import Cart from "../pages/Cart/Cart";
import Verification from "../pages/Verification/Verification";
import Profile from "../pages/Profile/Profile";
import AdminRoute from "./AdminRoute";
import Orders from "../pages/Orders/Orders";

const Routes = () => {
   const element = useRoutes([
      {
         path: paths.home,
         element: (
            <ProtectedRoute>
               <Home />
            </ProtectedRoute>
         ),
      },
      {
         path: paths.login,
         element: (
            <LoginRoute>
               <Login />
            </LoginRoute>
         ),
      },
      {
         path: paths.Signup,
         element: <Signup />,
      },
      {
         path: paths.users,
         element: (
            <ProtectedRoute>
               <AllUsers />
            </ProtectedRoute>
         ),
      },
      {
         path: paths.user,
         element: (
            <ProtectedRoute>
               <SingleUser />
            </ProtectedRoute>
         ),
      },
      {
         path: paths.cart,
         element: (
            <ProtectedRoute>
               <Cart />
            </ProtectedRoute>
         ),
      },
      {
         path: paths.verification,
         element: (
            <ProtectedRoute>
               <AdminRoute>
                  <Verification />
               </AdminRoute>
            </ProtectedRoute>
         ),
      },
      {
         path: paths.profile,
         element: (
            <ProtectedRoute>
               <Profile />
            </ProtectedRoute>
         ),
      },
      {
         path: paths.orders,
         element: (
            <ProtectedRoute>
               <Orders />
            </ProtectedRoute>
         ),
      },
      // Create other routes bellow this line

      // Not found component should
      // always be last (convention)
      {
         path: paths.not_found,
         element: (
            <ProtectedRoute>
               <Navigate to={paths.home} />
            </ProtectedRoute>
         ),
      },
   ]);

   return element;
};

export default Routes;
