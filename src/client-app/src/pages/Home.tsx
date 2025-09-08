import { useEffect } from "react";
import { axiosInstance } from "../axiosInstance";
import { HeaderComponent } from "../UI/HeaderComponent";
import { ErrorComponent } from "../UI/ErrorComponent";

export const Home = () => {
  useEffect(() => {
    axiosInstance.get<string>("/ping").then((data) => console.log(data));
  }, []);

  return (
    <>
      <HeaderComponent />
      <div className="font-bold text-2xl">Home</div>
      <div>{axiosInstance.getUri()}</div>
    </>
  );
};
