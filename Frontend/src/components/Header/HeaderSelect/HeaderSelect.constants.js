import paths from "../../../routes/paths";

export const options = [
   { value: "profile", label: "Profile" },
   { value: "orders", label: "My orders" },
   { value: paths.users, label: "See all users" },
   { value: "fullscreen", label: "Fullscreen" },
   { value: "logout", label: "Logout" },
];

export const selectStyles = {
   control: (styles) => ({
      ...styles,
      border: 0,
      borderRadius: "4px",
      backgroundColor: "transparent",
      color: "white",
      cursor: "pointer",
      flexWrap: "nowrap",
      justifyContent: "space-between",
      maxWidth: "300px",
      right: "0px",
      boxShadow: "none",
   }),
   singleValue: (styles) => ({
      ...styles,
      fontSize: "16px",
      margin: 0,
      color: "white",
      cursor: "pointer",
   }),
   indicatorSeparator: (styles) => ({ ...styles, display: "none" }),
   menu: (styles) => ({ ...styles, width: "100%", zIndex: 200, border: 0 }),
   menuList: (styles) => ({
      ...styles,
      padding: 0,
      border: 0,
      borderRadius: "4px",
      boxShadow: "0px 0px 10px rgba(0, 0, 0, 0.05), 0px 1px 1px rgba(0, 0, 0, 0.04)",
      zIndex: 200,
   }),
   option: (styles) => ({
      ...styles,
      padding: "0 6px",
      border: 0,
      fontSize: "16px",
      minHeight: "40px",
      cursor: "pointer",

      display: "flex",
      alignItems: "center",
      justifyContent: "center",
      backgroundColor: "rgba(204, 204, 204, 0.12)",
      color: "black",
      zIndex: 200,
      ":active": {
         ...styles[":active"],
         backgroundColor: "rgba(204, 204, 204, 0.12)",
      },
      ":hover": {
         ...styles[":active"],
         backgroundColor: "gray",
         color: "white",
      },
   }),
};
