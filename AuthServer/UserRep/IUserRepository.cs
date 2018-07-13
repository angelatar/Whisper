using System.Threading.Tasks;

namespace AuthServer.UserRep
{
    public interface IUserRepository
    {
        Task<User> FindAsync(string userName);

        Task<User> FindAsync(int id);
    }
}