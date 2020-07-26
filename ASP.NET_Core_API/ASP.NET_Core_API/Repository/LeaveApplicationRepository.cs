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
    public class LeaveApplicationRepository : ILeaveApplicationRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public LeaveApplicationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(LeaveApplicationVM leaveApplicationVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Insert_LeaveApplication";
                parameters.Add("Reason", leaveApplicationVM.Reason);
                parameters.Add("Duration", leaveApplicationVM.LeaveDuration);
                parameters.Add("EmpId", leaveApplicationVM.EmployeeId);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Delete_LeaveApplication";
                parameters.Add("Id", Id);
                var delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return delete;
            }
        }

        public LeaveApplicationVM Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Get_LeaveApplication";
                parameters.Add("Id", Id);
                var getLA = connection.Query<LeaveApplicationVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getLA;
            }
        }

        public async Task<IEnumerable<LeaveApplicationVM>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_GetAll_LeaveApplication";
                var getAllHR = await connection.QueryAsync<LeaveApplicationVM>(procName, commandType: CommandType.StoredProcedure);
                return getAllHR;
            }
        }

        public int Update(LeaveApplicationVM leaveApplicationVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Update_LeaveApplication";
                parameters.Add("Id", Id);
                parameters.Add("Reason", leaveApplicationVM.Reason);
                parameters.Add("Duration", leaveApplicationVM.LeaveDuration);
                parameters.Add("EmpId", leaveApplicationVM.EmployeeId);
                var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }
    }
}
