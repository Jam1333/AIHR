import { Navigate } from "react-router-dom";

export const RedirectToLogin = () => {
  return <Navigate to={"/login"} replace />;
};