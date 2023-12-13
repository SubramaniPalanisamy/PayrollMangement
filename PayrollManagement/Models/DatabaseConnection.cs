
using PayrollManagement.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace PayrollManagement.Models{
    public static class DatabaseConnection
    {
        static SqlConnection connection;
          
        static SqlCommand command;

        static String? queryCommand = "";
        // static int EmployeeID;

        static DatabaseConnection()
        {
            connection = new SqlConnection();
            command = new SqlCommand();
            command.Connection = connection;
        }

        public static void connectDatabase()
        {

            connection.ConnectionString = getConnectionString();

            try{
                connection.Open();
                Console.WriteLine("Connected to Database");
                connection.Close();
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public static String getConnectionString()
        {
             var build = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = build.Build();

            String? connectionString = Convert.ToString(configuration.GetConnectionString("connection1"));
            if(connectionString != null)
                return connectionString;
            return "";
        }

        // string ConnectionString = getConnectionString();
        public static string enrollEmployee(Employee employee)
        {
            String? EmployeeID = Convert.ToString(employee.EmployeeID);
            String? EmployeeName = Convert.ToString(employee.EmployeeName);
            int Age = employee.Age;
            int Salary = Convert.ToInt32(employee.Salary);
            String? Department = Convert.ToString(employee.Department);
            
            
            try{  
                using (SqlConnection sqlConnection = new SqlConnection(connection.ConnectionString))   
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UserDetailsStoredProcedure",sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure; 
                        sqlCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        sqlCommand.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                        sqlCommand.Parameters.AddWithValue("@Age", Age);
                        sqlCommand.Parameters.AddWithValue("@Salary", Salary);
                        sqlCommand.Parameters.AddWithValue("@Department", Department);
                        sqlCommand.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                        sqlCommand.Parameters.AddWithValue("@MailId", employee.MailId);
                        sqlCommand.Parameters.AddWithValue("@Password", employee.Password);
                        sqlCommand.Parameters.AddWithValue("@Usermode", employee.Usermode);
                        sqlCommand.Parameters.AddWithValue("@Image", employee.Image);
                        sqlCommand.Parameters.AddWithValue("@Action", "INSERT");
                        sqlConnection.Open();                     
                        sqlCommand.ExecuteNonQuery();
                        
                    }

                }   
                    Console.WriteLine("\nInserted Successfully..");
                    return "Registered" ;
                
                
            }catch(SqlException error){
                return error.Message;
                
            }
            catch(Exception error)
              {
                  Console.WriteLine(error.Message);
                  return error.Message;
              }
            
                        
        }

       
        public static DataSet viewDetails(){
            
            
            DataSet userDetails = new DataSet();
            try{
                using (SqlConnection sqlConnection = new SqlConnection(connection.ConnectionString))   
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UserDetailsStoredProcedure",sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Action", "SELECT");
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.Fill(userDetails);   
                    } 
                }         

            }catch(SqlException error){
                Console.WriteLine(error.Message);

            }
            catch(Exception error)
              {
                  Console.WriteLine(error.Message);
              }
            
            return userDetails;
            
            
        }
        public static DataSet getAdmin(){
            
            DataSet userDetails = new DataSet();
            try{
                using (SqlConnection sqlConnection = new SqlConnection(connection.ConnectionString))   
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UserDetailsStoredProcedure",sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Action", "getAdmin");
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.Fill(userDetails);   
                    }  
                }        

                Console.WriteLine("fetched");
                             

            }catch(SqlException error){
                Console.WriteLine(error.Message);

            }
            catch(Exception error)
              {
                  Console.WriteLine(error.Message);
              }
            
            
            return userDetails;
        }
        public static DataSet getEmployees(){
             DataSet userDetails = new DataSet();
            try{
                 using (SqlConnection sqlConnection = new SqlConnection(connection.ConnectionString))   
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UserDetailsStoredProcedure",sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@Action", "getEmployees");
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.Fill(userDetails);   
                    }   
                }          

            }catch(SqlException error){
                Console.WriteLine(error.Message);

            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
            
            
            return userDetails;
            
            
        }

        public static DataSet getEmployeeDetails(string? EmployeeId){
            
            DataSet userDetails = new DataSet();
            try{
                using (SqlConnection sqlConnection = new SqlConnection(connection.ConnectionString))   
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UserDetailsStoredProcedure",sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        
                        sqlCommand.Parameters.AddWithValue("@Action", "getEmployeeDetails");
                        SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                        adapter.Fill(userDetails);   
                    }  
                }           

            }catch(SqlException error){
                Console.WriteLine(error.Message);

            }
            catch(Exception error)
              {
                  Console.WriteLine(error.Message);
              }
            
            return userDetails;
            
            
        }

        public static string getDepartment(int ID)
        {
            DataSet userdata = viewDetails();
            foreach(DataRow row in userdata.Tables[0].Rows){
                if(Convert.ToString(row[0]) == Convert.ToString(ID))
                {
                    Console.WriteLine("Department fetched");
                    Console.WriteLine(Convert.ToString(row[4]));
                    return Convert.ToString(row[4]);
                    
                }
            }
            return "";
        }

        public static string getPhoneNumber(int ID)
        {
            DataSet userdata = viewDetails();
            foreach(DataRow row in userdata.Tables[0].Rows){
                if(Convert.ToString(row[0]) == Convert.ToString(ID))
                {
                    Console.WriteLine("phone number fetched");
                    Console.WriteLine(Convert.ToString(row[5]));
                    return Convert.ToString(row[5]);
                    
                }
            }
            return "";
        }
        public static string getUserName(int ID)
        {
            DataSet userdata = viewDetails();
            foreach(DataRow row in userdata.Tables[0].Rows){
                if(Convert.ToString(row[0]) == Convert.ToString(ID))
                {
                    Console.WriteLine("phone number fetched");
                    Console.WriteLine(Convert.ToString(row[0]));
                    return Convert.ToString(row[0]);
                    
                }
            }
            return "";
        }

          public static string getPassword(int ID)
        {
            DataSet userdata = viewDetails();
            foreach(DataRow row in userdata.Tables[0].Rows){
                if(Convert.ToString(row[0]) == Convert.ToString(ID))
                {
                    Console.WriteLine("phone number fetched");
                    Console.WriteLine(Convert.ToString(row[7]));
                    return Convert.ToString(row[7]);
                    
                }
            }
            return "";
        }

         public static string getRole(int ID)
        {
            DataSet userdata = viewDetails();
            foreach(DataRow row in userdata.Tables[0].Rows){
                if(Convert.ToString(row[0]) == Convert.ToString(ID))
                {
                    Console.WriteLine("phone number fetched");
                    Console.WriteLine(Convert.ToString(row[8]));
                    return Convert.ToString(row[8]);
                    
                }
            }
            return "";
        }

         public static string isExist(int ID)
        {
            DataSet userdata = viewDetails();
            foreach(DataRow row in userdata.Tables[0].Rows){
                if(Convert.ToString(row[0]) == Convert.ToString(ID))
                {
                    Console.WriteLine(Convert.ToString(row[0]));
                    Console.WriteLine(Convert.ToString(ID));
                    Console.WriteLine("not exist");
                    return "Exist";
                    
                }
            }
            return "not Exist";
        }

        public static int loginUser(Authentication user){
            // String? EmailID = Convert.ToString(user.EmailID);
            String? Password = Convert.ToString(user.Password);
            String? EmployeeID = Convert.ToString(user.EmployeeID);

            // String? Usermode = Convert.ToString(user.Usermode);
            DataSet userdata = viewDetails();
            foreach(DataRow row in userdata.Tables[0].Rows){
                if(Convert.ToString(row[0]) == EmployeeID && Convert.ToString(row[7]) == Password && Convert.ToString(row[8]) == Convert.ToString(user.Usermode)){
                    Console.WriteLine(" log in success");
                    return 1;
                }
            }
            return 0;

         } 

        public static string getUserName(Authentication user){
            String? EmployeeID = Convert.ToString(user.EmployeeID);
            DataSet userdata = viewDetails();
            foreach(DataRow row in userdata.Tables[0].Rows){
                if(Convert.ToString(row[0]) == EmployeeID){
                    Console.WriteLine("name fetched");
                    return Convert.ToString(row[1]);
                    
                }
            }
            return "not found";

        }

        public static String deleteData(string? EmployeeId){
            Console.WriteLine(EmployeeId);
            
            try{
                using (SqlConnection sqlConnection = new SqlConnection(connection.ConnectionString))   
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UserDetailsStoredProcedure",sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure; 
                        sqlCommand.Parameters.AddWithValue("@EmployeeID", EmployeeId);
                        
                        sqlCommand.Parameters.AddWithValue("@Action", "DELETE");
                        sqlConnection.Open();                     
                        sqlCommand.ExecuteNonQuery();
                        
                    }
                    return "Deleted" ;
                }
            }
            catch(SqlException error){
                return error.Message;
                
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
                return error.Message;
            }
            
        }

        

        

         public static string updateData(Employee employee)
         {
             String? EmployeeID = Convert.ToString(employee.EmployeeID);
             String? EmployeeName = Convert.ToString(employee.EmployeeName);
             int Age = Convert.ToInt32(employee.Age);
             int Salary = Convert.ToInt32(employee.Salary);
             String? Department = Convert.ToString(employee.Department);
             String? PhoneNumber = Convert.ToString(employee.PhoneNumber);
             String? MailId = Convert.ToString(employee.MailId);
             String? Password = Convert.ToString(employee.Password);
             
             try{
                using (SqlConnection sqlConnection = new SqlConnection(connection.ConnectionString))   
                {
                   using (SqlCommand sqlCommand = new SqlCommand("UserDetailsStoredProcedure",sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure; 
                        sqlCommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        sqlCommand.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                        sqlCommand.Parameters.AddWithValue("@Age", Age);
                        sqlCommand.Parameters.AddWithValue("@Salary", Salary);
                        sqlCommand.Parameters.AddWithValue("@Department", Department);
                        sqlCommand.Parameters.AddWithValue("@PhoneNumber",PhoneNumber);
                        sqlCommand.Parameters.AddWithValue("@MailId", MailId);
                        sqlCommand.Parameters.AddWithValue("@Password", Password);
                        sqlCommand.Parameters.AddWithValue("@Action", "UPDATE");
                        sqlConnection.Open();                    
                        sqlCommand.ExecuteNonQuery();
                        
                    }
                }
                
                    Console.WriteLine("\nUpdated Successfully..");
                    return "Updated";

           
                
             }catch(SqlException error){
                Console.WriteLine(error.Message);
                return error.Message;
             }
             catch(Exception error)
              {
                  Console.WriteLine(error.Message);
                  return error.Message;
              }
             
                
         }
        
        
        

        public static bool isExistEmployee(int employeeID){
            string?EmployeeID = Convert.ToString(employeeID);
            queryCommand = $"select EmployeeID from UserRecord where EmployeeID = '{EmployeeID}'";
            try{
                using(connection){
                    connection.Open();
                    command.CommandText = queryCommand;
                    SqlDataReader existUser = command.ExecuteReader();
                   
                    //connection.Close();
                    if(existUser.HasRows){
                        return false;
                    }
                     
                    
                }
                
            }catch(SqlException error){
                Console.WriteLine(error.Message);
            }
            catch(Exception error)
              {
                  Console.WriteLine(error.Message);
              }
            return true;           

         }
        
    }

}


