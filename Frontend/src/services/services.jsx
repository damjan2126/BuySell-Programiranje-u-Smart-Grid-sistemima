// This is for later implementation
// We sould put our axios instance fetch functions here
// And only return a promise from those functions

import instance from "./instance";

// Then we pass those functions (which return a promise)
// to react-query and thats it.

export const login = (data) =>
   instance.post("/Users/authenticate", {
      password: data.password,
      userName: data.username,
   });

export const signup = (data) =>
   instance.post("/Users", {
      role: data.role,
      imageUrl: data.imageUrl,
      address: data.address,
      dateOfBirth: data.dateOfBirth,
      password: data.password,
      email: data.email,
      userName: data.userName,
      lastName: data.lastName,
      firstname: data.firstname,
   });

export const get_items = (userid) =>
   instance.get("/Item", {
      params: {
         CreatedByUserId: userid,
      },
   });

export const get_me = () => instance.get("/Users/Me");
export const get_pending_sellers = () => instance.get("/getPendingSellers");
export const pending_sellers_approve = (userId) => instance.post(`/Users/${userId}/approve?userId=${userId}`);
export const pending_sellers_reject = (userId) => instance.post(`/Users/${userId}/reject?userId=${userId}`);

export const get_all_users = () => instance.get("/Users");
export const get_user_with_id = (id) => instance.get(`/Users/${id}`);

export const Add_Item = (data) => instance.post("/Item", data);
export const Edit_Item = (id, data) => instance.post(`/Item/${id}`, { ...data });

export const profile_change = (id, data) => instance.put(`/Users/${id}`, { ...data });
export const add_order = (data) => instance.post(`/Order/addItem`, { ...data });

export const remove_order = (id) => instance.post(`/Order/removeItem`, { items: [{ id }] });

export const get_orders = (id, status, SellerId) =>
   instance.get(`/Order/All`, {
      params: {
         CreatedByUserId: id,
         status,
         SellerId,
      },
   });

export const get_order_state = (id) => instance.patch(`/Order/${id}/state`);

export const addIMG = () => instance.post(`/Images`);
