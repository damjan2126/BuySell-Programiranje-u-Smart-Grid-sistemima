import { useQuery } from "react-query";
import { toast } from "react-toastify";

const useCustomQuery = (key, query_func, settings) => {
   return useQuery(key, query_func, {
      refetchOnWindowFocus: false,
      onError: (error) => {
         toast.error(`${error?.response?.data?.title || "Error, Bad Request"}`);
         // toast.error(`Code: ${error.code}`);
      },
      ...settings,
   });
};

export default useCustomQuery;
