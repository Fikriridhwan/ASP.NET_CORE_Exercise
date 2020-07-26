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
    public class ManagersController : Controller
    {
        HelperApi _api = new HelperApi();
        
        public IActionResult Index()
        {
            return View();
        }
        
        public JsonResult LoadManagers()
        {
            IEnumerable<Manager> managers = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("Managers");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Manager>>();
                readTask.Wait();
                managers = readTask.Result;
            }
            else
            {
                managers = Enumerable.Empty<Manager>();
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(managers);
        }

        public JsonResult GetById(int Id)
        {
            Manager manager = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("Managers/" + Id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                manager = JsonConvert.DeserializeObject<Manager>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(manager);
        }

        public JsonResult InsertUpdate(Manager manager, int Id)
        {
            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(manager);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (manager.Id == 0)
            {
                try
                {
                    var result = client.PostAsync("Managers", byteContent).Result;
                    return Json(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (manager.Id == Id)
            {
                try
                {
                    var result = client.PutAsync("Managers/" + Id, byteContent).Result;
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
            var result = client.DeleteAsync("Managers/" + id).Result;
            return Json(result);
        }

        public async Task<IActionResult> Excel()
        {
            List<Manager> managers = new List<Manager>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = await clientView.GetAsync("Managers");
            var resultView = resView.Content.ReadAsStringAsync().Result;
            managers = JsonConvert.DeserializeObject<List<Manager>>(resultView);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Managers");
                var currentRow = 1;
                var number = 0;
                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "Id";
                worksheet.Cell(currentRow, 3).Value = "Name";
                worksheet.Cell(currentRow, 4).Value = "NIP";
                worksheet.Cell(currentRow, 5).Value = "Division";

                foreach (var man in managers)
                {
                    currentRow++;
                    number++;
                    worksheet.Cell(currentRow, 1).Value = number;
                    worksheet.Cell(currentRow, 2).Value = man.Id;
                    worksheet.Cell(currentRow, 3).Value = man.Name;
                    worksheet.Cell(currentRow, 4).Value = man.Nip;
                    worksheet.Cell(currentRow, 5).Value = man.Division;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var conten = stream.ToArray();
                    return File(conten, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Managers_Data.xlsx");
                }
            }
        }
    }
}
