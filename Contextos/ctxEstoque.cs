using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxEstoque
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateEstoque(Estoque estoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Estoque (ID_EMPRESA, ID_RESPONSAVEL, NM_ESTOQUE, IN_ATIVO) " +
                                   "VALUES (@ID_EMPRESA, @ID_RESPONSAVEL, @NM_ESTOQUE, @IN_ATIVO)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_EMPRESA", estoque.ID_EMPRESA);
                    command.Parameters.AddWithValue("@ID_RESPONSAVEL", estoque.ID_RESPONSAVEL);
                    command.Parameters.AddWithValue("@NM_ESTOQUE", estoque.NM_ESTOQUE);
                    command.Parameters.AddWithValue("@IN_ATIVO", estoque.IN_ATIVO);

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

        public static async Task<Estoque> GetEstoque(long id)
        {
            try
            {
                Estoque estoque = new Estoque();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Estoque WHERE ID_ESTOQUE = @ID_ESTOQUE";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            estoque = new Estoque
                            {
                                ID_ESTOQUE = (long)reader["ID_ESTOQUE"],
                                ID_EMPRESA = (long)reader["ID_EMPRESA"],
                                ID_RESPONSAVEL = (long)reader["ID_RESPONSAVEL"],
                                NM_ESTOQUE = reader["NM_ESTOQUE"].ToString(),
                                IN_ATIVO = (bool)reader["IN_ATIVO"]
                            };
                        }
                    }
                }

                return estoque;
            }
            catch (Exception)
            {
                return new Estoque();
            }
        }

        public static async Task<bool> UpdateEstoque(Estoque estoque)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Estoque SET NM_ESTOQUE = @NM_ESTOQUE, IN_ATIVO = @IN_ATIVO WHERE ID_ESTOQUE = @ID_ESTOQUE";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NM_ESTOQUE", estoque.NM_ESTOQUE);
                    command.Parameters.AddWithValue("@IN_ATIVO", estoque.IN_ATIVO);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", estoque.ID_ESTOQUE);

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

        public static async Task<bool> DeleteEstoque(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Estoque SET IN_ATIVO = 0 WHERE ID_ESTOQUE = @ID_ESTOQUE";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_ESTOQUE", id);

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