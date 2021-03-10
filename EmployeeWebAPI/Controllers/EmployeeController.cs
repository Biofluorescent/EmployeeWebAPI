using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using EmployeeWebAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace EmployeeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //SQL query, table to store returned data, connection string to desired db
            string query = @"select Id, Name, Department,
                            convert(varchar(10), DateOfJoining,120) as DateOfJoining,
                            PhotoFileName from dbo.Employee";
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
        public JsonResult Post(Employee employee)
        {
            //SQL query, table to store returned data, connection string to desired db
            string query = @"insert into dbo.Employee 
                            (Name,Department,DateOfJoining,PhotoFileName)
                            values 
                            (
                            '" + employee.Name + @"',
                            '" + employee.Department + @"',
                            '" + employee.DateOfJoining + @"',
                            '" + employee.PhotoFileName + @"'
                            )
                            ";

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
        public JsonResult Put(Employee employee)
        {
            //SQL query, table to store returned data, connection string to desired db
            string query = @"
                            update dbo.Employee set
                            Name = '" + employee.Name + @"', 
                            Department = '" + employee.Department + @"',
                            DateOfJoining = '" + employee.DateOfJoining + @"'
                            where Id = " + employee.Id + @"";
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
                            delete from dbo.Employee 
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

        // Create route to save uploaded files
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }catch(Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            //SQL query, table to store returned data, connection string to desired db
            string query = @"select Name from dbo.Department";
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
