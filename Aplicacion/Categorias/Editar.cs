using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Categorias
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public int Id_categoria { get; set; }

            public string nombre_categoria { get; set; }
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
                var categoria = await _context.Categoria.FindAsync(request.Id_categoria);
                if (categoria == null)
                {
                    throw new Exception("La Categoria no existe");
                }

                categoria.nombre_categoria = request.nombre_categoria ?? categoria.nombre_categoria;
                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo editar la categoria");
            }
        }
    }
}
