using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Employee
{
    public class CreateEmployeeModel : PageModel
    {
        public void OnGet()
        {
         
        }

        public string errMsg = "";
        public string successMsg = "";

        Employee2 employee = new Employee2();
        public void OnPost() {
            SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-C6I5CDE;Initial Catalog=\"practice database\";Integrated Security=True;Encrypt=False;");
            //Data Source=DESKTOP-C6I5CDE;Initial Catalog="practice database";Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
            sqlconn.Open();

            


            
            try
            {
                errMsg = "";
                successMsg = "";

                employee.id = Convert.ToInt32(Request.Form["id"]);
                employee.technology = Request.Form["technology"];
                employee.salary = Convert.ToInt32(Request.Form["salary"]);
                employee.basedLocation = Request.Form["loc"];
                employee.dateOfBirth = Convert.ToDateTime(Request.Form["dob"]);
                employee.name = Request.Form["name"];
                employee.department = Request.Form["department"];


                string query = $"INSERT INTO EMPLOYEE VALUES " +
                $"( '{employee.name}' ,'{employee.dateOfBirth}', '{employee.department}'," +
                $"'{employee.technology}','{employee.basedLocation}',{employee.salary})";

                Console.WriteLine(query);
                SqlCommand cmd = new SqlCommand(query, sqlconn);

                var r = cmd.ExecuteReader();
                successMsg = "Book Added Successfully";
                Console.WriteLine("ADDED!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR!");
                Console.WriteLine(ex.Message); 
                errMsg = ex.Message;
            }

            

            

        }
    }

    public class Employee2
    {
        public int id { get; set; }
        public string name { get; set; }
        public int salary { get; set; } 
        public DateTime dateOfBirth { get; set;}  
        public string department { get; set; }  
        public string technology { get; set; }
        public string basedLocation { get; set;}

    }

    
}
