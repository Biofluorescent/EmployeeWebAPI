using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EmployeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //SQL query, table to store returned data, connection string to desired db
            string query = @"select Id, Name from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            // Create reader to get data, create and open a connection to the db
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                // Create the T-SQL command
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    // Execute the command, load data into table
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    // Close connections
                    myReader.Close();
                    myCon.Close();
                }
            }

            // Create and return Json from the table data
            return new JsonResult(table);
        }
    }
}
