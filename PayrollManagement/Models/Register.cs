using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PayrollManagement.Models
{
public class Register
{
    public int EmployeeID{get; set;}
    // [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only letters are allowed.")]
    public string? Name{get; set;}

    public string? EmailID{get; set;}
    [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "It must contains one uppercase, lowercase,special character and length must be greater than 8 characters ")]
    public string? Password{get; set;}
    
    public string? Usermode{get;set;}
    


    


}
}