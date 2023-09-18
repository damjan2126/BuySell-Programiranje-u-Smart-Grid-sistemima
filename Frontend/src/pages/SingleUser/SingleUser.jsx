import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import useCustomQuery from "../../hooks/useCustomQuery";
import { get_user_with_id } from "../../services/services";
const SingleUser = () => {
   const { id } = useParams();

   const { data: user } = useCustomQuery(`User/${id}`, () => get_user_with_id(id));
   const [myUser, setMyUser] = useState({});

   useEffect(() => {
      if (user?.data) setMyUser(user?.data);
   }, [user]);

   // const changeUserHandle = (e) => {
   //    setMyUser((prev) => ({ ...prev, [e.target.name]: e.target.value }));
   // };

   const allRoles = myUser?.roles?.map((item) => (
      <span key={item} style={{ margin: "5px" }}>
         {item}
      </span>
   ));

   return (
      <div style={{ width: "500px", maxWidth: "100%", textAlign: "center", margin: "40px auto" }}>
         <div
            style={{
               width: "150px",
               backgroundColor: "white",
               borde: "1px solid gray",
               aspectRatio: "1/1",
               borderRadius: "50%",
               margin: "0 auto",
               overflow: "hidden",
            }}
         >
            <img
               src={myUser.imageUrl}
               style={{ width: "100%", height: "100%", display: "block", objectFit: "conver" }}
               alt="user_avatar"
            />
         </div>
         <div style={{ display: "inline-block", margin: "20px 10px" }}>{myUser.firstName}</div>
         <div style={{ display: "inline-block", margin: "20px 10px" }}>{myUser.lastName}</div>
         <div>User roles: {allRoles}</div>
      </div>
   );
};

export default SingleUser;
