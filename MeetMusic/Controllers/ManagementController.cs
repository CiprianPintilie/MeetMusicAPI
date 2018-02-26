using MeetMusic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeetMusic.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ManagementController : Controller
    {
        private readonly IManagementService _managementService;

        public ManagementController(IManagementService managementService)
        {
            _managementService = managementService;
        }


    }
}