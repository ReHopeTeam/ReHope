using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReHope.Models;

public partial class ReHopeContext : DbContext
{
    public ReHopeContext()
    {
    }

    public ReHopeContext(DbContextOptions<ReHopeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Localizacao> Localizacaos { get; set; }

    public virtual DbSet<LogProduto> LogProdutos { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<TipoProduto> TipoProdutos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1C5ADCD0332");

            entity.HasIndex(e => e.NomeCategoria, "UQ__Categori__98459A0BD246A42C").IsUnique();

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.NomeCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoProdutoId).HasColumnName("TipoProdutoID");

            entity.HasOne(d => d.TipoProduto).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.TipoProdutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categoria_TipoProduto");
        });

        modelBuilder.Entity<Localizacao>(entity =>
        {
            entity.HasKey(e => e.LocalizacaoId).HasName("PK__Localiza__83ABDECA5F4A5258");

            entity.ToTable("Localizacao");

            entity.HasIndex(e => e.NomeLocalizacao, "UQ__Localiza__76AA639AB07244D8").IsUnique();

            entity.Property(e => e.LocalizacaoId).HasColumnName("LocalizacaoID");
            entity.Property(e => e.NomeLocalizacao)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LogProduto>(entity =>
        {
            entity.HasKey(e => e.LogProdutoId).HasName("PK__LogProdu__C4788A9BC43AC1C4");

            entity.ToTable("LogProduto");

            entity.Property(e => e.LogProdutoId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("LogProdutoID");
            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.LocalizacaoIdanterior).HasColumnName("LocalizacaoIDAnterior");
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecoAnterior).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProdutoId).HasColumnName("ProdutoID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.CodigoNavigation).WithMany(p => p.LogProdutoCodigoNavigations)
                .HasPrincipalKey(p => p.Codigo)
                .HasForeignKey(d => d.Codigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProduto_Codigo");

            entity.HasOne(d => d.LocalizacaoIdanteriorNavigation).WithMany(p => p.LogProdutos)
                .HasForeignKey(d => d.LocalizacaoIdanterior)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProduto_Localizacao");

            entity.HasOne(d => d.Produto).WithMany(p => p.LogProdutoProdutos)
                .HasForeignKey(d => d.ProdutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProduto_Produto");

            entity.HasOne(d => d.Usuario).WithMany(p => p.LogProdutos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProduto_Usuario");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.ProdutoId).HasName("PK__Produto__9C8800C335F2B4F9");

            entity.ToTable("Produto", tb =>
                {
                    tb.HasTrigger("trg_AlteracaoProduto");
                    tb.HasTrigger("trg_InativarProduto");
                });

            entity.HasIndex(e => e.Codigo, "UQ__Produto__06370DAC4AA8C673").IsUnique();

            entity.Property(e => e.ProdutoId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ProdutoID");
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.Imagem).IsUnicode(false);
            entity.Property(e => e.LocalizacaoId).HasColumnName("LocalizacaoID");
            entity.Property(e => e.NomeProduto).HasMaxLength(100);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StatusProduto).HasDefaultValue(true);
            entity.Property(e => e.Tamanho)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Categoria");

            entity.HasOne(d => d.Localizacao).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.LocalizacaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Localizacao");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Usuario");
        });

        modelBuilder.Entity<TipoProduto>(entity =>
        {
            entity.HasKey(e => e.TipoProdutoId).HasName("PK__TipoProd__99B538EB0D5BCA82");

            entity.ToTable("TipoProduto");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoProd__7859A10AB926F350").IsUnique();

            entity.Property(e => e.TipoProdutoId).HasColumnName("TipoProdutoID");
            entity.Property(e => e.NomeTipo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE798D70F35F0");

            entity.ToTable("Usuario", tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.HasIndex(e => e.Telefone, "UQ__Usuario__4EC504B6FD7EDF50").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534674AFF21").IsUnique();

            entity.Property(e => e.UsuarioId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UsuarioID");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Senha).HasMaxLength(32);
            entity.Property(e => e.StatusUsuario).HasDefaultValue(true);
            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
