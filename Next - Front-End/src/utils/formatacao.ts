//? ===================================
//? Transformar preços em BRl (Reais).
//? ===================================
export function formatarPreco(valor: number) {
  return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

export const desformatarPreco = (precoStr: any): number => {
  if (typeof precoStr === "number") {
    return precoStr;
  }

  if (!precoStr || typeof precoStr !== "string") {
    return 0;
  }

  let limpo = precoStr.replace(/R\$\s?/, "").trim();
  limpo = limpo.replace(/\./g, "");
  limpo = limpo.replace(",", ".");

  const resultado = parseFloat(limpo);
  return isNaN(resultado) ? 0 : resultado;
};
