using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Models;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Repositorio
{
    public class RepoUsuario
    {
        #region Construtor
        // Inicialização, se necessária, pode ser adicionada aqui
        #endregion

        #region Métodos Públicos

        // Carregar usuário por ID
        public async Task<Models.Usuario> CarregarUsuario(long id)
        {
            try
            {
                var selectedUser = await ctxUsuario.GetUser(id);
                if (selectedUser != null && selectedUser.ID_USUARIO > 0)
                {
                    return MapToModel(selectedUser);
                }

                return GetDefaultUser();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar usuário", e);
            }
        }

        // Carregar usuário por nome de login (TX_LOGIN)
        public async Task<Models.Usuario> CarregarUsuario(string login)
        {
            try
            {
                var selectedUser = await ctxUsuario.GetUser(login);
                if (selectedUser != null && selectedUser.ID_USUARIO > 0)
                {
                    return MapToModel(selectedUser);
                }

                return GetDefaultUser();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar usuário", e);
            }
        }

        // Criar novo usuário
        public async Task<Models.Usuario> CriarUsuario(Models.Usuario userModel)
        {
            try
            {
                var newUser = new Database.Dados.Objetos.Usuario
                {
                    ID_USUARIO = userModel.Id,
                    ID_FUNCIONARIO = userModel.IdFuncionario,  // Assuming IdFuncionario is mapped to ID_FUNCIONARIO
                    TX_LOGIN = userModel.Login,
                    TX_SENHA = userModel.Password,
                    TX_EMAIL = userModel.Email,
                    IN_ATIVO = userModel.Ativo,
                    DT_CRIACAO = userModel.DataCriacao
                };

                var ok = await ctxUsuario.CreateUser(newUser);
                if (ok)
                {
                    var selectedUser = await ctxUsuario.GetUser(userModel.Login);
                    return MapToModel(selectedUser);
                }

                return GetDefaultUser();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao criar usuário", e);
            }
        }

        // Alterar dados do usuário
        public async Task<Models.Usuario> AlterarUsuario(Models.Usuario userModel)
        {
            try
            {
                var updatedUser = new Database.Dados.Objetos.Usuario
                {
                    ID_USUARIO = userModel.Id,
                    ID_FUNCIONARIO = userModel.IdFuncionario,  // Assuming IdFuncionario is mapped to ID_FUNCIONARIO
                    TX_LOGIN = userModel.Login,
                    TX_SENHA = userModel.Password,
                    TX_EMAIL = userModel.Email,
                    IN_ATIVO = userModel.Ativo,
                    DT_CRIACAO = userModel.DataCriacao
                };

                var ok = await ctxUsuario.AlterUser(updatedUser);
                if (ok)
                {
                    var selectedUser = await ctxUsuario.GetUser(userModel.Login);
                    return MapToModel(selectedUser);
                }

                return GetDefaultUser();
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao alterar usuário", e);
            }
        }

        #endregion

        #region Métodos Auxiliares

        // Mapeamento do objeto Database.Dados.Objetos.Usuario para Models.User
        private Models.Usuario MapToModel(Database.Dados.Objetos.Usuario user)
        {
            return new Models.Usuario
            {
                Id = user.ID_USUARIO,
                IdFuncionario = user.ID_FUNCIONARIO,  // Assuming IdFuncionario maps to ID_FUNCIONARIO
                Login = user.TX_LOGIN,
                Password = user.TX_SENHA,
                Email = user.TX_EMAIL,
                Ativo = user.IN_ATIVO,
                DataCriacao = user.DT_CRIACAO
            };
        }

        // Retorna um usuário padrão em caso de erro ou não encontrado
        private Models.Usuario GetDefaultUser()
        {
            return new Models.Usuario
            {
                Id = -1,
                IdFuncionario = -1,
                Login = string.Empty,
                Password = string.Empty,
                Email = string.Empty,
                Ativo = false,
                DataCriacao = DateTime.MinValue
            };
        }

        #endregion
    }
}
