import Button from "@/components/button/button";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";
import Lucide from "@/utils/lucide";
import { ChangeEvent, useEffect, useState, useRef } from "react";
import Link from "next/link";
import { erro, notificacao } from "@/utils/toast";
import {
  cadastrarProduto,
  Categoria,
  editarProduto,
  listarCategoria,
  listarLocalizacao,
  listarProdutoPorId,
  listarTipoProduto,
  listarUsuario,
  Localizacao,
  ProdutoForm,
  TipoProduto,
  Usuario,
} from "../api/genericService";
import { useRouter } from "next/router";

const METADADOS_SELECTS = {
  tipo: { label: "Tipo", icone: "Package" as const },
  categoria: { label: "Categoria", icone: "Grid2X2" as const },
  localizacao: { label: "Localização", icone: "MapPin" as const },
  usuario: { label: "Usuário", icone: "User" as const },
};

const CadastroProduto = () => {
  const router = useRouter();
  const { id } = router.query;
  const telaEditar = !!id;

  const [titulo, setTitulo] = useState("");
  const [preco, setPreco] = useState("");
  const [descricao, setDescricao] = useState("");
  const [tamanho, setTamanho] = useState("");

  const [preview, setPreview] = useState<string | null>(null);
  const [arquivoImagem, setArquivoImagem] = useState<File | null>(null);

  const [listaTipos, setListaTipos] = useState<TipoProduto[]>([]);
  const [listaLocalizacoes, setListaLocalizacoes] = useState<Localizacao[]>([]);
  const [listaUsuarios, setListaUsuarios] = useState<Usuario[]>([]);
  const [listaCategorias, setListaCategorias] = useState<Categoria[]>([]);
  const [categoriasFiltradas, setCategoriasFiltradas] = useState<Categoria[]>(
    [],
  );

  const [valoresSelect, setValoresSelect] = useState<Record<string, string>>({
    tipo: "",
    categoria: "",
    localizacao: "",
    usuario: "",
  });

  const [selectAberto, setSelectAberto] = useState<Record<string, boolean>>({
    tipo: false,
    categoria: false,
    localizacao: false,
    usuario: false,
  });

  const formRef = useRef<HTMLFormElement>(null);

  const renderizarPreview = (url: string | null) => {
    if (!url) return "";
    if (url.startsWith("blob:")) {
      return url;
    }
    return url.startsWith("data:") ? url : `data:image/jpeg;base64,${url}`;
  };

  async function carregarInformacoes() {
    if (!id) return;

    try {
      const produto = await listarProdutoPorId(id as string);

      setTitulo(produto.nomeProduto || "");
      setPreco(produto.preco || "");
      setDescricao(produto.descricao || "");
      setTamanho(produto.tamanho || (produto as any).Tamanho || "");

      const tipoSeguro =
        produto.tipoProdutoID ?? (produto as any).tipoProdutoId;
      const categoriaSegura =
        produto.categoriaID ?? (produto as any).categoriaId;
      const localizacaoSegura =
        produto.localizacaoID ?? (produto as any).localizacaoId;
      const usuarioSeguro = produto.usuarioID ?? (produto as any).usuarioId;

      setValoresSelect({
        tipo: tipoSeguro != null ? String(tipoSeguro) : "",
        categoria: categoriaSegura != null ? String(categoriaSegura) : "",
        localizacao: localizacaoSegura != null ? String(localizacaoSegura) : "",
        usuario: usuarioSeguro != null ? String(usuarioSeguro) : "",
      });

      if (produto.imagem) {
        setPreview(produto.imagem);
      } else if (produto.imagemUrl) {
        setPreview(produto.imagemUrl);
      }
    } catch (error) {
      erro("Erro ao carregar dados do produto");
    }
  }

  // Hook unificado para filtrar as categorias baseadas no Tipo selecionado
  useEffect(() => {
    if (!valoresSelect.tipo || listaCategorias.length === 0) {
      setCategoriasFiltradas([]);
      return;
    }

    const categorias = listaCategorias.filter(
      (categoria) => categoria.tipoProdutoID === Number(valoresSelect.tipo),
    );

    setCategoriasFiltradas(categorias);
  }, [valoresSelect.tipo, listaCategorias]);

  // Dispara a busca do produto quando o Router está pronto
  useEffect(() => {
    if (!router.isReady) return;
    if (telaEditar && listaCategorias.length > 0) {
      carregarInformacoes();
    }
  }, [router.isReady, id, listaCategorias.length]);

  const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      setArquivoImagem(file);
      const imagemUrl = URL.createObjectURL(file);
      setPreview(imagemUrl);
    }
  };

  const handleRemoveImage = (e: React.MouseEvent) => {
    e.preventDefault();
    e.stopPropagation();

    if (preview && preview.startsWith("blob:")) {
      URL.revokeObjectURL(preview);
    }
    setPreview(null);
    setArquivoImagem(null);
  };

  useEffect(() => {
    return () => {
      if (preview && preview.startsWith("blob:")) URL.revokeObjectURL(preview);
    };
  }, [preview]);

  useEffect(() => {
    async function carregarCombos() {
      try {
        const [tipos, categorias, localizacoes, usuarios] = await Promise.all([
          listarTipoProduto().catch(() => []),
          listarCategoria().catch(() => []),
          listarLocalizacao().catch(() => []),
          listarUsuario().catch(() => []),
        ]);

        setListaTipos(Array.isArray(tipos) ? tipos : []);
        setListaCategorias(Array.isArray(categorias) ? categorias : []);
        setListaLocalizacoes(Array.isArray(localizacoes) ? localizacoes : []);
        setListaUsuarios(Array.isArray(usuarios) ? usuarios : []);
      } catch (err) {
        console.error("Erro ao carregar dados dos selects:", err);
      }
    }

    carregarCombos();
  }, []);

  useEffect(() => {
    const fecharAoClicarFora = (event: MouseEvent) => {
      if (formRef.current && !formRef.current.contains(event.target as Node)) {
        setSelectAberto({
          tipo: false,
          categoria: false,
          localizacao: false,
          usuario: false,
        });
      }
    };
    document.addEventListener("mousedown", fecharAoClicarFora);
    return () => document.removeEventListener("mousedown", fecharAoClicarFora);
  }, []);

  async function salvarProduto(e: React.FormEvent) {
    e.preventDefault();

    if (telaEditar && !id) {
      erro("ID do produto não encontrado para edição.");
      return;
    }

    try {
      const dados: ProdutoForm = {
        nomeProduto: titulo,
        preco: preco,
        descricao: descricao,
        tamanho: tamanho,
        statusProduto: true,
        categoriaID: Number(valoresSelect.categoria) || 0,
        localizacaoID: Number(valoresSelect.localizacao) || 0,
        usuarioID: valoresSelect.usuario,
        tipoProdutoID: Number(valoresSelect.tipo) || 0,
        imagem: arquivoImagem,
      };

      if (
        !dados.categoriaID ||
        !dados.localizacaoID ||
        !dados.usuarioID ||
        !dados.tipoProdutoID
      ) {
        erro("Por favor, selecione todas as opções obrigatórias.");
        return;
      }

      if (telaEditar) {
        await editarProduto(String(id), dados);
        notificacao("Produto editado com sucesso!");
      } else {
        await cadastrarProduto(dados);
        notificacao("Produto cadastrado com sucesso!");
      }
      router.push("/home");
    } catch (error: any) {
      erro(error.message || "Erro ao salvar o produto.");
    }
  }

  const alternarSelect = (campo: string) => {
    setSelectAberto((prev) => ({
      tipo: campo === "tipo" ? !prev.tipo : false,
      categoria: campo === "categoria" ? !prev.categoria : false,
      localizacao: campo === "localizacao" ? !prev.localizacao : false,
      usuario: campo === "usuario" ? !prev.usuario : false,
    }));
  };

  const handleSelecionarOpcao = (campo: string, valor: string) => {
    setValoresSelect((prev) => {
      const novosValores = { ...prev, [campo]: valor };

      if (campo === "tipo") {
        novosValores.categoria = "";
      }

      return novosValores;
    });

    setSelectAberto((prev) => ({ ...prev, [campo]: false }));
  };

  const renderSelectCustomizado = (campo: keyof typeof METADADOS_SELECTS) => {
    const config = METADADOS_SELECTS[campo];
    const valorAtual = valoresSelect[campo];
    const aberto = selectAberto[campo];

    let listaAlvo: any[] = [];
    let extrairNome = (item: any) => "";
    let extrairId = (item: any) => "";

    if (campo === "tipo") {
      listaAlvo = listaTipos;
      extrairNome = (item) => item.nomeTipo || "";
      extrairId = (item) =>
        String(
          item.tipoId ?? item.tipoProdutoID ?? item.tipoProdutoId ?? item.id,
        );
    } else if (campo === "categoria") {
      listaAlvo = categoriasFiltradas;
      extrairNome = (item) => item.nomeCategoria || "";
      extrairId = (item) =>
        String(item.categoriaId ?? item.categoriaID ?? item.id);
    } else if (campo === "localizacao") {
      listaAlvo = listaLocalizacoes;
      extrairNome = (item) => item.nomeLocalizacao || "";
      extrairId = (item) =>
        String(item.localizacaoId ?? item.localizacaoID ?? item.id);
    } else if (campo === "usuario") {
      listaAlvo = listaUsuarios;
      extrairNome = (item) => item.nome || item.nomeUsuario || "";
      extrairId = (item) => String(item.usuarioId ?? item.usuarioID ?? item.id);
    }

    const itemSelecionado = listaAlvo.find(
      (item) => extrairId(item) === valorAtual,
    );
    const labelExibida = itemSelecionado ? extrairNome(itemSelecionado) : "";

    return (
      <div
        className={`campo_select ${aberto ? "open" : ""} ${valorAtual ? "has-value" : ""}`}
      >
        <Lucide
          name={config.icone}
          className="lucide rotate"
          style={{
            transition: "transform 0.2s ease",
            transform: aberto
              ? "translateY(-50%) rotate(180deg)"
              : "translateY(-50%)",
          }}
        />
        <div
          className="select"
          tabIndex={0}
          onClick={() => alternarSelect(campo)}
          style={{
            display: "flex",
            alignItems: "center",
            cursor: "pointer",
            paddingLeft: "50px",
          }}
        >
          <span>{labelExibida}</span>
        </div>
        <label className="label">{config.label}</label>

        {aberto && (
          <ul className="dropdown_options">
            <li onClick={() => handleSelecionarOpcao(campo, "")}>
              <Lucide name="RectangleEllipsis" className="reset_lucide" />{" "}
              Nenhum
            </li>
            {listaAlvo.map((opcao, index) => {
              const idMapeado = extrairId(opcao);
              return (
                <li
                  key={`select-${campo}-${index}`}
                  onClick={() => handleSelecionarOpcao(campo, idMapeado)}
                >
                  {extrairNome(opcao)}
                </li>
              );
            })}
          </ul>
        )}
      </div>
    );
  };

  return (
    <>
      <Header />
      <main className="min_height">
        <svg
          width="313"
          height="590"
          viewBox="0 0 313 590"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
          className="fixed path2"
          style={{ left: "0" }}
        >
          <path
            d="M0 109.002C17.5003 109.002 147.819 -34.6658 208.195 28.5686C281.301 105.137 137.521 125.098 138.875 209.814C140.446 308.076 298.245 287.843 299.985 396.703C302.34 544.035 22.0093 577.488 0 577.488"
            strokeWidth="20"
            strokeLinecap="round"
          />
        </svg>

        <svg
          width="313"
          height="590"
          viewBox="0 0 313 590"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
          className="fixed path2"
          style={{ right: "0" }}
        >
          <path
            d="M312.5 577.5C312.5 577.5 77.8148 563.562 77.8148 469.65C77.8148 387.284 204.507 379.047 204.507 298.199C204.507 219.164 12.5 241 12.5 131.741C12.5 -29.8913 196.591 -4.19628 312.5 73.8153"
            strokeWidth="20"
            strokeLinecap="round"
          />
        </svg>

        <section className="container column">
          <h1 className="h1">{telaEditar ? "Editar" : "Cadastrar"} Produto</h1>

          <div className="info column">
            <form
              className="form grid"
              ref={formRef}
              onSubmit={salvarProduto}
              id="form-produto"
            >
              {/* Coluna 1 - Upload Imagem */}
              <div className="column full_height">
                <div className="campo_img">
                  <label htmlFor="upload-foto" className="input_upload">
                    {preview ? (
                      <div className="relative_pos full_size_preview">
                        <img
                          src={renderizarPreview(preview)}
                          alt="Preview"
                          className="preview_img"
                        />
                        <button
                          type="button"
                          onClick={handleRemoveImage}
                          className="btn_delete"
                        >
                          X
                        </button>
                      </div>
                    ) : (
                      <>
                        <Lucide
                          name="Upload"
                          size={24}
                          className="upload_lucide"
                        />
                        <span>Escolher Imagem</span>
                      </>
                    )}
                  </label>
                  <input
                    id="upload-foto"
                    type="file"
                    accept="image/*"
                    className="input_img"
                    required={!telaEditar && !preview}
                    onChange={handleFileChange}
                  />
                </div>
              </div>

              {/* Coluna 2 - Dados de Texto */}
              <div className="column full_height">
                <div className="campo_form">
                  <Lucide name="ALargeSmall" className="lucide" />
                  <input
                    type="text"
                    id="titulo"
                    placeholder=" "
                    className="input"
                    value={titulo}
                    onChange={(e) => setTitulo(e.target.value)}
                    required
                  />
                  <label htmlFor="titulo" className="label">
                    Título
                  </label>
                </div>
                <div className="campo_form">
                  <Lucide name="Tag" className="lucide" />
                  <input
                    type="text"
                    id="preco"
                    placeholder=" "
                    className="input"
                    value={preco}
                    onChange={(e) => setPreco(e.target.value)}
                    required
                  />
                  <label htmlFor="preco" className="label">
                    Preço
                  </label>
                </div>
                <div className="campo_form">
                  <Lucide
                    name="MessageSquareText"
                    className="lucide desc_lucide"
                  />
                  <textarea
                    id="descricao"
                    placeholder=" "
                    className="textarea"
                    value={descricao}
                    onChange={(e) => setDescricao(e.target.value)}
                  />
                  <label htmlFor="descricao" className="label">
                    Descrição
                  </label>
                </div>
              </div>

              {/* Coluna 3 - Selects Dinâmicos */}
              <div className="column full_height">
                {renderSelectCustomizado("tipo")}
                {renderSelectCustomizado("categoria")}
                {renderSelectCustomizado("localizacao")}
                {renderSelectCustomizado("usuario")}

                <div className="campo_form">
                  <Lucide name="RulerDimensionLine" className="lucide" />
                  <input
                    type="text"
                    id="tamanho"
                    placeholder=" "
                    className="input"
                    value={tamanho}
                    onChange={(e) => setTamanho(e.target.value)}
                    required
                  />
                  <label htmlFor="tamanho" className="label">
                    Tamanho
                  </label>
                </div>
              </div>
            </form>
            <div className="row">
              <Link href="/home" className="btn2">
                Voltar
              </Link>
              <Button type="submit" form="form-produto">
                Salvar
              </Button>
            </div>
          </div>
        </section>
      </main>
      <Footer />
    </>
  );
};

export default CadastroProduto;
