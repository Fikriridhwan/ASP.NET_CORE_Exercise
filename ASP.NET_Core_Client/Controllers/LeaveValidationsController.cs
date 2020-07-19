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
    public class LeaveValidationsController : Controller
    {
        HelperApi _api = new HelperApi();
        public async Task<IActionResult> Index()
        {
            List<LeaveValidation> leaveValidations = new List<LeaveValidation>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveValidations");
            var result = res.Content.ReadAsStringAsync().Result;
            leaveValidations = JsonConvert.DeserializeObject<List<LeaveValidation>>(result);
            return View(leaveValidations);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var leaveValidation = new LeaveValidation();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveValidations/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            leaveValidation = JsonConvert.DeserializeObject<LeaveValidation>(result.Substring(1, result.Length - 2));

            return View(leaveValidation);
        }

        public async Task<ActionResult> Create()
        {
            List<Manager> managers = new List<Manager>();
            HttpClient clientM = _api.Initial();
            HttpResponseMessage resM = await clientM.GetAsync("Managers");
            var resultM = resM.Content.ReadAsStringAsync().Result;
            managers = JsonConvert.DeserializeObject<List<Manager>>(resultM);
            var list1 = managers.Select(r => r.Id);
            ViewBag.managersId = new SelectList(list1, "Id");

            List<LeaveApplication> LeaveApp = new List<LeaveApplication>();
            HttpClient clientLA = _api.Initial();
            HttpResponseMessage resLA = await clientLA.GetAsync("LeaveApplications");
            var resultLA = resLA.Content.ReadAsStringAsync().Result;
            LeaveApp = JsonConvert.DeserializeObject<List<LeaveApplication>>(resultLA);
            var list2 = LeaveApp.Select(r => r.Id);
            ViewBag.LeaveAppId = new SelectList(list2, "Id");

            return View();
        }

        [HttpPost]
        public ActionResult Create(LeaveValidation leaveValidation)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PostAsJsonAsync("LeaveValidations", leaveValidation).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int Id)
        {
            List<Manager> managers = new List<Manager>();
            HttpClient clientM = _api.Initial();
            HttpResponseMessage resM = await clientM.GetAsync("Managers");
            var resultM = resM.Content.ReadAsStringAsync().Result;
            managers = JsonConvert.DeserializeObject<List<Manager>>(resultM);
            var list1 = managers.Select(r => r.Id);
            ViewBag.managersId = new SelectList(list1, "Id");

            List<LeaveApplication> LeaveApp = new List<LeaveApplication>();
            HttpClient clientLA = _api.Initial();
            HttpResponseMessage resLA = await clientLA.GetAsync("LeaveApplications");
            var resultLA = resLA.Content.ReadAsStringAsync().Result;
            LeaveApp = JsonConvert.DeserializeObject<List<LeaveApplication>>(resultLA);
            var list2 = LeaveApp.Select(r => r.Id);
            ViewBag.LeaveAppId = new SelectList(list2, "Id");

            var leaveValidation = new LeaveValidation();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveValidations/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            leaveValidation = JsonConvert.DeserializeObject<LeaveValidation>(result.Substring(1, result.Length - 2));

            return View(leaveValidation);
        }

        [HttpPost]
        public ActionResult Edit(LeaveValidation leaveValidation, int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PutAsJsonAsync("LeaveValidations/" + Id.ToString(), leaveValidation).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int Id)
        {
            var leaveValidation = new LeaveValidation();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveValidations/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            leaveValidation = JsonConvert.DeserializeObject<LeaveValidation>(result.Substring(1, result.Length - 2));
            return View(leaveValidation);
        }

        [HttpPost]
        public ActionResult DeleteSend(int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.DeleteAsync("LeaveValidations/" + Id.ToString()).Result;
            if (res.Content.ReadAsStringAsync().Result != "True")
            {
                TempData["msg"] = "<script>alert('Data failed to deleted!');</script>";
            }
            TempData["msg"] = "<script>alert('Data successfully deleted!');</script>";
            return RedirectToAction("Index");
        }

        public ActionResult Report(LeaveValidation leaveValidation)
        {
            List<LeaveValidation> leaveValidations = new List<LeaveValidation>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = clientView.GetAsync("LeaveValidations").Result;
            var resultView = resView.Content.ReadAsStringAsync().Result;
            leaveValidations = JsonConvert.DeserializeObject<List<LeaveValidation>>(resultView);

            LeaveValsReport leaveValReport = new LeaveValsReport();
            byte[] abyte = leaveValReport.PrepareReport(leaveValidations);
            return File(abyte, "application/pdf");
        }
    }
}
