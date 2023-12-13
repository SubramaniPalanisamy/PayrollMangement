using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PayrollManagement.Models;
using Microsoft.AspNetCore.Http;
using System.Threading;

namespace PayrollManagement.Controllers;

public class HomeController : Controller
{
    
    
   
    string? status = "logged";
    string? usermode = "user";
    public HomeController(){
        DatabaseConnection.connectDatabase();
        
        // String username = HttpContext.Session.GetString("username");
    }
    public IActionResult index()
    {
        return View();
    }

    

    public IActionResult login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult login(Authentication user){
       
        int dbstatus = DatabaseConnection.loginUser(user);
        
        if(dbstatus == 1){
            // HttpContext.Session.SetString("emailId",user.EmailID);
            HttpContext.Session.SetString("status",status);
            String? username = DatabaseConnection.getUserName(user);
            HttpContext.Session.SetString("username",username);
            HttpContext.Session.SetString("usermode",user.Usermode);
            HttpContext.Session.SetString("ID",Convert.ToString(user.EmployeeID));
            Console.WriteLine(Convert.ToString(user.EmployeeID));
            if(user.Usermode == "Admin"){
                var cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("LastLoginTime",DateTime.Now.ToString(),cookieOptions);
                Console.WriteLine(Request.Cookies["LastLoginTime"]);
                TempData["Time"]="Last Login:"+Request.Cookies["LastLoginTime"];
            }
            if(user.Usermode == "Admin")
            {
                return RedirectToRoute(new{controller ="Admin",action="index"});
            }
            else{
                return RedirectToRoute(new{controller ="Employee",action = "index"});
            }
            
            //  return View("signUp");
        }else{
            return View("login");
        }
    }
    

    public IActionResult logout(){
        HttpContext.Session.SetString("status","loggedout");
        HttpContext.Session.SetString("usemode","user");
        return RedirectToRoute(new{controller ="Home",action="login"});
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}
