import DataTable from "@/components/datatable/datatable";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";
import { useEffect, useState } from "react";
import { erro } from "@/utils/toast";
import {
  listarLogProdutoPorId,
  listarProdutoPorId,
  listarLocalizacao,
} from "@/pages/api/genericService";
import { useRouter } from "next/router";
import Link from "next/link";

type HistoricoAlteracao = {
  logID: number;
  dataAlteracao: string;
  nomeAnterior: string;
  precoAnterior: number;
  localizacaoAnterior: string;
};

const HistoricoPorID = () => {
  const router = useRouter();
  const { id } = router.query;

  const [historico, setHistorico] = useState<HistoricoAlteracao[]>([]);
  const [produtoAtual, setProdutoAtual] = useState<any>(null);
  const [paginaAtual, setPaginaAtual] = useState(1);

  const itensPorPagina = 5;
  const registros = historico ?? [];

  const indiceInicial = (paginaAtual - 1) * itensPorPagina;
  const indiceFinal = indiceInicial + itensPorPagina;

  const historicoPaginado = registros.slice(indiceInicial, indiceFinal);
  const totalPaginas = Math.ceil(registros.length / itensPorPagina);
  const maxBotoesVisiveis = 5;

  const obterIntervaloPaginas = () => {
    if (totalPaginas <= maxBotoesVisiveis) {
      return Array.from({ length: totalPaginas }, (_, i) => i + 1);
    }

    const metadeJulgada = Math.floor(maxBotoesVisiveis / 2);
    let inicio = paginaAtual - metadeJulgada;
    let fim = paginaAtual + metadeJulgada;

    if (inicio < 1) {
      inicio = 1;
      fim = maxBotoesVisiveis;
    }

    if (fim > totalPaginas) {
      fim = totalPaginas;
      inicio = totalPaginas - maxBotoesVisiveis + 1;
    }

    return Array.from({ length: fim - inicio + 1 }, (_, i) => inicio + i);
  };

  const paginasVisiveis = obterIntervaloPaginas();

  async function listarHistorico() {
    if (!id) return;
    try {
      const [listaLogs, listaLocalizacoes, dadosProduto] = await Promise.all([
        listarLogProdutoPorId(String(id)),
        listarLocalizacao(),
        listarProdutoPorId(String(id)),
      ]);

      setProdutoAtual(dadosProduto);

      const logsFormatados: HistoricoAlteracao[] = listaLogs.map(
        (item: any, index: number) => {
          const localizacao = listaLocalizacoes.find(
            (loc: any) => loc.localizacaoID === item.localizacaoIDAnterior,
          );

          return {
            logID: index,
            dataAlteracao: item.dataAlteracao,
            nomeAnterior: item.nomeAnterior,
            precoAnterior: item.precoAnterior,
            localizacaoAnterior:
              localizacao?.nomeLocalizacao ??
              `ID ${item.localizacaoIDAnterior}`,
          };
        },
      );

      setHistorico(logsFormatados);
    } catch (error: any) {
      erro(error.message || "Erro ao carregar os dados do produto.");
      setHistorico([]);
    }
  }

  useEffect(() => {
    if (router.isReady && id) {
      listarHistorico();
    }
  }, [id, router.isReady]);

  return (
    <>
      <Header />
      <main className="min_height">
        <section className="container column space_between">
          <h1 className="h1">
            Histórico:{" "}
            <span className="h1">
              {produtoAtual?.nomeProduto || "Carregando..."}
            </span>
          </h1>
          {historico.length === 0 ? (
            <p>Esse produto ainda não tem histórico</p>
          ) : (
            <table className="table">
              <thead>
                <tr className="tr small_padding">
                  <th>Data da alteração</th>
                  <th>Produto</th>
                  <th>Preço Anterior</th>
                  <th>Local Anterior</th>
                </tr>
              </thead>
              <tbody className="line column">
                {historicoPaginado.map((item) => (
                  <DataTable
                    key={item.logID}
                    dataAlteracao={item.dataAlteracao}
                    nomeAnterior={item.nomeAnterior}
                    precoAnterior={item.precoAnterior}
                    nomeLocalizacaoAnterior={item.localizacaoAnterior}
                  />
                ))}
              </tbody>
            </table>
          )}

          {totalPaginas > 1 ? (
            <div className="row">
              <Link href={`/detalhe/${id}`} className="btn2">
                Voltar
              </Link>
              <nav>
                <ul className="paginacao">
                  <li
                    className="btn small_width"
                    onClick={() => paginaAtual > 1 && setPaginaAtual(1)}
                    style={{
                      opacity: paginaAtual === 1 ? 0.25 : 1,
                      cursor: paginaAtual === 1 ? "not-allowed" : "pointer",
                    }}
                  >
                    {"<<"}
                  </li>

                  <li
                    className="btn small_width"
                    onClick={() =>
                      paginaAtual > 1 && setPaginaAtual(paginaAtual - 1)
                    }
                    style={{
                      opacity: paginaAtual === 1 ? 0.5 : 1,
                      cursor: paginaAtual === 1 ? "not-allowed" : "pointer",
                    }}
                  >
                    {"<"}
                  </li>

                  {paginasVisiveis.map((pagina) => (
                    <li
                      key={pagina}
                      onClick={() => setPaginaAtual(pagina)}
                      className={`${paginaAtual === pagina ? "btn" : "btn2"} small_width`}
                    >
                      {pagina}
                    </li>
                  ))}

                  <li
                    className="btn small_width"
                    onClick={() =>
                      paginaAtual < totalPaginas &&
                      setPaginaAtual(paginaAtual + 1)
                    }
                    style={{
                      opacity: paginaAtual === totalPaginas ? 0.5 : 1,
                      cursor:
                        paginaAtual === totalPaginas
                          ? "not-allowed"
                          : "pointer",
                    }}
                  >
                    {">"}
                  </li>

                  <li
                    className="btn small_width"
                    onClick={() =>
                      paginaAtual < totalPaginas && setPaginaAtual(totalPaginas)
                    }
                    style={{
                      opacity: paginaAtual === totalPaginas ? 0.25 : 1,
                      cursor:
                        paginaAtual === totalPaginas
                          ? "not-allowed"
                          : "pointer",
                    }}
                  >
                    {">>"}
                  </li>
                </ul>
              </nav>
            </div>
          ) : (
            <Link href={`/detalhe/${id}`} className="btn2">
              Voltar
            </Link>
          )}
        </section>
      </main>
      <Footer />
    </>
  );
};

export default HistoricoPorID;