import { yupResolver } from "@hookform/resolvers/yup";
import React from "react";
import { useForm } from "react-hook-form";
import YupInput from "../../components/Input/YupInput";
import { AddItemValidation } from "../../validations/AddItemValidation";
import Form from "../../components/Form/Form";
import Button from "../../components/Button/Button";
import { Edit_Item } from "../../services/services";
import { useMutation } from "react-query";
import { toast } from "react-toastify";

const EditItem = (props) => {
   const { control, handleSubmit } = useForm({
      resolver: yupResolver(AddItemValidation),
      defaultValues: {
         name: props.name,
         price: props.price,
         ammount: props.ammount,
         description: props.description,
      },
   });

   console.log(props);

   const { mutate } = useMutation((data) => Edit_Item(props.id, data), {
      onError: (data) => {
         toast.error(`${data?.response?.statusText}`);
      },
      onSuccess: () => toast.success("Item editted"),
   });

   const onSubmit = (data) => {
      mutate(
         { ...data, imageUrl: "asdasdasdasd" },
         {
            onSuccess: () => {
               props.refetch();
               props.closeModal();
            },
         }
      );
   };

   return (
      <div>
         <Form onSubmit={handleSubmit(onSubmit)}>
            <YupInput control={control} name="name" type="text" placeholder="Name" errorBorder />
            <YupInput control={control} name="price" type="number" placeholder="Price" errorBorder />
            <YupInput control={control} name="ammount" type="number" placeholder="Ammount" errorBorder />
            <YupInput control={control} name="description" type="text" placeholder="Description" errorBorder />

            <div style={{ display: "flex", justifyContent: "center" }}>
               <Button type="submit">Submit</Button>
            </div>
         </Form>
      </div>
   );
};

export default EditItem;
