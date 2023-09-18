import * as yup from "yup";

export const LoginValidation = yup.object().shape({
	username: yup
		.string()
		.min(5, "Length can't be lower than 2 characters")
		.max(35, "Length can't be more than 15 characters")
		.required("Username can't be empty"),
	password: yup
		.string()
		.min(4, "Password must be at least 4 characters")
		.max(30, "Password can't be more than 30 characters")
		.required("Password is required"),
});
