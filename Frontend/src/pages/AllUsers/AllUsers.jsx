import React from "react";
import { get_all_users } from "../../services/services";
import useCustomQuery from "../../hooks/useCustomQuery";
import SingleUser from "./SingleUser";
import { AllUsersContainer } from "./AlllUsers.styled";

const AllUsers = () => {
   const { data: all_users } = useCustomQuery("Users", get_all_users);

   const mappedAllUsers = all_users?.data?.users.map((el) => {
      return <SingleUser key={el.id} {...el} />;
   });

   return <AllUsersContainer>{mappedAllUsers}</AllUsersContainer>;
};

export default AllUsers;
