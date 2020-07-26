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
    public class LeaveApplicationsController : Controller
    {
        HelperApi _api = new HelperApi();

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadLeaveApplications()
        {
            IEnumerable<LeaveApplication> leaveApplications = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("LeaveApplications");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<LeaveApplication>>();
                readTask.Wait();
                leaveApplications = readTask.Result;
            }
            else
            {
                leaveApplications = Enumerable.Empty<LeaveApplication>();
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(leaveApplications);
        }

        public JsonResult GetById(int Id)
        {
            LeaveApplication leaveApplication = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("LeaveApplications/" + Id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                leaveApplication = JsonConvert.DeserializeObject<LeaveApplication>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(leaveApplication);
        }

        public JsonResult InsertUpdate(LeaveApplication leaveApplication, int Id)
        {
            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(leaveApplication);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (leaveApplication.Id == 0)
            {
                try
                {
                    var result = client.PostAsync("LeaveApplications", byteContent).Result;
                    return Json(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (leaveApplication.Id == Id)
            {
                try
                {
                    var result = client.PutAsync("LeaveApplications/" + Id, byteContent).Result;
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
            var result = client.DeleteAsync("LeaveApplications/" + id).Result;
            return Json(result);
        }

        public async Task<IActionResult> Excel()
        {
            List<LeaveApplication> leaveApplications = new List<LeaveApplication>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = await clientView.GetAsync("LeaveApplications");
            var resultView = resView.Content.ReadAsStringAsync().Result;
            leaveApplications = JsonConvert.DeserializeObject<List<LeaveApplication>>(resultView);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LeaveApplications");
                var currentRow = 1;
                var number = 0;
                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "Employee Id";
                worksheet.Cell(currentRow, 3).Value = "Name";
                worksheet.Cell(currentRow, 4).Value = "NIP";
                worksheet.Cell(currentRow, 5).Value = "Reason";
                worksheet.Cell(currentRow, 6).Value = "Leave Duration";

                foreach (var LeaveApp in leaveApplications)
                {
                    currentRow++;
                    number++;
                    worksheet.Cell(currentRow, 1).Value = number;
                    worksheet.Cell(currentRow, 2).Value = LeaveApp.EmployeeId;
                    worksheet.Cell(currentRow, 3).Value = LeaveApp.EmployeeName;
                    worksheet.Cell(currentRow, 4).Value = LeaveApp.Nip;
                    worksheet.Cell(currentRow, 5).Value = LeaveApp.Reason;
                    worksheet.Cell(currentRow, 6).Value = LeaveApp.LeaveDuration;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var conten = stream.ToArray();
                    return File(conten, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "LeaveApplications_Data.xlsx");
                }
            }
        }
    }
}
