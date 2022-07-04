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
    public class ConsultaId
    {
        public class DocumentoUnico : IRequest<Documento>
        {
            public int id_documento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<DocumentoUnico>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.id_documento).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<DocumentoUnico, Documento>
        {
            private readonly PagosOnlineContext _context;

            public Manejador(PagosOnlineContext context)
            {
                this._context = context;
            }

            public async Task<Documento> Handle(DocumentoUnico request, CancellationToken cancellationToken)
            {
                var documento = await _context.Documento.FindAsync(request.id_documento);

                if (documento == null)
                {
                    //throw new Exception("No se pudo eliminar la categoria");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el documento" });
                }

                return documento;
            }
        }
    }
}
