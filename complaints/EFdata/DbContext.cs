using System;
using Microsoft.EntityFrameworkCore;
using complaintsWebApi.Models;

namespace  complaintsWebApi.EFData{
    public class ComplaintsDbContext :DbContext
    {
        public ComplaintsDbContext(DbContextOptions<ComplaintsDbContext> options):base(options){

        } 

        public DbSet<Complaints>EmployeeComplaint{get;set;}
        
    }
}