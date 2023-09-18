import React, { useMemo } from "react";
import { useSelector } from "react-redux";
import { cartSliceState } from "../../store/features/cartSlice/cartSlice";
import CartSingleItem from "./CartSingleItem";
import { CartContainer, TotalPrice, TPT } from "./Cart.styled";
import Button from "../../components/Button/Button";

import Modal from "../../components/Modal/Modal";
import OrderModal from "./OrderModal";

const Cart = () => {
   const state = useSelector(cartSliceState);
   const mappedItems = state?.map((item) => {
      return <CartSingleItem key={item.item.id} {...item} />;
   });

   const itemsForOrder = useMemo(() => {
      let order = {
         items: [],
      };
      state.forEach((item) => {
         order.items.push({ itemId: item.item.id, amount: item.quantity });
      });
      return order;
   }, [state]);

   const totalPrice = useMemo(() => {
      let total = 0;
      state.forEach((item) => {
         total += item.quantity * item.item.price;
      });
      return total;
   }, [state]);

   return (
      <>
         <CartContainer>{mappedItems}</CartContainer>
         {state.length === 0 && <div style={{ textAlign: "center" }}>No items in cart</div>}
         {state.length > 0 && (
            <>
               <TotalPrice>
                  <TPT>Total Price:</TPT> {totalPrice} €
               </TotalPrice>
               <Modal></Modal>
               <Modal passesProps button={<Button style={{ margin: "0 auto" }}>Order</Button>}>
                  <OrderModal itemsForOrder={itemsForOrder} />
               </Modal>
            </>
         )}
      </>
   );
};

export default Cart;
