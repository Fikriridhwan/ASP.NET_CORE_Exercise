using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ASP.NET_Core_Client.Helper;
using ASP.NET_Core_Client.Models;
using ASP.NET_Core_Client.Report;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ASP.NET_Core_Client.Controllers
{
    public class EmployeesController : Controller
    {
        readonly HelperApi _api = new HelperApi();
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadEmployees()
        {
            IEnumerable<Employee> employees = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("Employees");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                readTask.Wait();
                employees = readTask.Result;
            }
            else
            {
                employees = Enumerable.Empty<Employee>();
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(employees);
        }

        public JsonResult GetById(int Id)
        {
            Employee employee = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("Employees/" + Id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                employee = JsonConvert.DeserializeObject<Employee>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(employee);
        }

        public JsonResult InsertUpdate(Employee employee, int Id)
        {
            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(employee);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (employee.Id == 0)
            {
                try
                {
                    var result = client.PostAsync("Employees", byteContent).Result;
                    return Json(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (employee.Id == Id)
            {
                try
                {
                    var result = client.PutAsync("Employees/" + Id, byteContent).Result;
                    return Json(result);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return Json(404);
        }

        public JsonResult Delete(int id)
        {
            HttpClient client = _api.Initial();
            var result = client.DeleteAsync("Employees/" + id).Result;
            return Json(result);
        }

        public async Task<IActionResult> Excel()
        {
            List<Employee> employees = new List<Employee>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = await clientView.GetAsync("Employees");
            var resultView = resView.Content.ReadAsStringAsync().Result;
            employees = JsonConvert.DeserializeObject<List<Employee>>(resultView);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");
                var currentRow = 1;
                var number = 0;
                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "Employee Id";
                worksheet.Cell(currentRow, 3).Value = "Employee Name";
                worksheet.Cell(currentRow, 4).Value = "Employee NIP";
                worksheet.Cell(currentRow, 5).Value = "Employee Annual Leave Remain";

                foreach (var emp in employees)
                {
                    currentRow++;
                    number++;
                    worksheet.Cell(currentRow, 1).Value = number;
                    worksheet.Cell(currentRow, 2).Value = emp.Id;
                    worksheet.Cell(currentRow, 3).Value = emp.Name;
                    worksheet.Cell(currentRow, 4).Value = emp.Nip;
                    worksheet.Cell(currentRow, 5).Value = emp.annualLeaveRemaining;
                }

                using( var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var conten = stream.ToArray();
                    return File(conten, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Employees_Data.xlsx");
                }
            }
        }
        public IActionResult TestView()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TestView(Employee employee)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PostAsJsonAsync("Employees/Login", employee).Result;
            if (res.IsSuccessStatusCode == false)
            {
                TempData["msg"] = "Login Failed!";
                return View();
            }
            TempData["msg"] = "<script>alert('Login Success!');</script>";
            return RedirectToAction("Index");
        }
    }
}
