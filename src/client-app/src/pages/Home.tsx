import { useEffect } from "react";
import { axiosInstance } from "../axiosInstance";

export const Home = () => {
  useEffect(() => {
    axiosInstance.get<string>("/ping").then((data) => console.log(data));
  }, []);

  return (
    <>
      <div className="font-bold text-2xl">Home</div>
      <div>{axiosInstance.getUri()}</div>
    </>
  );
};
