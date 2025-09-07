import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { backendUrl } from "../constants/apiConstants";

export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery: fetchBaseQuery({
    baseUrl: `${backendUrl}/auth`,
    credentials: "include",
  }),
  endpoints: (build) => ({
    registerUser: build.mutation<
      string,
      { username: string; email: string; password: string }
    >({
      query: (request) => ({
        url: "/register",
        method: "POST",
        body: request,
      }),
    }),
    loginUser: build.mutation<null, { email: string; password: string }>({
      query: (request) => ({
        url: "/login",
        method: "POST",
        body: request,
      }),
    }),
    logoutUser: build.mutation<null, null>({
      query: () => ({
        url: "/logout",
        method: "POST",
      }),
    }),
  }),
});
