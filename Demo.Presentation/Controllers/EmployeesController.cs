using System;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.models.EmployeeModel;
using Demo.DataAccess.models.Shared.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, ILogger<EmployeesController> _logger,
        IWebHostEnvironment environment) : Controller
    {
        #region GetAllEmploy
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
            return View(Employees);
        } 
        #endregion

        #region Crate Employee
        [HttpGet]
        public IActionResult Create() => View();


        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    int Result = _employeeService.CreateEmployee(employeeDto);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Can't Create Employee");
                    }
                }
                catch (Exception ex)
                {
                    {
                        if (environment.IsDevelopment())
                            ModelState.AddModelError(string.Empty, ex.Message);
                        else
                            _logger.LogError(ex.Message);

                    }
                }
            }

            return View(employeeDto);
        }
        #endregion

        #region Detiles Employee

        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id: id.Value);
            return employee is null ? NotFound() : View(employee);
        }

        #endregion

        #region Edit Employee
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeService.GetEmployeebyId(id: id.Value);
            if (employee is null) return NotFound();
            var employeeDto = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(value: employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType)
            };
            return View(employeeDto);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdatedEmployeeDto employeeDto)
        {
            if (!id.HasValue || id != employeeDto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(employeeDto);
            try
            {
                var Result = _employeeService.UpdatedEmployee(employeeDto);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(key: string.Empty, "Employee is not Updated");
                    return View(model: employeeDto);
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {

                    ModelState.AddModelError( string.Empty, ex.Message);
                    return View(model: employeeDto);

                }
                else
                {
                   _logger.LogError( ex.Message);
                    return View( "ErrorView",  ex);
                }
            }
        }





        #endregion

        #region Delete Employee
        [HttpPost]
        public IActionResult Delete(int id)
        {

            if (id == 0) return BadRequest();
            try
            {
                var Deleted = _employeeService.DeleteEmployee(id);
                if (Deleted)
                    return RedirectToAction(actionName: nameof(Index));
                else
                {
                    ModelState.AddModelError(key: string.Empty, "Employed is not Deleted");
                    return RedirectToAction(nameof(Delete), new { id = id });

                }

            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    return RedirectToAction(actionName: nameof(Index));
                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View( "Error", model: ex);


                }
            }


        }

        #endregion
    }


}
