import styles from "./login.module.css";
import { useRouter } from "next/router";
import { useState, useEffect } from "react";
import { login, logout, obterUsuarioAutenticado } from "../api/authService";
import { erro, notificacao } from "@/utils/toast";
import Lucide from "@/utils/lucide";
import Button from "@/components/button/button";
import { TrocaTema } from "@/utils/trocaTema";
import Link from "next/link";

interface Token {
  nome: string;
}

const Login = () => {
  const [usuario, setUsuario] = useState<Token | null>(null);
  const [estaAutenticado, setEstaAutenticado] = useState(false);
  const [email, setEmail] = useState<string>("");
  const [senha, setSenha] = useState<string>("");
  const [ativo, setAtivo] = useState<boolean>(false);

  const imagens = [
    "/imgs/ImagemDoLogin.png",
    "/imgs/ImagemDoLogin2.png",
    "/imgs/ImagemDoLogin3.png",
  ];

  const [imagemAtual, setImagemAtual] = useState(0);

  const handleLogout = async () => {
    await logout();
    setUsuario(null);
    setEstaAutenticado(false);
    router.push("/login");
  };

  useEffect(() => {
    const interval = setInterval(() => {
      setImagemAtual((prev) => (prev + 1) % imagens.length);
    }, 10000);

    return () => clearInterval(interval);
  }, []);

  useEffect(() => {
    const dados = obterUsuarioAutenticado();

    if (dados) {
      setUsuario(dados);
      setEstaAutenticado(true);
    } else {
      setEstaAutenticado(false);
      setUsuario(null);
    }
  }, []);

  const router = useRouter();

  async function autenticar(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    try {
      await login(email, senha);
      notificacao("Login bem sucedido!");
      router.push("/home");
    } catch (error: any) {
      const mensagemErro =
        error.response?.data || error.message || "Erro ao fazer login";
      erro(mensagemErro);
    }
  }

  return (
    <main id={styles.login}>
      {/* SVG de Background Superior */}
      <svg
        width="660"
        height="300"
        viewBox="0 0 660 300"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
        className={styles.login_svg}
        style={{ top: "0", left: "0" }}
      >
        <path
          fillRule="evenodd"
          clipRule="evenodd"
          d="M-3.57628e-05 291.348C35.4471 306.36 67.5363 300.369 96.6246 284.609C122.304 270.697 146.993 248.334 170.454 227.083C174.173 223.715 177.861 220.375 181.517 217.1C209.405 192.124 237.023 169.32 268.381 156.214C298.845 143.482 333.578 139.631 376.494 154.578C477.351 189.706 549.227 173.359 596.123 134.16C641.695 96.0657 660 39.2759 660 -1.68808e-06C650.825 -1.68808e-06 623.344 -6.95761e-05 617.419 -6.95761e-05C616.35 27.5325 602.401 71.9349 569.522 99.4184C536.579 126.956 480.713 143.979 389.958 112.37C337.067 93.9492 291.961 98.4398 252.567 114.905C214.066 130.996 181.804 158.244 153.805 183.32C149.803 186.905 145.908 190.422 142.104 193.858C142.097 193.864 142.09 193.871 142.083 193.877C118.173 215.473 97.8463 233.831 77.0235 245.113C54.8034 257.151 34.1441 259.914 9.52032 247.049C6.18032 245.304 3.00665 243.317 -3.57628e-05 241.095V291.348Z"
          className="fill"
        />
      </svg>

      {/* SVG de Background Inferior */}
      <svg
        width="660"
        height="300"
        viewBox="0 0 660 300"
        fill="none"
        xmlns="http://www.w3.org/2000/svg"
        className={styles.login_svg}
        style={{ bottom: "0", right: "0" }}
      >
        <path
          fillRule="evenodd"
          clipRule="evenodd"
          d="M660 8.65239C624.553 -6.36049 592.464 -0.368699 563.375 15.3907C537.696 29.3032 513.007 51.6664 489.546 72.9166C485.827 76.2849 482.139 79.6253 478.483 82.8996C450.595 107.876 422.977 130.68 391.619 143.786C361.155 156.518 326.422 160.369 283.506 145.422C182.649 110.294 110.773 126.641 63.8772 165.84C18.3049 203.934 0 260.724 0 300C9.17545 300 36.6562 300 42.5806 300C43.6505 272.468 57.5986 228.065 90.4776 200.582C123.421 173.044 179.287 156.021 270.042 187.63C322.933 206.051 368.039 201.56 407.433 185.095C445.934 169.004 478.196 141.756 506.195 116.68C510.197 113.095 514.092 109.578 517.896 106.142C517.903 106.136 517.91 106.129 517.917 106.123C541.827 84.5274 562.154 66.1687 582.976 54.8874C605.197 42.849 625.856 40.0864 650.48 52.9507C653.82 54.6957 656.993 56.6828 660 58.9049V8.65239Z"
          className="fill"
        />
      </svg>

      <div className={styles.img_wrapper}>
        {imagens.map((imagem, index) => (
          <img
            key={imagem}
            src={imagem}
            alt="Imagem do Bazar ReHope"
            className={`${styles.login_img} ${
              index === imagemAtual ? styles.ativa : ""
            }`}
          />
        ))}
      </div>

      <section id={styles.login_form} className="column">
        <div id={styles.tema_btn}>
          <TrocaTema />
        </div>

        <form className="form" id={styles.form} onSubmit={autenticar}>
          <img
            className="img"
            id={styles.img}
            src="/imgs/LogoBranca.svg"
            alt="Logo do site"
          />
          <h1 className="h1" id={styles.h1}>
            Login
          </h1>

          {/* Campo de E-mail */}
          <div className="campo_form">
            <Lucide name="Mail" className="lucide" />
            <input
              type="email"
              id="email"
              placeholder=" "
              className="input"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <label htmlFor="email" className="label">
              Email
            </label>
          </div>

          {/* Campo de Senha */}
          <div className="campo_form">
            <Lucide name="Lock" className="lucide" />
            <input
              type={ativo ? "text" : "password"}
              id="senha"
              placeholder=" "
              className="input"
              value={senha}
              onChange={(e) => setSenha(e.target.value)}
              required
            />
            <label htmlFor="senha" className="label">
              Senha
            </label>

            <button
              type="button"
              className="btn_icon"
              onClick={() => setAtivo(!ativo)}
            >
              <Lucide
                name={ativo ? "EyeOff" : "Eye"}
                className="reset_lucide"
              />
            </button>
          </div>

          <Button type="submit" className="btn">
            Entrar
          </Button>
          {estaAutenticado && (
            <div className="column" id={styles.voltar}>
              <p className="p">
                Você já está logado como{" "}
                <span className="p">{usuario?.nome}</span>
                <br />
                Deseja voltar para tela principal?
              </p>
              <div className="row">
                <Link href="/home" className="btn2">
                  Voltar
                </Link>
                <Button onClick={handleLogout}>Logout</Button>
              </div>
            </div>
          )}
        </form>
      </section>
    </main>
  );
};

export default Login;
