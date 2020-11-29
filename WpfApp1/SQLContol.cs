using System;
using System.Data.SqlClient;

namespace BookSharing
{
    /// <summary>
    /// Логика взаимодействия для EntryPage.xaml
    /// </summary>
    /// 


    public class SQLContol
    {
        SqlConnection sqlConnection;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Afflicted\Desktop\BookSharing\WpfApp1\Database1.mdf;Integrated Security=True";
        SqlDataReader sqlReader;
        SqlCommand command;



        public bool LoginPasswordCheck(string login, string password)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlReader = null;
            command = new SqlCommand("select * from [User] where Login=@Login and Password=@Password", sqlConnection);
            command.Parameters.AddWithValue("Login", login);
            command.Parameters.AddWithValue("Password", password);
            sqlReader = command.ExecuteReader();
            //sqlReader["Login"] == null
            if (sqlReader.Read())
            {
                sqlConnection.Close();
                return true;
            }
            else
            {
                sqlConnection.Close();
                return false;
            }
        }

        public string RightsCheck(string login) 
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlReader = null;
            command = new SqlCommand("select Rights from [User] where Login=@Login", sqlConnection);
            command.Parameters.AddWithValue("Login", login);
            sqlReader = command.ExecuteReader();
            sqlReader.Read();
            return Convert.ToString(sqlReader["Rights"]);
        }





    }
}
