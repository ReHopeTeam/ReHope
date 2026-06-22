import { formatarPreco } from "@/utils/formatacao";

type Dados = {
  dataAlteracao: string;
  nomeAnterior: string;
  precoAnterior: number;
  nomeLocalizacaoAnterior: string;
};

const DataTable = ({
  dataAlteracao,
  nomeAnterior,
  precoAnterior,
  nomeLocalizacaoAnterior,
}: Dados) => {
  return (
    <tr className="tr info">
      <td>{dataAlteracao}</td>
      <td>{nomeAnterior}</td>
      <td>{formatarPreco(precoAnterior)}</td>
      <td>{nomeLocalizacaoAnterior}</td>
    </tr>
  );
};

export default DataTable;
