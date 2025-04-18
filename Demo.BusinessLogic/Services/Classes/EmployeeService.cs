using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.models.EmployeeModel;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {
        //public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking=false)
        //{
        //    var Employees = _employeeRepository.GetAll( WithTracking);
        //    var employeesDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(Employees);
        //    //var employeesDto = Employees.Select(selector: Emp => new EmployeeDto()
        //    //{
        //    //    Id = Emp.Id,
        //    //    Name = Emp.Name,
        //    //    Age = Emp.Age,
        //    //    Email = Emp.Email,
        //    //    IsActive = Emp.IsActive,
        //    //    Salary = Emp.Salary,
        //    //    EmployeeType = Emp.EmployeeType.ToString(),
        //    //    Gender = Emp.Gender.ToString()
        //    //});
        //    return employeesDto;
        //}


        //use  I IQueryable
        public IEnumerable<EmployeeDto> GetAllEmployees(bool WithTracking = false)
        {
            var employeesDto = _employeeRepository.GetAll(E => new EmployeeDto()
            {
                Id = E.Id,
                Name = E.Name,
                Salary = E.Salary,
                Age = E.Age,
                Email = E.Email,
                IsActive = E.IsActive,
                EmpType = E.EmployeeType.ToString(),
                EmpGender = E.Gender.ToString(),





            });
            //var employeesDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(Employees);

            return employeesDto;
        }


        public EmployeeDetailsDto? GetEmployeebyId(int id)
        {
            var employee = _employeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(source: employeeDto);
            return _employeeRepository.Add(entity: employee);
        }
        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto)
        {
            return _employeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }

        public object GetAllEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
