using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Globalization;

namespace WebApplication1.Pages.Employee
{
    public class UpdateEmployeeModel : PageModel
    {
        public string errMsg = "";
        public string successMsg = "";
        public Employee2 employeeObj = new Employee2();
        public void OnGet()
        {

            try {

                string Eid = Request.Query["id"];
                SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-C6I5CDE;Initial Catalog=\"practice database\";Integrated Security=True;Encrypt=False;");
                //Data Source=DESKTOP-C6I5CDE;Initial Catalog="practice database";Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
                sqlconn.Open();
                SqlCommand cmd = sqlconn.CreateCommand();
           

                cmd.CommandText = $"SELECT * FROM EMPLOYEE WHERE empId = {Eid}";

                SqlDataReader reader = cmd.ExecuteReader(); 
                while(reader.Read())
                {
                    
                    employeeObj.id = (int)reader["empId"];
                    employeeObj.name = (string)reader["empName"];
                    employeeObj.department = (string)reader["department"];
                    employeeObj.technology = (string)reader["technology"];
                    employeeObj.salary = (int)(double)reader["salary"];
                    employeeObj.basedLocation = (string)reader["baseLocation"];
                    employeeObj.dateOfBirth = (DateTime)reader["dateOfBirth"];
                    Console.WriteLine("NEW!");
                }
                Console.WriteLine("EMPLYOEE DATA FETCHED!");
                Console.WriteLine(employeeObj.name);
                Console.WriteLine(employeeObj.dateOfBirth.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public void OnPost()
        {
            SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-C6I5CDE;Initial Catalog=\"practice database\";Integrated Security=True;Encrypt=False;");
            //Data Source=DESKTOP-C6I5CDE;Initial Catalog="practice database";Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
            sqlconn.Open();
            Console.WriteLine("INSIDE POST METHOD!");
            try
            {
                errMsg = "";
                successMsg = "";
                //CultureInfo cultures = new CultureInfo("en-US");
                //Console.WriteLine("ALL FINE TILL HERE!");
                employeeObj.id = Convert.ToInt32(Request.Form["id"]);
                //Console.WriteLine(employeeObj.id);
                employeeObj.technology = Request.Form["technology"];
                //Console.WriteLine(employeeObj.technology);
                employeeObj.salary = Convert.ToInt32(Request.Form["salary"]);
                //Console.WriteLine(employeeObj.salary);
                employeeObj.basedLocation = Request.Form["loc"];
                //Console.WriteLine(employeeObj.basedLocation);
                employeeObj.dateOfBirth = Convert.ToDateTime(Request.Form["dob"]);
                
                //Console.WriteLine(employeeObj.dateOfBirth);
                employeeObj.name = Request.Form["name"];
                Console.WriteLine(employeeObj.name.Length);
                employeeObj.department = Request.Form["department"];
                //Console.WriteLine(employeeObj.department);

                Console.WriteLine("DATA RETRIVED!");

               

                string query = $"UPDATE EMPLOYEE SET empName = '{employeeObj.name}'," +
                    $"dateOfBirth = '{employeeObj.dateOfBirth}'," +
                    $"department = '{employeeObj.department}'," +
                    $"technology = '{employeeObj.technology}'," +
                    $"baseLocation = '{employeeObj.basedLocation}'," +
                    $"salary = {employeeObj.salary} where empId = {employeeObj.id}";



                Console.WriteLine(query);

                SqlCommand cmd = new SqlCommand(query, sqlconn);

                var r = cmd.ExecuteNonQuery();
                successMsg = "Book Added Successfully";
                Console.WriteLine("ADDED!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!");
                Console.WriteLine(ex.Message);
                errMsg = ex.Message;
            }





        }
    }
}
