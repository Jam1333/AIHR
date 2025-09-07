import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { backendUrl } from "../constants/apiConstants";
import { appendArrayToFormData } from "../utils/appendArrayToFormData";
import { IAnalysis } from "../types/analysis";
import { appendRecordToFormData } from "../utils/appendRecordToFormData";

export const analysisApi = createApi({
  reducerPath: "analysisApi",
  baseQuery: fetchBaseQuery({
    baseUrl: `${backendUrl}/analyses`,
    credentials: "include",
  }),
  tagTypes: ["Analyses", "Analysis"],
  endpoints: (build) => ({
    fetchAllAnalyses: build.query<IAnalysis[], { vacancyId: string }>({
      query: (request) => ({
        url: "/",
        params: {
          vacancyId: request.vacancyId,
        },
      }),
      providesTags: ["Analyses"],
    }),
    fetchAnalysisById: build.query<IAnalysis, string>({
      query: (id) => ({
        url: `/${id}`,
      }),
      providesTags: ["Analysis"],
    }),
    createAnalysis: build.mutation<
      string,
      {
        title: string;
        weights: Record<string, number>;
        vacancyId: string;
        files: File[];
      }
    >({
      query: (request) => {
        const formData = new FormData();

        formData.append("title", request.title);
        appendRecordToFormData(formData, request.weights, "weights");
        formData.append("vacancyId", request.vacancyId);
        appendArrayToFormData(formData, request.files, "files");

        return {
          url: "/",
          method: "POST",
          body: formData,
        };
      },
      invalidatesTags: ["Analyses"],
    }),
  }),
});
