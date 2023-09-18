import React from "react";
import { AiOutlineUser } from "react-icons/ai";
import { UserIconHolder, UserText, UserIcon as Uicon } from "../Header.styles";

const UserIcon = () => {
	return (
		<UserIconHolder>
			<UserText>My Profile</UserText>
			<Uicon>
				<AiOutlineUser />
			</Uicon>
		</UserIconHolder>
	);
};

export default UserIcon;
