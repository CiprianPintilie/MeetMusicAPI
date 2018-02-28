using System.Threading.Tasks;
using MeetMusicModels.Models;

namespace MeetMusic.Interfaces
{
    public interface IManagementService
    {
        Task<MusicGenreModel[]> GetAllGenres();
        Task<MusicFamilyModel[]> GetAllFamilies();
        Task UpdateFamilies(MusicFamilyUpdateModel[] model);
    }
}
