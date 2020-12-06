using System;
using System.Data.SqlClient;

namespace BookSharing
{
    public class SQLRegestration : SQLContol
    {
        public bool UniqLogin(string login)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlReader = null;
            command = new SqlCommand("select Login from [User] where Login=@Login", sqlConnection);
            command.Parameters.AddWithValue("Login", login);
            sqlReader = command.ExecuteReader();

            if (sqlReader.Read())
            {
                sqlConnection.Close();
                return false;
            }
            else
            {
                sqlConnection.Close();
                return true;
            }
        }

        public bool UniqEmail(string email)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlReader = null;
            command = new SqlCommand("select Email from [User] where Email=@Email", sqlConnection);
            command.Parameters.AddWithValue("Email", email);
            sqlReader = command.ExecuteReader();

            if (sqlReader.Read())
            {
                sqlConnection.Close();
                return false;
            }
            else
            {
                sqlConnection.Close();
                return true;
            }
        }

        public void Registration(string login, string password, string email)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO [User] (Login, Password, Email, Rights) Values (@Login, @Password, @Email, @Rights)", sqlConnection);
            command.Parameters.AddWithValue("Login", login);
            command.Parameters.AddWithValue("Password", password);
            command.Parameters.AddWithValue("Email", email);
            command.Parameters.AddWithValue("Rights", "user");

            command.ExecuteNonQuery();
        }
    }
}
