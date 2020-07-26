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
    public class LeaveReportsController : Controller
    {
        HelperApi _api = new HelperApi();

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadLeaveReports()
        {
            IEnumerable<LeaveReport> leaveReports = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("LeaveReports");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<LeaveReport>>();
                readTask.Wait();
                leaveReports = readTask.Result;
            }
            else
            {
                leaveReports = Enumerable.Empty<LeaveReport>();
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(leaveReports);
        }

        public JsonResult GetById(int Id)
        {
            LeaveReport leaveReport = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("LeaveReports/" + Id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                leaveReport = JsonConvert.DeserializeObject<LeaveReport>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(leaveReport);
        }

        public JsonResult InsertUpdate(LeaveReport leaveReport, int Id)
        {
            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(leaveReport);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (leaveReport.Id == 0)
            {
                try
                {
                    var result = client.PostAsync("LeaveReports", byteContent).Result;
                    return Json(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (leaveReport.Id == Id)
            {
                try
                {
                    var result = client.PutAsync("LeaveReports/" + Id, byteContent).Result;
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
            var result = client.DeleteAsync("LeaveReports/" + id).Result;
            return Json(result);
        }

        public async Task<IActionResult> Excel()
        {
            List<LeaveReport> leaveReports = new List<LeaveReport>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = await clientView.GetAsync("LeaveReports");
            var resultView = resView.Content.ReadAsStringAsync().Result;
            leaveReports = JsonConvert.DeserializeObject<List<LeaveReport>>(resultView);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LeaveValidations");
                var currentRow = 1;
                var number = 0;
                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "Application Entry";
                worksheet.Cell(currentRow, 3).Value = "Leave Validation Id";
                worksheet.Cell(currentRow, 4).Value = "Action";
                worksheet.Cell(currentRow, 5).Value = "Valid Duration";
                worksheet.Cell(currentRow, 6).Value = "Human Resource Id";

                foreach (var leaveRep in leaveReports)
                {
                    currentRow++;
                    number++;
                    worksheet.Cell(currentRow, 1).Value = number;
                    worksheet.Cell(currentRow, 2).Value = leaveRep.ApplicationEntry;
                    worksheet.Cell(currentRow, 3).Value = leaveRep.LeaveValidationId;
                    worksheet.Cell(currentRow, 4).Value = leaveRep.Action;
                    worksheet.Cell(currentRow, 5).Value = leaveRep.DurationLV;
                    worksheet.Cell(currentRow, 6).Value = leaveRep.HumanResourceId;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var conten = stream.ToArray();
                    return File(conten, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "LeaveReports_Data.xlsx");
                }
            }
        }
    }
}
