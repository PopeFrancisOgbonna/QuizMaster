using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuizMaster
{

    class Database_Connectivity
    {
        public static string connectionString = @"Data Source=.;Initial Catalog=QuizMaster;Integrated Security=true;";

        public SqlConnection connect = new SqlConnection(connectionString);

        public string message;

        public void connecting()
        {
            try
            {
                connect.Open();
                if (connect.State == ConnectionState.Open)
                {
                    message = "Database Connected Successfully";
                }
                connect.Close();
            }
            catch (Exception e)
            {
                message = e.ToString();

            }
        }

        public DataTable tbl = new DataTable();
        public void viewQuestions()//view all question 
        {
            string query = "select Question.Quest_Id as 'Question Number',Subject.Subject_Id as 'Subject',Questions as 'Question',Class.Class_Id as'Class' from Question, Class, Subject";
            SqlCommand command = new SqlCommand(query, connect);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            adapt.Fill(tbl);
        }
        public void viewStudent(string klass) // view student from a specific class
        {
            int id = 0;
            switch (klass)
            {
                case "primary 1":
                    id = 1;
                    break;
                case "primary 2":
                    id = 2;
                    break;
                case "primary 3":
                    id = 3;
                    break;
                case "primary 4":
                    id = 4;
                    break;
                case "primary 5":
                    id = 5;
                    break;
                case "primary 6":
                    id = 6;
                    break;
            }
            string query = "select concat(FName,' ',LName) as 'Name',ClassName as'Class', GroupName as 'Group',Instructor as 'Class Teacher' from Student s,Class c,Groups g where s.Class_Id=@param and s.Class_Id=c.Class_Id  and s.Group_Id=g.Group_Id;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@param", id);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            adapt.Fill(tbl);
        }
        public void viewParticipants()//shows all participants 
        {
            string query = "select concat(Student.FName, ' ', Student.LName) as 'Name', Class.ClassName as 'Class',Groups.GroupName as 'Group',Groups.Instructor as 'Class Teacher' from Student, Class, Groups where Class.Class_Id = Student.Class_Id and Student.Group_Id = Groups.Group_Id;";
            SqlCommand command = new SqlCommand(query, connect);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            adapt.Fill(tbl);
        }
    }
}
