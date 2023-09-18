import React from "react";
import { useController } from "react-hook-form";
import { InputFieldContainer, InputFieldError } from "./Input.styled";
import { Input } from "./Input.styled";

const YupInput = (props) => {
	const { field, fieldState } = useController(props);
	const { type, placeholder, errorBorder } = props;
	const errorMessage = fieldState.error?.message;

	return (
		<InputFieldContainer>
			<Input
				type={type || "text"}
				min={type === "number" ? 0 : ""}
				$hasError={errorBorder && !!errorMessage}
				onBlur={field.onBlur}
				onChange={field.onChange}
				name={field.name}
				defaultValue={field.value}
				placeholder={placeholder || "Default placeholder"}
				autoComplete={type === "email" ? "true" : "false"}
			/>
			<InputFieldError>{errorMessage}</InputFieldError>
		</InputFieldContainer>
	);
};

export default YupInput;
