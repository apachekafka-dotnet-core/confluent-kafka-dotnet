using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI_MessageDispatcher.CQRS.Base;
using DI_MessageDispatcher.CQRS.Command;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DI_MessageDispatcher.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CQRSController : ControllerBase
	{
		IMessageDispatcher _messageDispatcher;
		public CQRSController(IMessageDispatcher messageDispatcher)
		{
			_messageDispatcher = messageDispatcher;
		}
		// GET: api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
			var result = _messageDispatcher
						.SendAndRetry<CommandResult>
							(new CreateUserCommand
								{
									UserId = "31232",
									UserName = "312323"
								}
							);
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
			
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
