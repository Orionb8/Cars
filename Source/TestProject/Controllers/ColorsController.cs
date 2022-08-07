using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Interfaces;
using TestProject.Models;
using TestProject.Mvc.Controller;
using TestProject.ViewModels;

namespace TestProject.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ColorsController : BaseController<ColorModel, IRepo<ColorModel>, ColorViewModel> {
        public ColorsController(
            IRepo<ColorModel> repo,
            IMapper mapper,
            ILoggerFactory loggerFactory) : base(repo, mapper, loggerFactory) {
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async override Task<ActionResult<ColorViewModel>> Post([FromBody] ColorViewModel vm, CancellationToken cancellationToken = default) {
            return Ok();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async override Task<ActionResult<ColorViewModel>> Put([FromBody] ColorViewModel vm, CancellationToken cancellationToken = default) {
            return Ok();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public async override Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default) {
            return Ok();
        }
    }
}
