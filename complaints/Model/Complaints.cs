using System.ComponentModel.DataAnnotations;
using complaintsWebApi.EFData;
using System.ComponentModel.DataAnnotations.Schema;
namespace complaintsWebApi.Models{
    public class Complaints{
        private int Id;
        private string? EmployeeID;
        private string? Complaint;
        private string? Status;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id{
            get{return Id;}
            set{Id=value;}
        }
        [Required]
        public string? employeeID{
            get{return EmployeeID;}
            set{EmployeeID=value;}
        }
       
        [Required]
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
