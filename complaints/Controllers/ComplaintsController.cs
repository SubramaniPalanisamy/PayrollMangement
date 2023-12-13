using Microsoft.AspNetCore.Mvc;
using complaintsWebApi.Models;
using complaintsWebApi.EFData;

namespace complaintsWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController: ControllerBase
{
    private ComplaintsDbContext _complaintDb;
    public  ComplaintController(ComplaintsDbContext database)
    {
        _complaintDb=database;
    }
   [HttpGet]
   public async Task<IEnumerable<Complaints>> getComplaint()
   {
    var listOfComplaints =_complaintDb.EmployeeComplaint.ToList();
    return listOfComplaints;
    }
    [HttpPost]
    public async Task<bool> postComplaint(Complaints complaints)
    {
        _complaintDb.EmployeeComplaint.Add(complaints);
        Console.WriteLine(complaints.employeeID+"***********************");
        _complaintDb.SaveChanges();
         return true;
    }
    

    

}