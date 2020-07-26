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
    public class LeaveValidationRepository : ILeaveValidationRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public LeaveValidationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(LeaveValidationVM leaveValidationVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Insert_LeaveValidation";
                parameters.Add("Action", leaveValidationVM.Action);
                parameters.Add("VDuration", leaveValidationVM.ValidDuration);
                parameters.Add("ManId", leaveValidationVM.ManagerId);
                parameters.Add("LeaveAppId", leaveValidationVM.LeaveApplicationId);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Delete_LeaveValidation";
                parameters.Add("Id", Id);
                var delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return delete;
            }
        }

        public LeaveValidationVM Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Get_LeaveValidation";
                parameters.Add("Id", Id);
                var getLV = connection.Query<LeaveValidationVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getLV;
            }
        }

        public async Task<IEnumerable<LeaveValidationVM>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_GetAll_LeaveValidation";
                var getAllLV = await connection.QueryAsync<LeaveValidationVM>(procName, commandType: CommandType.StoredProcedure);
                return getAllLV;
            }
        }

        public int Update(LeaveValidationVM leaveValidationVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Update_LeaveValidation";
                parameters.Add("Id", Id);
                parameters.Add("Action", leaveValidationVM.Action);
                parameters.Add("ValidDuration", leaveValidationVM.ValidDuration);
                parameters.Add("ManId", leaveValidationVM.ManagerId);
                parameters.Add("LeaveAppId", leaveValidationVM.LeaveApplicationId);
                var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }
    }
}
