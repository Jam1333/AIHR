import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { backendUrl } from "../constants/apiConstants";
import { IInterview } from "../types/interview";
import { appendRecordToFormData } from "../utils/appendRecordToFormData";

export const interviewApi = createApi({
  reducerPath: "interviewApi",
  baseQuery: fetchBaseQuery({
    baseUrl: `${backendUrl}/interviews`,
    credentials: "include",
  }),
  tagTypes: ["Interviews", "Interview"],
  endpoints: (build) => ({
    fetchAllInterviews: build.query<IInterview[], { vacancyId: string }>({
      query: (request) => ({
        url: "/",
        params: {
          vacancyId: request.vacancyId,
        },
      }),
      providesTags: ["Interviews"],
    }),
    fetchInterviewById: build.query<IInterview, string>({
      query: (id) => ({
        url: `/${id}`,
      }),
      providesTags: ["Interview"],
    }),
    createInterview: build.mutation<
      string,
      {
        title: string;
        weights: Record<string, number>;
        resumeFile: File;
        maxMessagesCount: number;
        vacancyId: string;
      }
    >({
      query: (request) => {
        const formData = new FormData();

        formData.append("title", request.title);
        appendRecordToFormData(formData, request.weights, "weights");
        formData.append("resumeFile", request.resumeFile);
        formData.append(
          "maxMessagesCount",
          request.maxMessagesCount.toString()
        );
        formData.append("vacancyId", request.vacancyId);

        return {
          url: "/",
          method: "POST",
          body: formData,
        };
      },
      invalidatesTags: ["Interviews"],
    }),
    provideInterviewContactInformation: build.mutation<
      null,
      { id: string; email: string; phoneNumber: string }
    >({
      query: (request) => ({
        url: `/${request.id}/contact-information`,
        method: "POST",
        body: {
          email: request.email,
          phoneNumber: request.phoneNumber,
        },
      }),
      invalidatesTags: ["Interviews", "Interview"],
    }),
    answerInterviewLastQuestion: build.mutation<
      null,
      { id: string; answer: string }
    >({
      query: (request) => ({
        url: `/${request.id}/answer-question`,
        method: "POST",
        body: {
          answer: request.answer,
        },
      }),
      invalidatesTags: ["Interview"],
    }),
    generateInterviewConclusion: build.mutation<null, string>({
      query: (id) => ({
        url: `/${id}/generate-conclusion`,
        method: "POST",
      }),
      invalidatesTags: ["Interview"],
    }),
    generateInterviewResult: build.mutation<null, string>({
      query: (id) => ({
        url: `/${id}/generate-result`,
        method: "POST",
      }),
      invalidatesTags: ["Interview"],
    }),
  }),
});
