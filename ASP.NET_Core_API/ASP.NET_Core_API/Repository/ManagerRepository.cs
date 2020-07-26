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
    public class ManagerRepository : IManagerRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public ManagerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(ManagerVM managerVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Insert_Manager";
                parameters.Add("Div", managerVM.Division);
                parameters.Add("EmpId", managerVM.EmployeeId);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Delete_Manager";
                parameters.Add("Id", Id);
                var delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return delete;
            }
        }

        public ManagerVM Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Get_Manager";
                parameters.Add("Id", Id);
                var getManager =  connection.Query<ManagerVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getManager;
            }
        }

        public async Task<IEnumerable<ManagerVM>> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_GetAll_Manager";
                var getAllManager = await connection.QueryAsync<ManagerVM>(procName, commandType: CommandType.StoredProcedure);
                return getAllManager;
            }
        }

        public int Update(ManagerVM managerVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Update_Manager";
                parameters.Add("Id", Id);
                parameters.Add("Div", managerVM.Division);
                parameters.Add("EmpId", managerVM.EmployeeId);
                var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }
    }
}
