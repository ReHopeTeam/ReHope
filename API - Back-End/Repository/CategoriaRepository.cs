using ReHope.Contexts;
using ReHope.Domains;
﻿using Microsoft.EntityFrameworkCore;
using ReHope.DTOs.UsuarioDto;
using ReHope.Exceptions;
using ReHope.Interfaces;

namespace ReHope.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ReHopeContext _context;

        public CategoriaRepository(ReHopeContext context)
        {
            _context = context;
        }

        public List<Categoria> Listar()
        {
            return _context.Categoria
                .Include(c => c.TipoProduto)
                .ToList();
        }

        public Categoria? ObterPorId(int id)
        {
            Categoria? categoria = _context.Categoria
                .Include(c => c.TipoProduto)
                .FirstOrDefault(c => c.CategoriaID == id);

            return categoria;
        }

        public Categoria? ObterCategoriaPorTipo(string nomeTipo)
        {
            Categoria? categorias = _context.Categoria
                .Include(c => c.TipoProduto)
                .FirstOrDefault(c => c.TipoProduto.NomeTipo == nomeTipo);

            return categorias;
        }

        public Categoria? BuscarPorNome(string nomeCategoria)
        {
            return _context.Categoria
                .Include(c => c.TipoProduto)
                .FirstOrDefault(categoria => categoria.NomeCategoria == nomeCategoria);
        }

        public bool NomeCategoriaExiste(string nome, int? categoriaIdAtual = null)
        {
            var consulta = _context.Categoria.AsQueryable();

            if(categoriaIdAtual.HasValue)
            {
                consulta = consulta.Where(categoria => categoria.CategoriaID != categoriaIdAtual.Value);
            }

            return consulta.Any(c => c.NomeCategoria == nome);
        }

        public bool TipoProdutoExiste(int idTipoProduto)
        {
            return _context.TipoProduto.Any( tp => tp.TipoProdutoID == idTipoProduto)   ;
        }

        public void Adicionar(Categoria categoria)
        {
            _context.Categoria.Add(categoria);
            _context.SaveChanges();
        }

        public void Atualizar (Categoria categoria)
        {
            Categoria categoriaBanco = _context.Categoria.FirstOrDefault(c => c.CategoriaID == categoria.CategoriaID)!;

            if(categoriaBanco == null)
            {
                return;
            }

            categoriaBanco.NomeCategoria = categoria.NomeCategoria;
            categoriaBanco.TipoProdutoID = categoria.TipoProdutoID;

            _context.SaveChanges();
        }

        public void Remover (int id)
        {
            Categoria? categoriaBanco = _context.Categoria
                .Include(c => c.TipoProduto)
                .FirstOrDefault(c => c.CategoriaID == id);

            if(categoriaBanco == null)
            {
                return;
            }

            _context.Categoria.Remove(categoriaBanco);
             _context.SaveChanges();
        }
    }
}