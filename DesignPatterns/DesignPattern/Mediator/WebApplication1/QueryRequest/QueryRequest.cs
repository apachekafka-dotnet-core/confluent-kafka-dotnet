using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.Colleague
{
	public class QueryResponse 
	{
		public string Message { get; set; }
	}
	public class QueryRequest : IRequest<QueryResponse>
	{
		public string Message { get; set; }
	}

	public class QueryRequestHandler : IRequestHandler<QueryRequest, QueryResponse>
	{
		public Task<QueryResponse> Handle(QueryRequest request, CancellationToken cancellationToken)
		{
			return Task.FromResult(new QueryResponse() { Message = request.Message });
		}
	}
}
