using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TKompTask.API.Attributes;
using TKompTask.DataAccess.Models;
using TKompTask.DataAccess.Queries;

namespace TKompTask.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColumnController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ColumnController> _logger;
        private readonly IMediator _mediator;

        public ColumnController(ILogger<ColumnController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        
        [HttpGet("test"), ApiAuth]
        public ActionResult<bool> Get()
        {
            return true; // Nie ma sensu implementować tu nic więcej, bo filtr autoryzacyjny sam w sobie - jak nazwa wskazuje - robi nam auth'a. 
            // Możemy więc założyć, że jeżeli doszliśmy już tutaj, to można spokojnie zwrócić true.
        }

        [HttpGet("ints"), ApiAuth]
        public async Task<ActionResult<List<ColumnInfo>>> GetColumnInfoForInts()
        {
            return await _mediator.Send(new GetColumnInfoForIntsQuery());
        }
    }
}
