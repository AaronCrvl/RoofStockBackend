using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxEmpresa
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateEmpresa(Empresa empresa)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Empresa (TX_RAZAO_SOCIAL, TX_TOKEN, TX_CNPJ, IN_ATIVO, TX_EMAIL, DT_CRIACAO) " +
                                   "VALUES (@TX_RAZAO_SOCIAL, @TX_TOKEN, @TX_CNPJ, @IN_ATIVO, @TX_EMAIL, @DT_CRIACAO)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_RAZAO_SOCIAL", empresa.TX_RAZAO_SOCIAL);
                    command.Parameters.AddWithValue("@TX_TOKEN", empresa.TX_TOKEN);
                    command.Parameters.AddWithValue("@TX_CNPJ", empresa.TX_CNPJ);
                    command.Parameters.AddWithValue("@IN_ATIVO", empresa.IN_ATIVO);
                    command.Parameters.AddWithValue("@TX_EMAIL", empresa.TX_EMAIL);
                    command.Parameters.AddWithValue("@DT_CRIACAO", empresa.DT_CRIACAO);

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

        public static async Task<Empresa> GetEmpresa(long id)
        {
            try
            {
                Empresa empresa = new Empresa();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Empresa WHERE ID_EMPRESA = @ID_EMPRESA";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_EMPRESA", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            empresa = new Empresa
                            {
                                ID_EMPRESA = (long)reader["ID_EMPRESA"],
                                TX_RAZAO_SOCIAL = reader["TX_RAZAO_SOCIAL"].ToString(),
                                TX_TOKEN = reader["TX_TOKEN"].ToString(),
                                TX_CNPJ = reader["TX_CNPJ"].ToString(),
                                IN_ATIVO = (bool)reader["IN_ATIVO"],
                                TX_EMAIL = reader["TX_EMAIL"].ToString(),
                                DT_CRIACAO = (DateTime)reader["DT_CRIACAO"]
                            };
                        }
                    }
                }

                return empresa;
            }
            catch (Exception)
            {
                return new Empresa();
            }
        }

        public static async Task<bool> UpdateEmpresa(Empresa empresa)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Empresa SET TX_RAZAO_SOCIAL = @TX_RAZAO_SOCIAL, TX_TOKEN = @TX_TOKEN, " +
                                   "TX_CNPJ = @TX_CNPJ, IN_ATIVO = @IN_ATIVO, TX_EMAIL = @TX_EMAIL, DT_CRIACAO = @DT_CRIACAO " +
                                   "WHERE ID_EMPRESA = @ID_EMPRESA";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_RAZAO_SOCIAL", empresa.TX_RAZAO_SOCIAL);
                    command.Parameters.AddWithValue("@TX_TOKEN", empresa.TX_TOKEN);
                    command.Parameters.AddWithValue("@TX_CNPJ", empresa.TX_CNPJ);
                    command.Parameters.AddWithValue("@IN_ATIVO", empresa.IN_ATIVO);
                    command.Parameters.AddWithValue("@TX_EMAIL", empresa.TX_EMAIL);
                    command.Parameters.AddWithValue("@DT_CRIACAO", empresa.DT_CRIACAO);
                    command.Parameters.AddWithValue("@ID_EMPRESA", empresa.ID_EMPRESA);

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

        public static async Task<bool> DeleteEmpresa(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Empresa SET IN_ATIVO = 0 WHERE ID_EMPRESA = @ID_EMPRESA";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_EMPRESA", id);

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