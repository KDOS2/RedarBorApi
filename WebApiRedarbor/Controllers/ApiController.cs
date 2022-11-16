namespace WebApiRedarbor.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RedarBorModels.Facades.IFacade;
    using RedarBorModels.Requests;
    using RedarBorModels.Responses;

    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IEmployeeFacade _employeeFacade;

        public ApiController(IEmployeeFacade employeeFacade)
        {
            this._employeeFacade = employeeFacade;
        }

        [HttpPost("GetToken")]
        public async Task<ActionResult<ResponseRedarBorRequest>> GetToken(RequestLogin userCredentials)
        {
            var response = await this._employeeFacade.GetToken(userCredentials);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("redarbor")]
        public async Task<ActionResult<ResponseRedarBorRequest>> AddNewItemEmployee(RedarBorEmployeeRequest employee)
        {
            var response = await this._employeeFacade.AddNewItemEmployee(employee);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("redarbor")]
        public async Task<ActionResult<ResponseRedarBorRequest>> GetAllItemsEmployees()
        {
            var response = await this._employeeFacade.GetAllItemsEmployees();
            return Ok(response);
        }

        [Authorize]
        [HttpGet("redarbor/{id}")]
        public async Task<ActionResult<ResponseRedarBorRequest>> GetItemEmployeeById(long id)
        {
            var response = await this._employeeFacade.GetItemEmployeeById(id);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("redarbor")]
        public async Task<ActionResult<ResponseRedarBorRequest>> UpdateItemEmployee(RedarBorEmployUpdateRequest employee)
        {
            var response = await this._employeeFacade.UpdateItemEmployee(employee);
            return Ok(response);
        }

        [Authorize]
        [HttpDelete("redarbor/{id}")]
        public async Task<ActionResult<ResponseRedarBorRequest>> DeleteItemEmployee(long id)
        {
            var response = await this._employeeFacade.DeleteItemEmployee(id);
            return Ok(response);
        }
    }
}
