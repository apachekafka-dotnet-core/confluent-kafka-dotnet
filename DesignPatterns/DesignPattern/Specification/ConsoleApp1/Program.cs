using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
namespace ConsoleApp1
{
    public class Specification<T>
    {
        public Expression<Func<T, bool>> Expression;
        public Specification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfied(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            var level1Employee = new Specification<Employee>((a) => a.Age > 30 && a.Salary > 30000);
            var empList = new List<Employee> {
                new Employee { EmployeeId = "A0001", Age = 33, DepartmentId = "IT", Salary = 313213 },
                new Employee { EmployeeId = "A0002", Age = 30, DepartmentId = "FINANCE", Salary = 444333 },
                new Employee { EmployeeId = "A0003", Age = 33, DepartmentId = "ITA", Salary = 3322 },
                new Employee { EmployeeId = "A0004", Age = 30, DepartmentId = "IT", Salary = 444333 }
            };

            var verfiedEmployess = GetEmployees(empList)
                                    .Where(level1Employee.Expression)
                                    .ToList();

            verfiedEmployess.ForEach((emp) => { Console.WriteLine($"{emp.ToString()}"); });

            Console.Read();
        }
        //public static List<Employee> GetEmployees(List<Employee> employees)
        //{
        //    Expression<Func<Employee, bool>> levelEmployee = (a) => a.Age > 30 && a.Salary>30000;
        //    var exp = levelEmployee.Compile();

        //    var aa = from ee in employees
        //             where exp(ee)
        //             select ee;

        //    return employees.Where(a => exp(a)).ToList();
        //}

        //public static List<Employee> GetEmployees(List<Employee> employees)
        //{
        //    var exp= new Specification<Employee>((a)=>a.Age > 30 && a.Salary > 30000);
        //    return employees.AsQueryable().Where(exp.Expression).ToList();
        //}

        public static IQueryable<Employee> GetEmployees(List<Employee> employees)
        {
            //var exp = new Specification<Employee>((a) => a.Age > 30 && a.Salary > 30000);
            return employees.AsQueryable();
        }
    }
}
