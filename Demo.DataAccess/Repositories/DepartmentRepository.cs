using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.Data.Contexts;

namespace Demo.DataAccess.Repositories
{
    //use primery constractor
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;


        // Get All

        #region getall
        public IEnumerable<Department> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Department.ToList();
            else
                return _dbContext.Department.AsNoTracking().ToList();
        }
        #endregion
        // Get By Id
        #region GetById
        public Department? GetById(int id)
        {

            var department = _dbContext.Department.Find(keyValues: id);
            return department;


        }
        #endregion

        // Update

        public int Update(Department department)
        {
            _dbContext.Department.Update(department); // Update Locally

            return _dbContext.SaveChanges(); ;  // uopdat dataBasse

        }


        // Delete
        public int remove(Department department)
        {
            _dbContext.Department.Remove(department); // remove Locally

            return _dbContext.SaveChanges(); ;  // remove dataBasse

        }

        // Insert

        public int Add(Department department)
        {
            _dbContext.Department.Add(department); // add Locally

            return _dbContext.SaveChanges(); ;  // add dataBasse

        }
    }
}
