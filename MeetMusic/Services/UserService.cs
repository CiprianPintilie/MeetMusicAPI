using System;
using System.Linq;
using System.Threading.Tasks;
using MeetMusic.Context;
using MeetMusic.ExceptionMiddleware;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Utils.Hash;

namespace MeetMusic.Services
{
    public class UserService : IUserService
    {
        private readonly MeetMusicDbContext _context;

        public UserService(MeetMusicDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel[]> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToArrayAsync();
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound,
                        $"No user with the id '{id}' found");
                return user;
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<UserMusicModel[]> GetUserTastes(Guid userId)
        {
            try
            {
                var musicTastes = await _context.UserMusicModels.ToArrayAsync();
                return musicTastes.Where(t => t.UserId.Equals(userId)).ToArray();
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<Guid> CreateUser(UserModel userModelModel)
        {
            try
            {
                userModelModel.Id = Guid.NewGuid();
                userModelModel.Password = PasswordTool.HashPassword(userModelModel.Password);
                await _context.Users.AddAsync(userModelModel);
                await _context.SaveChangesAsync();
                return userModelModel.Id;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.Message.Contains("uplicate"))
                    throw new HttpStatusCodeException(StatusCodes.Status409Conflict, e.InnerException.Message);
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<Guid> UpdateUser(Guid id, UserModel userModelModel)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound,
                        $"No user with the id '{id}' found");
                _context.Users.Update(CopyUser(userModelModel, user));
                await _context.SaveChangesAsync();
                return user.Id;
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task UpdateUserTastes(Guid userId, UserMusicModel[] models)
        {
            try
            {
                var musicTastes = await _context.UserMusicModels.ToArrayAsync();
                var userMusicTastes = musicTastes.Where(t => t.UserId.Equals(userId)).ToArray();
                if (userMusicTastes.Any())
                {
                    _context.UserMusicModels.RemoveRange(userMusicTastes);
                    await _context.SaveChangesAsync();
                }
                foreach (var model in models)
                {
                    model.UserId = userId;
                    await _context.AddAsync(model);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound, $"No user with the id '{id}' found");
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<Guid> AuthenticateUser(AuthModel authModel)
        {
            try
            {
                var users = await _context.Users.ToArrayAsync();
                var user = users.Single(u => u.Username == authModel.Username);
                if (!PasswordTool.ValidatePassword(authModel.Password, user.Password))
                    throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "Invalid password");
                return user.Id;
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "User not found");
            }
            catch (InvalidOperationException)
            {
                throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "User not found");
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        private UserModel CopyUser(UserModel sourceUserModel, UserModel destUserModel)
        {
            destUserModel.FirstName = sourceUserModel.FirstName;
            destUserModel.LastName = sourceUserModel.LastName;
            destUserModel.Email = sourceUserModel.Email;
            destUserModel.Gender = sourceUserModel.Gender;
            destUserModel.AvatarUrl = sourceUserModel.AvatarUrl;
            destUserModel.Phone = sourceUserModel.Phone;
            destUserModel.BirthDate = sourceUserModel.BirthDate;
            destUserModel.Description = sourceUserModel.Description;
            destUserModel.Latitude = sourceUserModel.Longitude;
            destUserModel.Longitude = sourceUserModel.Longitude;

            return destUserModel;
        }
    }
}
