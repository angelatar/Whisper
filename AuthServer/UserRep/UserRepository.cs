using AuthServer.DataAccess;
using System.Threading.Tasks;

namespace AuthServer.UserRep
{
    public class UserRepository : IUserRepository
    {
        private DataAccessor dataAccessor;

        public UserRepository()
        {
            this.dataAccessor = new DataAccessor();
        }

        public Task<User> FindAsync(string userName)
        {
            var task = new Task<User>(() =>
            {
                var user = this.dataAccessor.GetUserByUsername(userName);
                // stex mi ban en chi
                if (user.Count == 0)
                    return null; 
                return new User()
                {
                    Id = (int)user["Id"],
                    UserName = user["Username"].ToString(),
                    Password = user["Password"].ToString()
                };
            });

            task.Start();

            return task;
        }

        public Task<User> FindAsync(int id)
        {
            var task = new Task<User>(() =>
            {
                var user = this.dataAccessor.GetUserByID(id);
                if (user.Count == 0)
                    return null;

                return new User()
                {
                    Id = (int)user["Id"],
                    UserName = user["Username"].ToString(),
                    Password = user["Password"].ToString()
                };
            });

            task.Start();

            return task;
        }
    }
}
