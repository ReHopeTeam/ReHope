import { useState } from "react";
import Button from "@/components/button/button";
import Footer from "@/components/footer/footer";
import Header from "@/components/header/header";
import Link from "next/link";
import Lucide from "@/utils/lucide";
import { erro, notificacao } from "@/utils/toast";
import { cadastrarUsuario } from "../api/genericService";

const CadastroUsuario = () => {
  const [nome, setNome] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [senha, setSenha] = useState<string>("");
  const [telefone, setTelefone] = useState<string>("");

  async function salvarUsuario(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    try {
      const dados = {
        nome,
        email,
        senha,
        telefone,
      };

      await cadastrarUsuario(dados);
      notificacao("Usuário cadastrado!");
    } catch (error: any) {
      erro("Erro ao cadastrar!");
    }
  }

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
          {/* Adicionado o onSubmit aqui */}
          <form className="form info2" onSubmit={salvarUsuario}>
            <h1>Cadastrar Usuário</h1>

            <div className="campo_form max_width">
              <Lucide name="UserRoundPlus" className="lucide" />
              <input
                type="text"
                id="nome"
                placeholder=" "
                className="input"
                value={nome}
                onChange={(e) => setNome(e.target.value)}
                required
              />
              <label htmlFor="nome" className="label">
                Nome
              </label>
            </div>

            <div className="campo_form max_width">
              <Lucide name="MailPlus" className="lucide" />
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
                E-Mail
              </label>
            </div>

            <div className="campo_form max_width">
              <Lucide name="Lock" className="lucide" />
              <input
                type="text"
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
            </div>

            <div className="campo_form max_width">
              <Lucide name="Phone" className="lucide" />
              <input
                type="text"
                id="telefone"
                placeholder=" "
                className="input"
                value={telefone}
                onChange={(e) => setTelefone(e.target.value)}
                required
              />
              <label htmlFor="telefone" className="label">
                Telefone
              </label>
            </div>

            <div className="row">
              <Link href="/home" className="btn2">
                Voltar
              </Link>
              <Button type="submit">Salvar</Button>
            </div>
          </form>
        </section>
      </main>
      <Footer />
    </>
  );
};

export default CadastroUsuario;
