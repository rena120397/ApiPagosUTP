using Dominio;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Categorias
{
    public class ConsultaId
    {
        public class CategoriaUnica : IRequest<Categoria>
        {
            public int Id_categoria { get; set; }
        }

        public class Manejador : IRequestHandler<CategoriaUnica, Categoria>
        {
            private readonly PagosOnlineContext _context;

            public Manejador(PagosOnlineContext context)
            {
                this._context = context;
            }

            public async Task<Categoria> Handle(CategoriaUnica request, CancellationToken cancellationToken)
            {
                var categoria = await _context.Categoria.FindAsync(request.Id_categoria);
                return categoria;
            }
        }
    }
}
