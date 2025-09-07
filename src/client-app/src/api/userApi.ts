import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { backendUrl } from "../constants/apiConstants";
import { IUser } from "../types/user";

export const userApi = createApi({
  reducerPath: "userApi",
  baseQuery: fetchBaseQuery({
    baseUrl: `${backendUrl}/users`,
    credentials: "include",
  }),
  tagTypes: ["Users", "User"],
  endpoints: (build) => ({
    fetchAllUsers: build.query<IUser[], null>({
      query: () => ({
        url: "/",
      }),
      providesTags: ["Users"],
    }),
    fetchUserById: build.query<IUser, string>({
      query: (id) => ({
        url: `/${id}`,
      }),
      providesTags: ["User"],
    }),
    updateUser: build.mutation<
      null,
      { id: string; username: string; email: string }
    >({
      query: (request) => ({
        url: `/${request.id}`,
        method: "PUT",
        body: { username: request.username, email: request.email },
      }),
      invalidatesTags: ["Users", "User"],
    }),
    deleteUser: build.mutation<null, string>({
      query: (id) => ({
        url: `/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Users", "User"],
    }),
  }),
});
