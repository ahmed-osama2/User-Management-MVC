using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.Factories;
using Demo.DataAccess.models;
using Demo.DataAccess.Repositories;

namespace Demo.BusinessLogic.Services
{
    public class DepartmentService(IDepartmentRepository _departmentRepository) : IDepartmentService
    {

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            #region Manual Mapping
            ////Manual Mapping
            //var departmentsToReturn = departments.Select(D => new DepartmentDto()
            //{
            //    DeptId = D.Id,
            //    Name = D.Name,
            //    Code = D.Code,
            //    Description = D.Description,
            //    DateOfCreation = DateOnly.FromDateTime(D.CreatedOn)


            //});

            //return departmentsToReturn; 
            #endregion

            #region Extension Methods

            return departments.Select(selector: D => D.ToDepartmentDto());
            #endregion
        }

        //// Get Department By Id
        public DepartmentDetialsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentDetialsDto();

        }
        // Create New Department
        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = departmentDto.ToEntity();
            return _departmentRepository.Add(department);


        }
        // update Department
        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(department: departmentDto.ToEntity());
        }

        // Delete Department
        public bool DeleteDepartment(int id)
        {
            var Department = _departmentRepository.GetById(id);
            if (Department is null) return false;
            else
            {
                int Result = _departmentRepository.remove(Department);
                return Result > 0 ? true : false;
            }

        }


    }
}







