using System;
using MeetMusicModels.Models;

namespace MeetMusic.Interfaces
{
    public interface IUserService
    {
        User[] GetAllUsers();
        Guid AuthenticateUser(AuthModel authModel);
        Guid CreateUser(User userModel);
    }
}
