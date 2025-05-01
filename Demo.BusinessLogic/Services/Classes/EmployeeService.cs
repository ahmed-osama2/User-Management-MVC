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
    public class EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper) : IEmployeeService
    {
  


        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            //var employeesDto = _employeeRepository.GetAll(E => new EmployeeDto()
            //{
            //    Id = E.Id,
            //    Name = E.Name,
            //    Salary = E.Salary,
            //    Age = E.Age,
            //    Email = E.Email,
            //    IsActive = E.IsActive,
            //    EmpType = E.EmployeeType.ToString(),
            //    EmpGender = E.Gender.ToString(),
            //    Department = E.Department !=null ? E.Department.Name : null

            //});



            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace( EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
                employees = _unitOfWork.EmployeeRepository.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            var employeesDto = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(source: employees);
            return employeesDto;


        }


        public EmployeeDetailsDto? GetEmployeebyId(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee is null ? null : _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>( employeeDto);
             _unitOfWork.EmployeeRepository.Add( employee);
            return _unitOfWork.SaveChanges();
        }
        public int UpdatedEmployee(UpdatedEmployeeDto employeeDto) 
        {
             _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdatedEmployeeDto, Employee>(employeeDto));
            return _unitOfWork.SaveChanges();   
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                 _unitOfWork.EmployeeRepository.Update(employee) ;
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }

        public object GetAllEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
