using Microsoft.AspNetCore.Http.HttpResults;
using RoofStockBackend.Contextos;
using RoofStockBackend.Contexts;
using RoofStockBackend.Database.Data.Objects;
using RoofStockBackend.Models;

namespace RoofStockBackend.Repositorio
{
    public class RepoUsuario
    {
        

        #region Construtor                
        #endregion        

        #region Métodos Públicos
        public async Task<Models.User> CarregarUsuario(long id)
        {
            try
            {
                var selectedUser = ctxUsuario.GetUser(id).Result;
                if (selectedUser.Id > 0)
                    return new Models.User
                    {
                        Id = selectedUser.Id,
                        UserEmail = selectedUser.Username,
                        Username = selectedUser.Email,
                        Password = selectedUser.Senha,
                        CreationDate = selectedUser.DataCriacao
                    };
                else
                    return new Models.User
                    {
                        Id = -1,
                        UserEmail = string.Empty,
                        Username = string.Empty,
                        Password = string.Empty,
                        CreationDate = new DateTime()
                    };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Models.User> CarregarUsuario(string username)
        {
            try
            {
                var selectedUser = ctxUsuario.GetUser(username).Result;
                if (selectedUser.Id > 0)
                    return new Models.User
                    {
                        Id = selectedUser.Id,
                        UserEmail = selectedUser.Username,
                        Username = selectedUser.Email,
                        Password = selectedUser.Senha,
                        CreationDate = selectedUser.DataCriacao
                    };
                else
                    return new Models.User
                    {
                        Id = -1,
                        UserEmail = string.Empty,
                        Username = string.Empty,
                        Password = string.Empty,
                        CreationDate = new DateTime()
                    };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Models.User> CriarUsuario(Models.User userModel)
        {
            try
            {
                var ok = ctxUsuario.CreateUser(new Database.Dados.Objetos.Usuario { Id = userModel.Id, Username = userModel.Username, Senha = userModel.Password, DataCriacao = userModel.CreationDate }).Result;
                var selectedUser = ctxUsuario.GetUser(userModel.Username).Result;

                if (ok)
                    return new Models.User
                    {
                        Id = selectedUser.Id,
                        UserEmail = selectedUser.Username,
                        Username = selectedUser.Email,
                        Password = selectedUser.Senha,
                        CreationDate = selectedUser.DataCriacao
                    };
                else
                    return new Models.User
                    {
                        Id = -1,
                        UserEmail = string.Empty,
                        Username = string.Empty,
                        Password = string.Empty,
                        CreationDate = new DateTime()
                    };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Models.User> AlterarUsuario(Models.User userModel)
        {
            try
            {
                var ok = ctxUsuario.AlterUser(new Database.Dados.Objetos.Usuario { Id = userModel.Id, Username = userModel.Username, Senha = userModel.Password, DataCriacao = userModel.CreationDate }).Result;
                var selectedUser = ctxUsuario.GetUser(userModel.Username).Result;

                if (ok)
                    return new Models.User
                    {
                        Id = selectedUser.Id,
                        UserEmail = selectedUser.Username,
                        Username = selectedUser.Email,
                        Password = selectedUser.Senha,
                        CreationDate = selectedUser.DataCriacao
                    };
                else
                    return new Models.User
                    {
                        Id = -1,
                        UserEmail = string.Empty,
                        Username = string.Empty,
                        Password = string.Empty,
                        CreationDate = new DateTime()
                    };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}