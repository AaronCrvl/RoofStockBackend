using Microsoft.Data.SqlClient;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Threading.Tasks;

namespace RoofStockBackend.Contextos
{
    public static class ctxFuncionario
    {
        private static string connectionString = "";

        #region Métodos

        public static async Task<bool> CreateFuncionario(Funcionario funcionario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Funcionario (ID_EMPRESA, ID_CARGO, TX_NOME, TX_CPF, TX_EMAIL, TX_TELEFONE, DT_ENTRADA) " +
                                   "VALUES (@ID_EMPRESA, @ID_CARGO, @TX_NOME, @TX_CPF, @TX_EMAIL, @TX_TELEFONE, @DT_ENTRADA)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_EMPRESA", funcionario.ID_EMPRESA);
                    command.Parameters.AddWithValue("@ID_CARGO", funcionario.ID_CARGO);
                    command.Parameters.AddWithValue("@TX_NOME", funcionario.TX_NOME);
                    command.Parameters.AddWithValue("@TX_CPF", funcionario.TX_CPF);
                    command.Parameters.AddWithValue("@TX_EMAIL", funcionario.TX_EMAIL);
                    command.Parameters.AddWithValue("@TX_TELEFONE", funcionario.TX_TELEFONE);
                    command.Parameters.AddWithValue("@DT_ENTRADA", funcionario.DT_ENTRADA);

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

        public static async Task<Funcionario> GetFuncionario(long id)
        {
            try
            {
                Funcionario funcionario = new Funcionario();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Funcionario WHERE ID_FUNCIONARIO = @ID_FUNCIONARIO";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            funcionario = new Funcionario
                            {
                                ID_FUNCIONARIO = (long)reader["ID_FUNCIONARIO"],
                                ID_EMPRESA = (long)reader["ID_EMPRESA"],
                                ID_CARGO = (long)reader["ID_CARGO"],
                                TX_NOME = reader["TX_NOME"].ToString(),
                                TX_CPF = reader["TX_CPF"].ToString(),
                                TX_EMAIL = reader["TX_EMAIL"].ToString(),
                                TX_TELEFONE = reader["TX_TELEFONE"].ToString(),
                                DT_ENTRADA = (DateTime)reader["DT_ENTRADA"]
                            };
                        }
                    }
                }

                return funcionario;
            }
            catch (Exception)
            {
                return new Funcionario();
            }
        }

        public static async Task<bool> UpdateFuncionario(Funcionario funcionario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Funcionario SET TX_NOME = @TX_NOME, TX_CPF = @TX_CPF, TX_EMAIL = @TX_EMAIL, " +
                                   "TX_TELEFONE = @TX_TELEFONE, DT_ENTRADA = @DT_ENTRADA WHERE ID_FUNCIONARIO = @ID_FUNCIONARIO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TX_NOME", funcionario.TX_NOME);
                    command.Parameters.AddWithValue("@TX_CPF", funcionario.TX_CPF);
                    command.Parameters.AddWithValue("@TX_EMAIL", funcionario.TX_EMAIL);
                    command.Parameters.AddWithValue("@TX_TELEFONE", funcionario.TX_TELEFONE);
                    command.Parameters.AddWithValue("@DT_ENTRADA", funcionario.DT_ENTRADA);
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", funcionario.ID_FUNCIONARIO);

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

        public static async Task<bool> DeleteFuncionario(long id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Funcionario SET IN_ATIVO = 0 WHERE ID_FUNCIONARIO = @ID_FUNCIONARIO";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID_FUNCIONARIO", id);

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