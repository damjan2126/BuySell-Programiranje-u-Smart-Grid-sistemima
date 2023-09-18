import * as yup from "yup";

export const AddItemValidation = yup.object().shape({
   name: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Name can't be empty"),
   price: yup
      .number()
      .min(0, "Price must be at least 4 characters")
      .max(1000000, "Price can't be more than 1000000")
      .required("Price is required"),
   ammount: yup
      .number()
      .min(0, "Ammount must be at least 0 characters")
      .max(1000000, "Ammount can't be more than 1000000")
      .required("Ammount is required"),
   description: yup
      .string()
      .min(10, "Description must be at least 10 characters")
      .max(130, "Description can't be more than 130 characters")
      .required("Description is required"),
   // imageUrl: yup.string().required("Password is required"),
});
