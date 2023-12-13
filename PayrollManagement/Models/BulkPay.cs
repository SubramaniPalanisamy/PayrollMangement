using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PayrollManagement.Models
{
    public class BulkPay{
        
       
        [Required]
        public String? Department {get; set;}
        [Required]
        public double Amount {get;set;}
    }
    
}