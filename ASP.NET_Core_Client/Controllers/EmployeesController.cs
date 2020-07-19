using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ASP.NET_Core_Client.Helper;
using ASP.NET_Core_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ASP.NET_Core_Client.Controllers
{
    public class EmployeesController : Controller
    {
        HelperApi _api = new HelperApi();
        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> employees;
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Employees");   
            employees = res.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
            return View(employees);
        }

        public async Task<IActionResult> Details(int Id)
        {
            var employees = new Employee();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Employees/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            employees = JsonConvert.DeserializeObject<Employee>(result.Substring(1, result.Length - 2));
            return View(employees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PostAsJsonAsync("Employees", employee).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int Id)
        {
            var employees = new Employee();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Employees/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            employees = JsonConvert.DeserializeObject<Employee>(result.Substring(1, result.Length - 2));
            return View(employees);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee, int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.PutAsJsonAsync("Employees/" + Id.ToString(), employee).Result;
            if (res.Content.ReadAsStringAsync().Result == "False")
            {
                return View();
            }
            TempData["msg"] = "<script>alert('Saved Successfully!');</script>";
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int Id)
        {
            var employees = new Employee();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Employees/" + Id.ToString());
            var result = res.Content.ReadAsStringAsync().Result;
            employees = JsonConvert.DeserializeObject<Employee>(result.Substring(1, result.Length - 2));
            return View(employees);
        }

        [HttpPost]
        public ActionResult DeleteSend(int Id)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = client.DeleteAsync("Employees/" + Id.ToString()).Result;
            if (res.Content.ReadAsStringAsync().Result != "True")
            {
                TempData["msg"] = "<script>alert('Data failed to deleted!');</script>";
            }
            TempData["msg"] = "<script>alert('Data successfully deleted!');</script>";
            return RedirectToAction("Index");
        }
    }
}
