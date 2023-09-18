import React from "react";
import { VerificationItem, VerificationItemLeft, VerificationItemRight } from "./Verification.styled";
import Button from "../../components/Button/Button";
import ButtonPrimary from "../../components/Button/ButtonPrimary";
import { useMutation } from "react-query";
import { pending_sellers_approve, pending_sellers_reject } from "../../services/services";
import { toast } from "react-toastify";

const VerificationSingle = (props) => {
   const { mutate: approve } = useMutation(pending_sellers_approve, {
      onError: () => {
         toast.error("Error on Approval");
      },
      onSuccess: (data) => {
         toast.success("Successfully approved a user");
         props.refetch();
      },
   });
   const { mutate: reject } = useMutation(pending_sellers_reject, {
      onError: () => {
         toast.error("Error on Rejct");
      },
      onSuccess: () => {
         toast.success("Successfully rejected a user");
         props.refetch();
      },
   });

   return (
      <VerificationItem>
         <VerificationItemLeft>
            ID: {props.user.id}
            <br />
            <br />
            {props.user.firstName} {props.user.lastName} - {props.user.email}
         </VerificationItemLeft>
         <VerificationItemRight>
            <Button onClick={() => approve(props.user.id)} canShrink>
               Approve
            </Button>
            <ButtonPrimary onClick={() => reject(props.user.id)} style={{ margin: "0px 5px" }} canShrink>
               Reject
            </ButtonPrimary>
         </VerificationItemRight>
      </VerificationItem>
   );
};

export default VerificationSingle;
