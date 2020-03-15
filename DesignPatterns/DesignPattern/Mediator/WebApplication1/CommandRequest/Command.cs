using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.Colleague
{
	public class RetrievalRequest:IRequest
	{
		public string Message { get; set; }
	}

	public class Colleague :IRequestHandler<RetrievalRequest>
	{
		
		Task<Unit> IRequestHandler<RetrievalRequest, Unit>.Handle(RetrievalRequest request, CancellationToken cancellationToken)
		{
			Console.WriteLine($"Colleague1 service received a notification:{request.Message}");
			return Task.FromResult(Unit.Value);
		}
	}
}
