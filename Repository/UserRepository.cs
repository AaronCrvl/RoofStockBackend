using

using RoofStockBackend.Contexts;

namespace RoofStockBackend.Repository
{
    public class UserRepository
    {
        public UserRepository()
        {
            this.id = -1;
            this.username = string.Empty;
            this.password = string.Empty;
            this.creationDate = new DateTime();
        }

        #region Private Propeties
        private long id;
        string username;
        string password;
        DateTime creationDate;
        #endregion

        #region Properties
        public long Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return this.creationDate;
            }
            set
            {
                this.creationDate = value;
            }
        }
        #endregion

        #region Public Methods
        public async Task<Models.User> GetUser(long Id)
        {
            try
            {
                Contexts.UserContext context = new Contexts.UserContext();
                Models.User user = context.GetUser(Id).Result;
                return user;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Models.User> CreateUser(Models.User user)
        {
            try
            {
                UserContext context = new UserContext();
                var retorno = context.CreateUser(user);

                var createdUser = context.GetUser(user.Username).Result;
                return createdUser;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        #endregion
    }
}