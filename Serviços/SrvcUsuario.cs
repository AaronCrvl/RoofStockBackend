using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Usuario;
using RoofStockBackend.Repositorios;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcUsuario
    {
        #region Propriedades Privadas
        private Repository<Usuario> _usuarioRepository;
        private Repository<Funcionario> _funcionarioRepository;
        private Repository<Empresa> _empresaRepository;
        #endregion

        #region Construtor
        public SrvcUsuario(AppDbContext context)
        {
            _usuarioRepository = new Repository<Usuario>(context);
            _funcionarioRepository = new Repository<Funcionario>(context);
            _empresaRepository = new Repository<Empresa>(context);
        }
        #endregion

        #region Métodos Públicos
        public async Task<bool> CriarUsuarioAsync(UsuarioCriarDto usuarioCriar)
        {
            try
            {
                if (usuarioCriar == null) throw new ArgumentNullException(nameof(usuarioCriar));

                var empresa = (await _empresaRepository.GetAllAsync()).Where(emp => emp.TX_CNPJ == usuarioCriar.cnpjEmpresa).FirstOrDefault();
                if (empresa == null) throw new Exception("Empresa não encontrado.");

                var funcionario = new Funcionario
                {
                    DT_ENTRADA = DateTime.Now,
                    ID_CARGO = usuarioCriar.cargo,
                    ID_EMPRESA = empresa.ID_EMPRESA,
                    TX_CPF = usuarioCriar.cpf,
                    TX_EMAIL = usuarioCriar.email,
                    TX_NOME = usuarioCriar.nomePessoa,
                    TX_TELEFONE = usuarioCriar.telefone,

                };
                await _funcionarioRepository.AddAsync(funcionario);

                var usuario = new Usuario
                {
                    ID_FUNCIONARIO = (await _funcionarioRepository.GetAllAsync()).Where(func => func.TX_CPF == usuarioCriar.cpf).FirstOrDefault().ID_FUNCIONARIO,
                    TX_LOGIN = usuarioCriar.login,
                    TX_SENHA = usuarioCriar.senha,
                    TX_EMAIL = usuarioCriar.email,
                    IN_ATIVO = usuarioCriar.ativo,
                    IN_ADMIN = usuarioCriar.admin,
                    DT_CRIACAO = DateTime.Now

                };
                await _usuarioRepository.AddAsync(usuario);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar usuário: {ex.Message} InnerException: {ex.InnerException}");
                throw;
            }
        }

        public async Task<UsuarioDto> CarregarUsuarioPorIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID inválido.");

                var usuarioBD = await _usuarioRepository.GetByIdAsync(id);
                return new UsuarioDto
                {
                    id = usuarioBD.ID_USUARIO,
                    idFuncionario = usuarioBD.ID_FUNCIONARIO,
                    admin = usuarioBD.IN_ADMIN,
                    ativo = usuarioBD.IN_ATIVO,
                    email = usuarioBD.TX_EMAIL
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar usuário: {ex.Message}");
                return null;
            }
        }

        public async Task<UsuarioDto> CarregarUsuarioPorLoginAsync(string login)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login))
                    throw new ArgumentException("Login inválido.");

                var usuario = await _usuarioRepository.GetAllAsync();
                var usuarioBD = usuario.FirstOrDefault(u => u.TX_LOGIN == login);
                return new UsuarioDto
                {
                    id = usuarioBD.ID_USUARIO,
                    idFuncionario = usuarioBD.ID_FUNCIONARIO,
                    admin = usuarioBD.IN_ADMIN,
                    ativo = usuarioBD.IN_ATIVO,
                    email = usuarioBD.TX_EMAIL
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar usuário por login: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarUsuarioAsync(UsuarioAtualizarDto usuarioAtualizar)
        {
            try
            {
                if (usuarioAtualizar == null) throw new ArgumentNullException(nameof(usuarioAtualizar));

                var usuario = new Usuario
                {
                    TX_LOGIN = usuarioAtualizar.login,
                    TX_SENHA = usuarioAtualizar.senha,
                    TX_EMAIL = usuarioAtualizar.email,
                    IN_ATIVO = usuarioAtualizar.ativo,
                    IN_ADMIN = usuarioAtualizar.admin
                };

                await _usuarioRepository.UpdateAsync(usuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar usuário: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DesativarUsuarioAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                if (usuario == null) return false;

                usuario.IN_ATIVO = false;
                await _usuarioRepository.UpdateAsync(usuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao desativar usuário: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirUsuarioAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                await _usuarioRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir usuário: {ex.Message}");
                return false;
            }
        }

        #endregion        
    }
}