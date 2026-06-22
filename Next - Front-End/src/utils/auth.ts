import secureLocalStorage from "react-secure-storage";

export function verificarAutenticacao()
{
    const token = secureLocalStorage.getItem("Token");
    
    return !!token;

    //? Token passa a ser booleano. Se existir informação dentro do token, ele retornar TRUE.
    //? Caso contrário, retorna FALSE.
}