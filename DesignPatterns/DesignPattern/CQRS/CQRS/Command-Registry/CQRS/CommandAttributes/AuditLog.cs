using System;
namespace DI_MessageDispatcher.CQRS.CommandAttributes
{

	[AttributeUsage(AttributeTargets.Class,Inherited =false, AllowMultiple =true)]
	public class AuditLog:Attribute
	{
		public AuditLog()
		{
		}
	}
}
