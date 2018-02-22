using System;
using System.Linq;
using MeetMusic.Context;
using MeetMusic.ExceptionMiddleware;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MeetMusic.Services
{
    public class UserService : IUserService
    {
        private readonly MeetMusicDbContext _context;

        public UserService(MeetMusicDbContext context)
        {
            _context = context;
        }

        public User[] GetAllUsers()
        {
            try
            {
                return _context.Users.ToArray();
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public User GetUser(Guid id)
        {
            try
            {
                var user = _context.Users.Find(id);
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

        public Guid CreateUser(User userModel)
        {
            try
            {
                userModel.Id = Guid.NewGuid();
                _context.Users.Add(userModel);
                _context.SaveChanges();
                return userModel.Id;
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

        public void DeleteUser(Guid id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound, $"No user with the id '{id}' found");
                _context.Remove(user);
                _context.SaveChanges();
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

        public Guid AuthenticateUser(AuthModel authModel)
        {
            try
            {
                var user = _context.Users.ToArray().Single(u => u.Username == authModel.Username);
                if (authModel.Password != user.Password)
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
    }
}
