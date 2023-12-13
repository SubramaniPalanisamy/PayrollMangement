using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PayrollManagement.Models
{
    public class Salary{
        
        [Key]
        [Required]
        public int ID{get; set;}
        [Required]
        public int EmployeeID{get; set;}
        [Required]
        public int PaymentID{get; set;}
        [Required]
        public double SalaryPackage{get; set;}
        [Required]
        public double Hometaken{get; set;}
        [Required]
        public double TotalEarnings{get; set;}
        [Required]
        public double CreditAmount{get; set;}
        [Required]
        public double Bonus{get; set;}
        [Required]
        public double Dues{get; set;}
        
        [Required]
        public string? Department{get; set;}
        [Required]
        public double accountNumber{get; set;}
        public string? Status{get;set;}
    }
    
}