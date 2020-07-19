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
    public class HumanResourceRepository : IHumanResourceRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public HumanResourceRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(HumanResourceVM humanResourceVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Insert_HumanResource";
                parameters.Add("Name", humanResourceVM.Name);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Delete(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Delete_HumanResource";
                parameters.Add("Id", Id);
                var delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return delete;
            }
        }

        public async Task<IEnumerable<HumanResourceVM>> Get(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Get_HumanResource";
                parameters.Add("Id", Id);
                var getHR = await connection.QueryAsync<HumanResourceVM>(procName, parameters, commandType: CommandType.StoredProcedure);
                return getHR;
            }
        }

        public IEnumerable<HumanResourceVM> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_GetAll_HumanResource";
                var getAllHR = connection.Query<HumanResourceVM>(procName, commandType: CommandType.StoredProcedure);
                return getAllHR;
            }
        }

        public int Update(HumanResourceVM humanResourceVM, int Id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConn")))
            {
                var procName = "SP_Update_HumanResource";
                parameters.Add("Id", Id);
                parameters.Add("Name", humanResourceVM.Name);
                var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return update;
            }
        }
    }
}
