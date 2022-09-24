using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;


namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        //readonly prevents the dependency being changed
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            //expressão x => x.Name ordena a lista por nome
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
