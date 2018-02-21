using System;
using System.Linq;
using MeetMusic.Context;
using MeetMusic.ExceptionMiddleware;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Http;

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
            return _context.Users.ToArray();
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Guid AuthenticateUser(AuthModel authModel)
        {
            try
            {
                var user = _context.Users.ToArray().Single(u => u.Username == authModel.Username);
                return user.Id;
            }
            catch (ArgumentNullException)
            {
                throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "User not found");
            }
        }
    }
}
