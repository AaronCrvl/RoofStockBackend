using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Repositorios;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcUsuario
    {
        private readonly Repository<Usuario> _usuarioRepository;

        public SrvcUsuario(AppDbContext context)
        {
            _usuarioRepository = new Repository<Usuario>(context);
        }

        public async Task<bool> CriarUsuarioAsync(Usuario usuario)
        {
            try
            {
                if (usuario == null) throw new ArgumentNullException(nameof(usuario));
                await _usuarioRepository.AddAsync(usuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar usuário: {ex.Message}");
                return false;
            }
        }

        public async Task<Usuario> CarregarUsuarioPorIdAsync(long id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                return await _usuarioRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar usuário: {ex.Message}");
                return null;
            }
        }

        public async Task<Usuario> CarregarUsuarioPorLoginAsync(string login)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login)) throw new ArgumentException("Login inválido.");
                var usuario = await _usuarioRepository.GetAllAsync();
                return usuario.FirstOrDefault(u => u.TX_LOGIN == login);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar usuário por login: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarUsuarioAsync(Usuario usuario)
        {
            try
            {
                if (usuario == null) throw new ArgumentNullException(nameof(usuario));
                await _usuarioRepository.UpdateAsync(usuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar usuário: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DesativarUsuarioAsync(long id)
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

        public async Task<bool> ExcluirUsuarioAsync(long id)
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
    }
}