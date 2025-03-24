using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects;
using Demo.DataAccess.Repositories;

namespace Demo.BusinessLogic.Services
{
    internal class DepartmentService(IDepartmentRepository _departmentRepository)
    {
        private readonly IDepartmentRepository departmentRepository = _departmentRepository;

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();

            var departmentsToReturn = departments.Select(D => new DepartmentDto()
            {
                DeptId = D.Id,
                Name = D.Name,
                Code = D.Code,
                Description = D.Description,
                DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)


            });
            
            return departmentsToReturn;


        }
    }


        



}
