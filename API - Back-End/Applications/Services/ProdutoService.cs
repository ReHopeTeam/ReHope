using ReHope.Applications.Conversoes;
using ReHope.Applications.ImageDescription;
using ReHope.Domains;
using ReHope.DTOs.ProdutoDto;
using ReHope.Exceptions;
using ReHope.Interfaces;

namespace ReHope.Applications.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IContentSafetyRepository _contentSafety;
        private readonly IImageDescriptionRepository _imageDescription;

        public ProdutoService(IProdutoRepository repository, IContentSafetyRepository contentSafety, IImageDescriptionRepository imageDescription)
        {
            _repository = repository;
            _contentSafety = contentSafety;
            _imageDescription = imageDescription;
        }

        private async Task ValidarConteudoProdutoAsync(string nome, string descricao)
        {
            string textoParaValidar = $@"
                Nome do produto: {nome}
                Descrição do produto: {descricao}";

            var resultado = await _contentSafety.ValidarConteudo(textoParaValidar);

            if (!resultado.aprovado)
            {
                throw new DomainException(resultado.msg);
            }
        }

        private async Task<string> GerarDescricaoProdutoAsync(CriarProdutoDto produtoDto)
        {
            if (!string.IsNullOrWhiteSpace(produtoDto.Descricao))
            {
                return produtoDto.Descricao;
            }

            if (string.IsNullOrWhiteSpace(produtoDto.Descricao)
                 && (produtoDto.Imagem == null || produtoDto.Imagem.Length == 0))
            {
                throw new DomainException(
                    "Informe uma descrição ou envie uma imagem para gerar a descrição automaticamente.");
            }

            return await _imageDescription.CriarDescricao(produtoDto.Imagem);
        }

        public List<LerProdutoDto> Listar()
        {
            List<Produto> produtos = _repository.Listar();

            List<LerProdutoDto> produtoDto = produtos.Select(ConverterProdutoParaDto.ConverterParaDto).ToList();

            return produtoDto;
        }

        public LerProdutoDto ObterPorId(Guid id)
        {
            Produto produto = _repository.ObterPorId(id);

            if (produto == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            return ConverterProdutoParaDto.ConverterParaDto(produto);
        }

        public LerProdutoDto ObterPorCodigo(int codigo)
        {
            Produto produto = _repository.ObterPorCodigo(codigo);

            if (produto == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            return ConverterProdutoParaDto.ConverterParaDto(produto);
        }

        private static void ValidarCadastro(CriarProdutoDto produtoDto)
        {
            if (string.IsNullOrWhiteSpace(produtoDto.NomeProduto))
            {
                throw new DomainException("Nome é obrigatório.");
            }

            if (produtoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero.");
            }

            if ((string.IsNullOrWhiteSpace(produtoDto.Descricao))
                && (produtoDto.Imagem == null || produtoDto.Imagem.Length == 0))
            {
                throw new DomainException(
                    "Informe uma descrição ou envie uma imagem para gerar a descrição automaticamente.");
            }

            if (produtoDto.Tamanho == null)
            {
                throw new DomainException("Produto precisa de um tamanho.");
            }

            if (produtoDto.CategoriaID == 0)
            {
                throw new DomainException("Produto precisa de uma categoria.");
            }

            if (produtoDto.LocalizacaoID == 0)
            {
                throw new DomainException("Produto precisa de uma localização.");
            }
        }

        public async Task<LerProdutoDto> Adicionar(CriarProdutoDto produtoDto, Guid usuarioId, int categoriaId, int localizacaoId)
        {
            ValidarCadastro(produtoDto);

            string descricao = await GerarDescricaoProdutoAsync(produtoDto);

            await ValidarConteudoProdutoAsync(produtoDto.NomeProduto, descricao);

            Produto produto = new Produto
            {
                NomeProduto = produtoDto.NomeProduto,
                Preco = produtoDto.Preco,
                Descricao = descricao,
                Tamanho = produtoDto.Tamanho,
                Imagem = ConverterImagemParaBytes.ConverterImagem(produtoDto.Imagem),
                StatusProduto = true,
                UsuarioID = usuarioId,
                CategoriaID = produtoDto.CategoriaID,
                LocalizacaoID = produtoDto.LocalizacaoID
            };

            _repository.Adicionar(produto);

            return ConverterProdutoParaDto.ConverterParaDto(produto);
        }

        public LerProdutoDto Atualizar(Guid id, AtualizarProdutoDto produtoDto)
        {
            Produto produtoBanco = _repository.ObterPorId(id);

            Console.WriteLine(produtoDto.Tamanho);

            if (produtoBanco == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            if (produtoBanco.Imagem == null)
            {
                throw new DomainException("Produto precisa de uma imagem.");
            }

            if (produtoDto.NomeProduto == null)
            {
                throw new DomainException("Produto precisa de um nome.");
            }

            if (produtoDto.Tamanho == null)
            {
                throw new DomainException("Produto precisa de um tamanho.");
            }

            if (produtoDto.LocalizacaoID == 0)
            {
                throw new DomainException("Produto precisa de uma localização.");
            }

            if (produtoDto.Preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero.");
            }

            if (string.IsNullOrWhiteSpace(produtoDto.Descricao))
            {
                throw new DomainException("Produto precisa de uma descrição.");
            }

            produtoBanco.NomeProduto = produtoDto.NomeProduto;
            produtoBanco.Preco = produtoDto.Preco;
            produtoBanco.Descricao = produtoDto.Descricao;
            produtoBanco.CategoriaID = produtoDto.CategoriaID;

            if (produtoDto.Imagem != null && produtoDto.Imagem.Length > 0)
            {
                produtoBanco.Imagem = ConverterImagemParaBytes.ConverterImagem(produtoDto.Imagem);
            }

            if (produtoDto.StatusProduto != null)
            {
                produtoBanco.StatusProduto = produtoDto.StatusProduto.Value;
            }

            _repository.Atualizar(produtoBanco);
            return ConverterProdutoParaDto.ConverterParaDto(produtoBanco);
        }

        public void Remover(Guid id)
        {
            Produto produto = _repository.ObterPorId(id);

            if (produto == null)
            {
                throw new DomainException("Produto não encontrado.");
            }

            _repository.Remover(id);
        }
    } 
}
