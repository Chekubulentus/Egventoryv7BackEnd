using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.DataContext;
using Egventoryv7BackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace Egventoryv7BackEnd.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PoultryContext _context;
        public EmployeeRepository(PoultryContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployeeAsync(Employee request)
        {
            await _context.Employees.AddAsync(request);
            await _context.SaveChangesAsync();
            return request.Id;
        }

        public async Task<bool> DeleteEmployeeByIdAsync(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee is null)
                return false;
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees
                .Include(e => e.Role)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetDisplayEmployees()
        {
            return await _context.Employees
                .Take(10)
                .Include(e => e.Role)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByLastNameAsync(string lastName)
        {
            return await _context.Employees.Where(e => e.LastName.Contains(lastName))
                .Include(e => e.Role)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByRolePositionAsync(string rolePosition)
        {
            return await _context.Employees.Where(e => e.Role.RolePosition.Equals(rolePosition))
                .Include(e => e.Role)
                .ToListAsync();
        }

        public async Task<bool> UpdateEmployeeAsync(Employee request)
        {
            _context.Employees.Update(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
