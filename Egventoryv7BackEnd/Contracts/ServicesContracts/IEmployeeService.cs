using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.EmployeeModels;

namespace Egventoryv7BackEnd.Contracts.ServicesContracts
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeDTO>> GetAllEmployees();
        public Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        public Task<IEnumerable<EmployeeDTO>> GetEmployeesByLastNameAsync(string lastName);
        public Task<IEnumerable<EmployeeDTO>> GetEmployeesByRolePositionAsync(string rolePosition);
        public Task<IEnumerable<EmployeeDTO>> GetDisplayEmployees();
        public Task<int> CreateEmployeeAsync(CreateEmployeeRequest request);
        public Task<bool> DeleteEmployeeByIdAsync(int employeeId);
        public Task<bool> UpdateEmployeeAsync(UpdateEmployeeRequest request);
    }
}
