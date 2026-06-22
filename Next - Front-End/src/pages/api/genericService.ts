import { api } from "./api";

//? ========================
//?   Interfaces & Types
//? ========================
export interface Categoria {
  categoriaID?: number;
  nomeCategoria: string;
  tipoProdutoID: number;
}

export interface Localizacao {
  localizacaoID?: number;
  nomeLocalizacao: string;
}

export interface LogProduto {
  logID: number;
  dataAlteracao: string;
  nomeAnterior: string;
  precoAnterior: number;
  localizacaoIDAnterior: number;
  nomeLocalizacaoAnterior: string;
}

export interface ProdutoList {
  produtoID: number;
  nomeProduto: string;
  preco: string;
  descricao: string;
  tamanho: string;
  imagem: string;
  statusProduto: boolean;
  codigo: number;
  categoriaID: number;
  localizacaoID: number;
  usuarioID: string;
  tipoProdutoID: number;
}

export interface ProdutoDetalhe extends ProdutoList {
  imagemUrl: string | null;
}

export type ProdutoForm = {
  nomeProduto: string;
  preco: string;
  descricao: string;
  tamanho: string;
  imagem: File | null;
  statusProduto?: boolean;
  codigo?: number;
  categoriaID: number;
  localizacaoID: number;
  usuarioID: string;
  tipoProdutoID: number;
};

export interface TipoProduto {
  tipoId: number;
  nomeTipo: string;
  categoriaId: number;
  nomeCategoria: string;
}

export interface Usuario {
  usuarioID?: string;
  nome: string;
  email: string;
  senha?: string;
  telefone: string;
}

//? =============
//?   Cadastros
//? =============
export async function cadastrarCategoria(dados: Categoria): Promise<any> {
  try {
    const response = await api.post("Categoria", dados);
    return response.data;
  } catch (error: any) {
    const mensagemErro =
      error.response?.data?.message || error.response?.data || "Erro na API";
    throw new Error(mensagemErro);
  }
}

export async function cadastrarLocalizacao(dados: Localizacao): Promise<any> {
  try {
    const response = await api.post("Localizacao", dados);
    return response.data;
  } catch (error: any) {
    const mensagemErro =
      error.response?.data?.message || error.response?.data || "Erro na API";
    throw new Error(mensagemErro);
  }
}

export async function cadastrarProduto(dados: ProdutoForm): Promise<any> {
  try {
    const formData = new FormData();
    formData.append("NomeProduto", dados.nomeProduto);
    formData.append("Preco", dados.preco);
    formData.append("Descricao", dados.descricao || "");
    formData.append("Tamanho", dados.tamanho);
    formData.append("Codigo", String(dados.codigo ?? 0));

    if (dados.imagem) {
      formData.append("Imagem", dados.imagem);
    }

    formData.append("CategoriaID", dados.categoriaID.toString());
    formData.append("LocalizacaoID", dados.localizacaoID.toString());
    formData.append("UsuarioID", dados.usuarioID);
    formData.append("TipoProdutoID", dados.tipoProdutoID.toString());

    const response = await api.post("Produto", formData);
    return response.data;
  } catch (error: any) {
    throw new Error(
      error.response?.data || "Erro inesperado ao cadastrar produto.",
    );
  }
}

export async function cadastrarTipoProduto(dados: TipoProduto): Promise<any> {
  try {
    const response = await api.post("TipoProduto", dados);
    return response.data;
  } catch (error: any) {
    const mensagemErro =
      error.response?.data?.message || error.response?.data || "Erro na API";
    throw new Error(mensagemErro);
  }
}

export async function cadastrarUsuario(dados: Usuario): Promise<any> {
  try {
    const response = await api.post("Usuario", dados);
    return response.data;
  } catch (error: any) {
    const mensagemErro =
      error.response?.data?.message || error.response?.data || "Erro na API";
    throw new Error(mensagemErro);
  }
}

//? =============
//?   Listagens
//? =============
export async function listarCategoria(): Promise<Categoria[]> {
  try {
    const response = await api.get<Categoria[]>("Categoria");
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao listar categorias.");
  }
}

export async function listarLocalizacao(): Promise<Localizacao[]> {
  try {
    const response = await api.get<Localizacao[]>("Localizacao");
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao listar localizações.");
  }
}

export async function listarLogProduto(): Promise<LogProduto[]> {
  try {
    const response = await api.get<LogProduto[]>("LogProduto");
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao listar logs.");
  }
}

export async function listarProduto(): Promise<ProdutoList[]> {
  try {
    const response = await api.get<ProdutoList[]>("Produto");
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao listar produtos.");
  }
}

export async function listarTipoProduto(): Promise<TipoProduto[]> {
  try {
    const response = await api.get<TipoProduto[]>("TipoProduto");
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao listar tipos de produto.");
  }
}

export async function listarUsuario(): Promise<Usuario[]> {
  try {
    const response = await api.get<Usuario[]>("Usuario");
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao listar usuários.");
  }
}

//? ====================
//?   Listagens (IDs)
//? ====================
export async function listarCategoriaPorId(id: number): Promise<Categoria> {
  try {
    const response = await api.get<Categoria>("Categoria/" + id);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao buscar Categoria por ID");
  }
}

export async function listarLogProdutoPorId(
  produtoId: string,
): Promise<LogProduto[]> {
  try {
    const response = await api.get<LogProduto[]>(`LogProduto/${produtoId}`);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao listar histórico.");
  }
}

export async function listarLocalizacaoPorId(id: number): Promise<Localizacao> {
  try {
    const response = await api.get<Localizacao>("Localizacao/" + id);
    return response.data;
  } catch (error: any) {
    throw new Error(
      error.response?.data || "Erro ao buscar Localização por ID",
    );
  }
}

export async function listarProdutoPorId(id: string): Promise<ProdutoDetalhe> {
  try {
    const response = await api.get<ProdutoList>("Produto/" + id);

    const base64Crua = response.data.imagem;

    const produto: ProdutoDetalhe = {
      ...response.data,
      imagemUrl: base64Crua
        ? base64Crua.startsWith("data:")
          ? base64Crua
          : `data:image/jpeg;base64,${base64Crua}`
        : null,
    };

    return produto;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao buscar Produto por ID");
  }
}

export async function listarTipoProdutoPorId(id: number): Promise<TipoProduto> {
  try {
    const response = await api.get<TipoProduto>("TipoProduto/" + id);
    return response.data;
  } catch (error: any) {
    throw new Error(
      error.response?.data || "Erro ao buscar Tipo de Produto por ID",
    );
  }
}

export async function listarUsuarioPorId(id: string): Promise<Usuario> {
  try {
    const response = await api.get<Usuario>("Usuario/" + id);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao buscar Usuário por ID");
  }
}

//? ===============
//?     Edições
//? ===============
export async function editarProduto(
  produtoId: string,
  dados: ProdutoForm,
): Promise<any> {
  try {
    const formData = new FormData();

    formData.append("ProdutoID", produtoId);
    formData.append("NomeProduto", dados.nomeProduto);
    formData.append("Preco", String(dados.preco));
    formData.append("Descricao", dados.descricao);
    formData.append("Codigo", String(dados.codigo ?? 0));
    formData.append("Tamanho", dados.tamanho);
    formData.append("StatusProduto", String(dados.statusProduto ?? true));

    formData.append("CategoriaID", String(dados.categoriaID || 0));
    formData.append("LocalizacaoID", String(dados.localizacaoID || 0));
    formData.append("UsuarioID", dados.usuarioID || "");
    formData.append("TipoProdutoID", String(dados.tipoProdutoID || 0));
    if (dados.imagem instanceof File) {
      formData.append("Imagem", dados.imagem);
    }

    const response = await api.put(`Produto/${produtoId}`, formData);
    return response.data;
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao editar Produto.");
  }
}

//? ===============
//?     Deletes
//? ===============
export async function deletarCategoria(categoriaId: number): Promise<void> {
  try {
    await api.delete("Categoria/" + categoriaId);
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao deletar Categoria.");
  }
}

export async function deletarLocalizacao(localizacaoId: number): Promise<void> {
  try {
    await api.delete("Localizacao/" + localizacaoId);
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao deletar Localização.");
  }
}

export async function deletarProduto(produtoId: string): Promise<void> {
  try {
    await api.delete("Produto/" + produtoId);
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao deletar Produto.");
  }
}

export async function deletarTipoProduto(tipoProdutoId: number): Promise<void> {
  try {
    await api.delete("TipoProduto/" + tipoProdutoId);
  } catch (error: any) {
    const mensagemErro =
      error.response?.data?.message ||
      error.response?.data ||
      "Erro ao deletar Tipo de Produto.";

    throw new Error(mensagemErro);
  }
}

export async function deletarUsuario(usuarioId: string): Promise<void> {
  try {
    await api.delete("Usuario/" + usuarioId);
  } catch (error: any) {
    throw new Error(error.response?.data || "Erro ao deletar Usuário.");
  }
}
