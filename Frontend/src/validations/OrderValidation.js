import * as yup from "yup";

export const OrderValidation = yup.object().shape({
   address: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Address can't be empty"),
   comment: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Comment can't be empty"),
});
