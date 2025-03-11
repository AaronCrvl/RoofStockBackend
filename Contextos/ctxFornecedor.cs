using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxFornecedor
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Fornecedor (TX_NOME) VALUES (@TX_NOME)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_NOME", fornecedor.TX_NOME);

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

        public static async Task<Fornecedor> GetFornecedor(long id)
        {
            try
            {
                Fornecedor fornecedor = new Fornecedor();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Fornecedor WHERE ID_FORNECEDOR = @ID_FORNECEDOR";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FORNECEDOR", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            fornecedor = new Fornecedor
                            {
                                ID_FORNECEDOR = (long)reader["ID_FORNECEDOR"],
                                TX_NOME = reader["TX_NOME"].ToString()
                            };
                        }
                    }
                }

                return fornecedor;
            }
            catch (Exception)
            {
                return new Fornecedor();
            }
        }

        public static async Task<bool> UpdateFornecedor(Fornecedor fornecedor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Fornecedor SET TX_NOME = @TX_NOME WHERE ID_FORNECEDOR = @ID_FORNECEDOR";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_NOME", fornecedor.TX_NOME);
                    command.Parameters.AddWithValue("@ID_FORNECEDOR", fornecedor.ID_FORNECEDOR);

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

        public static async Task<bool> DeleteFornecedor(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Fornecedor WHERE ID_FORNECEDOR = @ID_FORNECEDOR";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FORNECEDOR", id);

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