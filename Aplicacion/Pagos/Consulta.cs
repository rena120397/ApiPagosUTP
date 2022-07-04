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
namespace Aplicacion.Pagos
{
    public class Consulta
    {
        public class ListaPago : IRequest<List<Pago>>
        {
        }

        public class Manejador : IRequestHandler<ListaPago, List<Pago>>
        {
            private readonly PagosOnlineContext _context;

            public Manejador(PagosOnlineContext context)
            {
                _context = context;
            }

            public async Task<List<Pago>> Handle(ListaPago request, CancellationToken cancellationToken)
            {
                var pagos = await _context.Pago.ToListAsync();
                return pagos;
            }
        }
    }
}
