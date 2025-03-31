using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcMarca
    {
        private readonly AppDbContext _context;

        public SrvcMarca(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CriarMarcaAsync(Marca marca)
        {
            try
            {
                if (marca == null) throw new ArgumentNullException(nameof(marca));

                _context.Marcas.Add(marca);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar marca: {ex.Message}");
                return false;
            }
        }

        public async Task<Marca> CarregarMarcaPorIdAsync(long id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                return await _context.Marcas.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar marca: {ex.Message}");
                return null;
            }
        }

        public async Task<Marca> CarregarMarcaPorNomeAsync(string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido.");
                return await _context.Marcas.FirstOrDefaultAsync(m => m.TX_NOME.Equals(nome, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar marca por nome: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarMarcaAsync(Marca marca)
        {
            try
            {
                if (marca == null) throw new ArgumentNullException(nameof(marca));

                _context.Marcas.Update(marca);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar marca: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirMarcaAsync(long id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var marca = await _context.Marcas.FindAsync(id);
                if (marca == null) return false;

                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir marca: {ex.Message}");
                return false;
            }
        }
    }
}
