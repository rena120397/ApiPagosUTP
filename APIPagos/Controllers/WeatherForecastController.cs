using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPagos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly PagosOnlineContext context;

        public WeatherForecastController(PagosOnlineContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public IEnumerable<Categoria> Get()
        {
            return context.Categoria.ToList();
        }
    }
}
