import React from "react";
import { VerificationContainer } from "./Verification.styled";
import { get_pending_sellers } from "../../services/services";
import useCustomQuery from "../../hooks/useCustomQuery";
import VerificationSingle from "./VerificationSingle";

const Verification = () => {
   const { data, refetch, isFetched } = useCustomQuery("users-status", get_pending_sellers);

   const mappedData = data?.data?.map((item) => <VerificationSingle refetch={refetch} key={item.user.id} {...item} />);

   return (
      <VerificationContainer>
         {mappedData}
         {isFetched && data.data.length === 0 && <div style={{ textAlign: "center" }}>No users left to verify</div>}
      </VerificationContainer>
   );
};

export default Verification;
