using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication1.Pages.Employee
{
    public class IndexModel : PageModel
    {
        public List<employee> employeeList = new List<employee>();
        public void OnGet()
        {

            //List<employee> employeeList = new List<employee>();

            try
            {
                SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-C6I5CDE;Initial Catalog=\"practice database\";Integrated Security=True;Encrypt=False;");
                //Data Source=DESKTOP-C6I5CDE;Initial Catalog="practice database";Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
                sqlconn.Open();
                string query = "SELECT empId, empName, department, technology, salary FROM  EMPLOYEE";
                SqlCommand cmd = new SqlCommand(query, sqlconn);

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employee employeeObj = new employee();
                    employeeObj.Id = (int)reader["empId"];
                    employeeObj.Name = (string)reader["empName"];
                    employeeObj.dept = (string)reader["department"];
                    employeeObj.technology = (string)reader["technology"];
                    employeeObj.salary = (double)reader["salary"];

                    employeeList.Add(employeeObj);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string dept { get; set; }
        //public DateTime EDOB { get; set; }
        public string technology { get; set; }
        public double salary {get; set; } 
    }
}
