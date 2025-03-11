using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxErroFechamento
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateErroFechamento(ErroFechamento erroFechamento)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ErroFechamento (TX_ERRO, TX_DESCRICAO) VALUES (@TX_ERRO, @TX_DESCRICAO)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_ERRO", erroFechamento.TX_ERRO);
                    command.Parameters.AddWithValue("@TX_DESCRICAO", erroFechamento.TX_DESCRICAO);

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

        public static async Task<ErroFechamento> GetErroFechamento(long id)
        {
            try
            {
                ErroFechamento erroFechamento = new ErroFechamento();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ErroFechamento WHERE ID_ERRO = @ID_ERRO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ERRO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            erroFechamento = new ErroFechamento
                            {
                                ID_ERRO = (long)reader["ID_ERRO"],
                                TX_ERRO = reader["TX_ERRO"].ToString(),
                                TX_DESCRICAO = reader["TX_DESCRICAO"].ToString()
                            };
                        }
                    }
                }

                return erroFechamento;
            }
            catch (Exception)
            {
                return new ErroFechamento();
            }
        }

        public static async Task<bool> UpdateErroFechamento(ErroFechamento erroFechamento)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ErroFechamento SET TX_ERRO = @TX_ERRO, TX_DESCRICAO = @TX_DESCRICAO WHERE ID_ERRO = @ID_ERRO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_ERRO", erroFechamento.TX_ERRO);
                    command.Parameters.AddWithValue("@TX_DESCRICAO", erroFechamento.TX_DESCRICAO);
                    command.Parameters.AddWithValue("@ID_ERRO", erroFechamento.ID_ERRO);

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

        public static async Task<bool> DeleteErroFechamento(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ErroFechamento WHERE ID_ERRO = @ID_ERRO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ERRO", id);

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