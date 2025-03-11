using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxItemMovimentacaoEstoque
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateItemMovimentacaoEstoque(ItemMovimentacaoEstoque itemMovimentacaoEstoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ItemMovimentacaoEstoque (ID_MOVIMENTACAO, ID_PRODUTO, QN_MOVIMENTACAO, IN_PROCESSADO) " +
                                   "VALUES (@ID_MOVIMENTACAO, @ID_PRODUTO, @QN_MOVIMENTACAO, @IN_PROCESSADO)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_MOVIMENTACAO", itemMovimentacaoEstoque.ID_MOVIMENTACAO);
                    command.Parameters.AddWithValue("@ID_PRODUTO", itemMovimentacaoEstoque.ID_PRODUTO);
                    command.Parameters.AddWithValue("@QN_MOVIMENTACAO", itemMovimentacaoEstoque.QN_MOVIMENTACAO);
                    command.Parameters.AddWithValue("@IN_PROCESSADO", itemMovimentacaoEstoque.IN_PROCESSADO);

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

        public static async Task<ItemMovimentacaoEstoque> GetItemMovimentacaoEstoque(long id)
        {
            try
            {
                ItemMovimentacaoEstoque itemMovimentacaoEstoque = new ItemMovimentacaoEstoque();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ItemMovimentacaoEstoque WHERE ID_ITEM_MOVIMENTACAO = @ID_ITEM_MOVIMENTACAO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ITEM_MOVIMENTACAO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            itemMovimentacaoEstoque = new ItemMovimentacaoEstoque
                            {
                                ID_ITEM_MOVIMENTACAO = (long)reader["ID_ITEM_MOVIMENTACAO"],
                                ID_MOVIMENTACAO = (long)reader["ID_MOVIMENTACAO"],
                                ID_PRODUTO = (long)reader["ID_PRODUTO"],
                                QN_MOVIMENTACAO = (int)reader["QN_MOVIMENTACAO"],
                                IN_PROCESSADO = (bool)reader["IN_PROCESSADO"]
                            };
                        }
                    }
                }

                return itemMovimentacaoEstoque;
            }
            catch (Exception)
            {
                return new ItemMovimentacaoEstoque();
            }
        }

        public static async Task<bool> UpdateItemMovimentacaoEstoque(ItemMovimentacaoEstoque itemMovimentacaoEstoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE ItemMovimentacaoEstoque SET ID_MOVIMENTACAO = @ID_MOVIMENTACAO, ID_PRODUTO = @ID_PRODUTO, " +
                                   "QN_MOVIMENTACAO = @QN_MOVIMENTACAO, IN_PROCESSADO = @IN_PROCESSADO " +
                                   "WHERE ID_ITEM_MOVIMENTACAO = @ID_ITEM_MOVIMENTACAO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_MOVIMENTACAO", itemMovimentacaoEstoque.ID_MOVIMENTACAO);
                    command.Parameters.AddWithValue("@ID_PRODUTO", itemMovimentacaoEstoque.ID_PRODUTO);
                    command.Parameters.AddWithValue("@QN_MOVIMENTACAO", itemMovimentacaoEstoque.QN_MOVIMENTACAO);
                    command.Parameters.AddWithValue("@IN_PROCESSADO", itemMovimentacaoEstoque.IN_PROCESSADO);
                    command.Parameters.AddWithValue("@ID_ITEM_MOVIMENTACAO", itemMovimentacaoEstoque.ID_ITEM_MOVIMENTACAO);

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

        public static async Task<bool> DeleteItemMovimentacaoEstoque(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM ItemMovimentacaoEstoque WHERE ID_ITEM_MOVIMENTACAO = @ID_ITEM_MOVIMENTACAO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ITEM_MOVIMENTACAO", id);

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