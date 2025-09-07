import axios from "axios";
import { backendUrl } from "./constants/apiConstants";

export const axiosInstance = axios.create({
  baseURL: backendUrl,
});
