import React from "react";
import { SingleUsersContainer } from "./AlllUsers.styled";
import { useTheme } from "styled-components";
import { useNavigate } from "react-router-dom";
import { AiOutlineStop } from "react-icons/ai";

const SingleUser = (props) => {
   const navigate = useNavigate();
   const theme = useTheme();
   return (
      <SingleUsersContainer onClick={() => navigate(`/user/${props.id}`)}>
         <div style={{ display: "flex", justifyContent: "space-between", marginBottom: "20px" }}>
            <div className="id-user">ID: {props.id}</div>
            <div
               style={{
                  color: props.isActive && theme.tabActive,
                  fontSize: "11px",
                  display: "flex",
                  alignItems: "center",
                  marginRight: "5px",
                  textDecoration: props.isActive && "underline",
               }}
            >
               {props.isActive ? (
                  "Active"
               ) : (
                  <>
                     Not active
                     <AiOutlineStop fill="red" style={{position:"relative",top:"1px",marginLeft:"5px"}} />
                  </>
               )}
            </div>
         </div>

         <div
            className="user-info"
            style={{ display: "flex", flexWrap: "wrap", justifyContent: "space-between", fontSize: "13px" }}
         >
            <div>First name: {props.firstName}</div>
            <div>Last name: {props.lastName}</div>
            <div>Address: {props.address || "No address"}</div>
         </div>

         <div
            className="user-info-2"
            style={{ display: "flex", flexWrap: "wrap", justifyContent: "space-between", fontSize: "13px" }}
         >
            <div>Email: {props.email}</div>
            <div>Data of birth: {new Date(props.dateOfBirth).toLocaleString()}</div>
         </div>
         <div
            className="user-info-2"
            style={{ display: "flex", flexWrap: "wrap", justifyContent: "space-between", fontSize: "13px" }}
         >
            <div>Created at: {new Date(props.createdAtUtc).toLocaleString()}</div>
            <div>Last login date: {new Date(props.lastLoginDate).toLocaleString()}</div>
         </div>
      </SingleUsersContainer>
   );
};

export default SingleUser;
