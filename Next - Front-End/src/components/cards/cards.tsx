import Link from "next/link";
import styles from "./cards.module.css";
import Button from "@/components/button/button";
import Lucide from "@/utils/lucide";
import { formatarPreco } from "@/utils/formatacao";

type CardProps = {
  produtoID: number;
  nomeProduto: string;
  preco: string | number;
  imagem: string | null | any;
  fantasma?: boolean;
  onDelete: (produtoId: number) => void;
};

const Card = ({
  produtoID,
  nomeProduto,
  preco,
  imagem,
  fantasma = false,
  onDelete,
}: CardProps) => {
  const config = {
    id: !fantasma ? styles.card : undefined,
    className: fantasma ? styles.cardFantasma : "",

    imagemSrc:
      imagem && !fantasma
        ? imagem.startsWith("data:")
          ? imagem
          : `data:image/jpeg;base64,${imagem}`
        : "/imgs/CardFantasma.png",

    imagemAlt: fantasma ? "Produto fantasma" : nomeProduto,
    titulo: fantasma ? "Preço" : formatarPreco(Number(preco || 0)),
    tag: fantasma ? "CircleQuestionMark" : undefined,
    linkEditar: fantasma ? "/login" : `/cProduto?id=${produtoID}`,
    onExcluir: fantasma ? undefined : () => onDelete(produtoID),
  };

  return (
    <article className="column">
      <li id={config.id} className={config.className}>
        <div className={`${styles.imagemContainer} fit_content`}>
          {!fantasma ? (
            <Link href={`/detalhe/${produtoID}`}>
              <img
                className={styles.img}
                src={config.imagemSrc}
                alt={config.imagemAlt}
              />
            </Link>
          ) : (
            <img
              className={styles.img}
              src={config.imagemSrc}
              alt={config.imagemAlt}
            />
          )}

          <h3 className={`${styles.tituloProduto} title dark`}>
            {fantasma ? (
              <>
                <Lucide name="CircleQuestionMark" className="reset_lucide" />
                <Lucide name="CircleQuestionMark" className="reset_lucide" />
                <Lucide name="CircleQuestionMark" className="reset_lucide" />
              </>
            ) : (
              nomeProduto
            )}
          </h3>
        </div>

        <span className={styles.preco}>{config.titulo}</span>

        <div
          className={`row no_gap to_column2 ${styles.botoes}`}
          id={fantasma ? styles.botoes : undefined}
        >
          <Button
            className={`${styles.btn_card} ${styles.excluir}`}
            onClick={config.onExcluir}
            disabled={fantasma}
          >
            <Lucide name="Delete" className="reset_lucide icon_branco" />
            <p className="p white">Excluir</p>
          </Button>

          <Link
            href={config.linkEditar}
            className={`btn ${styles.btn_card} ${styles.editar}`}
          >
            <Lucide name="SquarePen" className="reset_lucide icon_branco" />
            <p className="p white">Editar</p>
          </Link>
        </div>
      </li>
    </article>
  );
};

export default Card;
