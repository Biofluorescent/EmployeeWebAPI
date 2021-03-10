using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EmployeeWebAPI.Models;

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


        [HttpPost]
        public JsonResult Post(Department dep)
        {
            //SQL query, table to store returned data, connection string to desired db
            string query = @"insert into dbo.Department values ('" + dep.Name + @"')";
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

            return new JsonResult("Added Sucessfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            //SQL query, table to store returned data, connection string to desired db
            string query = @"
                            update dbo.Department set
                            Name = '" + dep.Name + @"' 
                            where Id = " + dep.Id + @"";
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

            return new JsonResult("Updated Sucessfully");
        }

        // Add what is sent to root parameter
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            //SQL query, table to store returned data, connection string to desired db
            string query = @"
                            delete from dbo.Department 
                            where Id = " + id + @"";
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

            return new JsonResult("Deleted Sucessfully");
        }


    }
}
