using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
namespace ConsoleApp1
{
    public abstract class BaseSpecification<T>
    {
        public static readonly AllEmployeeSpecificationn<T> ALL = new AllEmployeeSpecificationn<T>();   
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfied(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public BaseSpecification<T> And(BaseSpecification<T> specification)
        {
            return new AndSpecificationn<T>(this, specification);
        }
        public BaseSpecification<T> Or(BaseSpecification<T> specification)
        {
            return new OrSpecificationn<T>(this, specification);
        }
    }

    public sealed class AllEmployeeSpecificationn<T> : BaseSpecification<T>
    {
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x=>true;
        }
    }
    public sealed class AndSpecificationn<T> : BaseSpecification<T>
    {
        BaseSpecification<T> _left;
        BaseSpecification<T> _right;
        public AndSpecificationn(BaseSpecification<T> left, BaseSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var andExpression = Expression.AndAlso(_left.ToExpression().Body, _right.ToExpression().Body);
            return Expression.Lambda<Func<T, bool>>(andExpression, _left.ToExpression().Parameters.Single());
        }
    }

    public sealed class OrSpecificationn<T> : BaseSpecification<T>
    {
        BaseSpecification<T> _left;
        BaseSpecification<T> _right;
        public OrSpecificationn(BaseSpecification<T> left, BaseSpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var left = _left.ToExpression();
            var right = _right.ToExpression();

            var andExpression = Expression.OrElse(left.Body, right.Body);
            return Expression.Lambda<Func<T, bool>>(andExpression, left.Parameters.Single());
        }
    }

    public class Level1Employee : BaseSpecification<Employee>
    {
        public override Expression<Func<Employee, bool>> ToExpression()
        {
            return emp => emp.Salary > 30000 && emp.DepartmentId == "IT";
        }
    }
    public class Level2Employee : BaseSpecification<Employee>
    {
        public override Expression<Func<Employee, bool>> ToExpression()
        {
            return emp => emp.Salary > 30000 && emp.DepartmentId == "FINANCE";
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            //var level1Employee = new Specification<Employee>((a) => a.Age > 30 && a.Salary > 30000);
            var empList = new List<Employee> {
                new Employee { EmployeeId = "A0001", Age = 33, DepartmentId = "IT", Salary = 313213 },
                new Employee { EmployeeId = "A0002", Age = 30, DepartmentId = "FINANCE", Salary = 444333 },
                new Employee { EmployeeId = "A0003", Age = 33, DepartmentId = "ITA", Salary = 3322 },
                new Employee { EmployeeId = "A0004", Age = 30, DepartmentId = "IT", Salary = 444333 }
            };

            var verfiedEmployess = GetEmployees(empList);

            verfiedEmployess.ForEach((emp) => { Console.WriteLine($"{emp.ToString()}"); });

            Console.Read();
        }
        
        public static List<Employee> GetEmployees(List<Employee> employees)
        {
            var spec = new AllEmployeeSpecificationn<Employee>();
            //spec.Or(new Level1Employee());
            //spec.Or(new Level2Employee());

            return employees.Where(a => spec.IsSatisfied(a)).ToList();
        }


    }
}
