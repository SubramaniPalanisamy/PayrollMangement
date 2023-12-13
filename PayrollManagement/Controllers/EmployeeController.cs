using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Models;
using System.Data;
using System.Collections;
using Microsoft.AspNetCore.Http;
using PayrollManagement.EfData;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace PayrollManagement.Controllers;

public class EmployeeController : Controller
{
    
    // public IActionResult index(){
    //     Authentication employee = new Authentication();
        
        
    //     ViewBag.username = HttpContext.Session.GetString("username");
    //     ViewBag.usermode = HttpContext.Session.GetString("usermode");
       

        
    //     DataSet profileInfo = DatabaseConnection.getDetails(employee);
        
    //     return View("profile",profileInfo);
    // }
    private readonly PaymentDbContext _paymentDb;
    public EmployeeController(PaymentDbContext database){
        DatabaseConnection.connectDatabase();
        _paymentDb = database;
        
        // String username = HttpContext.Session.GetString("username");
    }

    public IActionResult index(){
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        ViewBag.status = HttpContext.Session.GetString("status");
        String? employeeID =HttpContext.Session.GetString("ID");
        IEnumerable<Salary> employeeList = _paymentDb.PaymentManagement.Where(employee=> employee.EmployeeID == Convert.ToInt32(employeeID));
        return View(employeeList);
    }
    public IActionResult profile(){
        
        Console.Write(HttpContext.Session.GetString("ID"));
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        ViewBag.status = HttpContext.Session.GetString("status");
        DataSet profileInfo = DatabaseConnection.getEmployeeDetails(HttpContext.Session.GetString("ID"));
        
        return View("profile",profileInfo);
    }
    public IActionResult getComplaints(){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        return View();
    }
    [HttpPost]
     public async Task<IActionResult> getComplaints(ComplaintsModel complaintsModel){
            ViewBag.username = HttpContext.Session.GetString("username");
            ViewBag.usermode = HttpContext.Session.GetString("usermode");
            ViewBag.status = HttpContext.Session.GetString("status");
            HttpClient httpClient =new HttpClient();
            string apiurl="http://localhost:5267/api/Complaint";
            var jsondata=JsonConvert.SerializeObject(complaintsModel);
            var data=new StringContent(jsondata, Encoding.UTF8,"application/json");

            var httpresponse=httpClient.PostAsync(apiurl,data);
            var result=await httpresponse.Result.Content.ReadAsStringAsync();
            if (result=="true")
            {
                return RedirectToAction("Index","Employee");
            }
            return View();
    }
}