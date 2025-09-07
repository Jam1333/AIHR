import { IProblemDetails } from "../types/problemDetails";

export function toProblemDetails(error: any): IProblemDetails {
  if ("response" in error) {
    error = error.response;
  }

  if (!("status" in error) || !("data" in error)) {
    return defaultProblemDetails;
  }

  if (!isProblemDetails(error.data)) {
    return {
      ...defaultProblemDetails,
      status: error.status,
    };
  }

  return error.data as IProblemDetails;
}

const defaultProblemDetails: IProblemDetails = {
  status: 500,
  title: "Server Failure",
  type: "https://tools.ietf.org/html/rfc7231#section-6.6.1",
  detail: "Server Failure",
};

export function isProblemDetails(error: any) {
  if (!(error instanceof Object)) {
    return false;
  }

  return (
    "status" in error &&
    "title" in error &&
    "type" in error &&
    "detail" in error
  );
}


