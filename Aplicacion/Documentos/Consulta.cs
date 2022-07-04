using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Dominio;
using System.Threading.Tasks;
using System.Threading;
using Persistencia;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Aplicacion.Documentos
{
    public class Consulta
    {
        public class ListaDocumento : IRequest<List<Documento>>
        {
        }

        public class Manejador : IRequestHandler<ListaDocumento, List<Documento>>
        {
            private readonly PagosOnlineContext _context;

            public Manejador(PagosOnlineContext context)
            {
                _context = context;
            }

            public async Task<List<Documento>> Handle(ListaDocumento request, CancellationToken cancellationToken)
            {
                var documentos = await _context.Documento.ToListAsync();
                return documentos;
            }
        }
    }
}
