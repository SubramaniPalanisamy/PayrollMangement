using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Models;
using System.Data;
using  PayrollManagement.EfData;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using iTextSharp ;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Microsoft.EntityFrameworkCore.SqlServer;

namespace PayrollManagement.Controllers;

public class SalaryController : Controller
{
    private readonly PaymentDbContext _paymentDb;
    public SalaryController(PaymentDbContext database)
    {
        DatabaseConnection.connectDatabase();
        _paymentDb = database;
    }
    public IActionResult manageSalary(int EmpID){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        ViewBag.employeeID = Convert.ToString(EmpID);
        HttpContext.Session.SetString("EmployeeID",Convert.ToString(EmpID));
        ViewBag.department = DatabaseConnection.getDepartment(EmpID);
        var status=_paymentDb.PaymentManagement.Where(payment=>payment.EmployeeID == EmpID ).ToList();
        
       foreach(var item in status)
       {
           if(item.Status=="permanent")
           {
               return View("payBonusAndDues");
           }
       }
      
        return View("manageSalary");
    }

    [HttpPost]
    public IActionResult manageSalary(Salary payment){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        payment.Status= "temporary";
        _paymentDb.PaymentManagement.Add(payment);
        _paymentDb.SaveChanges();

        payment.Status = "permanent";
        _paymentDb.PaymentManagement.Update(payment);
        _paymentDb.SaveChanges();
       
        IEnumerable<Salary> employeeList = _paymentDb.PaymentManagement;
        
     
        Console.WriteLine("inserted successfully");
        return View("paymentData",employeeList);
  
    }
    //group payment depends on department
    public IActionResult payInBulk(){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        
        return View();
    }
    [HttpPost]
    public IActionResult payInBulk(BulkPay bulkPay){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        // String result = DatabaseConnection.payBulk(bulkPay);
        
        List<Salary> employee=_paymentDb.PaymentManagement.Where(department=>department.Department==bulkPay.Department).ToList();
         foreach(var item in employee)
         {
             
             item.CreditAmount=bulkPay.Amount;
             item.TotalEarnings+=bulkPay.Amount;
             _paymentDb.PaymentManagement.Update(item);
             _paymentDb.SaveChanges();
         }
        IEnumerable<Salary> employeeList = _paymentDb.PaymentManagement;
        return View("paymentData",employeeList);

    }
    [HttpGet]
    public IActionResult payBonusAndDues(){
        ViewBag.EmployeeID = HttpContext.Session.GetString("EmployeeID");
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        return View();
    }
    [HttpPost]
    public IActionResult payBonusAndDues(Salary salary){
        Console.WriteLine(salary.EmployeeID);
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");

        List<Salary> employee = _paymentDb.PaymentManagement.Where(employee => employee.EmployeeID==salary.EmployeeID).ToList();
        Console.WriteLine(employee.Count);
        foreach(var item in employee){
            item.Bonus += salary.Bonus;
            item.Dues += salary.Dues;
            item.TotalEarnings += salary.Bonus;
            item.TotalEarnings -= salary.Dues;
            _paymentDb.PaymentManagement.Update(item);
            _paymentDb.SaveChanges();
        }
        
         IEnumerable<Salary> employeeList = _paymentDb.PaymentManagement;
        
     
        
        return View("paymentData",employeeList);

    }
    public IActionResult paymentData(){
        ViewBag.status = HttpContext.Session.GetString("status");
        ViewBag.username = HttpContext.Session.GetString("username");
        ViewBag.usermode = HttpContext.Session.GetString("usermode");
        
        IEnumerable<Salary> employeeList = _paymentDb.PaymentManagement;
        Console.WriteLine("Payment Data feteched");
        return View(employeeList);
     
    }
      
    


}