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
    public class LeaveReportRepository : ILeaveReportRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public LeaveReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(LeaveReportVM leaveReportVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Insert_LeaveReport";
                parameters.Add("AppEntry", leaveReportVM.ApplicationEntry);
                parameters.Add("LeaveValId", leaveReportVM.LeaveValidationId);
                parameters.Add("HumanResId", leaveReportVM.HumanResourceId);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Delete_LeaveReport";
                parameters.Add("Id", Id);
                var delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return delete;
            }
        }

        public async Task<IEnumerable<LeaveReportVM>> Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Get_LeaveReport";
                parameters.Add("Id", Id);
                var getLR = await connection.QueryAsync<LeaveReportVM>(procName, parameters, commandType: CommandType.StoredProcedure);
                return getLR;
            }
        }

        public IEnumerable<LeaveReportVM> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_GetAll_LeaveReport";
                var getLRAll =  connection.Query<LeaveReportVM>(procName, commandType: CommandType.StoredProcedure);
                return getLRAll;
            }
        }

        public int Update(LeaveReportVM leaveReportVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Update_LeaveReport";
                parameters.Add("Id", Id);
                parameters.Add("AppEntry", leaveReportVM.ApplicationEntry);
                parameters.Add("LeaveValId", leaveReportVM.LeaveValidationId);
                parameters.Add("HumanResId", leaveReportVM.HumanResourceId);
                var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }
    }
}
