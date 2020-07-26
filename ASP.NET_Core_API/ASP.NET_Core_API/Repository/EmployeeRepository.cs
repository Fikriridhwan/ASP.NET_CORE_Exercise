using ASP.NET_Core_API.Repository.Interface;
using ASP.NET_Core_API.ViewModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(EmployeeVM employeeVM)
        {
            using(SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Insert_Employee";
                parameters.Add("Name", employeeVM.Name);
                parameters.Add("Nip", employeeVM.Nip);
                parameters.Add("Annual", employeeVM.AnnualLeaveRemaining);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Delete_Employee";
                parameters.Add("Id", Id);
                var delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return delete;
            }
        }

        public EmployeeVM Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Get_Employee";
                parameters.Add("Id", Id);
                var getEmployee = connection.Query<EmployeeVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getEmployee;
            }
        }

        public async Task<IEnumerable<EmployeeVM>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_GetAll_Employee";
                var getAllEmployee = await connection.QueryAsync<EmployeeVM>(procName, commandType: CommandType.StoredProcedure);
                return getAllEmployee;
            }
        }

        public int Login(EmployeeVM employeeVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Login_Employee";
                SqlCommand com = new SqlCommand(procName, connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", employeeVM.Id);
                com.Parameters.AddWithValue("@Password", employeeVM.Name);
                SqlParameter Log = new SqlParameter();
                Log.ParameterName = "@IsValid";
                Log.SqlDbType = SqlDbType.Bit;
                Log.Direction = ParameterDirection.Output;
                com.Parameters.Add(Log);
                connection.Open();
                com.ExecuteNonQuery();
                int result = Convert.ToInt32(Log.Value);
                connection.Close();
                return result;
            }
        }

        public int Update(EmployeeVM employeeVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Update_Employee";
                parameters.Add("Id", Id);
                parameters.Add("Name", employeeVM.Name);
                parameters.Add("Nip", employeeVM.Nip);
                parameters.Add("Annual", employeeVM.AnnualLeaveRemaining);
                var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }
    }
}
