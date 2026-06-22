import Link from "next/link";
import styles from "./header.module.css";
import { useRouter } from "next/router";
import { TrocaTema } from "@/utils/trocaTema";
import { useEffect, useState } from "react";
import { logout, obterUsuarioAutenticado } from "@/pages/api/authService";
import Button from "../button/button";
import { createPortal } from "react-dom";
import Lucide from "@/utils/lucide";

interface Token {
  id: string;
  nome: string;
  email: string;
}

const Header = () => {
  const [usuario, setUsuario] = useState<Token | null>(null);
  const [estaAutenticado, setEstaAutenticado] = useState(false);
  const [menuAberto, setMenuAberto] = useState(false);
  const [estaFechando, setEstaFechando] = useState(false);

  const [mounted, setMounted] = useState(false);
  const router = useRouter();

  useEffect(() => {
    setMounted(true);
    const dados = obterUsuarioAutenticado();

    if (dados) {
      setUsuario(dados);
      setEstaAutenticado(true);
    } else {
      setEstaAutenticado(false);
      setUsuario(null);
    }
  }, []);

  const handleLogout = async () => {
    await logout();
    setUsuario(null);
    setEstaAutenticado(false);
    router.push("/login");
  };

  const handleFecharMenu = () => {
    setEstaFechando(true);
    setTimeout(() => {
      setMenuAberto(false);
      setEstaFechando(false);
    }, 300);
  };

  const menuLateral = (
    <>
      <div
        className={estaFechando ? styles.overlayClosing : styles.overlay}
        onClick={handleFecharMenu}
      />

      <aside className={estaFechando ? styles.sidebarClosing : styles.sidebar}>
        <button
          type="button"
          className={styles.closeButton}
          onClick={handleFecharMenu}
        >
          <Lucide name="X" className="reset_lucide" />
        </button>

        <Link
          href="/login"
          className={styles.menuLink}
          onClick={handleFecharMenu}
        >
          Login
          <Lucide name="LogIn" className="reset_lucide" />
        </Link>
        <Link
          href="/cCategoria"
          className={styles.menuLink}
          onClick={handleFecharMenu}
        >
          + Criar Categoria
          <Lucide name="Grid2X2Plus" className="reset_lucide" />
        </Link>
        <Link
          href="/cLocalizacao"
          className={styles.menuLink}
          onClick={handleFecharMenu}
        >
          + Criar Localização
          <Lucide name="MapPinPlus" className="reset_lucide" />
        </Link>
        <Link
          href="/cTProduto"
          className={styles.menuLink}
          onClick={handleFecharMenu}
        >
          + Criar Tipo
          <Lucide name="PackagePlus" className="reset_lucide" />
        </Link>
        <Link
          href="/cProduto"
          className={styles.menuLink}
          onClick={handleFecharMenu}
        >
          + Cadastrar Produto
          <Lucide name="HeartPlus" className="reset_lucide" />
        </Link>
        <Link
          href="/cUsuario"
          className={styles.menuLink}
          onClick={handleFecharMenu}
        >
          + Cadastrar Usuário
          <Lucide name="UserRoundPlus" className="reset_lucide" />
        </Link>
        <Link
          href="/historico"
          className={styles.menuLink}
          onClick={handleFecharMenu}
        >
          Histórico Geral
          <Lucide name="History" className="reset_lucide" />
        </Link>
        <Link
          href="/home"
          className={styles.menuLink}
          onClick={handleFecharMenu}
        >
          Tela Inicial
          <Lucide name="HouseHeart" className="reset_lucide" />
        </Link>
      </aside>
    </>
  );

  return (
    <>
      <header className="main_header" id={styles.header}>
        <div className="container row" id={styles.info_header}>
          <Link href="/home">
            <img
              className="img"
              id={styles.img}
              src="/imgs/Logo.svg"
              alt="Logo do site"
            />
          </Link>

          <div id={styles.div}>
            <div className="row">
              <Button
                className="column no_gap"
                id={styles.user_info}
                onClick={handleLogout}
              >
                {usuario ? (
                  <>
                    <h4 className="h4">{usuario.nome}</h4>
                    <p className="p">{usuario.email}</p>
                  </>
                ) : (
                  <>
                    <h4 className="h4">Nome</h4>
                    <p className="p">email@email.com</p>
                  </>
                )}
              </Button>
            </div>
            <TrocaTema className={`${styles.trocaTema} ${styles.headerIcon}`} />
            <button
              type="button"
              className={`menuIcon ${styles.headerIcon}`}
              onClick={() => setMenuAberto(true)}
            >
              <Lucide name="Menu" />
            </button>
          </div>
        </div>
      </header>

      {mounted && menuAberto && createPortal(menuLateral, document.body)}
    </>
  );
};

export default Header;
