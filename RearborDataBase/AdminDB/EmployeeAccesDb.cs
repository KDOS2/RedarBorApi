namespace RedarborDataBase.AdminDB
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using RedarBorDB;
    using RedarBorModels.AdminDb.Interfaces;
    using RedarBorModels.Entities;
    using RedarBorModels.Responses;
    using System;
    using System.Linq;
    using System.Transactions;

    public class EmployeeAccesDb : IEmployeeAccesDb
    {
        private RedarBorContext _context;
        private readonly string stringContection;

        public EmployeeAccesDb(RedarBorContext context, IConfiguration configuration)
        {
            this._context = context;
            this.stringContection = configuration.GetSection("ConnectionStrings:RedarborConnectionDb").Value;
        }

        #region Public methods
        /// <summary>
        /// Adds a new Item into RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        public async Task<ResponseAddItemEmpployee> AddEmployee(RedarBorEmployeesEntity employee)
        {
            string sql = "INSERT INTO [Redarbor].[Employees] (CompanyId, Email, Fax, Name, Username, " +
                         "Password, PortalId, RoleId, StatusId, Telephone) " +
                         "Values (@CompanyId, @Email, @Fax, @Name, @Username, " +
                         "@Password, @PortalId, @RoleId, @StatusId, @Telephone);";

            using (var connection = new SqlConnection(this.stringContection))
            {
                var affectedRows = await Task.Run(()=> (connection.Execute(sql, new 
                { 
                    CompanyId = employee.CompanyId, 
                    Email = employee.Email, 
                    Fax = employee.Fax, 
                    Name = employee.Name, 
                    Username = employee.Username,
                    Password = employee.Password, 
                    PortalId = employee.PortalId, 
                    RoleId = employee.RoleId,
                    StatusId = employee.StatusId, 
                    Telephone = employee.Telephone
                })));

                string sqlResponse = "Select [EmployeeId], [CreatedOn] " +
                                      "FROM [Redarbor].[Employees] WHERE Name = '" + employee.Name + "' and Username = '" + employee.Username + "' " +
                                      "and Password = '" + employee.Password + "'";

                using (var txn = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }, TransactionScopeAsyncFlowOption.Enabled))
                {
                    ResponseAddItemEmpployee? responseEmployee = await Task.Run(() => connection.Query<ResponseAddItemEmpployee>(sqlResponse).FirstOrDefault());
                    return responseEmployee;
                }
            }            
        }

        /// <summary>
        /// Updates an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        public async Task<bool> UpdateEmployee(RedarBorEmployeesEntity employee)
        {
            RedarBorEmployeesEntity? responseAcces = await this.GetEmployeeItem(employee);

            string sql = "UPDATE [Redarbor].[Employees] SET CompanyId = @CompanyId, Email = @Email, Fax = @Fax, Name = @Name, Username = @Username, " +
                         "Lastlogin = @Lastlogin, Password = @Password, PortalId = @PortalId, RoleId = @RoleId, StatusId = @StatusId, Telephone = @Telephone, UpdatedOn = @UpdatedOn " +
            "WHERE EmployeeId = @EmployeeId;";

            using (var connection = new SqlConnection(this.stringContection))
            {
                int affectedRows = await Task.Run(() => (connection.Execute(sql, new
                {
                    CompanyId = employee.CompanyId,
                    Email = employee.Email,
                    Fax = employee.Fax,
                    Name = employee.Name,
                    Lastlogin = employee.Lastlogin,
                    Username = employee.Username,
                    Password = employee.Password,
                    PortalId = employee.PortalId,
                    RoleId = employee.RoleId,
                    StatusId = employee.StatusId,
                    Telephone = employee.Telephone,
                    UpdatedOn = employee.UpdatedOn,
                    EmployeeId = employee.id
                })));

                return affectedRows > 0 ? true : false;
            }
        }

        /// <summary>
        /// Deletes an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employeeId">Employee id used to find the register to be deleted</param>
        /// <returns>operation response</returns>
        public async Task<bool> DeleteEmployee(long employeeId)
        {
            string sql = "DELETE FROM [Redarbor].[Employees] WHERE EmployeeId = @EmployeeId;";

            using (var connection = new SqlConnection(this.stringContection))
            {
                int affectedRows = await Task.Run(() => (connection.Execute(sql, new
                {
                    employeeId = employeeId
                })));

                return affectedRows > 0 ? true : false;
            }
        }

        /// <summary>
        /// Deletes an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employeeId">Employee id used to find the register to be deleted</param>
        /// <returns>operation response</returns>
        public async Task<bool> DeleteEmployeeEntity(long employeeId)
        {
            RedarBorEmployeesEntity delete = new RedarBorEmployeesEntity() { id = employeeId };
            await Task.Run(()=>this._context.Remove(delete));
            this._context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Gets one or over items from RedarBorEmployees table
        /// </summary>
        /// <param name="companyId">company id</param>
        /// <param name="employeeId">employee id</param>
        /// <returns>all information related to all items stored into the table RedarBorEmployees</returns>
        public async Task<List<RedarBorEmployeesEntity>> GetEmployees(long? employeeId = null)
        {
            using (var txn = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }, TransactionScopeAsyncFlowOption.Enabled))
            {
                List<RedarBorEmployeesEntity> responseAcces =  await (from em in this._context.Employees.AsNoTracking()
                                                                      where !employeeId.HasValue || em.id.Equals(Convert.ToInt64(employeeId))
                                                                      select em).ToListAsync();
                return responseAcces;
            }
        }
        #endregion

        #region "Private methods"
        /// <summary>
        /// Gets an Employee item after an insert or update sentence
        /// </summary>
        /// <param name="employee">object with employee data</param>
        /// <returns>object with employee data</returns>
        private async Task<RedarBorEmployeesEntity> GetEmployeeItem(RedarBorEmployeesEntity employee)
        {
            RedarBorEmployeesEntity? responseEmployee = await(from em in this._context.Employees.AsNoTracking()
                                                             where em.id.Equals(employee.id)
                                                             select em).FirstOrDefaultAsync();
            return responseEmployee;
        }
        #endregion
    }
}
