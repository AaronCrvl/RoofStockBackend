using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxMarca
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateMarca(Marca marca)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Marca (TX_NOME) VALUES (@TX_NOME)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_NOME", marca.TX_NOME);

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

        public static async Task<Marca> GetMarca(long id)
        {
            try
            {
                Marca marca = new Marca();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Marca WHERE ID_MARCA = @ID_MARCA";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_MARCA", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            marca = new Marca
                            {
                                ID_MARCA = (long)reader["ID_MARCA"],
                                TX_NOME = (string)reader["TX_NOME"]
                            };
                        }
                    }
                }

                return marca;
            }
            catch (Exception)
            {
                return new Marca();
            }
        }

        public static async Task<bool> UpdateMarca(Marca marca)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Marca SET TX_NOME = @TX_NOME WHERE ID_MARCA = @ID_MARCA";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_NOME", marca.TX_NOME);
                    command.Parameters.AddWithValue("@ID_MARCA", marca.ID_MARCA);

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

        public static async Task<bool> DeleteMarca(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Marca WHERE ID_MARCA = @ID_MARCA";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_MARCA", id);

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