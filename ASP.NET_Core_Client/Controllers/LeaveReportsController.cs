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
    public class LeaveReportsController : Controller
    {
        HelperApi _api = new HelperApi();
        public async Task<IActionResult> Index()
        {
            List<LeaveReport> leaveReports = new List<LeaveReport>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveReports");
            var result = res.Content.ReadAsStringAsync().Result;
            leaveReports = JsonConvert.DeserializeObject<List<LeaveReport>>(result);
            return View(leaveReports);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var leaveReport = new LeaveReport();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveReports/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            leaveReport = JsonConvert.DeserializeObject<LeaveReport>(result.Substring(1, result.Length - 2));
            return View(leaveReport);
        }

        public async Task<ActionResult> Create()
        {
            List<LeaveValidation> leaveValidations = new List<LeaveValidation>();
            HttpClient clientLV = _api.Initial();
            HttpResponseMessage resLV = await clientLV.GetAsync("LeaveValidations");
            var resultLV = resLV.Content.ReadAsStringAsync().Result;
            leaveValidations = JsonConvert.DeserializeObject<List<LeaveValidation>>(resultLV);
            var list1 = leaveValidations.Select(r => r.Id);
            ViewBag.leaveValId = new SelectList(list1, "Id");

            List<HumanResource> humanResources = new List<HumanResource>();
            HttpClient clientHR = _api.Initial();
            HttpResponseMessage resHR = await clientHR.GetAsync("HumanResources");
            var resultHR = resHR.Content.ReadAsStringAsync().Result;
            humanResources = JsonConvert.DeserializeObject<List<HumanResource>>(resultHR);
            var list2 = humanResources.Select(r => r.Id);
            ViewBag.humanResId = new SelectList(list2, "Id");

            return View();
        }

        [HttpPost]
        public ActionResult Create(LeaveReport leaveReport)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PostAsJsonAsync("LeaveReports", leaveReport).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int Id)
        {
            List<LeaveValidation> leaveValidations = new List<LeaveValidation>();
            HttpClient clientLV = _api.Initial();
            HttpResponseMessage resLV = await clientLV.GetAsync("LeaveValidations");
            var resultLV = resLV.Content.ReadAsStringAsync().Result;
            leaveValidations = JsonConvert.DeserializeObject<List<LeaveValidation>>(resultLV);
            var list1 = leaveValidations.Select(r => r.Id);
            ViewBag.leaveValId = new SelectList(list1, "Id");

            List<HumanResource> humanResources = new List<HumanResource>();
            HttpClient clientHR = _api.Initial();
            HttpResponseMessage resHR = await clientHR.GetAsync("HumanResources");
            var resultHR = resHR.Content.ReadAsStringAsync().Result;
            humanResources = JsonConvert.DeserializeObject<List<HumanResource>>(resultHR);
            var list2 = humanResources.Select(r => r.Id);
            ViewBag.humanResId = new SelectList(list2, "Id");

            var leaveReport = new LeaveReport();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveReports/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            leaveReport = JsonConvert.DeserializeObject<LeaveReport>(result.Substring(1, result.Length - 2));

            return View(leaveReport);
        }

        [HttpPost]
        public ActionResult Edit(LeaveReport leaveReport, int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PutAsJsonAsync("LeaveReports/" + Id.ToString(), leaveReport).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int Id)
        {
            var leaveReport = new LeaveReport();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("LeaveReports/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            leaveReport = JsonConvert.DeserializeObject<LeaveReport>(result.Substring(1, result.Length - 2));

            return View(leaveReport);
        }

        [HttpPost]
        public ActionResult DeleteSend(int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.DeleteAsync("LeaveReports/" + Id.ToString()).Result;
            if (res.Content.ReadAsStringAsync().Result != "True")
            {
                TempData["msg"] = "<script>alert('Data failed to deleted!');</script>";
            }
            TempData["msg"] = "<script>alert('Data successfully deleted!');</script>";
            return RedirectToAction("Index");
        }

        public ActionResult Report(LeaveReport leaveReport)
        {
            List<LeaveReport> leaveReports = new List<LeaveReport>();
            HttpClient clientView = _api.Initial();
            HttpResponseMessage resView = clientView.GetAsync("LeaveReports").Result;
            var resultView = resView.Content.ReadAsStringAsync().Result;
            leaveReports = JsonConvert.DeserializeObject<List<LeaveReport>>(resultView);

            LeaveRepsReport leaveRepReport = new LeaveRepsReport();
            byte[] abyte = leaveRepReport.PrepareReport(leaveReports);
            return File(abyte, "application/pdf");
        }
    }
}
