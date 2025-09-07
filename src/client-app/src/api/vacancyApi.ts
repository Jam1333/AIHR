import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { backendUrl } from "../constants/apiConstants";
import { IVacancy } from "../types/vacancy";
import { appendArrayToFormData } from "../utils/appendArrayToFormData";

export const vacancyApi = createApi({
  reducerPath: "vacancyApi",
  baseQuery: fetchBaseQuery({
    baseUrl: `${backendUrl}/vacancies`,
    credentials: "include",
  }),
  tagTypes: ["Vacancies", "Vacancy"],
  endpoints: (build) => ({
    fetchAllVacancies: build.query<IVacancy[], { userId: string }>({
      query: (request) => ({
        url: "/",
        params: {
          userId: request.userId,
        },
      }),
      providesTags: ["Vacancies"],
    }),
    fetchVacancyById: build.query<IVacancy, string>({
      query: (id) => ({
        url: `/${id}`,
      }),
      providesTags: ["Vacancy"],
    }),
    createVacancy: build.mutation<
      string,
      {
        title: string;
        language: string;
        categories: string[];
        file: File;
      }
    >({
      query: (request) => {
        const formData = new FormData();

        formData.append("title", request.title);
        formData.append("language", request.language);
        appendArrayToFormData(formData, request.categories, "categories");
        formData.append("file", request.file);

        return {
          url: "/",
          method: "POST",
          body: formData,
        };
      },
      invalidatesTags: ["Vacancies"],
    }),
    deleteVacancy: build.mutation<null, string>({
      query: (id) => ({
        url: `/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: ["Vacancies", "Vacancy"],
    }),
  }),
});
