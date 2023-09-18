import axios from "axios";

const instance = axios.create({
   baseURL: "http://localhost:5018",
});

instance.interceptors.request.use(
   // This is for later implementation
   (config) => {
      if (localStorage.token) {
         config.headers.Authorization = `Bearer ${JSON.parse(localStorage.token)}`;
      }
      return config;
   }
);

instance.interceptors.response.use(
   (response) => {
      return response;
   },
   (error) => {
      return Promise.reject(error);
   }
);

export default instance;
