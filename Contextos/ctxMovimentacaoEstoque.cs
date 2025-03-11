using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxMovimentacaoEstoque
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateMovimentacaoEstoque(MovimentacaoEstoque movimentacaoEstoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO MovimentacaoEstoque (ID_ESTOQUE, ID_USUARIO, DT_MOVIMENTACAO, IN_ENTRADA, IN_PROCESSADO) " +
                                   "VALUES (@ID_ESTOQUE, @ID_USUARIO, @DT_MOVIMENTACAO, @IN_ENTRADA, @IN_PROCESSADO)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", movimentacaoEstoque.ID_ESTOQUE);
                    command.Parameters.AddWithValue("@ID_USUARIO", movimentacaoEstoque.ID_USUARIO);
                    command.Parameters.AddWithValue("@DT_MOVIMENTACAO", movimentacaoEstoque.DT_MOVIMENTACAO);
                    command.Parameters.AddWithValue("@IN_ENTRADA", movimentacaoEstoque.IN_ENTRADA);
                    command.Parameters.AddWithValue("@IN_PROCESSADO", movimentacaoEstoque.IN_PROCESSADO);

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

        public static async Task<MovimentacaoEstoque> GetMovimentacaoEstoque(long id)
        {
            try
            {
                MovimentacaoEstoque movimentacaoEstoque = new MovimentacaoEstoque();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM MovimentacaoEstoque WHERE ID_MOVIMENTACAO = @ID_MOVIMENTACAO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_MOVIMENTACAO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            movimentacaoEstoque = new MovimentacaoEstoque
                            {
                                ID_MOVIMENTACAO = (long)reader["ID_MOVIMENTACAO"],
                                ID_ESTOQUE = (long)reader["ID_ESTOQUE"],
                                ID_USUARIO = (long)reader["ID_USUARIO"],
                                DT_MOVIMENTACAO = (DateTime)reader["DT_MOVIMENTACAO"],
                                IN_ENTRADA = (bool)reader["IN_ENTRADA"],
                                IN_PROCESSADO = (bool)reader["IN_PROCESSADO"]
                            };
                        }
                    }
                }

                return movimentacaoEstoque;
            }
            catch (Exception)
            {
                return new MovimentacaoEstoque();
            }
        }

        public static async Task<bool> UpdateMovimentacaoEstoque(MovimentacaoEstoque movimentacaoEstoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE MovimentacaoEstoque SET ID_ESTOQUE = @ID_ESTOQUE, ID_USUARIO = @ID_USUARIO, " +
                                   "DT_MOVIMENTACAO = @DT_MOVIMENTACAO, IN_ENTRADA = @IN_ENTRADA, IN_PROCESSADO = @IN_PROCESSADO " +
                                   "WHERE ID_MOVIMENTACAO = @ID_MOVIMENTACAO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", movimentacaoEstoque.ID_ESTOQUE);
                    command.Parameters.AddWithValue("@ID_USUARIO", movimentacaoEstoque.ID_USUARIO);
                    command.Parameters.AddWithValue("@DT_MOVIMENTACAO", movimentacaoEstoque.DT_MOVIMENTACAO);
                    command.Parameters.AddWithValue("@IN_ENTRADA", movimentacaoEstoque.IN_ENTRADA);
                    command.Parameters.AddWithValue("@IN_PROCESSADO", movimentacaoEstoque.IN_PROCESSADO);
                    command.Parameters.AddWithValue("@ID_MOVIMENTACAO", movimentacaoEstoque.ID_MOVIMENTACAO);

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

        public static async Task<bool> DeleteMovimentacaoEstoque(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM MovimentacaoEstoque WHERE ID_MOVIMENTACAO = @ID_MOVIMENTACAO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_MOVIMENTACAO", id);

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