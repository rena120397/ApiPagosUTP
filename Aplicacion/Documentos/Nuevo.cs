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

namespace Aplicacion.Documentos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public int id_usuario { get; set; }
            public string nombre_documento { get; set; }
            public string documento { get; set; }
            public string fechacreacion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.id_usuario).NotEmpty();
                RuleFor(x => x.nombre_documento).NotEmpty();
                RuleFor(x => x.documento).NotEmpty();
                RuleFor(x => x.fechacreacion).NotEmpty();
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
                var documento = new Documento
                {
                    id_usuario = request.id_usuario,
                    nombre_documento = request.nombre_documento,
                    documento = request.documento,
                    fechacreacion = request.fechacreacion
                };

                _context.Documento.Add(documento);

                //try
                //{
                    var valor = await _context.SaveChangesAsync();
                //catch(Exception ex)
                //{
                //    throw new Exception(ex.Message);
                //}
                
                if (valor > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.NotModified, new { mensaje = "No se insertó el documento" });
            }
        }
    }
}
