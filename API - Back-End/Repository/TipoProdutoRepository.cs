using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReHope.Contexts;
using ReHope.Domains;
using ReHope.Interfaces;

namespace ReHope.Repository
{
    public class TipoProdutoRepository : ITipoProdutoRepository
    {
        private readonly ReHopeContext _context;
        public TipoProdutoRepository(ReHopeContext context)
        {
            _context = context;
        }

        public List<TipoProduto> Listar()
        {
            List<TipoProduto> tipoProdutos = _context.TipoProduto.Include(tp => tp.Categoria).ToList();
            return tipoProdutos;
        }

        public List<TipoProduto> ListarCategoria(int tipoId)
        {
            List<TipoProduto> tipoProdutos = _context.TipoProduto.Where(tp => tp.TipoProdutoID == tipoId).Include(tp => tp.Categoria).ToList();
            return tipoProdutos;
        }

        public TipoProduto BuscarPorNome(string nomeTipo)
        {
            return _context.TipoProduto.FirstOrDefault(tipoUsuario => tipoUsuario.NomeTipo.ToLower() == nomeTipo.ToLower())!;
        }

        public TipoProduto BuscarPorID(int tipoId)
        {
            return _context.TipoProduto.Find(tipoId)!;
        }

        public void Adicionar(TipoProduto tipoProduto)
        {
            _context.TipoProduto.Add(tipoProduto);
            _context.SaveChanges();
        }

        public void Atualizar(TipoProduto tipoProduto)
        {
            if (tipoProduto == null)
            {
                return;
            }

            var existente = _context.TipoProduto.Find(tipoProduto.TipoProdutoID);
            if (existente == null)
            {
                return;
            }

            existente.NomeTipo = tipoProduto.NomeTipo;
            _context.SaveChanges();
        }


        public void Remover(int id)
        {
            TipoProduto? tpProduto = _context.TipoProduto.FirstOrDefault(tpProduto => tpProduto.TipoProdutoID == id);

            if (tpProduto == null)
            {
                return;
            }

            _context.TipoProduto.Remove(tpProduto);
            _context.SaveChanges();
        }
    }
}