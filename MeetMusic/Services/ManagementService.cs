using System.Threading.Tasks;
using MeetMusic.Context;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;

namespace MeetMusic.Services
{
    public class ManagementService : IManagementService
    {
        private readonly MeetMusicDbContext _context;

        public ManagementService(MeetMusicDbContext context)
        {
            _context = context;
        }
        
        public async Task UpdateFamilies(MusicFamilyUpdateModel[] model)
        {
            
        }
    }
}
