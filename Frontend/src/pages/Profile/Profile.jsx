import React, { useEffect } from "react";
import useCustomQuery from "../../hooks/useCustomQuery";
import { get_me } from "../../services/services";
import YupInput from "../../components/Input/YupInput";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { ProfileValidation } from "../../validations/ProfileValidation";
import Form from "../../components/Form/Form";
import Button from "../../components/Button/Button";
import { ProfileFormComponent } from "./Profile.styled";
import { useMutation } from "react-query";
import { profile_change } from "../../services/services";
import { toast } from "react-toastify";
import { useTheme } from "styled-components";
import { ProfileValidationBuyer } from "../../validations/ProfileValidationBuyer";
import instance from "../../services/instance";
import { useState } from "react";

const Profile = () => {
   const { data, refetch } = useCustomQuery("my_profile", get_me);
   const theme = useTheme();
   const [imageURL, setImageURL] = useState("");
   const [file, setFile] = useState();
   const isBuyer = data?.data.roles.includes("Buyer");

   const { handleSubmit, control, setValue } = useForm({
      resolver: yupResolver(isBuyer ? ProfileValidationBuyer : ProfileValidation),
   });

   const { mutateAsync } = useMutation((stuff) => profile_change(data?.data?.id, stuff), {
      onSuccess: () => toast.success("Profile updated"),
      onError: () => toast.error("Couldn't update your profile"),
   });

   useEffect(() => {
      if (data) {
         setValue("firstName", data.data.firstName);
         setValue("lastName", data.data.lastName);
         // Format and set dateOfBirth
         const formattedDate = data?.data?.dateOfBirth?.split("T")[0]; // Extract yyyy-MM-dd
         setValue("dateOfBirth", formattedDate);

         setValue("address", data.data.address);

         !isBuyer && setValue("deliveryFee", data.data.deliveryFee || null);
      }
   }, [data, setValue, isBuyer]);

   const send_img = (file) => instance.post("/Images", file);

   const onSubmit = async (data) => {
      try {
         const formData = new FormData();
         formData.append("image", file);
         let response = await send_img(formData);
         const newData = {
            ...data,
            imageUrl: response.data[0],
         };
         await mutateAsync(newData);
         refetch();
      } catch (err) {
         toast.error("Error");
      }
   };

   useEffect(() => {
      const getImg = async () => {
         try {
            const response = await instance.get("/Images", {
               params: {
                  imagePath: data?.data.imageUrl,
               },
               responseType: "arraybuffer",
            });
            const blob = new Blob([response.data], { type: "image/jpeg" });

            const url = URL.createObjectURL(blob);
            setImageURL(url);
         } catch (err) {
            console.log(err);
         }
      };

      if (data?.data.imageUrl) {
         getImg();
      }

      // Clean up the object URL when the component unmounts
      return () => {
         if (imageURL) {
            URL.revokeObjectURL(imageURL);
         }
      };
      //eslint-disable-next-line
   }, [data?.data]);

   return (
      <>
         {data?.data && (
            <ProfileFormComponent>
               <Form header="Your info" onSubmit={handleSubmit(onSubmit)} button={<Button>Update your profile</Button>}>
                  <div
                     style={{
                        height: "200px",
                        aspectRatio: "1/1",
                        backgroundColor: "gray",
                        margin: "0 auto",
                        borderRadius: "50%",
                        overflow: "hidden",
                     }}
                  >
                     <img
                        style={{ height: "100%", width: "100%", objectFit: "cover", display: "block" }}
                        src={imageURL}
                        alt="img"
                     />
                  </div>
                  <div style={{ margin: "20px 0px" }}>
                     Roles:
                     {data?.data.roles.map((el, index) => (
                        <span key={index} style={{ color: theme.buttonTertiary, marginLeft: "7px" }}>
                           {el}
                        </span>
                     ))}
                  </div>
                  <YupInput name="firstName" type="text" control={control} placeholder="Name" />
                  <YupInput name="lastName" type="text" control={control} placeholder="Last name" />
                  <YupInput name="dateOfBirth" type="date" control={control} placeholder="Date" />
                  <YupInput name="address" type="text" control={control} placeholder="Address" />
                  {!isBuyer && (
                     <YupInput name="deliveryFee" type="number" control={control} placeholder="Delivery Fee" />
                  )}
                  <input style={{ marginBottom: "20px" }} type="file" onChange={(e) => setFile(e.target.files[0])} />
               </Form>
            </ProfileFormComponent>
         )}
      </>
   );
};

export default Profile;
