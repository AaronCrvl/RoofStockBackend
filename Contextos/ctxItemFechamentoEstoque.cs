using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxItemFechamentoEstoque
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateItemFechamentoEstoque(ItemFechamentoEstoque itemFechamentoEstoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ItemFechamentoEstoque (ID_FECHAMENTO, ID_ESTOQUE, DT_FECHAMENTO, IN_ERRO) " +
                                   "VALUES (@ID_FECHAMENTO, @ID_ESTOQUE, @DT_FECHAMENTO, @IN_ERRO)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FECHAMENTO", itemFechamentoEstoque.ID_FECHAMENTO);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", itemFechamentoEstoque.ID_ESTOQUE);
                    command.Parameters.AddWithValue("@DT_FECHAMENTO", itemFechamentoEstoque.DT_FECHAMENTO);
                    command.Parameters.AddWithValue("@IN_ERRO", itemFechamentoEstoque.IN_ERRO);

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

        public static async Task<ItemFechamentoEstoque> GetItemFechamentoEstoque(long id)
        {
            try
            {
                ItemFechamentoEstoque itemFechamentoEstoque = new ItemFechamentoEstoque();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ItemFechamentoEstoque WHERE ID_FECHAMENTO = @ID_FECHAMENTO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FECHAMENTO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            itemFechamentoEstoque = new ItemFechamentoEstoque
                            {
                                ID_FECHAMENTO = (long)reader["ID_FECHAMENTO"],
                                ID_ESTOQUE = (long)reader["ID_ESTOQUE"],
                                DT_FECHAMENTO = (DateTime)reader["DT_FECHAMENTO"],
                                IN_ERRO = (bool)reader["IN_ERRO"]
                            };
                        }
                    }
                }

                return itemFechamentoEstoque;
            }
            catch (Exception)
            {
                return new ItemFechamentoEstoque();
            }
        }

        public static async Task<bool> UpdateItemFechamentoEstoque(ItemFechamentoEstoque itemFechamentoEstoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ItemFechamentoEstoque SET ID_ESTOQUE = @ID_ESTOQUE, DT_FECHAMENTO = @DT_FECHAMENTO, " +
                                   "IN_ERRO = @IN_ERRO WHERE ID_FECHAMENTO = @ID_FECHAMENTO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", itemFechamentoEstoque.ID_ESTOQUE);
                    command.Parameters.AddWithValue("@DT_FECHAMENTO", itemFechamentoEstoque.DT_FECHAMENTO);
                    command.Parameters.AddWithValue("@IN_ERRO", itemFechamentoEstoque.IN_ERRO);
                    command.Parameters.AddWithValue("@ID_FECHAMENTO", itemFechamentoEstoque.ID_FECHAMENTO);

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

        public static async Task<bool> DeleteItemFechamentoEstoque(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ItemFechamentoEstoque WHERE ID_FECHAMENTO = @ID_FECHAMENTO";

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