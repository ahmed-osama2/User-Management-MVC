using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Repositories;

namespace Demo.BusinessLogic.Services
{
    internal class DepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository) //  01-injection
        {
            this.departmentRepository = departmentRepository;
        } 
    }
}
