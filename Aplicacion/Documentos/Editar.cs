﻿using Aplicacion.ManejadorError;
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
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public int id_documento { get; set; }
            public int id_usuario { get; set; }
            public string nombre_documento { get; set; }
            public string documento { get; set; }
            public string fechacreacion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                //RuleFor(x => x.Id_categoria).NotEmpty();
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
                var documento = await _context.Documento.FindAsync(request.id_documento);
                if (documento == null)
                {
                    //throw new Exception("La Categoria no existe");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el documento" });
                }

                documento.id_usuario = request.id_usuario;
                documento.nombre_documento = request.nombre_documento ?? documento.nombre_documento;
                documento.documento = request.documento ?? documento.documento; ;
                documento.fechacreacion = request.fechacreacion ?? documento.fechacreacion;

                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }

                throw new ManejadorExcepcion(HttpStatusCode.NotModified, new { mensaje = "No se editó el documento" });
            }
        }
    }
}
