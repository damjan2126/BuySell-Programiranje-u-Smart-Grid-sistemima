import React from "react";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { OrderValidation } from "../../validations/OrderValidation";
import YupInput from "../../components/Input/YupInput";
import instance from "../../services/instance";
import Form from "../../components/Form/Form";
import { Button } from "../../components/Button/Button.styled";
import { removeCartItems } from "../../store/features/cartSlice/cartSlice";
import { useDispatch } from "react-redux";
import { toast } from "react-toastify";

const OrderModal = ({ closeModal, itemsForOrder }) => {
   const addOrder = () => instance.post(`/Order/addItem`, itemsForOrder);
   const addOrderInfo = (id, addCom) => instance.patch(`/Order/${id}`, { ...addCom });
   const get_order_state = (id) => instance.patch(`/Order/${id}/state`, { state: 1 });
   const dispatch = useDispatch();

   const myOrderLele = async (addCom) => {
      try {
         let {
            data: { id },
         } = await addOrder();
         await addOrderInfo(id, addCom);
         await get_order_state(id);
         toast.success("Success");
         dispatch(removeCartItems());
         closeModal();
      } catch (error) {
         toast.error("Error while making the order");
      }
   };

   const { handleSubmit, control } = useForm({
      resolver: yupResolver(OrderValidation),
   });

   const onSubmit = (data) => {
      myOrderLele({ address: data.address, comment: data.comment });
   };

   return (
      <Form onSubmit={handleSubmit(onSubmit)}>
         <YupInput control={control} name="address" placeholder="Enter address" />
         <YupInput control={control} name="comment" placeholder="Enter comment" />

         <Button>Send it</Button>
      </Form>
   );
};

export default OrderModal;
