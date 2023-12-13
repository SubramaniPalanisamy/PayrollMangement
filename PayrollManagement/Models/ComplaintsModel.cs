using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
namespace PayrollManagement.Models{
    public class ComplaintsModel{
        private int Id;
        private string? EmployeeID;
        private string? Complaint;
        private string? Status;
 
        public int id{
            get{return Id;}
            set{Id=value;}
        }
    
        public string? employeeID{
            get{return EmployeeID;}
            set{EmployeeID=value;}
        }
       
       
        public string? complaint{
            get{return Complaint;}
            set{Complaint=value;}
        }

       
        public string? status{
            get{return Status;}
            set{Status="waiting";}
        }
        
    }
}
