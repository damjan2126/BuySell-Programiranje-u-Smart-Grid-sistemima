import { useNavigate } from "react-router-dom";
import paths from "../routes/paths";

const useUiNavigation = () => {
   const navigate = useNavigate();

   const navigateToHome = () => navigate(paths.home);
   const navigateToLogin = () => navigate(paths.login);
   const navigateToSignup = () => navigate(paths.Signup);
   const navigateToCart = () => navigate(paths.cart);
   const navigateToVerification = () => navigate(paths.verification);
   const navigateToProfile = () => navigate(paths.profile);

   return {
      navigateToHome,
      navigateToLogin,
      navigateToSignup,
      navigateToCart,
      navigateToVerification,
      navigateToProfile,
   };
};

export default useUiNavigation;
