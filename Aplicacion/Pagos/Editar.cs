using Aplicacion.ManejadorError;
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
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public int Id_pago { get; set; }

            public int? Id_usuario { get; set; }

            public int? id_categoria { get; set; }

            public string nombre_pago { get; set; }

            public string fechaingreso { get; set; }

            public string fechapago { get; set; }

            public int? alerta { get; set; }

            public int? estado { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                //RuleFor(x => x.Id_pago).NotEmpty();
                RuleFor(x => x.Id_usuario).NotEmpty();
                RuleFor(x => x.id_categoria).NotEmpty();
                RuleFor(x => x.nombre_pago).NotEmpty();
                RuleFor(x => x.fechaingreso).NotEmpty();
                RuleFor(x => x.fechapago).NotEmpty();
                RuleFor(x => x.alerta).NotEmpty();
                RuleFor(x => x.estado).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly PagosOnlineContext _context;

            public Manejador(PagosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var pago = await _context.Pago.FindAsync(request.Id_pago);
                if (pago == null)
                {
                    //throw new Exception("La Categoria no existe");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el pago" });
                }

                pago.Id_usuario = request.Id_usuario;
                pago.id_categoria = request.id_categoria;
                pago.nombre_pago = request.nombre_pago ?? pago.nombre_pago;
                pago.fechaingreso = request.fechaingreso ?? pago.fechaingreso;
                pago.fechapago = request.fechapago ?? pago.fechapago;
                pago.alerta = request.alerta;
                pago.estado = request.estado;

                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.NotModified, new { mensaje = "No se editó el pago" });
            }
        }
    }
}
