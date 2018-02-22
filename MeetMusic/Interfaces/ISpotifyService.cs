using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetMusic.Interfaces
{
    public interface ISpotifyService
    {
        Task Authenticate();
    }
}
