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
using Newtonsoft.Json;

namespace ASP.NET_Core_Client.Controllers
{
    public class HumanResourcesController : Controller
    {
        HelperApi _api = new HelperApi();
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadHumanResources()
        {
            IEnumerable<HumanResource> humanResources = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("HumanResources");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<HumanResource>>();
                readTask.Wait();
                humanResources = readTask.Result;
            }
            else
            {
                humanResources = Enumerable.Empty<HumanResource>();
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(humanResources);
        }

        public JsonResult GetById(int Id)
        {
            HumanResource humanResource = null;
            HttpClient client = _api.Initial();
            var responseTask = client.GetAsync("HumanResources/" + Id);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                humanResource = JsonConvert.DeserializeObject<HumanResource>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server ERror Try after some time.");
            }
            return Json(humanResource);
        }

        public JsonResult InsertUpdate(HumanResource humanResource, int Id)
        {
            HttpClient client = _api.Initial();
            var myContent = JsonConvert.SerializeObject(humanResource);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (humanResource.Id == 0)
            {
                try
                {
                    var result = client.PostAsync("HumanResources", byteContent).Result;
                    return Json(result);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else if (humanResource.Id == Id)
            {
                try
                {
                    var result = client.PutAsync("HumanResources/" + Id, byteContent).Result;
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
            var result = client.DeleteAsync("HumanResources/" + id).Result;
            return Json(result);
        }

        public async Task<IActionResult> Excel()
        {
            List<HumanResource> humanResources = new List<HumanResource>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = await clientView.GetAsync("HumanResources");
            var resultView = resView.Content.ReadAsStringAsync().Result;
            humanResources = JsonConvert.DeserializeObject<List<HumanResource>>(resultView);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HumanResources");
                var currentRow = 1;
                var number = 0;
                worksheet.Cell(currentRow, 1).Value = "No";
                worksheet.Cell(currentRow, 2).Value = "Id";
                worksheet.Cell(currentRow, 3).Value = "Name";

                foreach (var hr in humanResources)
                {
                    currentRow++;
                    number++;
                    worksheet.Cell(currentRow, 1).Value = number;
                    worksheet.Cell(currentRow, 2).Value = hr.Id;
                    worksheet.Cell(currentRow, 3).Value = hr.Name;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var conten = stream.ToArray();
                    return File(conten, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "HR_Data.xlsx");
                }
            }
        }
        
    }
}
