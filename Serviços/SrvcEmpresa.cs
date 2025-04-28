using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Empresa;
using RoofStockBackend.Repositorios;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcEmpresa
    {
        #region Propriedades Privadas
        private readonly Repository<Empresa> _empresaRepository;
        private readonly Repository<EmpresaUsuario> _empresaUsuarioRepository;

        #endregion        

        #region Construtor
        public SrvcEmpresa(AppDbContext context)
        {
            _empresaRepository = new Repository<Empresa>(context);
            _empresaUsuarioRepository = new Repository<EmpresaUsuario>(context);
        }
        #endregion        

        #region Métodos Públicos

        #region Métodos EmpresaUsuario
        async Task<IEnumerable<EmpresaUsuario>> RetornarEmpresasVisiveisAtivasPorUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                return new List<EmpresaUsuario> { };

            var empresasBD = await _empresaUsuarioRepository.GetAllAsync();
            return empresasBD.Where(emp => emp.ID_USUARIO == idUsuario && emp.IN_ATIVO);
        }
        #endregion

        #region Métodos Empresa
        public async Task<IEnumerable<EmpresaDto>> CarregarEmpresasPorUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                return new List<EmpresaDto> { };

            var empresas = await _empresaRepository.GetAllAsync();
            var empresasAtivasUsuario = await RetornarEmpresasVisiveisAtivasPorUsuario(idUsuario);

            if (empresas.Count() == 0 || empresasAtivasUsuario.Count() == 0)
                return new List<EmpresaDto> { };

            var empresasRetorno = empresas.Join(
                empresasAtivasUsuario,
                empresasBD => empresasBD.ID_EMPRESA,
                empresasUsuario => empresasUsuario.ID_EMPRESA,
                (empresasBD, empresasUsuario) => new { empresasBD, empresasUsuario });

            var listaEmpresas = new List<EmpresaDto> { };
            foreach (var emp in empresasRetorno)
                listaEmpresas.Add(new EmpresaDto
                {
                    id = emp.empresasBD.ID_EMPRESA,
                    ativo = emp.empresasBD.IN_ATIVO,
                    cnpj = emp.empresasBD.TX_CNPJ,
                    email = emp.empresasBD.TX_EMAIL,
                    razaoSocial = emp.empresasBD.TX_RAZAO_SOCIAL,
                });

            return listaEmpresas;
        }

        public async Task<bool> CriarEmpresaAsync(Empresa empresa)
        {
            try
            {
                if (empresa == null) throw new ArgumentNullException(nameof(empresa));
                await _empresaRepository.AddAsync(empresa);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<EmpresaDto> CarregarEmpresaPorIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var empresaBD = await _empresaRepository.GetByIdAsync(id);

                return new EmpresaDto
                {
                    id = empresaBD.ID_EMPRESA,
                    razaoSocial = empresaBD.TX_RAZAO_SOCIAL,
                    cnpj = empresaBD.TX_RAZAO_SOCIAL,
                    ativo = empresaBD.IN_ATIVO,
                    email = empresaBD.TX_EMAIL
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar empresa: {ex.Message}");
                return null;
            }
        }

        public async Task<EmpresaDto> CarregarEmpresaPorNomeAsync(string nomeEmpresa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomeEmpresa)) throw new ArgumentException("Nome inválido.");
                var empresas = await _empresaRepository.GetAllAsync();
                var empresaBD = empresas.FirstOrDefault(e => e.TX_RAZAO_SOCIAL == nomeEmpresa);

                return new EmpresaDto
                {
                    id = empresaBD.ID_EMPRESA,
                    razaoSocial = empresaBD.TX_RAZAO_SOCIAL,
                    cnpj = empresaBD.TX_RAZAO_SOCIAL,
                    ativo = empresaBD.IN_ATIVO,
                    email = empresaBD.TX_EMAIL
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar empresa por nome: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarEmpresaAsync(Empresa empresa)
        {
            try
            {
                if (empresa == null) throw new ArgumentNullException(nameof(empresa));
                await _empresaRepository.UpdateAsync(empresa);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirEmpresaAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                await _empresaRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DesativarEmpresaAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var empresa = await _empresaRepository.GetByIdAsync(id);
                if (empresa == null) return false;

                empresa.IN_ATIVO = false;
                await _empresaRepository.UpdateAsync(empresa);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao desativar empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AtivarEmpresaAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var empresa = await _empresaRepository.GetByIdAsync(id);
                if (empresa == null) return false;

                empresa.IN_ATIVO = true;
                await _empresaRepository.UpdateAsync(empresa);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ativar empresa: {ex.Message}");
                return false;
            }
        }
        #endregion

        #endregion
    }
}
