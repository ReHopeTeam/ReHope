import { useEffect, useState } from "react";

type TrocaTemaProps = {
  className?: string;
};

export function TrocaTema({ className }: TrocaTemaProps) {
  const [isDark, setIsDark] = useState(false);

  // 1. Carrega o tema salvo assim que o componente é montado
  useEffect(() => {
    const root = document.documentElement;
    const temaSalvo = localStorage.getItem("theme");

    // Verifica se há tema salvo OU se o sistema do usuário prefere darkmode
    const prefereDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

    if (temaSalvo === "dark" || (!temaSalvo && prefereDark)) {
      root.classList.add("darkmode");
      setIsDark(true);
    } else {
      root.classList.remove("darkmode");
      setIsDark(false);
    }
  }, []);

  // 2. Alterna o tema ao clicar
  const trocaTema = () => {
    const root = document.documentElement;

    if (root.classList.contains("darkmode")) {
      root.classList.remove("darkmode");
      localStorage.setItem("theme", "light");
      setIsDark(false);
    } else {
      root.classList.add("darkmode");
      localStorage.setItem("theme", "dark");
      setIsDark(true);
    }
  };

  return (
    <button onClick={trocaTema} className={className} aria-label="Alternar tema">
      {isDark ? (
        <svg width="100" height="100" viewBox="0 0 100 100" fill="none" xmlns="http://www.w3.org/2000/svg" className="iconLua">
          <path fillRule="evenodd" clipRule="evenodd" d="M95 50C95 74.8528 74.8528 95 50 95C25.1472 95 5 74.8528 5 50C5 25.1472 25.1472 5 50 5C50 41 59 50 95 50Z" stroke="white" strokeWidth="10" />
        </svg>
      ) : (
        <svg width="185" height="185" viewBox="0 0 185 185" fill="none" xmlns="http://www.w3.org/2000/svg" className="iconSol">
          <path d="M92.5 5V30M154.372 30.6282L136.694 48.3058M180 92.5L155 92.5M154.372 154.372L136.694 136.694M92.5 180V155M30.6281 154.372L48.3058 136.694M5 92.5H30M30.6282 30.6281L48.3058 48.3058M137.5 93C137.5 117.853 117.353 138 92.5 138C67.6472 138 47.5 117.853 47.5 93C47.5 68.1472 67.6472 48 92.5 48C117.353 48 137.5 68.1472 137.5 93Z" stroke="white" strokeWidth="10" strokeLinecap="round" />
        </svg>
      )}
    </button>
  );
}