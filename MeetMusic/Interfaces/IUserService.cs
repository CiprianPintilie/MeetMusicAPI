using System;
using MeetMusicModels.Models;

namespace MeetMusic.Interfaces
{
    public interface IUserService
    {
        User[] GetAllUsers();
    }
}
