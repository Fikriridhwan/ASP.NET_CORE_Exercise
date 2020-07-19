using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ASP.NET_Core_Client.Helper;
using ASP.NET_Core_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ASP.NET_Core_Client.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        HelperApi _api = new HelperApi();
        public async Task<IActionResult> Index()
        {
            List<LeaveApplication> LeaveApp = new List<LeaveApplication>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveApplications");
            var result = res.Content.ReadAsStringAsync().Result;
            LeaveApp = JsonConvert.DeserializeObject<List<LeaveApplication>>(result);
            return View(LeaveApp);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var LeaveApp = new LeaveApplication();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveApplications/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            LeaveApp = JsonConvert.DeserializeObject<LeaveApplication>(result.Substring(1, result.Length - 2));
            return View(LeaveApp);
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
        public ActionResult Create(LeaveApplication leaveApplication)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PostAsJsonAsync("LeaveApplications", leaveApplication).Result;
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

            var LeaveApp = new LeaveApplication();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveApplications/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            LeaveApp = JsonConvert.DeserializeObject<LeaveApplication>(result.Substring(1, result.Length - 2));

            return View(LeaveApp);
        }

        [HttpPost]
        public ActionResult Edit(LeaveApplication leaveApplication, int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PutAsJsonAsync("LeaveApplications/" + Id.ToString(), leaveApplication).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int Id)
        {
            var LeaveApp = new LeaveApplication();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveApplications/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            LeaveApp = JsonConvert.DeserializeObject<LeaveApplication>(result.Substring(1, result.Length - 2));
            return View(LeaveApp);
        }

        [HttpPost]
        public ActionResult DeleteSend(int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.DeleteAsync("LeaveApplications/" + Id.ToString()).Result;
            if (res.Content.ReadAsStringAsync().Result != "True")
            {
                TempData["msg"] = "<script>alert('Data failed to deleted!');</script>";
            }
            TempData["msg"] = "<script>alert('Data successfully deleted!');</script>";
            return RedirectToAction("Index");
        }
    }
}
