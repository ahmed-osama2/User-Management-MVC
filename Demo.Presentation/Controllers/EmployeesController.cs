using System;
using Demo.BusinessLogic.DataTransferObjects.EmployeeDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.models.EmployeeModel;
using Demo.DataAccess.models.Shared.Enums;
using Demo.Presentation.ViewModels.EmployeeViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, ILogger<EmployeesController> _logger,
        IWebHostEnvironment environment ) : Controller
    {
        #region GetAllEmploy
        public IActionResult Index(string? EmployeeSearchName)
        {
            var Employees = _employeeService.GetAllEmployees(EmployeeSearchName);
            return View(Employees);
        }
        #endregion

        #region Crate Employee
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = departmentService.GetAllDepartments();
            return View();
        } 


        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var employeeDto = new CreatedEmployeeDto()
                    {

                        Name = employeeViewModel.Name,
                        Age = employeeViewModel.Age,
                        Address = employeeViewModel.Address,
                        Email = employeeViewModel.Email,
                        EmployeeType = employeeViewModel.EmployeeType,
                        Gender = employeeViewModel.Gender,
                        HiringDate = employeeViewModel.HiringDate,
                        IsActive = employeeViewModel.IsActive,
                        PhoneNumber = employeeViewModel.PhoneNumber,
                        Salary = employeeViewModel.Salary,
                        DepartmentId = employeeViewModel.DepartmentId,
                        Image = employeeViewModel.Image,

                    };
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

            return View(employeeViewModel);
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
            var employeeView = new EmployeeViewModel()
            {
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                HiringDate = employee.HiringDate,
                Gender = Enum.Parse<Gender>(value: employee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmployeeType),
                DepartmentId = employee.DepartmentId
            };
            return View(employeeView);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel employeeViewModel)
        {
            if (!id.HasValue ) return BadRequest();
            if (!ModelState.IsValid) return View(employeeViewModel);
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeViewModel.Name,
                    Salary = employeeViewModel.Salary,
                    Address = employeeViewModel.Address,
                    Age = employeeViewModel.Age, 
                    Email = employeeViewModel.Email,
                    PhoneNumber = employeeViewModel.PhoneNumber,
                    IsActive = employeeViewModel.IsActive,
                    HiringDate = employeeViewModel.HiringDate,
                    EmployeeType = employeeViewModel.EmployeeType,
                    Gender = employeeViewModel.Gender,
                    DepartmentId = employeeViewModel.DepartmentId,

                };
                var Result = _employeeService.UpdatedEmployee(employeeDto);
                if (Result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(key: string.Empty, "Employee is not Updated");
                    return View( employeeDto);
                }
            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeViewModel);

                }
                else
                {
                    _logger.LogError(ex.Message);
                    return View("ErrorView", ex);
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
                    return View("Error", model: ex);


                }
            }


        }

        #endregion
    }


}
