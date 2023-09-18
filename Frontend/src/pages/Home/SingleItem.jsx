import React, { useEffect, useState } from "react";
import {
   SingleItem as SI,
   SingleItemDescription,
   SingleItemHeader,
   SingleItemImage,
   SingleItempPrice,
} from "./Home.styled";
import { SingleItemQ } from "../Cart/Cart.styled";
import { useDispatch } from "react-redux";
import { toggleItem } from "../../store/features/cartSlice/cartSlice";
import { AiOutlineEdit } from "react-icons/ai";
import { useTheme } from "styled-components";
import Modal from "../../components/Modal/Modal";
import EditItem from "./EditItem";
import instance from "../../services/instance";

const SingleItem = (props) => {
   const [imageURL, setImageURL] = useState("");
   const dispatch = useDispatch();
   const theme = useTheme();
   const hasSellerRole = props.user?.roles.includes("Seller");
   const hasBuyerRole = props.user?.roles.includes("Buyer");
   const hasAdminRole = props.user?.roles.includes("Admin");
   const addItemToCart = () => hasBuyerRole && canClick && dispatch(toggleItem(props));
   const { canClick = true, anotherFunc, anotherFuncText } = props;

   useEffect(() => {
      const getImg = async () => {
         try {
            const response = await instance.get("/Images", {
               params: {
                  imagePath: props.imageUrl,
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

   const { showText = true } = props;

   return (
      <SI style={{ position: "relative" }}>
         {hasSellerRole && !hasAdminRole && (
            <div style={{ position: "absolute", top: "16px", right: "10px" }}>
               <Modal passesProps headline="Edit your Item" button={<AiOutlineEdit fill={theme.buttonTertiary} />}>
                  <EditItem {...props} />
               </Modal>
            </div>
         )}
         <SingleItemHeader>{props.name}</SingleItemHeader>
         <SingleItemImage>
            <img
               style={{ height: "100%", width: "100%", objectFit: "cover", display: "block" }}
               src={imageURL}
               alt="img"
            />
         </SingleItemImage>
         <SingleItemDescription>{props.description}</SingleItemDescription>
         <SingleItemQ style={{ fontSize: "10px" }}>Ammount: {props.ammount}</SingleItemQ>
         {showText && (
            <SingleItempPrice onClick={anotherFunc ? anotherFunc : addItemToCart}>
               {anotherFuncText ? anotherFuncText : `${props.price} €`}
            </SingleItempPrice>
         )}
      </SI>
   );
};

export default SingleItem;
