using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxCargo
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateCargo(Cargo cargo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Cargo (TX_NOME) VALUES (@TX_NOME)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_NOME", cargo.TX_NOME);

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

        public static async Task<Cargo> GetCargo(long id)
        {
            try
            {
                Cargo cargo = new Cargo();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Cargo WHERE ID_CARGO = @ID_CARGO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_CARGO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cargo = new Cargo
                            {
                                ID_CARGO = (long)reader["ID_CARGO"],
                                TX_NOME = reader["TX_NOME"].ToString()
                            };
                        }
                    }
                }

                return cargo;
            }
            catch (Exception)
            {
                return new Cargo();
            }
        }

        public static async Task<bool> UpdateCargo(Cargo cargo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Cargo SET TX_NOME = @TX_NOME WHERE ID_CARGO = @ID_CARGO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_NOME", cargo.TX_NOME);
                    command.Parameters.AddWithValue("@ID_CARGO", cargo.ID_CARGO);

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

        public static async Task<bool> DeleteCargo(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Cargo WHERE ID_CARGO = @ID_CARGO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_CARGO", id);

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