using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxEstoqueProduto
    {
        private static string connectionString = "your_connection_string_here"; // Substitua pelo seu connection string.

        #region Métodos

        public static async Task<bool> CreateEstoqueProduto(EstoqueProduto estoqueProduto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO EstoqueProduto (ID_ESTOQUE, ID_PRODUTO, QN_ESTOQUE) VALUES (@ID_ESTOQUE, @ID_PRODUTO, @QN_ESTOQUE)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", estoqueProduto.ID_ESTOQUE);
                    command.Parameters.AddWithValue("@ID_PRODUTO", estoqueProduto.ID_PRODUTO);
                    command.Parameters.AddWithValue("@QN_ESTOQUE", estoqueProduto.QN_ESTOQUE);

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

        public static async Task<EstoqueProduto> GetEstoqueProduto(long idEstoque, long idProduto)
        {
            try
            {
                EstoqueProduto estoqueProduto = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM EstoqueProduto WHERE ID_ESTOQUE = @ID_ESTOQUE AND ID_PRODUTO = @ID_PRODUTO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", idEstoque);
                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            estoqueProduto = new EstoqueProduto
                            {
                                ID_ESTOQUE = (long)reader["ID_ESTOQUE"],
                                ID_PRODUTO = (long)reader["ID_PRODUTO"],
                                QN_ESTOQUE = (int)reader["QN_ESTOQUE"]
                            };
                        }
                    }
                }

                return estoqueProduto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<bool> UpdateEstoqueProduto(EstoqueProduto estoqueProduto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE EstoqueProduto SET QN_ESTOQUE = @QN_ESTOQUE WHERE ID_ESTOQUE = @ID_ESTOQUE AND ID_PRODUTO = @ID_PRODUTO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@QN_ESTOQUE", estoqueProduto.QN_ESTOQUE);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", estoqueProduto.ID_ESTOQUE);
                    command.Parameters.AddWithValue("@ID_PRODUTO", estoqueProduto.ID_PRODUTO);

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

        public static async Task<bool> DeleteEstoqueProduto(long idEstoque, long idProduto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM EstoqueProduto WHERE ID_ESTOQUE = @ID_ESTOQUE AND ID_PRODUTO = @ID_PRODUTO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", idEstoque);
                    command.Parameters.AddWithValue("@ID_PRODUTO", idProduto);

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