import "@/styles/globals.css";
import type { AppProps } from "next/app";
import { Quicksand, Comfortaa } from "next/font/google";
import { useRouter } from "next/router";
import { useEffect, useRef } from "react";
import { ToastContainer } from "react-toastify";

const quicksand = Quicksand({
  variable: "--font-quicksand",
  weight: ["300", "400", "500", "600"],
  subsets: ["latin"],
});

const comfortaa = Comfortaa({
  variable: "--font-comfortaa",
  weight: ["400", "600"],
  subsets: ["latin"],
});

export default function App({ Component, pageProps }: AppProps) {
  const router = useRouter();

  const resumeNavigationRef = useRef<() => void>(null);

  useEffect(() => {
    const savedTheme = localStorage.getItem("theme");
    const prefersDark = window.matchMedia(
      "(prefers-color-scheme: dark)",
    ).matches;
    const root = document.documentElement;

    if (savedTheme === "dark" || (!savedTheme && prefersDark)) {
      root.classList.add("darkmode");
    } else {
      root.classList.remove("darkmode");
    }
  }, []);

  useEffect(() => {
    const handleRouteChangeStart = () => {
      if (!document.startViewTransition) return;

      const transitionPromise = new Promise<void>((resolve) => {
        resumeNavigationRef.current = resolve;
      });

      document.startViewTransition(() => transitionPromise);
    };

    const handleRouteChangeComplete = () => {
      if (resumeNavigationRef.current) {
        resumeNavigationRef.current();
        resumeNavigationRef.current = null;
      }
    };

    router.events.on("routeChangeStart", handleRouteChangeStart);
    router.events.on("routeChangeComplete", handleRouteChangeComplete);

    return () => {
      router.events.off("routeChangeStart", handleRouteChangeStart);
      router.events.off("routeChangeComplete", handleRouteChangeComplete);
    };
  }, [router]);

  return (
    <main className={`${quicksand.variable} ${comfortaa.variable}`}>
      <ToastContainer />
      <Component {...pageProps} />
    </main>
  );
}
