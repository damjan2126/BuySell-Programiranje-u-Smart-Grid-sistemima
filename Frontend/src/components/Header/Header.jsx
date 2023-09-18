import React, { memo } from "react";
import { HeaderLeft, HeaderRight, Header as MyHeader } from "./Header.styles";
import { useLocation } from "react-router-dom";
import paths from "../../routes/paths";
import Modal from "../Modal/Modal";
import ThemePicker from "../ThemePicker/ThemePicker";
import HeaderSelect from "./HeaderSelect/HeaderSelect";
import Button from "../Button/Button";
import { GoStack } from "react-icons/go";
import BackButton from "../Button/BackButton";
import { AiOutlineShoppingCart, AiOutlineCheckSquare } from "react-icons/ai";
import { useSelector } from "react-redux/es/hooks/useSelector";
import { cartSliceState } from "../../store/features/cartSlice/cartSlice";
import useUiNavigation from "../../hooks/useUiNavigation";
import { userSliceState } from "../../store/features/userSlice/userSlice";

const Header = () => {
   const cart = useSelector(cartSliceState);
   const { pathname } = useLocation();
   const user = useSelector(userSliceState);

   const hasBuyerRole = user?.roles.includes("Buyer");
   const { navigateToCart, navigateToVerification } = useUiNavigation();

   if (pathname === paths.login || pathname === paths.Signup || user === null) return;
   return (
      <MyHeader>
         <HeaderLeft>DAMJAN APP</HeaderLeft>
         <HeaderRight>
            <BackButton>Home</BackButton>
            {cart.length > 0 && hasBuyerRole && (
               <Button onClick={navigateToCart} $canShrink style={{ marginRight: "10px" }}>
                  <AiOutlineShoppingCart style={{ margin: "0px 4px" }} />
                  {cart.length}
               </Button>
            )}
            {user.roles.includes("Admin") && pathname !== "/verification" && (
               <Button onClick={navigateToVerification} $canShrink style={{ marginRight: "10px" }}>
                  <AiOutlineCheckSquare style={{ margin: "0px 4px" }} />
               </Button>
            )}
            <Modal
               headline="Pick a theme"
               button={
                  <Button $canShrink>
                     <GoStack style={{ margin: "0px 4px" }} />
                  </Button>
               }
            >
               <ThemePicker />
            </Modal>

            <HeaderSelect />
         </HeaderRight>
      </MyHeader>
   );
};

export default memo(Header);
