import * as yup from "yup";

export const SignupValidation = yup.object().shape({
   firstname: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("First name can't be empty"),
   lastName: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Last name can't be empty"),
   userName: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Username can't be empty"),
   email: yup
      .string()
      .email("Must be a valid email")
      .min(5, "Length can't be lower than 5 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Email can't be empty"),
   password: yup
      .string()
      .required("Šifra je obavezna!")
      .matches(/[A-Z]/, "Šifra mora da sadrži najmanje jedno veliko slovo.")
      .matches(/[!@#$%^&*(),.?":{}|<>]/, "Šifra mora da sadrži najmanje jedan simbol.")
      .matches(/[0-9]/, "Šifra mora da sadrži najmanje jedan broj."),
   dateOfBirth: yup.date().required("Date of birth can't be empty"),
   address: yup
      .string()
      .min(2, "Length can't be lower than 2 characters")
      .max(35, "Length can't be more than 35 characters")
      .required("Address can't be empty"),
});
