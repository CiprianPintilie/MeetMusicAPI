using System;
using MeetMusicModels.Models;

namespace MeetMusic.Interfaces
{
    public interface IUserService
    {
        User[] GetAllUsers();
        User GetUser(Guid id);
        Guid CreateUser(User userModel);
        void DeleteUser(Guid id);
        Guid AuthenticateUser(AuthModel authModel);
    }
}
