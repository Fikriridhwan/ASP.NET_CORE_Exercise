using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ASP.NET_Core_Client.Helper;
using ASP.NET_Core_Client.Models;
using ASP.NET_Core_Client.Report;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASP.NET_Core_Client.Controllers
{
    public class HumanResourcesController : Controller
    {
        HelperApi _api = new HelperApi();
        public async Task<IActionResult> Index()
        {
            List<HumanResource> humanResources = new List<HumanResource>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("HumanResources");
            var result = res.Content.ReadAsStringAsync().Result;
            humanResources = JsonConvert.DeserializeObject<List<HumanResource>>(result);
            return View(humanResources);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var humanResource = new HumanResource();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("HumanResources/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            humanResource = JsonConvert.DeserializeObject<HumanResource>(result.Substring(1, result.Length - 2));
            return View(humanResource);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HumanResource humanResource)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PostAsJsonAsync("HumanResources", humanResource).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int Id)
        {
            var humanResource = new HumanResource();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("HumanResources/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            humanResource = JsonConvert.DeserializeObject<HumanResource>(result.Substring(1, result.Length - 2));
            return View(humanResource);
        }

        [HttpPost]
        public ActionResult Edit(HumanResource humanResource, int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PutAsJsonAsync("HumanResources/" + Id.ToString(), humanResource).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int Id)
        {
            var humanResource = new HumanResource();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("HumanResources/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            humanResource = JsonConvert.DeserializeObject<HumanResource>(result.Substring(1, result.Length - 2));
            return View(humanResource);
        }

        [HttpPost]
        public ActionResult DeleteSend(int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.DeleteAsync("HumanResources/" + Id.ToString()).Result;
            if (res.Content.ReadAsStringAsync().Result != "True")
            {
                TempData["msg"] = "<script>alert('Data failed to deleted!');</script>";
            }
            TempData["msg"] = "<script>alert('Data successfully deleted!');</script>";
            return RedirectToAction("Index");
        }

        public ActionResult Report(Employee employee)
        {
            List<HumanResource> humanResources = new List<HumanResource>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = clientView.GetAsync("HumanResources").Result;
            var resultView = resView.Content.ReadAsStringAsync().Result;
            humanResources = JsonConvert.DeserializeObject<List<HumanResource>>(resultView);
            //var list = employees.Select(r => r.Id);
            //ViewBag.employee = new SelectList(list, "Id");

            HumanResourcesReport humanResourcesReport = new HumanResourcesReport();
            byte[] abyte = humanResourcesReport.PrepareReport(humanResources);
            return File(abyte, "application/pdf");
        }
    }
}
