import { yupResolver } from "@hookform/resolvers/yup";
import React, { useState } from "react";
import { useForm } from "react-hook-form";
import YupInput from "../../components/Input/YupInput";
import { AddItemValidation } from "../../validations/AddItemValidation";
import Form from "../../components/Form/Form";
import Button from "../../components/Button/Button";

import { toast } from "react-toastify";
import instance from "../../services/instance";

const AddItem = ({ refetch, closeModal }) => {
   const { control, handleSubmit } = useForm({
      resolver: yupResolver(AddItemValidation),
   });
   const [file, setFile] = useState();

   // const { mutateAsync: addImg } = useMutation(() => addIMG());
   // const { mutateAsync: mutateData } = useMutation(Add_Item);

   const send_img = (file) => instance.post("/Images", file);
   const add_i = (z) => instance.post("/Item", z);

   const onSubmit = async (data) => {
      try {
         const formData = new FormData();
         formData.append("image", file);
         let response = await send_img(formData);
         const newData = {
            ...data,
            imageUrl: response.data[0],
         };
         console.log(newData);
         await add_i(newData);
         refetch();
         closeModal();
         toast.success("Success");
      } catch (err) {
         toast.error("Error");
      }
   };

   return (
      <div>
         <Form onSubmit={handleSubmit(onSubmit)}>
            <YupInput control={control} name="name" type="text" placeholder="Name" errorBorder />
            <YupInput control={control} name="price" type="number" placeholder="Price" errorBorder />
            <YupInput control={control} name="ammount" type="number" placeholder="Ammount" errorBorder />
            <YupInput control={control} name="description" type="text" placeholder="Description" errorBorder />
            <input type="file" onChange={(e) => setFile(e.target.files[0])} />

            <div style={{ display: "flex", justifyContent: "center" }}>
               <Button type="submit">Submit</Button>
            </div>
         </Form>
      </div>
   );
};

export default AddItem;
