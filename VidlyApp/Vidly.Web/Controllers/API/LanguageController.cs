using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.Web.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly IStringLocalizer<LanguageController> _localizer;

        public LanguageController(IStringLocalizer<LanguageController> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var guid = Guid.NewGuid();
            return Ok(_localizer["RandomGUID", guid.ToString()].Value);
        }
    }
}
