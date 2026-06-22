import Footer from "@/components/footer/footer";
import Button from "@/components/button/button";
import Header from "@/components/header/header";
import styles from "./detalhe.module.css";
import { erro } from "@/utils/toast";
import { useEffect, useState } from "react";
import Link from "next/link";
import Lucide from "@/utils/lucide";
import {
  listarCategoriaPorId,
  listarLocalizacaoPorId,
  listarProdutoPorId,
  listarUsuarioPorId,
} from "@/pages/api/genericService";
import { useRouter } from "next/router";
import { formatarPreco } from "@/utils/formatacao";

type Produto = {
  produtoID: number;
  codigo: number;
  nomeProduto: string;
  preco: string;
  statusProduto: boolean;
  descricao: string;
  tamanho: string;
  imagem: string;
  imagemUrl?: string | null;
  categoriaID: number;
  localizacaoID: number;
  usuarioID: string;
  tipoProdutoID: number;
  nomeUsuario: string;
  nomeCategoria: string;
  nomeLocalizacao: string;
};

const Detalhe = () => {
  const [produto, setProduto] = useState<Produto | null>(null);
  const router = useRouter();
  const { id } = router.query;

  async function listarProdutos() {
    try {
      const response = await listarProdutoPorId(id as string);
      const produtoDados = response as unknown as Produto;

      const [resUsuario, resCategoria, resLocalizacao] =
        await Promise.allSettled([
          produtoDados.usuarioID
            ? listarUsuarioPorId(produtoDados.usuarioID)
            : null,
          produtoDados.categoriaID
            ? listarCategoriaPorId(produtoDados.categoriaID)
            : null,
          produtoDados.localizacaoID
            ? listarLocalizacaoPorId(produtoDados.localizacaoID)
            : null,
        ]);

      if (resUsuario.status === "fulfilled" && resUsuario.value) {
        produtoDados.nomeUsuario = resUsuario.value.nome;
      } else {
        produtoDados.nomeUsuario = "Usuário não encontrado";
      }

      if (resCategoria.status === "fulfilled" && resCategoria.value) {
        produtoDados.nomeCategoria = resCategoria.value.nomeCategoria;
      } else {
        produtoDados.nomeCategoria = "Categoria não encontrada";
      }

      if (resLocalizacao.status === "fulfilled" && resLocalizacao.value) {
        produtoDados.nomeLocalizacao = resLocalizacao.value.nomeLocalizacao;
      } else {
        produtoDados.nomeLocalizacao = "Localização não encontrada";
      }

      setProduto(produtoDados);
    } catch (error: any) {
      erro(error.message || "Erro ao carregar os detalhes do produto.");
    }
  }

  useEffect(() => {
    if (!router.isReady || !id) return;
    listarProdutos();
  }, [router.isReady, id]);

  // 🟢 Tratamento centralizado da imagem Base64 idêntico ao do Card
  const imagemSrc = produto?.imagemUrl
    ? produto.imagemUrl
    : produto?.imagem
      ? produto.imagem.startsWith("data:")
        ? produto.imagem
        : `data:image/jpeg;base64,${produto.imagem}`
      : "/imgs/CardFantasma.png";

  return (
    <>
      <Header />
      <main className="min_height">
        <section className="container column">
          <article className="grid info">
            <div className="column">
              <div className={styles.imgContainer}>
                <img
                  id={styles.img}
                  src={imagemSrc} // 🟢 Consome a variável tratada acima
                  alt={produto?.nomeProduto || "Imagem do produto"}
                  className={`img small_radius ${
                    produto?.statusProduto ? styles.ativoImg : styles.inativoImg
                  }`}
                />

                <h3
                  className={
                    produto?.statusProduto ? styles.ativoH3 : styles.inativoH3
                  }
                >
                  {produto?.statusProduto ? "Ativo" : "Inativo"}
                </h3>
              </div>
              <div className="row">
                <h4>Código:</h4>
                <p>{produto?.codigo || "Carregando..."}</p>
              </div>
            </div>

            <div className="column start grid_to_column">
              <h1>{produto?.nomeProduto || "Carregando..."}</h1>
              <h3>
                {formatarPreco(Number(produto?.preco)) || "Carregando..."}
              </h3>
              <p>{produto?.descricao || "Carregando..."}</p>
            </div>

            <div className="column start">
              <div className="row">
                <Lucide name="Package" className="reset_lucide" />
                <div className="column start small_gap grid_to_row">
                  <h3>Tipo:</h3>
                  <p>{produto?.tipoProdutoID || "Não informado"}</p>
                </div>
              </div>

              <div className="row">
                <Lucide name="Grid2X2" className="reset_lucide" />
                <div className="column start small_gap grid_to_row">
                  <h3>Categoria:</h3>
                  <p>
                    {produto?.nomeCategoria ||
                      (produto?.categoriaID
                        ? "Carregando..."
                        : "Não informado")}
                  </p>
                </div>
              </div>

              <div className="row">
                <Lucide name="MapPin" className="reset_lucide" />
                <div className="column start small_gap grid_to_row">
                  <h3>Localização:</h3>
                  <p>
                    {produto?.nomeLocalizacao ||
                      (produto?.localizacaoID
                        ? "Carregando..."
                        : "Não informado")}
                  </p>
                </div>
              </div>
              <div className="row">
                <Lucide name="RulerDimensionLine" className="reset_lucide" />
                <div className="column start small_gap grid_to_row">
                  <h3>Tamanho:</h3>
                  <p>{produto?.tamanho || "Não informado"}</p>
                </div>
              </div>

              <div className="row">
                <Lucide name="User" className="reset_lucide" />
                <div className="column start small_gap grid_to_row">
                  <h3>Usuário:</h3>
                  <p>
                    {produto?.nomeUsuario ||
                      (produto?.usuarioID ? "Carregando..." : "Não informado")}
                  </p>
                </div>
              </div>
            </div>
          </article>

          <div className="row">
            <Link
              id={styles.historico}
              className="small_width"
              href={`/historico/${id}`}
            >
              <Lucide name="NotepadText" className="reset_lucide" />
            </Link>
            <Link href="/home" className="btn2">
              Voltar
            </Link>
            <Link
              id={styles.button}
              href={`/cProduto?id=${id}`}
              className="btn"
            >
              Editar
            </Link>
          </div>
        </section>
      </main>
      <Footer />
    </>
  );
};

export default Detalhe;
