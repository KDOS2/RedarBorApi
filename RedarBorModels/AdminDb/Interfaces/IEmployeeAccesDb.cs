namespace RedarBorModels.AdminDb.Interfaces
{
    using RedarBorModels.Entities;
    using RedarBorModels.Responses;

    public interface IEmployeeAccesDb
    {
        /// <summary>
        /// Adds a new Item into RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        Task<ResponseAddItemEmpployee> AddEmployee(RedarBorEmployeesEntity employee);

        /// <summary>
        /// Gets one or over items from RedarBorEmployees table
        /// </summary>
        /// <param name="companyId">company id</param>
        /// <param name="employeeId">employee id</param>
        /// <returns>all information related to all items stored into the table RedarBorEmployees</returns>
        Task<List<RedarBorEmployeesEntity>> GetEmployees(long? employeeId = null);

        /// <summary>
        /// Updates an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        Task<bool> UpdateEmployee(RedarBorEmployeesEntity employee);

        /// <summary>
        /// Deletes an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employeeId">Employee id used to find the register to be deleted</param>
        /// <returns>operation response</returns>
        Task<bool> DeleteEmployee(long employeeId);

        /// <summary>
        /// Deletes an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employeeId">Employee id used to find the register to be deleted</param>
        /// <returns>operation response</returns>
        Task<bool> DeleteEmployeeEntity(long employeeId);
    }
}
