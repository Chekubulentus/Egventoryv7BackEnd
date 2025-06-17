using Egventoryv7BackEnd.Contracts.RepositoryContracts;
using Egventoryv7BackEnd.Contracts.ServicesContracts;
using Egventoryv7BackEnd.Entities;
using Egventoryv7BackEnd.Models.EmployeeModels;
using System.Net;

namespace Egventoryv7BackEnd.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepos;
        public EmployeeService(IEmployeeRepository repos)
        {
            _employeeRepos = repos;
        }

        public async Task<int> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            int roleId;
            switch (request.RolePosition)
            {
                case "Administrator":
                    roleId = 1;
                    break;
                case "Cashier":
                    roleId = 2;
                    break;
                case "InventoryManager":
                    roleId = 3;
                    break;
                default:
                    return 0;
            }
            var newEmployee = new Employee
            {
                LastName = request.LastName,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                Age = request.Age,
                Gender = request.Gender,
                Address = request.Address,
                Email = request.Email,
                RoleId = roleId
            };
            var createEmployee = await _employeeRepos.CreateEmployeeAsync(newEmployee);
            if (createEmployee == 0)
                return 0;
            return createEmployee;
        }

        public async Task<bool> DeleteEmployeeByIdAsync(int employeeId)
        {
            if (employeeId == 0)
                return false;
            var deleteEmployee = await _employeeRepos.DeleteEmployeeByIdAsync(employeeId);
            if (!deleteEmployee)
                return false;
            return deleteEmployee;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployees()
        {
            var employees = await _employeeRepos.GetAllEmployees();
            var modEmployees = employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                LastName = e.LastName,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                Age = e.Age,
                Gender = e.Gender,
                Address = e.Address,
                Email = e.Email,
                RolePosition = e.Role.RolePosition,
                Name = e.Name
            });
            return modEmployees;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetDisplayEmployees()
        {
            var employees = await _employeeRepos.GetDisplayEmployees();
            if (!employees.Any() || employees.Count() == 0)
                return new List<EmployeeDTO>();
            var modEmployees = employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                LastName = e.LastName,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                Age = e.Age,
                Gender = e.Gender,
                Address = e.Address,
                Email = e.Email,
                RolePosition = e.Role.RolePosition,
                Name = e.Name
            });
            return modEmployees;
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            if (id <= 0)
                return null;
            var employee = await _employeeRepos.GetEmployeeByIdAsync(id);
            if (employee is null)
                return null;
            var modEmployee = new EmployeeDTO
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                Age = employee.Age,
                Gender = employee.Gender,
                Address = employee.Address,
                Email = employee.Email,
                RolePosition = employee.Role.RolePosition,
                Name = employee.Name
            };
            return modEmployee;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByLastNameAsync(string lastName)
        {
            var employees = await _employeeRepos.GetEmployeesByLastNameAsync(lastName);
            var modEmployees = employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                LastName = e.LastName,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                Age = e.Age,
                Gender = e.Gender,
                Address = e.Address,
                Email = e.Email,
                RolePosition = e.Role.RolePosition,
                Name = e.Name
            });
            return modEmployees;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByRolePositionAsync(string rolePosition)
        {
            var employees = await _employeeRepos.GetEmployeesByRolePositionAsync(rolePosition);
            var modEmployees = employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                LastName = e.LastName,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                Age = e.Age,
                Gender = e.Gender,
                Address = e.Address,
                Email = e.Email,
                RolePosition = e.Role.RolePosition,
                Name = e.Name
            });
            return modEmployees;
        }

        public async Task<bool> UpdateEmployeeAsync(UpdateEmployeeRequest request)
        {
            var employeeToUpdate = await _employeeRepos.GetEmployeeByIdAsync(request.Id);
            if (employeeToUpdate is null)
                return false;
            int roleId;
            switch(request.RolePosition)
            {
                case "Administrator":
                    roleId = 1;
                    break;
                case "Cashier":
                    roleId = 2;
                    break;
                case "InventoryManager":
                    roleId = 3;
                    break;
                default:
                    return false;
            }
            employeeToUpdate.LastName = request.LastName;
            employeeToUpdate.FirstName = request.FirstName;
            employeeToUpdate.MiddleName = request.MiddleName;
            employeeToUpdate.Age = request.Age;
            employeeToUpdate.Gender = request.Gender;
            employeeToUpdate.Address = request.Address;
            employeeToUpdate.Email = request.Email;
            employeeToUpdate.RoleId = roleId;

            var updateEmployeeDetails = await _employeeRepos.UpdateEmployeeAsync(employeeToUpdate);
            if (!updateEmployeeDetails)
                return false;
            return updateEmployeeDetails;
        }
    }
}
