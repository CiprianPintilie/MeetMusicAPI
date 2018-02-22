using System;
using System.Threading.Tasks;
using MeetMusicModels.Models;

namespace MeetMusic.Interfaces
{
    public interface IUserService
    {
        Task<User[]> GetAllUsers();
        Task<User> GetUser(Guid id);
        Task<Guid> CreateUser(User userModel);
        Task DeleteUser(Guid id);
        Task<Guid> AuthenticateUser(AuthModel authModel);
    }
}
