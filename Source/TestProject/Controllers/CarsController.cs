using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Interfaces;
using TestProject.Models;
using TestProject.Mvc.Controller;
using TestProject.ViewModels;

namespace TestProject.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CarsController : BaseController<CarModel, IRepo<CarModel>, CarViewModel> {
        public CarsController(
            IRepo<CarModel> repo,
            IMapper mapper, 
            ILoggerFactory loggerFactory) : base(repo, mapper, loggerFactory) {
        }
    }
}
