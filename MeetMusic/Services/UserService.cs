using System;
using System.Linq;
using MeetMusic.Context;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;

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

        public Guid AuthenticateUser(User userModel)
        {
            try
            {
                var user = _context.Users.ToArray().Single(u => u.Username == userModel.Username);
                return user.Id;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
