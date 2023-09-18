import React, { useState } from "react";
import ButtonPrimary from "../../components/Button/ButtonPrimary";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import LandingPage from "../../components/LandingPage/LandingPage";
import YupInput from "../../components/Input/YupInput";
import Form from "../../components/Form/Form";
import { SignupValidation } from "../../validations/SignupValidation";
import useUiNavigation from "../../hooks/useUiNavigation";
import { useMutation } from "react-query";
import { signup } from "../../services/services";
import { toast } from "react-toastify";
import { Button } from "../../components/Button/Button.styled";

const Signup = () => {
   const [role, setRole] = useState("Buyer");
   const { handleSubmit, control } = useForm({
      resolver: yupResolver(SignupValidation),
   });

   const { navigateToLogin } = useUiNavigation();

   const { mutate } = useMutation(signup, {
      onError: (data) => toast.error(data.response.data.message),
      onSuccess: () => {
         toast.success("User sucessfully created, going back to login...");
         setTimeout(() => {
            navigateToLogin();
         }, 3000);
      },
   });

   const onSubmit = (data) => {
      mutate({
         role: role,
         imageUrl: null,
         address: data.address,
         dateOfBirth: data.dateOfBirth,
         password: data.password,
         email: data.email,
         userName: data.userName,
         lastName: data.lastName,
         firstname: data.firstname,
      });
   };

   return (
      <LandingPage>
         <Form
            nav={<span onClick={navigateToLogin}>Go to login</span>}
            header="Signup"
            button={<ButtonPrimary>Signup</ButtonPrimary>}
            onSubmit={handleSubmit(onSubmit)}
         >
            <YupInput control={control} name="firstname" type="text" placeholder="First name" errorBorder />
            <YupInput control={control} name="lastName" type="text" placeholder="Last name" errorBorder />
            <YupInput control={control} name="userName" type="text" placeholder="User name" errorBorder />
            <YupInput control={control} name="email" type="text" placeholder="Email" errorBorder />
            <YupInput control={control} name="password" type="password" placeholder="Password" errorBorder />
            <YupInput control={control} name="dateOfBirth" type="date" placeholder="Date" errorBorder />
            <YupInput control={control} name="address" type="text" placeholder="Address" errorBorder />
            <div style={{ display: "flex", maxWidth: "100%", justifyContent: "space-around", marginBottom: "30px" }}>
               {role === "Buyer" ? (
                  <Button onClick={() => setRole("Buyer")} disabled={role === "Buyer"}>
                     Buyer
                  </Button>
               ) : (
                  <ButtonPrimary onClick={() => setRole("Buyer")} disabled={role === "Buyer"}>
                     {" "}
                     Buyer
                  </ButtonPrimary>
               )}
               {role === "Seller" ? (
                  <Button onClick={() => setRole("Seller")} disabled={role === "Seller"}>
                     Seller
                  </Button>
               ) : (
                  <ButtonPrimary onClick={() => setRole("Seller")} disabled={role === "Seller"}>
                     Seller
                  </ButtonPrimary>
               )}
            </div>
         </Form>
      </LandingPage>
   );
};

export default Signup;
