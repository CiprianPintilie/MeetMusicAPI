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
    }
}
