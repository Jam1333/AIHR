import { Navigate } from "react-router-dom";

export const RedirectToRegistration = () => {
  return <Navigate to={"/registration"} replace />;
};
