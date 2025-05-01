using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Estoque;
using RoofStockBackend.Modelos.DTO.Estoque.Produto;
using RoofStockBackend.Modelos.DTO.Estoque.Produto.Interface;
using RoofStockBackend.Repositorios;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcEstoque
    {
        private readonly Repository<Estoque> _estoqueRepository;
        private readonly Repository<EstoqueUsuario> _estoqueUsuarioRepository;
        private readonly Repository<Produto> _produtoRepository;
        private readonly Repository<EstoqueProduto> _estoqueProdutoRepository;
        private readonly Repository<Marca> _marcaRepository;

        #region Construtor
        public SrvcEstoque(AppDbContext context)
        {
            _estoqueRepository = new Repository<Estoque>(context);
            _estoqueUsuarioRepository = new Repository<EstoqueUsuario>(context);
            _produtoRepository = new Repository<Produto>(context);
            _estoqueProdutoRepository = new Repository<EstoqueProduto>(context);
            _marcaRepository = new Repository<Marca>(context);
        }
        #endregion

        #region Métodos Públicos

        #region Métodos Estoque
        public async Task<IEnumerable<EstoqueDto>> CarregarEstoquePorUsuario(int idUsuario, int idEmpresa)
        {
            try
            {
                if (idUsuario <= 0)
                    return new List<EstoqueDto> { };

                var estoques = await _estoqueRepository.GetAllAsync();
                var estoquesAtivosUsuario = await _estoqueUsuarioRepository.GetAllAsync();
                if (estoques.Count() == 0 || estoquesAtivosUsuario.Count() == 0)
                    return new List<EstoqueDto> { };

                var estoquesRetorno = estoques.Join(
                    estoquesAtivosUsuario,
                    estoquesBD => estoquesBD.ID_ESTOQUE,
                    estoquesAtivosU => estoquesAtivosU.ID_ESTOQUE,
                    (estoquesBD, estoquesAtivosU) => new { estoquesBD, estoquesAtivosU })
                    .Where(estoque => estoque.estoquesAtivosU.ID_USUARIO == idUsuario && estoque.estoquesAtivosU.IN_ATIVO); ;

                return estoquesRetorno.Select(estq => new EstoqueDto
                {
                    idEstoque = estq.estoquesBD.ID_ESTOQUE,
                    ativo = estq.estoquesBD.IN_ATIVO,
                    nomeResponsavel = "",
                    nomeEstoque = estq.estoquesBD.TX_NOME
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> CriarEstoqueAsync(Estoque estoque)
        {
            try
            {
                if (estoque == null) throw new ArgumentNullException(nameof(estoque));
                await _estoqueRepository.AddAsync(estoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<Estoque> CarregarEstoquePorIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                return await _estoqueRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar estoque: {ex.Message}");
                return null;
            }
        }

        public async Task<Estoque> CarregarEstoquePorNomeAsync(string nomeEstoque)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomeEstoque)) throw new ArgumentException("Nome do estoque inválido.");
                var estoques = await _estoqueRepository.GetAllAsync();
                return estoques.FirstOrDefault(e => e.TX_NOME.Equals(nomeEstoque, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar estoque por nome: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarEstoqueAsync(Estoque estoque)
        {
            try
            {
                if (estoque == null) throw new ArgumentNullException(nameof(estoque));
                await _estoqueRepository.UpdateAsync(estoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirEstoqueAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                await _estoqueRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AtivarEstoqueAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var estoque = await _estoqueRepository.GetByIdAsync(id);
                if (estoque == null) return false;

                estoque.IN_ATIVO = true;
                await _estoqueRepository.UpdateAsync(estoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ativar estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DesativarEstoqueAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var estoque = await _estoqueRepository.GetByIdAsync(id);
                if (estoque == null) return false;

                estoque.IN_ATIVO = false;
                await _estoqueRepository.UpdateAsync(estoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao desativar estoque: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Métodos Produtos
        public async Task<bool> CadastrarProdutoAsync(ProdutoCadastrarDto produtoASerCadastrado)
        {
            try
            {
                ValidarProduto(produtoASerCadastrado);

                await _produtoRepository.AddAsync(new Produto
                {
                    ID_MARCA = produtoASerCadastrado.idMarca,
                    TX_NOME = produtoASerCadastrado.nomeProduto,
                    IN_PROMOCAO = produtoASerCadastrado.promocao,
                    VALOR = produtoASerCadastrado.valor
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcluirProdutoAsync(int idProduto)
        {
            try
            {
                if (idProduto <= 0) return false;
                await _produtoRepository.DeleteAsync(idProduto);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProdutoDto> AlterarProdutoAsync(ProdutoAtualizarDto produtoASerAtualizado)
        {
            try
            {
                ValidarProduto(produtoASerAtualizado);

                await _produtoRepository.UpdateAsync(new Produto
                {
                    ID_PRODUTO = produtoASerAtualizado.idProduto,
                    TX_NOME = produtoASerAtualizado.nomeProduto,
                    ID_MARCA = produtoASerAtualizado.idMarca,
                    IN_PROMOCAO = produtoASerAtualizado.promocao,
                    VALOR = produtoASerAtualizado.valor
                });

                var produtoAtualizado = await _produtoRepository.GetByIdAsync(produtoASerAtualizado.idProduto);
                if (produtoAtualizado.ID_PRODUTO <= 0)
                    return new ProdutoDto
                    {
                        idProduto = -1,
                        nomeMarca = string.Empty,
                        nomeProduto = string.Empty,
                        promocao = false,
                        valor = 0.0
                    };

                var marca = await _marcaRepository.GetByIdAsync(produtoAtualizado.ID_MARCA);
                return new ProdutoDto
                {
                    idProduto = produtoAtualizado.ID_PRODUTO,
                    nomeMarca = marca.TX_NOME,
                    nomeProduto = produtoAtualizado.TX_NOME,
                    promocao = produtoAtualizado.IN_PROMOCAO,
                    valor = produtoAtualizado.VALOR
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProdutoDto>> CarregarProdutosEstoque(int idEstoque)
        {
            try
            {
                if (idEstoque <= 0)
                    return new List<ProdutoDto>
                    { };

                var produtosAdicionadosAoEstoque = await _estoqueProdutoRepository.GetAllAsync();
                var produtoCadastrados = await _produtoRepository.GetAllAsync();
                var marcasCadastradas = await _marcaRepository.GetAllAsync();

                // Join Produto x Marca
                var produtosEmEstoque = produtoCadastrados.Join(marcasCadastradas,
                prodCad => prodCad.ID_MARCA,
                    marcaCad => marcaCad.ID_MARCA,
                    (prodCad, marcaCad) => new { prodCad, marcaCad });

                // Join EstoqueProduto x Produto
                var produtosInfo = produtosAdicionadosAoEstoque.Join(
                    produtosEmEstoque,
                    prodAdd => prodAdd.ID_PRODUTO,
                    prodCad => prodCad.prodCad.ID_PRODUTO,
                    (prodAdd, prodCad) => new { prodAdd, prodCad })
                    .Where(p => p.prodAdd.ID_ESTOQUE == idEstoque);

                return produtosEmEstoque.Select(prod => new ProdutoDto
                {
                    idProduto = prod.prodCad.ID_PRODUTO,
                    nomeMarca = prod.marcaCad.TX_NOME,
                    nomeProduto = prod.prodCad.TX_NOME,
                    promocao = prod.prodCad.IN_PROMOCAO,
                    valor = prod.prodCad.VALOR
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void ValidarProduto(object produtoParaValidar)
        {
            if (produtoParaValidar is ProdutoAtualizarDto atualizar && atualizar.idProduto <= 3)
                throw new Exception("Produto não identificado.");

            if (produtoParaValidar is IProdutoDtoBase produto)
            {
                if (string.IsNullOrWhiteSpace(produto.nomeProduto) || produto.nomeProduto.Length <= 3)
                    throw new Exception("O nome do produto deve ter mais de 3 caracteres.");

                if (produto.idMarca <= 0)
                    throw new Exception("Marca inválida para o produto.");

                if (produto.valor <= 0.0 && !produto.promocao)
                    throw new Exception("O produto não pode ter valor zerado caso não esteja em promoção.");
            }
            else
                throw new Exception("Tipo de produto inválido.");
        }

        #endregion

        #endregion
    }
}