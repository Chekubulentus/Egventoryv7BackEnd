using Egventoryv7BackEnd.Entities;

namespace Egventoryv7BackEnd.Contracts.RepositoryContracts
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<Employee> GetEmployeeByIdAsync(int id);
        public Task<IEnumerable<Employee>> GetEmployeesByLastNameAsync(string lastName);
        public Task<IEnumerable<Employee>> GetEmployeesByRolePositionAsync(string rolePosition);
        public Task<IEnumerable<Employee>> GetDisplayEmployees();
        public Task<int> CreateEmployeeAsync(Employee request);
        public Task<bool> DeleteEmployeeByIdAsync(int employeeId);
        public Task<bool> UpdateEmployeeAsync(Employee request);
    }
}
