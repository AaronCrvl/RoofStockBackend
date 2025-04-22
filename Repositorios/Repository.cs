using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Repositorios
{
    public class Repository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

            // DEBUG
            //Console.WriteLine($"Registros encontrados.");

            //foreach (Usuario usuario in _context.Usuario.AsEnumerable())            
            //    Console.WriteLine($"Usuario {usuario.TX_LOGIN} encontrado");

            //foreach (Estoque estoque in _context.Estoque.AsEnumerable())
            //    Console.WriteLine($"Estoque {estoque.NM_ESTOQUE} encontrado");

            //foreach (Empresa empresa in _context.Empresa.AsEnumerable())
            //    Console.WriteLine($"Empresa {empresa.TX_RAZAO_SOCIAL} encontrada");
        }

        // Criação
        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Leitura
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Atualização
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Exclusão
        public async Task DeleteAsync(long id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(long id, long id2)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
