using System;
using System.Threading.Tasks;
using MeetMusicModels.Models;

namespace MeetMusic.Interfaces
{
    public interface IUserService
    {
        Task<UserModel[]> GetAllUsers();
        Task<UserModel> GetUser(Guid id);
        Task<UserMusicModel[]> GetUserTastes(Guid id);
        Task<double> GetUsersDistance(Guid firstId, Guid secondId);
        Task<Guid> CreateUser(UserModel model);
        Task<Guid> UpdateUser(Guid id, UserModel model);
        Task UpdateUserPosition(Guid id, string latitude, string longitude);
        Task SynchronizeUserTastes(Guid id, SynchronizedMusicGenresModel[] model);
        Task UpdateUserTastes(Guid userId, UserMusicModel[] models);
        Task<MatchModel[]> MatchUser(Guid id, MatchParametersModel model);
        Task DeleteUser(Guid id);
        Task<Guid> AuthenticateUser(AuthModel authModel);
    }
}
