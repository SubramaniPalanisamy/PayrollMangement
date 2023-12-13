using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PayrollManagement.Models
{
public class Employee
{
    public int EmployeeID{get; set;}

    // [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only letters are allowed.")]
    [StringLength(25,MinimumLength = 2,ErrorMessage="Name should be minimum 2 characters and maximum 25 characters")]
    public string? EmployeeName{get; set;}

    [Range(21,45,ErrorMessage="Age should be minimum 21 and maximum 45")]
    public byte Age{get; set;}
    [Range(300000,450000,ErrorMessage="Salary should be minimum 300000 and maximum 450000")]
    public double Salary {get; set;}
    [Required(ErrorMessage="Department is required")]
    public string? Department{get; set;}
    [RegularExpression(@"^[689][0-9]{9}",ErrorMessage="It must contain 10 numbers in a valid format")]
    public string? PhoneNumber{get;set;}
    [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$",ErrorMessage="Email should be in ****@gmail.com format")]
    public string? MailId{get;set;}
    
    [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = "It must contains one uppercase, lowercase,special character and length must be greater than 8 characters ")]
    public string? Password{get;set;}

    public string Usermode{get;set;}

    public byte[] Image{get;set;}
    


}
}