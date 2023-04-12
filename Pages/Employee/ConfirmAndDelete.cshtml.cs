using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplication1.Pages.Employee
{
    public class ConfirmAndDeleteModel : PageModel
    {

        public string errMsg = "";
        public string successMsg = "";
        public Employee2 employeeObj = new Employee2();
        public void OnGet()
        {

            try
            {

                string Eid = Request.Query["id"];
                SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-C6I5CDE;Initial Catalog=\"practice database\";Integrated Security=True;Encrypt=False;");
                //Data Source=DESKTOP-C6I5CDE;Initial Catalog="practice database";Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False
                sqlconn.Open();
                SqlCommand cmd = sqlconn.CreateCommand();


                cmd.CommandText = $"SELECT * FROM EMPLOYEE WHERE empId = {Eid}";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void OnPost() {
            Console.WriteLine("INSIDE POST OF CONFIORM & UPDATE");
            try
            {
                SqlConnection sqlconn = new SqlConnection("Data Source=DESKTOP-C6I5CDE;Initial Catalog=\"practice database\";Integrated Security=True;Encrypt=False;");
                sqlconn.Open();
                SqlCommand cmd = sqlconn.CreateCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@EID", System.Data.SqlDbType.Int).Value = employeeObj.id;
                cmd.CommandText = "deleteEmp";
                int raffected = cmd.ExecuteNonQuery();
                sqlconn.Close();
                Console.WriteLine("DELETED!");
                Console.WriteLine(raffected);
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
