import { Navigate } from "react-router-dom";

export const RedirectToHome = () => {
  return <Navigate to={"/"} replace />;
};
