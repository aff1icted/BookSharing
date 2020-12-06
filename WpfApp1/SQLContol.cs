using System;
using System.Data.SqlClient;

namespace BookSharing
{
    public class SQLContol
    {
        protected SqlConnection sqlConnection;
        protected string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Afflicted\Desktop\BookSharing\WpfApp1\Database1.mdf;Integrated Security=True";
        protected SqlDataReader sqlReader;
        protected SqlCommand command;       

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
