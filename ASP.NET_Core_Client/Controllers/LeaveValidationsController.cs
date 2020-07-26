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
    public class LeaveValidationsController : Controller
    {
        HelperApi _api = new HelperApi();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult LoadLeaveValidations()
        {
            IEnumerable<LeaveValidation> leaveValidations = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("LeaveValidations");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<LeaveValidation>>();
                readTask.Wait();
                leaveValidations = readTask.Result;
            }
            else
            {
                leaveValidations = Enumerable.Empty<LeaveValidation>();
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(leaveValidations);
        }

        public JsonResult GetById(int Id)
        {
            LeaveValidation leaveValidation = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("LeaveValidations/" + Id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                leaveValidation = JsonConvert.DeserializeObject<LeaveValidation>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(leaveValidation);
        }

        public JsonResult InsertUpdate(LeaveValidation leaveValidation, int Id)
        {
            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(leaveValidation);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (leaveValidation.Id == 0)
            {
                try
                {
                    var result = client.PostAsync("LeaveValidations", byteContent).Result;
                    return Json(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (leaveValidation.Id == Id)
            {
                try
                {
                    var result = client.PutAsync("LeaveValidations/" + Id, byteContent).Result;
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
            var result = client.DeleteAsync("LeaveValidations/" + id).Result;
            return Json(result);
        }

        public async Task<IActionResult> Excel()
        {
            List<LeaveValidation> leaveValidations = new List<LeaveValidation>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = await clientView.GetAsync("LeaveValidations");
            var resultView = resView.Content.ReadAsStringAsync().Result;
            leaveValidations = JsonConvert.DeserializeObject<List<LeaveValidation>>(resultView);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LeaveValidations");
                var currentRow = 1;
                var number = 0;
                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "Employee Nip";
                worksheet.Cell(currentRow, 3).Value = "Employee Name";
                worksheet.Cell(currentRow, 4).Value = "Leave Duration";
                worksheet.Cell(currentRow, 5).Value = "Reason";
                worksheet.Cell(currentRow, 6).Value = "Manager Id";
                worksheet.Cell(currentRow, 7).Value = "Valid Duration";
                worksheet.Cell(currentRow, 8).Value = "Manager Action";

                foreach (var leaveVal in leaveValidations)
                {
                    currentRow++;
                    number++;
                    worksheet.Cell(currentRow, 1).Value = number;
                    worksheet.Cell(currentRow, 2).Value = leaveVal.Nip;
                    worksheet.Cell(currentRow, 3).Value = leaveVal.Name;
                    worksheet.Cell(currentRow, 4).Value = leaveVal.Duration;
                    worksheet.Cell(currentRow, 5).Value = leaveVal.Reason;
                    worksheet.Cell(currentRow, 6).Value = leaveVal.ManagerId;
                    worksheet.Cell(currentRow, 7).Value = leaveVal.ValidDuration;
                    worksheet.Cell(currentRow, 8).Value = leaveVal.Action;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var conten = stream.ToArray();
                    return File(conten, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "LeaveValidations_Data.xlsx");
                }
            }
        }

    }
      
}
