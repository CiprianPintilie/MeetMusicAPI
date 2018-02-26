using System.Threading.Tasks;
using MeetMusicModels.Models;

namespace MeetMusic.Interfaces
{
    public interface IManagementService
    {
        Task UpdateFamilies(MusicFamilyUpdateModel[] model);
    }
}
