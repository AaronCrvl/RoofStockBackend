using RoofStockBackend.Contexts.Records;
using RoofStockBackend.Models;
using Microsoft.Data.SqlClient;

namespace RoofStockBackend.Contexts
{
    public class UserContext
    {
        string connectinString = string.Empty;

        #region Constructor
        public UserContext()
        {
            connectinString = "";
        }
        #endregion

        #region Private Methods
        public async Task<bool> CreateUser(Models.User User)
        {
            try
            {
                await using (var transaction = new Microsoft.Data.SqlClient.SqlConnection(connectinString))
                {
                    transaction.Open();
                    transaction.ExecuteAsync("(\"INSERT INTO [Usuario](LOGIN, PASSWORD, CREATION_DATE) VALUES(@username, @password, @creation_date)",
                        new
                        {
                            username = User.Username,
                            password = User.Password,
                            creation_date = DateTime.Now
                        });
                    transaction.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> AlterUser(Models.User User)
        {
            try
            {
                await using (var transaction = new Microsoft.Data.SqlClient.SqlConnection(connectinString))
                {
                    transaction.Open();
                    transaction.ExecuteAsync("(\"UPDATE [Usuario](LOGIN, PASSWORD, CREATION_DATE) VALUES(@username, @password, @creation_date)",
                        new
                        {
                            username = User.Username,
                            password = User.Password,
                            creation_date = DateTime.Now
                        });
                    transaction.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Models.User> GetUser(long id)
        {
            try
            {
                UserRecord userDb;
                await using (var transaction = new Microsoft.Data.SqlClient.SqlConnection(connectinString))
                    userDb = await transaction.QueryFirstOrDefaultAsync<userDb>("SELECT [Id], [Login], [Senha] FROM [User] WHERE [Id]=@id", new { id = id });

                return new Models.User
                {
                    Id = userDb.Id,
                    Username = userDb.login,
                    Password = userDb.password,
                    CreationDate = userDb.creation_date
                };
            }
            catch (Exception)
            {
                return new Models.User
                {
                    Id = -1,
                    Username = string.Empty,
                    Password = string.Empty,
                    CreationDate = new DateTime()
                };
            }
        }

        public async Task<Models.User> GetUser(string username)
        {
            try
            {
                UserRecord userDb;
                await using (var transaction = new Microsoft.Data.SqlClient.SqlConnection(connectinString))
                    userDb = await transaction.QueryFirstOrDefaultAsync<userDb>("SELECT [Id], [Login], [Senha] FROM [User] WHERE [USERNAME]=@username", new { username = username });

                return new Models.User
                {
                    Id = userDb.Id,
                    Username = userDb.login,
                    Password = userDb.password,
                    CreationDate = userDb.creation_date
                };
            }
            catch (Exception)
            {
                return new Models.User
                {
                    Id = -1,
                    Username = string.Empty,
                    Password = string.Empty,
                    CreationDate = new DateTime()
                };
            }
        }
        #endregion
    }
}
