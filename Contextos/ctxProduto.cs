using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxProduto
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateProduto(Produto produto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Produto (ID_MARCA, TX_NOME, TX_DESCRICAO, VALOR) VALUES (@ID_MARCA, @TX_NOME, @TX_DESCRICAO, @VALOR)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_MARCA", produto.ID_MARCA);
                    command.Parameters.AddWithValue("@TX_NOME", produto.TX_NOME);
                    command.Parameters.AddWithValue("@TX_DESCRICAO", produto.TX_DESCRICAO);
                    command.Parameters.AddWithValue("@VALOR", produto.VALOR);

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

        public static async Task<Produto> GetProduto(long id)
        {
            try
            {
                Produto produto = new Produto();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Produto WHERE ID_PRODUTO = @ID_PRODUTO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_PRODUTO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            produto = new Produto
                            {
                                ID_PRODUTO = (long)reader["ID_PRODUTO"],
                                ID_MARCA = (long)reader["ID_MARCA"],
                                TX_NOME = (string)reader["TX_NOME"],
                                TX_DESCRICAO = (string)reader["TX_DESCRICAO"],
                                VALOR = (float)reader["VALOR"]
                            };
                        }
                    }
                }

                return produto;
            }
            catch (Exception)
            {
                return new Produto();
            }
        }

        public static async Task<bool> UpdateProduto(Produto produto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Produto SET ID_MARCA = @ID_MARCA, TX_NOME = @TX_NOME, TX_DESCRICAO = @TX_DESCRICAO, " +
                                   "VALOR = @VALOR WHERE ID_PRODUTO = @ID_PRODUTO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_MARCA", produto.ID_MARCA);
                    command.Parameters.AddWithValue("@TX_NOME", produto.TX_NOME);
                    command.Parameters.AddWithValue("@TX_DESCRICAO", produto.TX_DESCRICAO);
                    command.Parameters.AddWithValue("@VALOR", produto.VALOR);
                    command.Parameters.AddWithValue("@ID_PRODUTO", produto.ID_PRODUTO);

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

        public static async Task<bool> DeleteProduto(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Produto WHERE ID_PRODUTO = @ID_PRODUTO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_PRODUTO", id);

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