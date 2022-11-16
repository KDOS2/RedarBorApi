using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedarBorModels.Responses
{
    public class ResponseRedarBorRequest
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ResponseRedarBorRequest()
        {
            succes = true;
            message = string.Empty;
        }

        /// <summary>
        /// this says that the operation was a succes process
        /// </summary>
        public bool succes { get; set; }

        ///show the message that get after an operation   
        public string message { get; set; } 

        /// <summary>
        /// error code generate after a bad process compilation
        /// </summary>
        public int? erroCode { get; set; }

        /// <summary>
        /// response after an operation or process
        /// </summary>
        public object response { get; set; }
    }

    /// <summary>
    /// maps the response after a new item has been inserted into the database
    /// </summary>
    public class ResponseAddItemEmpployee
    { 
        public long EmployeeId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
