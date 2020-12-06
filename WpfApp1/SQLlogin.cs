using System;
using System.Data.SqlClient;

namespace BookSharing
{
    class SQLlogin : SQLContol
    {
        public bool LoginPasswordCheck(string login, string password)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlReader = null;
            command = new SqlCommand("select * from [User] where Login=@Login and Password=@Password", sqlConnection);
            command.Parameters.AddWithValue("Login", login);
            command.Parameters.AddWithValue("Password", password);
            sqlReader = command.ExecuteReader();
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
    }
}
