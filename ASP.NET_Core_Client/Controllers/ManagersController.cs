using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ASP.NET_Core_Client.Helper;
using ASP.NET_Core_Client.Models;
using ASP.NET_Core_Client.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ASP.NET_Core_Client.Controllers
{
    public class ManagersController : Controller
    {
        HelperApi _api = new HelperApi();
        public async Task<IActionResult> Index()
        {
            List<Manager> managers = new List<Manager>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Managers");
            var result = res.Content.ReadAsStringAsync().Result;
            managers = JsonConvert.DeserializeObject<List<Manager>>(result);
            return View(managers);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var manager = new Manager();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Managers/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            manager = JsonConvert.DeserializeObject<Manager>(result.Substring(1, result.Length - 2));
            return View(manager);
        }

        public async Task<ActionResult> Create()
        {
            List<Employee> employees = new List<Employee>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Employees");
            var result = res.Content.ReadAsStringAsync().Result;
            employees = JsonConvert.DeserializeObject<List<Employee>>(result);
            var list = employees.Select(r => r.Id);
            ViewBag.employee = new SelectList(list, "Id");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Manager manager)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PostAsJsonAsync("Managers", manager).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int Id)
        {
            List<Employee> employees = new List<Employee>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = await clientView.GetAsync("Employees");
            var resultView = resView.Content.ReadAsStringAsync().Result;
            employees = JsonConvert.DeserializeObject<List<Employee>>(resultView);
            var list = employees.Select(r => r.Id);
            ViewBag.employee = new SelectList(list, "Id");

            var manager = new Manager();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Managers/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            manager = JsonConvert.DeserializeObject<Manager>(result.Substring(1, result.Length - 2));

            return View(manager);
        }

        [HttpPost]
        public ActionResult Edit(Manager manager, int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PutAsJsonAsync("Managers/" + Id.ToString(), manager).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int Id)
        {
            var manager = new Manager();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Managers/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            manager = JsonConvert.DeserializeObject<Manager>(result.Substring(1, result.Length - 2));
            return View(manager);
        }

        [HttpPost]
        public ActionResult DeleteSend(int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.DeleteAsync("Managers/" + Id.ToString()).Result;
            if (res.Content.ReadAsStringAsync().Result != "True")
            {
                TempData["msg"] = "<script>alert('Data failed to deleted!');</script>";
            }
            TempData["msg"] = "<script>alert('Data successfully deleted!');</script>";
            return RedirectToAction("Index");
        }

        public ActionResult Report(Manager manager)
        {
            List<Manager> managers = new List<Manager>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = clientView.GetAsync("Managers").Result;
            var resultView = resView.Content.ReadAsStringAsync().Result;
            managers = JsonConvert.DeserializeObject<List<Manager>>(resultView);

            ManagersReport managersReport = new ManagersReport();
            byte[] abyte = managersReport.PrepareReport(managers);
            return File(abyte, "application/pdf");
        }
    }
}
