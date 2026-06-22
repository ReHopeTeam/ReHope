import DataTable from "@/components/datatable/datatable";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";
import { useEffect, useState } from "react";
import { listarLogProduto, listarLocalizacao } from "../api/genericService";
import { erro } from "@/utils/toast";
import Link from "next/link";

type HistoricoAlteracao = {
  logID: number;
  dataAlteracao: string;
  nomeAnterior: string;
  precoAnterior: number;
  localizacaoAnterior: string;
};

const Historico = () => {
  const [logs, setLogs] = useState<HistoricoAlteracao[]>([]);
  const registros = logs ?? [];

  const [paginaAtual, setPaginaAtual] = useState(1);
  const itensPorPagina = 5;

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

  async function carregarHistoricoCompleto() {
    try {
      const [listaLogs, listaLocalizacoes] = await Promise.all([
        listarLogProduto(),
        listarLocalizacao(),
      ]);

      const logsFormatados: HistoricoAlteracao[] = listaLogs.map(
        (item, index) => {
          const localizacao = listaLocalizacoes.find(
            (loc) => loc.localizacaoID === item.localizacaoIDAnterior,
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

      setLogs(logsFormatados);
    } catch (error: any) {
      erro("Erro ao carregar o histórico geral: " + error.message);
      setLogs([]);
    }
  }

  useEffect(() => {
    carregarHistoricoCompleto();
  }, []);

  return (
    <>
      <Header />
      <main className="min_height">
        <section className="container column space_between">
          <h1 className="h1">Histórico Geral</h1>
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

          {totalPaginas > 1 ? (
            <div className="row">
              <Link href={`/home`} className="btn2">
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
            <Link href={`/home`} className="btn2">
              Voltar
            </Link>
          )}
        </section>
      </main>
      <Footer />
    </>
  );
};

export default Historico;
