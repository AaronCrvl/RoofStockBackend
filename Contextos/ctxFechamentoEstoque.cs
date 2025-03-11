using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxFechamentoEstoque
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateFechamentoEstoque(FechamentoEstoque fechamentoEstoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO FechamentoEstoque (ID_ESTOQUE, DT_FECHAMENTO, IN_ERRO) VALUES (@ID_ESTOQUE, @DT_FECHAMENTO, @IN_ERRO)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", fechamentoEstoque.ID_ESTOQUE);
                    command.Parameters.AddWithValue("@DT_FECHAMENTO", fechamentoEstoque.DT_FECHAMENTO);
                    command.Parameters.AddWithValue("@IN_ERRO", fechamentoEstoque.IN_ERRO);

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

        public static async Task<FechamentoEstoque> GetFechamentoEstoque(long id)
        {
            try
            {
                FechamentoEstoque fechamentoEstoque = new FechamentoEstoque();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM FechamentoEstoque WHERE ID_FECHAMENTO = @ID_FECHAMENTO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FECHAMENTO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            fechamentoEstoque = new FechamentoEstoque
                            {
                                ID_FECHAMENTO = (long)reader["ID_FECHAMENTO"],
                                ID_ESTOQUE = (long)reader["ID_ESTOQUE"],
                                DT_FECHAMENTO = (DateTime)reader["DT_FECHAMENTO"],
                                IN_ERRO = (bool)reader["IN_ERRO"]
                            };
                        }
                    }
                }

                return fechamentoEstoque;
            }
            catch (Exception)
            {
                return new FechamentoEstoque();
            }
        }

        public static async Task<bool> UpdateFechamentoEstoque(FechamentoEstoque fechamentoEstoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE FechamentoEstoque SET ID_ESTOQUE = @ID_ESTOQUE, DT_FECHAMENTO = @DT_FECHAMENTO, IN_ERRO = @IN_ERRO WHERE ID_FECHAMENTO = @ID_FECHAMENTO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", fechamentoEstoque.ID_ESTOQUE);
                    command.Parameters.AddWithValue("@DT_FECHAMENTO", fechamentoEstoque.DT_FECHAMENTO);
                    command.Parameters.AddWithValue("@IN_ERRO", fechamentoEstoque.IN_ERRO);
                    command.Parameters.AddWithValue("@ID_FECHAMENTO", fechamentoEstoque.ID_FECHAMENTO);

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

        public static async Task<bool> DeleteFechamentoEstoque(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM FechamentoEstoque WHERE ID_FECHAMENTO = @ID_FECHAMENTO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FECHAMENTO", id);

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