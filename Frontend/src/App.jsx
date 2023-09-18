import React from "react";
import GlobalStyles from "./components/GlobalStyles/GlobalStyles.styled";
import TableGlobalStyle from "./components/GlobalStyles/TableGlobalStyles";
import ScrollGlobalStyle from "./components/GlobalStyles/ScrollGlobalStyle";
import ToastifyGlobalStyle from "./components/GlobalStyles/ToastifyGlobalStyle";
import MapboxGlobalStyle from "./components/GlobalStyles/MapboxGlobalStyle";
import Routes from "./routes/Routes";
import theme from "./themes/Theme";
import { ThemeProvider } from "styled-components";
import { useDispatch, useSelector } from "react-redux";
import { themeSliceState } from "./store/features/themeSlice/themeSlice";
import { ToastContainer, toast } from "react-toastify";
import Header from "./components/Header/Header";
import MuiGlobalStyles from "./components/GlobalStyles/MuiGlobalStyles";
import useCustomQuery from "./hooks/useCustomQuery";
import { get_me } from "./services/services";
import { addUser, removeUser } from "./store/features/userSlice/userSlice";
import { removeToken, tokenSliceState } from "./store/features/tokenSlice/tokenSlice";
import Loader from "./components/Loader/Loader";

const App = () => {
   const token = useSelector(tokenSliceState);
   const themeVariant = useSelector(themeSliceState);
   const pickedTheme = theme[themeVariant] || theme["Black"];
   const dispatch = useDispatch();

   const { isFetching } = useCustomQuery("me", get_me, {
      enabled: !!token,
      onSuccess: (data) => {
         dispatch(addUser(data.data));
      },
      onError: () => {
         dispatch(removeUser());
         dispatch(removeToken());
         toast.error("Token expired");
      },
      onSettled: () => console.log("%cAuthentication completed", `color:${pickedTheme.buttonTertiary}`),
   });

   return (
      <ThemeProvider theme={pickedTheme}>
         <GlobalStyles />
         <TableGlobalStyle />
         <ScrollGlobalStyle />
         <ToastifyGlobalStyle />
         <MapboxGlobalStyle />
         <MuiGlobalStyles />
         {!isFetching ? (
            <>
               <Header />
               <Routes />
            </>
         ) : (
            <div style={{ height: "100vh", width: "100vw" }}>
               <Loader size={20} />
            </div>
         )}
         <ToastContainer position="top-center" />
      </ThemeProvider>
   );
};

export default App;
