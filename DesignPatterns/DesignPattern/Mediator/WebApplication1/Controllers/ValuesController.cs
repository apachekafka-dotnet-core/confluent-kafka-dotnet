using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		IMediator _mediator;
		public ValuesController(IMediator mediator)
		{
			_mediator = mediator;
		}
		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			//var response = _mediator.Send(new Colleague.RetrievalRequest() { Message ="Hello world" }, default(CancellationToken));

			//_mediator.Publish(new Notification.NotificationRequest() { Message = "Hello world" }, default(CancellationToken));
			var response = _mediator.Send(new Colleague.QueryRequest() { Message = "Hello world" }, default(CancellationToken));

			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
