using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.models.DepartmentModel;
using Demo.DataAccess.models.Shared;
using Demo.DataAccess.models.Shared.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.DataAccess.models.EmployeeModel
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }


        //relashinship whit department
        public int? DepartmentId { get; set; } // fk
        public virtual Department? Department { get; set; }
    }
}
