using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Pagos
{
    public class ConsultaId
    {
        public class PagoUnico : IRequest<Pago>
        {
            public int Id_pago { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<PagoUnico>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Id_pago).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<PagoUnico, Pago>
        {
            private readonly PagosOnlineContext _context;

            public Manejador(PagosOnlineContext context)
            {
                this._context = context;
            }

            public async Task<Pago> Handle(PagoUnico request, CancellationToken cancellationToken)
            {
                var pago = await _context.Pago.FindAsync(request.Id_pago);

                if (pago == null)
                {
                    //throw new Exception("No se pudo eliminar la categoria");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el pago" });
                }

                return pago;
            }
        }
    }
}
