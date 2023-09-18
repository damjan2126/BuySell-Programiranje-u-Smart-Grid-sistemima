import React from "react";
import { useTheme } from "styled-components";
import SingleItem from "../Home/SingleItem";
import { OrderContent, DeleteTrash } from "./Orders.styled";
import SingleInfo from "./SingleInfo";
import { toast } from "react-toastify";
import instance from "../../services/instance";
import { BsTrash } from "react-icons/bs";
const OContent = (props) => {
   const orderData = props?.data;
   const theme = useTheme();

   const get_order_state = (id) => instance.patch(`/Order/${id}/state`, { state: 3 });

   const remopveOrder = async () => {
      try {
         await get_order_state(orderData.id);
         props.refetchOrders();
         toast.success("Success");
      } catch (error) {
         toast.error(error.response.data.message);
      }
   };

   const mappedItems = orderData?.items.map((item) => (
      <SingleItem
         showText={false}
         key={item.id}
         anotherFunc={props.refetchOrders}
         anotherFuncText="Remove"
         canClick={false}
         {...item.item}
      />
   ));

   return (
      <OrderContent style={{ position: "relative" }}>
         {props.data.status !== "Aborted" && (
            <DeleteTrash>
               <BsTrash onClick={remopveOrder} />
            </DeleteTrash>
         )}
         <h4
            style={{
               color: theme.buttonTertiary,
               padding: "20px 10px 0px 10px",
               fontWeight: "600",
               textDecoration: "underline",
            }}
         >
            User Info:
         </h4>
         <div
            style={{
               margin: "20px 0",
            }}
         >
            <SingleInfo text="Name">{orderData?.createdByUser?.firstName}</SingleInfo>
            <SingleInfo text="Last name">{orderData?.createdByUser?.lastName}</SingleInfo>
            <SingleInfo text="Address">{orderData?.createdByUser?.address}</SingleInfo>
            <SingleInfo text="Email">{orderData?.createdByUser?.email}</SingleInfo>
            <SingleInfo text="Date of birth">
               {new Date(orderData?.createdByUser?.dateOfBirth).toLocaleString()}
            </SingleInfo>
            <SingleInfo text="Created At:">
               {new Date(orderData?.createdByUser?.createdAtUtc).toLocaleString()}
            </SingleInfo>
            <SingleInfo text="User ID:">{orderData?.createdByUser?.id}</SingleInfo>
         </div>
         <h4
            style={{
               color: theme.buttonTertiary,
               padding: "20px 10px 0px 10px",
               fontWeight: "600",
               textDecoration: "underline",
            }}
         >
            Order Info:
         </h4>
         <div
            style={{
               margin: "20px 0",
            }}
         >
            <SingleInfo text="Order ID:">{orderData?.id}</SingleInfo>
            <SingleInfo text="Address">{orderData?.address || "No data"}</SingleInfo>
            <SingleInfo text="Comment">{orderData?.comment || "No data"}</SingleInfo>
            <SingleInfo text="Cost:">{orderData?.cost}</SingleInfo>
            <SingleInfo text="Created at">{new Date(orderData?.createdAtUtc).toLocaleString() || "No data"}</SingleInfo>
            <SingleInfo text="Delivery time">
               {new Date(orderData?.deliveryTime).toLocaleString() || "No data"}
            </SingleInfo>
            <SingleInfo text="Status">{orderData?.status}</SingleInfo>
            <SingleInfo text="Updated At:">{new Date(orderData?.updatedAtUtc).toLocaleString(0)}</SingleInfo>
         </div>
         <h4
            style={{
               color: theme.buttonTertiary,
               padding: "20px 10px 0px 10px",
               fontWeight: "600",
            }}
         >
            Items: ({orderData?.items.length})
         </h4>
         <div style={{ display: "flex", overflow: "auto", flexWrap: "nowrap" }}>{mappedItems}</div>
      </OrderContent>
   );
};

export default OContent;
