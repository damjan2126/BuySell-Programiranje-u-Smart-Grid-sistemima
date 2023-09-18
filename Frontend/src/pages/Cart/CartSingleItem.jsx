import React, { useEffect, useState } from "react";
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
import instance from "../../services/instance";


const CartSingleItem = (props) => {
   const dispatch = useDispatch();
   const addPlus = () => dispatch(quantityPlus(props));
   const removeMinus = () => dispatch(quantityMinus(props));

   const [imageURL, setImageURL] = useState("");

   useEffect(() => {
      const getImg = async () => {
         try {
            const response = await instance.get("/Images", {
               params: {
                  imagePath: props.item.imageUrl,
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

      getImg();

      return () => {
         if (imageURL) {
            URL.revokeObjectURL(imageURL);
         }
      };
      //eslint-disable-next-line
   }, []);


   return (
      <SingleItem>
         <SingleItemHeader>{props.item.name}</SingleItemHeader>
         <SingleItemImage>
            <img
               style={{ height: "100%", width: "100%", objectFit: "cover", display: "block" }}
               src={imageURL}
               alt="img"
            />
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
