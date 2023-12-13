using System;
using Microsoft.EntityFrameworkCore;
using PayrollManagement.Models;
namespace  PayrollManagement.EfData{
    public class PaymentDbContext :DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options):base(options){

        } 

        public DbSet<Salary>PaymentManagement{get;set;}
        
    }
}