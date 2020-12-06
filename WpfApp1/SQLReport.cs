using System;
using System.Data.SqlClient;

namespace BookSharing
{
    class SQLReport : SQLContol
    {
        protected void NewBookReport(string user, DateTime time)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlReader = null;
            command = new SqlCommand("select Id from [UserBook] where [UserBook].[User]=@user and [UserBook].[Date]=@date", sqlConnection);
            command.Parameters.AddWithValue("user", user);
            command.Parameters.AddWithValue("date", time);
            sqlReader = command.ExecuteReader();
            sqlReader.Read();
            int id = Convert.ToInt32(sqlReader["id"]);
            sqlConnection.Close();
            sqlConnection.Open();
            command = new SqlCommand("INSERT INTO [Report] ([Report].[User],Book, Description, Date) Values (@User,@Book, @Description, @Date)", sqlConnection);
            command.Parameters.AddWithValue("User", user);
            command.Parameters.AddWithValue("Book", id);
            command.Parameters.AddWithValue("Description", "книга добавлена");
            command.Parameters.AddWithValue("Date", time);
            command.ExecuteNonQuery();
        }

        protected void DeleteBookReport(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM [Report] WHERE [Book]=@Book", sqlConnection);
            command.Parameters.AddWithValue("Book", id);
            command.ExecuteNonQuery();
        }

        protected void NewReport(int book, string user, string description)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            command = new SqlCommand("INSERT INTO [Report] ([Report].[User],Book, Description, Date) Values (@User,@Book, @Description, @Date)", sqlConnection);
            command.Parameters.AddWithValue("User", user);
            command.Parameters.AddWithValue("Book", book);
            command.Parameters.AddWithValue("Description", description);
            command.Parameters.AddWithValue("Date", DateTime.Now);

            command.ExecuteNonQuery();
        }
    }
}
