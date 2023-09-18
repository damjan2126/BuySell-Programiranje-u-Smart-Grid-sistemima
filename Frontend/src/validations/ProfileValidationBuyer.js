import * as yup from "yup";

export const ProfileValidationBuyer = yup.object().shape({
   firstName: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("First name can't be empty"),
   lastName: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Last name can't be empty"),
   dateOfBirth: yup.date().required("Date of birth can't be empty"),
   address: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Address can't be empty"),
});
