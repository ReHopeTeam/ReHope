import secureLocalStorage from "react-secure-storage";
import { erro } from "@/utils/toast";
import { api } from "./api";
import { jwtDecode } from "jwt-decode";

interface Token {
  id: string;
  nome: string;
  email: string;
}

export async function login(email: string, senha: string) {
  try {
    const response = await api.post("autenticacao/login", { email, senha });
    const token = response.data.token;

    secureLocalStorage.setItem("Token", token);
  } catch (error: any) {
    throw erro("Email ou senha inválidos");
  }
}

export async function logout() {
  try {
    secureLocalStorage.removeItem("Token");
  } catch (error: any) {
    throw erro("Erro ao sair da conta");
  }
}

export function obterUsuarioAutenticado(): Token | null {
  try {
    const token = secureLocalStorage.getItem("Token");
    
    if (token && typeof token === "string") {
      const decoded = jwtDecode<any>(token);

      const usuario: Token = {
        id: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/nameidentifier"],
        nome: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
      };

      return usuario;
    }
  } catch (error) {
    console.error("Erro ao decodificar o token de sessão:", error);
  }
  return null;
}