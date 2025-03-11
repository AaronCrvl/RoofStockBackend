using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxUsuario
    {
        private static string connectionString = "";

        #region Métodos

        // Criar Usuário
        public static async Task<bool> CreateUser(Usuario user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO [Usuario] (ID_FUNCIONARIO, TX_LOGIN, TX_SENHA, DT_CRIACAO, TX_EMAIL, IN_ATIVO) " +
                                   "VALUES (@ID_FUNCIONARIO, @TX_LOGIN, @TX_SENHA, @DT_CRIACAO, @TX_EMAIL, @IN_ATIVO)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", user.ID_FUNCIONARIO);
                    command.Parameters.AddWithValue("@TX_LOGIN", user.TX_LOGIN);
                    command.Parameters.AddWithValue("@TX_SENHA", user.TX_SENHA);
                    command.Parameters.AddWithValue("@DT_CRIACAO", user.DT_CRIACAO);
                    command.Parameters.AddWithValue("@TX_EMAIL", user.TX_EMAIL);
                    command.Parameters.AddWithValue("@IN_ATIVO", user.IN_ATIVO);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) for debugging purposes if needed
                return false;
            }
        }

        // Obter Usuário por ID
        public static async Task<Usuario> GetUser(long id)
        {
            try
            {
                Usuario systemUser = new Usuario();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Usuario WHERE ID_USUARIO = @ID_USUARIO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_USUARIO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            systemUser = new Usuario
                            {
                                ID_USUARIO = (long)reader["ID_USUARIO"],
                                ID_FUNCIONARIO = (long)reader["ID_FUNCIONARIO"],
                                TX_LOGIN = reader["TX_LOGIN"].ToString(),
                                TX_SENHA = reader["TX_SENHA"].ToString(),
                                TX_EMAIL = reader["TX_EMAIL"].ToString(),
                                IN_ATIVO = (bool)reader["IN_ATIVO"],
                                DT_CRIACAO = (DateTime)reader["DT_CRIACAO"]
                            };
                        }
                    }
                }

                return systemUser;
            }
            catch (Exception)
            {
                return new Usuario
                {
                    ID_USUARIO = -1,
                    ID_FUNCIONARIO = -1,
                    TX_LOGIN = string.Empty,
                    TX_SENHA = string.Empty,
                    TX_EMAIL = string.Empty,
                    IN_ATIVO = false,
                    DT_CRIACAO = DateTime.MinValue
                };
            }
        }

        // Obter Usuário por Username
        public static async Task<Usuario> GetUser(string username)
        {
            try
            {
                Usuario systemUser = new Usuario();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Usuario WHERE TX_LOGIN = @TX_LOGIN";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_LOGIN", username);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            systemUser = new Usuario
                            {
                                ID_USUARIO = (long)reader["ID_USUARIO"],
                                ID_FUNCIONARIO = (long)reader["ID_FUNCIONARIO"],
                                TX_LOGIN = reader["TX_LOGIN"].ToString(),
                                TX_SENHA = reader["TX_SENHA"].ToString(),
                                TX_EMAIL = reader["TX_EMAIL"].ToString(),
                                IN_ATIVO = (bool)reader["IN_ATIVO"],
                                DT_CRIACAO = (DateTime)reader["DT_CRIACAO"]
                            };
                        }
                    }
                }

                return systemUser;
            }
            catch (Exception)
            {
                return new Usuario
                {
                    ID_USUARIO = -1,
                    ID_FUNCIONARIO = -1,
                    TX_LOGIN = string.Empty,
                    TX_SENHA = string.Empty,
                    TX_EMAIL = string.Empty,
                    IN_ATIVO = false,
                    DT_CRIACAO = DateTime.MinValue
                };
            }
        }

        // Alterar Usuário
        public static async Task<bool> AlterUser(Usuario user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Usuario SET TX_LOGIN = @TX_LOGIN, TX_SENHA = @TX_SENHA, TX_EMAIL = @TX_EMAIL, IN_ATIVO = @IN_ATIVO WHERE ID_USUARIO = @ID_USUARIO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_USUARIO", user.ID_USUARIO);
                    command.Parameters.AddWithValue("@TX_LOGIN", user.TX_LOGIN);
                    command.Parameters.AddWithValue("@TX_SENHA", user.TX_SENHA);
                    command.Parameters.AddWithValue("@TX_EMAIL", user.TX_EMAIL);
                    command.Parameters.AddWithValue("@IN_ATIVO", user.IN_ATIVO);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Deletar Usuário
        public static async Task<bool> DeleteUser(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Usuario SET IN_ATIVO = 0 WHERE ID_USUARIO = @ID_USUARIO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_USUARIO", id);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}
// !_!