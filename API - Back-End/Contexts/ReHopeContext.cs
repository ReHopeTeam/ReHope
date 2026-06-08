using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReHope.Domains;

namespace ReHope.Contexts;

public partial class ReHopeContext : DbContext
{
    public ReHopeContext()
    {
    }

    public ReHopeContext(DbContextOptions<ReHopeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Localizacao> Localizacao { get; set; }

    public virtual DbSet<LogProduto> LogProduto { get; set; }

    public virtual DbSet<Produto> Produto { get; set; }

    public virtual DbSet<TipoProduto> TipoProduto { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ReHope;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaID).HasName("PK__Categori__F353C1C5DD3E22CF");

            entity.HasIndex(e => e.NomeCategoria, "UQ__Categori__98459A0B26730E78").IsUnique();

            entity.Property(e => e.NomeCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.TipoProduto).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.TipoProdutoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categoria_TipoProduto");
        });

        modelBuilder.Entity<Localizacao>(entity =>
        {
            entity.HasKey(e => e.LocalizacaoID).HasName("PK__Localiza__83ABDECA867D5636");

            entity.HasIndex(e => e.NomeLocalizacao, "UQ__Localiza__76AA639AD463D947").IsUnique();

            entity.Property(e => e.NomeLocalizacao)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LogProduto>(entity =>
        {
            entity.HasKey(e => e.LogProdutoID).HasName("PK__LogProdu__C4788A9B1C8DD276");

            entity.Property(e => e.LogProdutoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecoAnterior).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.CodigoNavigation).WithMany(p => p.LogProdutoCodigoNavigation)
                .HasPrincipalKey(p => p.Codigo)
                .HasForeignKey(d => d.Codigo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProduto_Codigo");

            entity.HasOne(d => d.LocalizacaoIDAnteriorNavigation).WithMany(p => p.LogProduto)
                .HasForeignKey(d => d.LocalizacaoIDAnterior)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProduto_Localizacao");

            entity.HasOne(d => d.Produto).WithMany(p => p.LogProdutoProduto)
                .HasForeignKey(d => d.ProdutoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProduto_Produto");

            entity.HasOne(d => d.Usuario).WithMany(p => p.LogProduto)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogProduto_Usuario");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.ProdutoID).HasName("PK__Produto__9C8800C3AFCF733A");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_AlteracaoProduto");
                    tb.HasTrigger("trg_InativarProduto");
                });

            entity.HasIndex(e => e.Codigo, "UQ__Produto__06370DACE9C97253").IsUnique();

            entity.Property(e => e.ProdutoID).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Codigo).ValueGeneratedOnAdd();
            entity.Property(e => e.Imagem).IsUnicode(false);
            entity.Property(e => e.NomeProduto).HasMaxLength(100);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StatusProduto).HasDefaultValue(true);
            entity.Property(e => e.Tamanho)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Produto)
                .HasForeignKey(d => d.CategoriaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Categoria");

            entity.HasOne(d => d.Localizacao).WithMany(p => p.Produto)
                .HasForeignKey(d => d.LocalizacaoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Localizacao");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Produto)
                .HasForeignKey(d => d.UsuarioID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_Usuario");
        });

        modelBuilder.Entity<TipoProduto>(entity =>
        {
            entity.HasKey(e => e.TipoProdutoID).HasName("PK__TipoProd__99B538EB1B2741FE");

            entity.HasIndex(e => e.NomeTipo, "UQ__TipoProd__7859A10A070D5393").IsUnique();

            entity.Property(e => e.NomeTipo)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PK__Usuario__2B3DE798C626C163");

            entity.ToTable(tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.HasIndex(e => e.Telefone, "UQ__Usuario__4EC504B6372D30AB").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534F4276365").IsUnique();

            entity.Property(e => e.UsuarioID).HasDefaultValueSql("(newid())");
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
