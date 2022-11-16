namespace RedarBorModels.Facades.IFacade
{
    using RedarBorModels.Entities;
    using RedarBorModels.Requests;
    using RedarBorModels.Responses;

    public interface IEmployeeFacade
    {
        /// <summary>
        /// Adds a new Item into RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        Task<ResponseRedarBorRequest> AddNewItemEmployee(RedarBorEmployeeRequest employee);

        /// <summary>
        /// Gets all items from RedarBorEmployees table
        /// </summary>
        /// <returns>all information related to all items stored into the table RedarBorEmployees</returns>
        Task<ResponseRedarBorRequest> GetAllItemsEmployees();

        /// <summary>
        /// Updates an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        Task<ResponseRedarBorRequest> UpdateItemEmployee(RedarBorEmployUpdateRequest employee);

        /// <summary>
        /// Deletes an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employeeId">Employee id used to find the register to be deleted</param>
        /// <returns>operation response</returns>
        Task<ResponseRedarBorRequest> DeleteItemEmployee(long employeeId);

        /// <summary>
        /// Gets an item that is looking for Id
        /// </summary>
        /// <param name="idEmploy">Employee id </param>
        /// <returns>object with the employee information</returns>
        Task<ResponseRedarBorRequest> GetItemEmployeeById(long idEmploy);

        Task<ResponseRedarBorRequest> GetToken(RequestLogin userCredentials);
    }
}
