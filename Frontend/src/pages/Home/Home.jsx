import React, { memo } from "react";
import useCustomQuery from "../../hooks/useCustomQuery";
import { get_items } from "../../services/services";
import { HomeContainer, HomeHolder, ItemsHolder } from "./Home.styled";
import SingleItem from "./SingleItem";
import Button from "../../components/Button/Button";
import { useTheme } from "styled-components";
import { AiOutlineFileAdd } from "react-icons/ai";
import Modal from "../../components/Modal/Modal";
import AddItem from "./AddItem";
import { useSelector } from "react-redux";
import { userSliceState } from "../../store/features/userSlice/userSlice";

const Home = () => {
   const user = useSelector(userSliceState);
   const hasSellerRole = user?.roles.includes("Seller");
   const hasAdminRole = user?.roles.includes("Admin");
   const qry = hasSellerRole && !hasAdminRole && user?.id && user?.id;

   const theme = useTheme();
   const { data, refetch, isLoading, isFetched } = useCustomQuery("all_items", () => get_items(qry || null));

   const mappedItems = data?.data?.items.map((item) => {
      return <SingleItem key={item.id} refetch={refetch} user={user} {...item} />;
   });

   return (
      <HomeContainer>
         <HomeHolder>
            <h3>Hello {user.firstName}, welcome!</h3>
            <div style={{ display: "flex", gap: "10px" }}>
               {user.roles.some((r) => r === "Admin" || r === "Seller") && (
                  <Modal
                     passesProps
                     headline="Add Item"
                     button={
                        <Button canShrink>
                           <AiOutlineFileAdd />
                        </Button>
                     }
                  >
                     <AddItem refetch={refetch} isLoading={isLoading} />
                  </Modal>
               )}
               <small
                  style={{
                     fontSize: "11px",
                     color: theme.textPrimary,
                     backgroundColor: theme.buttonTertiary,
                     padding: "10px",
                     borderRadius: "5px",
                  }}
               >
                  Total items: {data?.data.count || "No data"}
               </small>
            </div>
         </HomeHolder>
         {isFetched && !data?.data && <div style={{ textAlign: "center", marginTop: "20px" }}>No items</div>}
         <ItemsHolder>{mappedItems}</ItemsHolder>
      </HomeContainer>
   );
};

export default memo(Home);
