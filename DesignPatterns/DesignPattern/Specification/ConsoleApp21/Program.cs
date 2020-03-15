using Model;
using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsoleApp21
{
    
    public abstract class Specification<T> 
    {
        public abstract bool IsSatisfied(T model);
    }

    public class Level1EmployeeSpecification : Specification<Employee>
    {
        public override bool IsSatisfied(Employee model)
        {
            return model.DepartmentId == "IT";
        }
    }

    public class Level2EmployeeSpecification : Specification<Employee>
    {
        public override bool IsSatisfied(Employee model)
        {
            return model.DepartmentId == "FINANCE";
        }
    }


    

    class Program
    {
        static void Main(string[] args)
        {
            var empList = new List<Employee> {
                new Employee { EmployeeId = "A0001", Age = 33, DepartmentId = "IT", Salary = 313213 },
                new Employee { EmployeeId = "A0002", Age = 30, DepartmentId = "FINANCE", Salary = 444333 },
                new Employee { EmployeeId = "A0003", Age = 33, DepartmentId = "ITA", Salary = 3322 },
                new Employee { EmployeeId = "A0004", Age = 30, DepartmentId = "IT", Salary = 444333 }
            };

            var verfiedEmployess = GetEmployees<Employee>(empList, 
                new List<Specification<Employee>>
                {
                    new Level1EmployeeSpecification(),
                    new Level2EmployeeSpecification()
                }
            );

            verfiedEmployess.ForEach((emp) => { Console.WriteLine($"{emp.ToString()}"); });

            Console.Read();
        }

        public static List<T> GetEmployees<T>(List<T> employees, List<Specification<T>> specification )
        {
            //Level1 Employee
            return employees.Where((emp) => specification.Any(a=>a.IsSatisfied(emp))).ToList<T>();
        }
    }
}
