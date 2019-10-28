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

        public DataTable tblviewQuest = new DataTable();
        public DataTable tblclassquest = new DataTable();
        public DataTable tblviewStudent = new DataTable();
            public DataTable tblParticipant = new DataTable();
        public void viewQuestions()//view all question 
        {
            string query = "select q.Quest_Id as 'Question Number',q.Questions as 'Question',s.Name as 'Subject',c.ClassName as'Class' from Question q, Class c, Subject s where q.Class_Id = c.Class_Id and q.Subject_Id = s.Subject_Id; ";
            SqlCommand command = new SqlCommand(query, connect);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            adapt.Fill(tblviewQuest);
        }
        public void viewClassquestions(string klass,string subject)
        {
            string query = "select q.Quest_Id as 'Question Number',q.Questions as 'Question',s.Name as 'Subject',c.ClassName as'Class' from Question q, Class c, Subject s where q.Class_Id = c.Class_Id and q.Subject_Id = s.Subject_Id and c.ClassName=@param and s.Name=@param1; ";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@param", klass);
            command.Parameters.AddWithValue("@param1", subject);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            adapt.Fill(tblclassquest);
        }
        public void viewStudent(string klass) // view student from a specific class
        {
            string query = "select concat(FName,' ',LName) as 'Name',ClassName as'Class', GroupName as 'Group',Instructor as 'Class Teacher' from Student s,Class c,Groups g where c.ClassName=@param and s.Class_Id=c.Class_Id  and s.Group_Id=g.Group_Id;";
            SqlCommand command = new SqlCommand(query, connect);
            command.Parameters.AddWithValue("@param",klass);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            adapt.Fill(tblviewStudent);
        }
        public void viewParticipants()//shows all participants 
        {
            string query = "select concat(Student.FName, ' ', Student.LName) as 'Name', Class.ClassName as 'Class',Groups.GroupName as 'Group',Groups.Instructor as 'Class Teacher' from Student, Class, Groups where Class.Class_Id = Student.Class_Id and Student.Group_Id = Groups.Group_Id;";
            SqlCommand command = new SqlCommand(query, connect);
            SqlDataAdapter adapt = new SqlDataAdapter(command);
            adapt.Fill(tblParticipant);
        }
    }
}
