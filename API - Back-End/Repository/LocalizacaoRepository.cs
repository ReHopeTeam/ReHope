using ReHope.Contexts;
using ReHope.Domains;
using ReHope.Interfaces;

namespace ReHope.Repository
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly ReHopeContext _context;

        public LocalizacaoRepository(ReHopeContext context)
        {
            _context = context;
        }

        public List<Localizacao> Listar()
        {
            return _context.Localizacao.ToList();
        }

        public Localizacao? BuscarPorID(int id)
        {
            return _context.Localizacao.Find(id);
        }

        public bool NomeExiste(string nomeLocalizacao, int? localizacaoIDAtual = null)
        {
            var consulta = _context.Localizacao.AsQueryable();

            if (localizacaoIDAtual.HasValue)
            {
                consulta = consulta.Where(l => l.LocalizacaoID != localizacaoIDAtual.Value);
            }

            return consulta.Any(l => l.NomeLocalizacao == nomeLocalizacao);
        }
        public void Adicionar(Localizacao localizacao)
        {
            _context.Localizacao.Add(localizacao);
            _context.SaveChanges();
        }

        public void Atualizar(Localizacao localizacao)
        {
            if (localizacao == null)
            {
                return;
            }

            Localizacao localBanco = _context.Localizacao.Find(localizacao.LocalizacaoID);

            if (localizacao == null)
            {
                return;
            }

            localBanco.NomeLocalizacao = localizacao.NomeLocalizacao;
            _context.SaveChanges();
        }


        public void Remover(int id)
        {
            Localizacao? localizacao = _context.Localizacao.FirstOrDefault(l => l.LocalizacaoID == id);

            if (localizacao == null)
            {
                return;
            }

            _context.Localizacao.Remove(localizacao);
            _context.SaveChanges();
        }
    }
}
