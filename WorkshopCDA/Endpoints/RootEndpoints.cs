using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopFinal.Endpoints
{
    public class RootEndpoints : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Get("/");
            AllowAnonymous();
        }

        public async override Task HandleAsync(CancellationToken ct)
        {
            await SendAsync(new { Message = "Hello, FastEndpoints!" });
        }
    }
}
