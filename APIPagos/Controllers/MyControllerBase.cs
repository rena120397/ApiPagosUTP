﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace APIPagos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyControllerBase : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}
