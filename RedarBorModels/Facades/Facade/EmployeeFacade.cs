namespace RedarBorModels.Facades.Facade
{
    using AutoMapper;
    using Microsoft.Extensions.Configuration;
    using RedarBorModels.Entities;
    using RedarBorModels.Errors;
    using RedarBorModels.Facades.IFacade;
    using RedarBorModels.Requests;
    using RedarBorModels.Responses;
    using RedarBorModels.Services.Interfaces;
    using System.Text.RegularExpressions;

    public class EmployeeFacade : IEmployeeFacade
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="employeeService">references Employee service</param>
        public EmployeeFacade(IEmployeeService employeeService, IMapper mapper, IConfiguration config)
        {
            this._employeeService = employeeService;
            this._mapper = mapper;
            this._config = config;
        }

        #region "Public Methods"
        /// <summary>
        /// Adds a new Item into RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        public async Task<ResponseRedarBorRequest> AddNewItemEmployee(RedarBorEmployeeRequest employee)
        {
            ResponseRedarBorRequest response = new ResponseRedarBorRequest();

            try
            {
                RedarBorEmployeesEntity empl = this._mapper.Map<RedarBorEmployeesEntity>(employee);
                if (ValidateMandatoryParams(empl))
                {
                    if(this.ValidateEmail(empl.Email))
                        response = await this._employeeService.AddNewItemEmployee(empl);
                    else
                        throw new Exception(ProcessMessage.addUpdateItemEmailfacadeParamsError);
                }
                else
                    throw new Exception(ProcessMessage.addUpdateItemfacadeParamsError);

            }
            catch (Exception ex)
            {
                response.succes = false;
                response.erroCode = ProcessMessage.addUpdateItemfacadeParamsId;
                response.message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Deletes an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employeeId">Employee id used to find the register to be deleted</param>
        /// <returns>operation response</returns>
        public async Task<ResponseRedarBorRequest> DeleteItemEmployee(long employeeId)
        {
            var response = await this._employeeService.DeleteItemEmployee(employeeId);
            return response;
        }

        /// <summary>
        /// Gets all items from RedarBorEmployees table
        /// </summary>
        /// <returns>all information related to all items stored into the table RedarBorEmployees</returns>
        public async Task<ResponseRedarBorRequest> GetAllItemsEmployees()
        {
            ResponseRedarBorRequest response = await this._employeeService.GetAllItemsEmployees();
            return response;
        }

        /// <summary>
        /// Gets an item that is looking for Id
        /// </summary>
        /// <param name="idEmploy">Employee id </param>
        /// <returns>object with the employee information</returns>
        public async Task<ResponseRedarBorRequest> GetItemEmployeeById(long idEmploy)
        {
            ResponseRedarBorRequest response = await this._employeeService.GetItemEmployeeById(idEmploy);
            return response;
        }

        /// <summary>
        /// Updates an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        public async Task<ResponseRedarBorRequest> UpdateItemEmployee(RedarBorEmployUpdateRequest employee)
        {
            ResponseRedarBorRequest response = new ResponseRedarBorRequest();

            try
            {
                RedarBorEmployeesEntity empl = this._mapper.Map<RedarBorEmployeesEntity>(employee);
                if (ValidateMandatoryParams(empl))
                {
                    if (this.ValidateEmail(empl.Email))
                        response = await this._employeeService.UpdateItemEmployee(empl);
                    else
                        throw new Exception(ProcessMessage.addUpdateItemEmailfacadeParamsError);
                }
                else
                    throw new Exception(ProcessMessage.addUpdateItemfacadeParamsError);

            }
            catch (Exception ex)
            {
                response.succes = false;
                response.erroCode = ProcessMessage.addUpdateItemfacadeParamsId;
                response.message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// gets an user credentials token
        /// </summary>
        /// <param name="userCredentials">credentials</param>
        /// <returns></returns>
        /// <returns>token for authentication</returns>
        public async Task<ResponseRedarBorRequest> GetToken(RequestLogin userCredentials)
        {
            ResponseRedarBorRequest response = new ResponseRedarBorRequest();

            try
            {
                if (ValidateMandatoryParamsCredentials(userCredentials))
                    response = await this._employeeService.GetToken(userCredentials);
                else
                    throw new Exception(ProcessMessage.getTokenfacadeParamsError);

            }
            catch (Exception ex)
            {
                response.succes = false;
                response.erroCode = ProcessMessage.getUpdateTokefacadeParamsId;
                response.message = ex.Message;
            }

            return response;
        }
        #endregion

        #region "Private Methods"
        /// <summary>
        /// checks if any of the mandatory params is null or doesn't have value
        /// </summary>
        /// <param name="employee">object to be checked</param>
        /// <returns>true or false</returns>
        private bool ValidateMandatoryParams(RedarBorEmployeesEntity employee)
        {
            if (employee.CompanyId.Equals(0) || string.IsNullOrEmpty(employee.Email) || string.IsNullOrEmpty(employee.Password) ||
                employee.PortalId.Equals(0) || employee.RoleId.Equals(0) || employee.StatusId.Equals(0) || string.IsNullOrEmpty(employee.Username))
                return false;
            else
                return true;
        }

        /// <summary>
        /// checks if any credentials parameter are missed
        /// </summary>
        /// <param name="userLogIn">object to be checked</param>
        /// <returns>true or false</returns>
        private bool ValidateMandatoryParamsCredentials(RequestLogin userLogIn)
        {
            if (string.IsNullOrEmpty(userLogIn.password) || string.IsNullOrEmpty(userLogIn.user))
                return false;
            else
                return true;
        }

        /// <summary>
        /// validates if an email has a correct format
        /// </summary>
        /// <param name="email">email to be validated</param>
        /// <returns>true or falce</returns>
        private bool ValidateEmail(string email)
        {
            Regex exp = new Regex(this._config.GetSection("EmailFormatExp").Value);
            return exp.IsMatch(email);
        }
        #endregion
    }
}
