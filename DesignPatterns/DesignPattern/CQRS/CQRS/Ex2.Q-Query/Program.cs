using System;
using System.Collections.Generic;

namespace Ex2.Q_Query
{
    public interface IQuery
    {
    }

    public interface IQueryResult
    {
    }

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery where TResult : IQueryResult
    {
        TResult Handle(TQuery query);
    }

    public sealed class QueryResult : IQueryResult
    {
        public bool IsSuccess { get; set; }
        public List<User> Result { get; set; }
    }

    public sealed class UserQuery : IQuery
    {
        public string UserId { get; set; }
    }

    public class User
    {
		public string UserId { get; set; }
        public string UserName { get; set; }
    }

	public sealed class UserQueryHandler : IQueryHandler<UserQuery, QueryResult>
	{
        public QueryResult Handle(UserQuery query)
        {
            return new QueryResult
            {
                IsSuccess = true,
                Result = new List<User> { new User { UserId = "11", UserName = "32331" } }
            };
		}
	}


	class Program
    {
        static void Main(string[] args)
        {
            var query = new UserQueryHandler();
            var result = query.Handle(new UserQuery { UserId = "ukjkj" });
            Console.WriteLine("Hello World!");
        }
    }
}
