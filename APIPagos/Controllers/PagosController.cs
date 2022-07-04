using Aplicacion.Pagos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace APIPagos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IMediator mediator;

        public PagosController(IMediator _mediator)
        {
            this.mediator = _mediator;
        }

        //http://localhost:47113/api/Categorias
        [HttpGet]
        public async Task<ActionResult<List<Pago>>> Get()
        {
            return await mediator.Send(new Consulta.ListaPago());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> DetallePago(int id)
        {
            return await mediator.Send(new ConsultaId.PagoUnico { Id_pago = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id, Editar.Ejecuta data)
        {
            data.Id_pago = id;
            return await mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id)
        {
            return await mediator.Send(new Eliminar.Ejecuta { Id_pago = id });
        }
    }
}
