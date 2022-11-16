namespace RedarborDataBase.Services
{
    using RedarBorModels.Entities;
    using RedarBorModels.Responses;
    using RedarBorModels.Services.Interfaces;
    using Microsoft.ServiceFabric.Services.Remoting;
    using RedarBorModels.AdminDb.Interfaces;
    using RedarBorModels.Errors;
    using RedarBorModels.Requests;
    using RedarborDataBase.Users;
    using Microsoft.Extensions.Configuration;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Employee service 
    /// </summary>
    public class EmployeeServices : IEmployeeService, IService
    {
        private readonly IEmployeeAccesDb _accesDb;
        private readonly IConfiguration _config;

        public EmployeeServices(IEmployeeAccesDb accesDb, IConfiguration config)
        {
            this._accesDb = accesDb;
            this._config = config;
        }

        /// <summary>
        /// Adds a new Item into RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        public async Task<ResponseRedarBorRequest> AddNewItemEmployee(RedarBorEmployeesEntity employee)
        {
            ResponseRedarBorRequest responseAdditem = new ResponseRedarBorRequest();

            try
            {
                responseAdditem.succes = true;
                responseAdditem.message = ProcessMessage.addNewItemSucces;
                ResponseAddItemEmpployee responseAdd = await this._accesDb.AddEmployee(employee);
                responseAdditem.response = responseAdd;
            }
            catch (Exception ex)
            {
                responseAdditem.succes = false;
                responseAdditem.message = ProcessMessage.addNewItemError + " :: Informacion adicional :: " + ex.Message;
                responseAdditem.erroCode = ProcessMessage.addNewItemId;
            }

            return responseAdditem;
        }

        /// <summary>
        /// Deletes an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employeeId">Employee id used to find the register to be deleted</param>
        /// <returns>operation response</returns>
        public async Task<ResponseRedarBorRequest> DeleteItemEmployee(long employeeId)
        {
            ResponseRedarBorRequest responseAdditem = new ResponseRedarBorRequest();

            try
            {
                responseAdditem.succes = true;
                responseAdditem.message = ProcessMessage.deleteItemSucces;

                if (await this._accesDb.DeleteEmployee(employeeId))
                    responseAdditem.response = new { EmployeeId = employeeId };
            }
            catch (Exception ex)
            {
                responseAdditem.succes = false;
                responseAdditem.message = ProcessMessage.deleteItemError + " :: Informacion adicional :: " + ex.Message;
                responseAdditem.erroCode = ProcessMessage.deleteItemId;
            }

            return responseAdditem;
        }

        /// <summary>
        /// Deletes an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employeeId">Employee id used to find the register to be deleted</param>
        /// <returns>operation response</returns>
        public async Task<ResponseRedarBorRequest> DeleteEmployeeEntity(long employeeId)
        {
            ResponseRedarBorRequest responseAdditem = new ResponseRedarBorRequest();

            try
            {
                responseAdditem.succes = true;
                responseAdditem.message = ProcessMessage.deleteItemSucces;

                if (await this._accesDb.DeleteEmployeeEntity(employeeId))
                    responseAdditem.response = new { EmployeeId = employeeId };
            }
            catch (Exception ex)
            {
                responseAdditem.succes = false;
                responseAdditem.message = ProcessMessage.deleteItemError + " :: Informacion adicional :: " + ex.Message;
                responseAdditem.erroCode = ProcessMessage.deleteItemId;
            }

            return responseAdditem;
        }

        /// <summary>
        /// Gets all items from RedarBorEmployees table
        /// </summary>
        /// <returns>all information related to all items stored into the table RedarBorEmployees</returns>
        public async Task<ResponseRedarBorRequest> GetAllItemsEmployees()
        {
            ResponseRedarBorRequest responseAdditem = new ResponseRedarBorRequest();

            try
            {
                responseAdditem.succes = true;
                responseAdditem.message = ProcessMessage.getItemSucces;
                List<RedarBorEmployeesEntity> lstResponse = await this._accesDb.GetEmployees();
                responseAdditem.response = lstResponse;
            }
            catch (Exception ex)
            {
                responseAdditem.succes = false;
                responseAdditem.message = ProcessMessage.getItemError + " :: Informacion adicional :: " + ex.Message;
                responseAdditem.erroCode = ProcessMessage.getItemId;
            }

            return responseAdditem;
        }

        /// <summary>
        /// Gets an item that is looking for Id
        /// </summary>
        /// <param name="idEmploy">Employee id </param>
        /// <returns>object with the employee information</returns>
        public async Task<ResponseRedarBorRequest> GetItemEmployeeById(long idEmploy)
        {
            ResponseRedarBorRequest responseAdditem = new ResponseRedarBorRequest();

            try
            {
                responseAdditem.succes = true;
                responseAdditem.message = ProcessMessage.getItemSucces;
                List<RedarBorEmployeesEntity> lstResponse = await this._accesDb.GetEmployees(idEmploy);
                responseAdditem.response = lstResponse;
            }
            catch (Exception ex)
            {
                responseAdditem.succes = false;
                responseAdditem.message = ProcessMessage.getItemError + " :: Informacion adicional :: " + ex.Message;
                responseAdditem.erroCode = ProcessMessage.getItemId;
            }

            return responseAdditem;
        }

        /// <summary>
        /// Updates an Item from RedarBorEmployees table
        /// </summary>
        /// <param name="employee">object with the data to be recorder</param>
        /// <returns>object that has been storaged</returns>
        public async Task<ResponseRedarBorRequest> UpdateItemEmployee(RedarBorEmployeesEntity employee)
        {
            ResponseRedarBorRequest responseAdditem = new ResponseRedarBorRequest();

            try
            {
                responseAdditem.succes = true;
                responseAdditem.message = ProcessMessage.updateItemSucces;
                employee.UpdatedOn = DateTime.Now;

                if (await this._accesDb.UpdateEmployee(employee))
                    responseAdditem.response = new { EmployeeId = employee.id, UpdatedOn = employee.UpdatedOn };
            }
            catch (Exception ex)
            {
                responseAdditem.succes = false;
                responseAdditem.message = ProcessMessage.updateItemError + " :: Informacion adicional :: " + ex.Message;
                responseAdditem.erroCode = ProcessMessage.updateItemId;
            }

            return responseAdditem;
        }

        /// <summary>
        /// gets an user credentials token
        /// </summary>
        /// <param name="userCredentials">credentials</param>
        /// <returns></returns>
        /// <returns>token for authentication</returns>
        public async Task<ResponseRedarBorRequest> GetToken(RequestLogin userCredentials)
        {
            ResponseRedarBorRequest responseAdditem = new ResponseRedarBorRequest();

            try
            {
                responseAdditem.succes = true;
                List<UsersAccesModel>  lstUsers = await DBAccesUser.GetUser();
                UsersAccesModel user = lstUsers.Where(a => a.userlogin.Equals(userCredentials.user) && a.password.Equals(userCredentials.password)).FirstOrDefault();
                responseAdditem.response = user != null ? this.GetToken(user) : "No se encontro usuario con las credenciales ingresadas";
                responseAdditem.message = user != null ? ProcessMessage.getTokenSucces : "No se genero token";
            }
            catch (Exception ex)
            {
                responseAdditem.succes = false;
                responseAdditem.message = ProcessMessage.getTokenError + " :: Informacion adicional :: " + ex.Message;
                responseAdditem.erroCode = ProcessMessage.getTokenId;
            }

            return responseAdditem;
        }

        /// <summary>
        /// create a token
        /// </summary>
        /// <param name="user">user crerdentials</param>
        /// <returns>token</returns>
        private string GetToken(UsersAccesModel user)
        {
            string keyJwt = this._config.GetSection("Jwt:key").Value;
            string Issuer = this._config.GetSection("Jwt:Issuer").Value;
            string Audience = this._config.GetSection("Jwt:Audience").Value;
            string Subject = this._config.GetSection("Jwt:Subject").Value;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("IdUser", user.userlogin),
                new Claim("UserLogIn", user.userlogin)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJwt));
            var sigIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Issuer,
                Audience,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: sigIn
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
