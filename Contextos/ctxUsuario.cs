using RoofStockBackend.Contexts.Records;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Azure.Identity;
using RoofStockBackend.Database.Dados.Objetos;
//using RoofStockBackend.Database.Data.Lists;

namespace RoofStockBackend.Contextos
{
    public static class ctxUsuario
    {
        private static string connectinString = string.Empty;

        #region Methods
        public static async Task<bool> CreateUser(Usuario User)
        {
            try
            {
                await using (var transaction = new Microsoft.Data.SqlClient.SqlConnection(connectinString))
                {
                    using (SqlConnection connection = new SqlConnection(
                     connectinString))
                    {
                        SqlCommand command = new SqlCommand($"INSERT INTO [Usuario](LOGIN, PASSWORD, CREATION_DATE) VALUES({User.Username}, {User.Senha}, {User.DataCriacao})", connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static async Task<bool> AlterUser(Usuario User)
        {
            try
            {
                await using (var transaction = new Microsoft.Data.SqlClient.SqlConnection(connectinString))
                {
                    using (SqlConnection connection = new SqlConnection(
                     connectinString))
                    {
                        SqlCommand command = new SqlCommand($"UPDATE [Usuario](LOGIN, PASSWORD, CREATION_DATE) VALUES({User.Username}, {User.Senha}, {User.DataCriacao})", connection);
                        command.Connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<Usuario> GetUser(long id)
        {
            try
            {
                Usuario systemUser = new Usuario();

                await using (var transaction = new Microsoft.Data.SqlClient.SqlConnection(connectinString))
                {
                    using (SqlConnection connection = new SqlConnection(
                     connectinString))
                    {
                        string oString = $"Select * from User where Id={id}";
                        SqlCommand oCmd = new SqlCommand(oString, connection);
                        connection.Open();
                        using (SqlDataReader oReader = oCmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                systemUser = new Usuario
                                {
                                    Id = long.Parse(oReader["ID"].ToString()),
                                    Username = oReader["USERNAME"].ToString(),
                                    Senha = oReader["PASSWORD"].ToString(),
                                    DataCriacao = DateTime.Parse(oReader["CREATION_DATE"].ToString()),
                                };
                            }

                            connection.Close();
                        }
                    }

                    return systemUser;
                }
            }
            catch (Exception)
            {
                return new Usuario
                {
                    Id = -1,
                    Username = string.Empty,
                    Senha = string.Empty,
                    DataCriacao = new DateTime()
                };
            }
        }

        public static async Task<Usuario> GetUser(string username)
        {
            try
            {
                Usuario systemUser = new Usuario();

                await using (var transaction = new Microsoft.Data.SqlClient.SqlConnection(connectinString))
                {
                    using (SqlConnection connection = new SqlConnection(
                     connectinString))
                    {
                        string oString = $"Select * from User where Username={username}";
                        SqlCommand oCmd = new SqlCommand(oString, connection);
                        connection.Open();
                        using (SqlDataReader oReader = oCmd.ExecuteReader())
                        {
                            while (oReader.Read())
                            {
                                systemUser = new Usuario
                                {

                                    Id = long.Parse(oReader["ID"].ToString()),
                                    Username = oReader["USERNAME"].ToString(),
                                    Senha = oReader["PASSWORD"].ToString(),
                                    DataCriacao = DateTime.Parse(oReader["CREATION_DATE"].ToString()),
                                };
                            }

                            connection.Close();
                        }
                    }

                    return systemUser;
                }
            }
            catch (Exception)
            {
                return new Usuario
                {
                    Id = -1,
                    Username = string.Empty,
                    Senha = string.Empty,
                    DataCriacao = new DateTime()
                };
            }
        }
        #endregion
    }
}