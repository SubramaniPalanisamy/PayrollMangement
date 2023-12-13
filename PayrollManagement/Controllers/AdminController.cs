using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Models;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using PayrollManagement.EfData;
using Twilio;
using Twilio.Exceptions;
using System.Collections.Generic;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Microsoft.AspNetCore.Http;

namespace PayrollManagement.Controllers;
[Log]
public class AdminController : Controller
{   
    private readonly PaymentDbContext _paymentDb;
    private readonly IConfiguration _configuration;
    public AdminController(PaymentDbContext database,IConfiguration configuration ){
        DatabaseConnection.connectDatabase();
        _paymentDb = database;
        _configuration = configuration;
        

    }
    public IActionResult index()
    {
        ViewBag.userEmailId=HttpContext.Session.GetString("emailId");
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        
        if(@ViewBag.status == "logged"){
            return View();
        }else{
            return RedirectToRoute(new{controller ="Home",action="login"});
        }
          
    }

    public void sendCredentials(string phoneNum,string userName,string password,string role){
        string accountSid = _configuration["ApiCredentials : accountSid"];
        string authToken = _configuration ["ApiCredentials : authToken"] ;
        string content = userName + " " +password +"your added as"+ role;
        string code = "+91";
        
        string number = code+phoneNum;

        Console.WriteLine(phoneNum);
        TwilioClient.Init(accountSid, authToken);

        try
        {
            var message = MessageResource.Create(
                body: content,
                from: new Twilio.Types.PhoneNumber("+15073997127"),
                to: new Twilio.Types.PhoneNumber(number)
            );
            
            Console.WriteLine("sent");
        }
        catch (ApiException error)
        {
            Console.WriteLine(error.Message);
            
        }
        catch(Exception error){
            Console.WriteLine(error.Message);
        }
    }
    public IActionResult registration(){
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.status = HttpContext.Session.GetString("status");
        if(@ViewBag.status == "logged"){
            return View();
        }else{
            return RedirectToRoute(new{controller ="Home",action="login"});
        }
            
       
    }
    [HttpPost]
     public IActionResult registration(Employee employee){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        ViewBag.name = employee.EmployeeName;
        
        foreach (var file in Request.Form.Files)
        {
            MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            employee.Image = memoryStream.ToArray();
        }
        string? userStatus = " ";
        userStatus = DatabaseConnection.isExist(employee.EmployeeID);
        
        // DataSet employeeDetails = DatabaseConnection.getEmployees(); 
        ViewBag.status = userStatus;
        Console.WriteLine(userStatus);
        if(userStatus != "Exist"){
            string? result=DatabaseConnection.enrollEmployee(employee);
            if(result == "Registered")
            {
                string? phoneNum = DatabaseConnection.getPhoneNumber(employee.EmployeeID);
                string? userName = DatabaseConnection.getUserName(employee.EmployeeID);
                string? password = DatabaseConnection.getPassword(employee.EmployeeID);
                string? role     = DatabaseConnection.getRole(employee.EmployeeID);
                sendCredentials(phoneNum,userName,password,role);
                if(role == "Employee"){
                    DataSet employeeDetails = DatabaseConnection.getEmployees(); 
                    return View("viewEmployee",employeeDetails);
                }else{
                    DataSet adminDetail = DatabaseConnection.getAdmin();
                    return View("viewAdmin",adminDetail );

                }
              
            }
        }
        
        
        return View("registration");
    }
    
    public IActionResult viewEmployee(){
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.status = HttpContext.Session.GetString("status");
        DataSet employeeDetails = DatabaseConnection.getEmployees(); 
        if(@ViewBag.status == "logged"){
            return View("viewEmployee",employeeDetails);
        }else{
            return RedirectToRoute(new{controller ="Home",action="login"});
        }
               
            
       
    }

    public IActionResult viewAdmin(){

        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.status = HttpContext.Session.GetString("status");
        DataSet employeeDetails = DatabaseConnection.getAdmin(); 
        if(@ViewBag.status == "logged"){
            return View("viewEmployee",employeeDetails);
        }else{
            return RedirectToRoute(new{controller ="Home",action="login"});
        }
    }

    
   

    public string removePayment(int EmpID){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        try
            {
                List<Salary> salary = _paymentDb.PaymentManagement.Where(employee=> employee.EmployeeID == EmpID).ToList();
                foreach(var item in salary){
                    _paymentDb.PaymentManagement.Remove(item);
                    _paymentDb.SaveChanges();
                    Console.WriteLine("removed from payment history");
                }
                
            }    
            // catch (Exception error)
            // {
            //      throw new Exception("Data is not existed in the database");
                
            // }
            catch (Exception error)
            {
                return "Data is not existed in the database";
            }
            return"";
    }

    public async Task<ActionResult> removeEmployee(string? EmpID){
        Console.WriteLine(EmpID);
        int EmployeeID = Convert.ToInt32(EmpID);
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        DataSet employeeDetails = DatabaseConnection.getEmployees();
        try{
            var deleteData = Task.Run(() => DatabaseConnection.deleteData(EmpID));
            var deletePayment = Task.Run(()=> removePayment(EmployeeID));
            await Task.WhenAll(deleteData,deletePayment);
            DataSet employeeDetail = DatabaseConnection.getEmployees();
            return View("viewEmployee",employeeDetail );

            // records.Abort();
            // payment.Abort();

        }catch(Exception error){
           Console.WriteLine(error.Message);
           return Content(error.Message);
        }
        
        return View("viewEmployee",employeeDetails );
        
        
        
    }

    
    public async Task<ActionResult> removeAdmin(string? EmpID){
        Console.WriteLine(EmpID);
        int EmployeeID = Convert.ToInt32(EmpID);
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
       
        try{
            var deleteData = Task.Run(() => DatabaseConnection.deleteData(EmpID));
            var deletePayment = Task.Run(()=> removePayment(EmployeeID));
            await Task.WhenAll(deleteData,deletePayment);
            DataSet adminDetail = DatabaseConnection.getAdmin();
            return View("viewAdmin",adminDetail );

            // records.Abort();
            // payment.Abort();

        }catch(Exception error){
           Console.WriteLine(error.Message);
           return Content(error.Message);
        }
        
       
        
        
        
    }

    

    public IActionResult updateDetails(string? EmpID){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        DataSet employeeDetails = DatabaseConnection.getEmployeeDetails(EmpID);
        return View("updateDetails",employeeDetails);
    }

    [HttpPost]
    public IActionResult updateDetails(Employee employee){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        employee.EmployeeID = Convert.ToInt32(Request.Form["EmployeeID"]);
        employee.Age = Convert.ToByte(Request.Form["Age"]);
        employee.Salary = Convert.ToDouble(Request.Form["Salary"]);
        ViewBag.update = DatabaseConnection.updateData(employee);
        // DataTable employeeData = DatabaseConnection.getData(employee);
        
        
        if(ViewBag.update == "Updated"){
            if(ViewBag.usermode == "Admin")
            {
                return RedirectToAction("viewEmployee","Admin");
            }else{
                return RedirectToAction("profile","Employee");
            }
                
        }else{
            return View("updateDetails");
        }

        

    }
    
    public IActionResult viewComplaints(){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
    
        HttpClient httpclient=new HttpClient();
        string apiurl="http://localhost:5267/api/Complaint";
        var apiresponse=httpclient.GetAsync(apiurl).Result;
        var listofComplaints=apiresponse.Content.ReadAsAsync<IEnumerable<ComplaintsModel>>().Result;
        return View(listofComplaints);
    }

    


    

}