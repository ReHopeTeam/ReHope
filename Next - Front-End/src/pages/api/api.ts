import axios from "axios";
import secureLocalStorage from "react-secure-storage";
import { jwtDecode } from "jwt-decode";

const apiLocal = "https://localhost:7299/api/";

const apiRemota = "";

export const api = axios.create({
  baseURL: apiLocal,
});

api.interceptors.request.use((config) => {
  const token = secureLocalStorage.getItem("Token");

  
  if (token) {
    config.headers.Authorization = "Bearer " + token;

    try {
      if (typeof token === "string") {
        jwtDecode(token);
      }
    } catch (error) {
      console.error("Token inválido ou malformatado", error);
    }
  }

  return config;
});
