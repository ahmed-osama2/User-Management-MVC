using Demo.BusinessLogic.DataTransferObjects;
using Demo.BusinessLogic.DataTransferObjects.DepartmentDtos;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.models;
using Demo.Presentation.ViewModels;
using Demo.Presentation.ViewModels.DepartmentViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _environment) : Controller
    {

        #region GetAllDepartments
        //Get BassUrl /Department / Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }  
        #endregion

        #region Create Department
        [HttpGet]
        public IActionResult Create() => View();
    
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel DepartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var departmentDto = new CreatedDepartmentDto()
                    {
                        Name = DepartmentViewModel.Name,
                        Code = DepartmentViewModel.Code,
                        DateOfCreation = DepartmentViewModel.DateOfCreation,
                        Description = DepartmentViewModel.Description
                    };
                    int Result = _departmentService.AddDepartment(departmentDto);
                    string Message;
                    if (Result > 0)
                    {
                        Message = $"Department {DepartmentViewModel.Name} Is Created Successfully";
                    }
                    else
                    {
                        Message = $"Department {DepartmentViewModel.Name} Can't Be Created";

                        ModelState.AddModelError(string.Empty, "DepartMent Can't Be Created ");
                    }

                    TempData["Message"] = Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                { 
                    if (_environment.IsDevelopment())
                    {
                        // 1. Development => Log Error In Console and Return Same View With Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                        //return View(departmentDto);
                    }
                    else
                    {
                        // 2. Deployment => Log Error In File | Table in Database And Return Error View
                        _logger.LogError(ex.Message);
                        //return View(departmentDto);
                    }
                }
            }
            //else
            //{

            //}
            return View(DepartmentViewModel);
        }

        #endregion

        #region Detials
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); // 400
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound(); // 404 ≤ 160ms elapsed
            return View(department);
        }
        #endregion


        #region Edit
        [HttpGet]

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var departmentViewModel = new DepartmentViewModel()
            {

                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };


            return View(departmentViewModel);

        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UpdatedDepartment = new UpdatedDepartmentDto()
                    {
                        Id = id,
                        Code = viewModel.Code,
                        Name = viewModel.Name,
                        Description = viewModel.Description,
                        DateOfCreation = viewModel.DateOfCreation
                    };
                    int Result = _departmentService.UpdateDepartment(UpdatedDepartment);
                    if (Result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department is not Updated");
                        //return View(viewModel);
                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        // 1. Development => Log Error In Console and Return Same View With Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                        //return View(departmentDto);
                        //return View(viewModel);

                    }
                    else
                    {
                        // 2. Deployment => Log Error In File | Table in Database And Return Error View
                        _logger.LogError(ex.Message);
                        //return View(departmentDto);
                        return View("ErrorView", ex);

                    }
                }
            }
            return View(viewModel);

        }


        #endregion

        #region Delete Department
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = _departmentService.DeleteDepartment(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));

                else
                {
                    ModelState.AddModelError( string.Empty, errorMessage: "Department Is Not Deleted");
                    return RedirectToAction( nameof(Delete), new { id });
                }


            }

            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    // 1. Development => Log Error In Console and Return Same View With Error Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    //return View(departmentDto);
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    // 2. Deployment => Log Error In File | Table in Database And Return Error View
                    _logger.LogError(ex.Message);
                    //return View(departmentDto);
                    return View("ErrorView", ex);

                }
            }



        }
        #endregion

    }
}
