import React from "react";
import {
   SingleItem,
   SingleItemQ,
   SingleItemHeader,
   SingleItemImage,
   SingleItempPrice,
   SingleItemDescription,
   SingleItemT,
   ButtonZMP,
} from "./Cart.styled";
import { useDispatch } from "react-redux";
import { quantityMinus, quantityPlus } from "../../store/features/cartSlice/cartSlice";

const CartSingleItem = (props) => {
   const dispatch = useDispatch();
   const addPlus = () => dispatch(quantityPlus(props));
   const removeMinus = () => dispatch(quantityMinus(props));

   return (
      <SingleItem>
         <SingleItemHeader>{props.item.name}</SingleItemHeader>
         <SingleItemImage>
            <img src="www.wasdad.com" alt="img" />
         </SingleItemImage>
         <SingleItemDescription>{props.item.description}</SingleItemDescription>
         <SingleItemQ>Quantity: {props.quantity}</SingleItemQ>
         <SingleItemT>Total Price:</SingleItemT>
         <div style={{ display: "flex", alignItems: "baseline", margin: "15px auto 5px auto" }}>
            <ButtonZMP onClick={removeMinus}>-</ButtonZMP>
            <SingleItempPrice onClick={() => void 0}>{props.item.price * props.quantity} €</SingleItempPrice>
            <ButtonZMP onClick={addPlus}>+</ButtonZMP>
         </div>
      </SingleItem>
   );
};

export default CartSingleItem;
