import React, { useEffect, useState } from "react";
import { OrderContainer } from "./Orders.styled";
import useCustomQuery from "../../hooks/useCustomQuery";
import { get_orders } from "../../services/services";

import { useSelector } from "react-redux";
import { userSliceState } from "../../store/features/userSlice/userSlice";
import OContent from "./OContent";
import { Button } from "../../components/Button/Button.styled";

const items = [
   { id: 1, name: "In Progress", value: 1 },
   { id: 2, name: "Completed", value: 2 },
   { id: 3, name: "Aborted", value: 3 },
];

const Orders = () => {
   const user = useSelector(userSliceState);
   const id = user.id;
   const [s, setS] = useState(1);

   const isAdmin = user.roles.includes("Admin");
   const isSeller = user.roles.includes("Seller");

   const { data, refetch: refetchOrders } = useCustomQuery("my_orders", () =>
      get_orders(isAdmin ? null : id, s, isSeller && !isAdmin ? id : null)
   );

   const mappedZ = data?.data.orders.map((item, index) => {
      return <OContent refetchOrders={refetchOrders} key={index} data={item} />;
   });

   useEffect(() => {
      refetchOrders({ variables: { id, s } });
   }, [s, id, refetchOrders]);

   return (
      <>
         <div style={{ display: "flex", justifyContent: "center", marginTop: "10px" }}>
            {items.map((el) => {
               return (
                  <Button
                     key={el.id}
                     onClick={() => setS(el.value)}
                     style={{
                        margin: "0px 5px",
                        backgroundColor: s !== el.value && "gray",
                        boxShadow: "0px 0px 0px transparent",
                     }}
                  >
                     {el.name}
                  </Button>
               );
            })}
         </div>
         <OrderContainer>{mappedZ}</OrderContainer>
      </>
   );
};

export default Orders;
