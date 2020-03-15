using System;

namespace Model
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public double Salary { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"{this.EmployeeId}|{this.DepartmentId}|{this.Salary}|{this.Age}";
        }
    }
}
