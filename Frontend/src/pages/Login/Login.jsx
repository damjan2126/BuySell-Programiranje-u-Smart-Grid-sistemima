import React from "react";
import { useDispatch } from "react-redux";
import ButtonPrimary from "../../components/Button/ButtonPrimary";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { LoginValidation } from "../../validations/LoginValidation";
import LandingPage from "../../components/LandingPage/LandingPage";
import YupInput from "../../components/Input/YupInput";
import Form from "../../components/Form/Form";
import { useMutation } from "react-query";
import { login } from "../../services/services";
import { toast } from "react-toastify";
import useUiNavigation from "../../hooks/useUiNavigation";
import WriteToLocalStorage from "../../utils/WriteToLocalStorage";
import { addToken } from "../../store/features/tokenSlice/tokenSlice";
import { GoogleOAuthProvider } from "@react-oauth/google";
import { GoogleLogin } from "@react-oauth/google";
import instance from "../../services/instance";

const Login = () => {
   const dispatch = useDispatch();

   const { handleSubmit, control } = useForm({
      resolver: yupResolver(LoginValidation),
   });

   const { navigateToSignup } = useUiNavigation();

   const { mutate, isLoading } = useMutation(login, {
      onError: (data) => toast.error(data.response.data.message || "Server down"),
      onSuccess: (data) => {
         const { token, refreshToken } = data?.data;
         WriteToLocalStorage("token", JSON.stringify(token));
         WriteToLocalStorage("refreshToken", JSON.stringify(refreshToken));
         dispatch(addToken(token));
      },
   });

   const send_g_token = (t) =>
      instance.post("/Users/authenticate/google", {
         GoogleToken: t,
      });

   const responseGoogle = async (response) => {
      try {
         const gt = await response.credential;
         let g_response = await send_g_token(gt);
         const { token, refreshToken } = g_response.data;
         console.log(token, refreshToken);
         WriteToLocalStorage("token", JSON.stringify(token));
         WriteToLocalStorage("refreshToken", JSON.stringify(refreshToken));
         dispatch(addToken(token));
      } catch (error) {
         console.log(error);
      }
   };

   const onSubmit = (data) => {
      mutate({ username: data.username, password: data.password });
   };

   return (
      <LandingPage>
         <Form
            nav={<span onClick={navigateToSignup}>Go to signup</span>}
            header="Welcome!"
            button={<ButtonPrimary loading={isLoading}>Login</ButtonPrimary>}
            onSubmit={handleSubmit(onSubmit)}
         >
            <YupInput control={control} name="username" type="text" placeholder="Username" errorBorder />
            <YupInput control={control} name="password" type="password" placeholder="Password" errorBorder />
         </Form>

         <div style={{ display: "flex", justifyContent: "center", marginTop: "20px" }}>
            <GoogleOAuthProvider clientId="537961435311-thqj5cefqa83istba2gc2bcv9hl3iah8.apps.googleusercontent.com">
               <GoogleLogin
                  clientId="537961435311-thqj5cefqa83istba2gc2bcv9hl3iah8.apps.googleusercontent.com"
                  render={(renderProps) => (
                     <button
                        type="button"
                        className="bg-mainColor flex justify-center items-center p-3 rounded-lg cursor-pointer outline-none"
                        onClick={renderProps.onClick}
                        disabled={renderProps.disabled}
                     >
                        Sign in with your Google Account
                     </button>
                  )}
                  onSuccess={responseGoogle}
                  onFailure={responseGoogle}
                  cookiePolicy="single_host_origin"
               />
            </GoogleOAuthProvider>
         </div>
      </LandingPage>
   );
};

export default Login;
