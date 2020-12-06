using System;
using System.Data.SqlClient;

namespace BookSharing
{
    class SQLBook : SQLReport
    {
        public BookForPage GetBookForPage(int id)
        {
            BookForPage Book = new BookForPage();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select * from [UserBook] INNER JOIN [Book] on [Book].ISBN = [UserBook].ISBN INNER JOIN [User] on [UserBook].[User] = [User].Login where [UserBook].[Id] = @id", sqlConnection);
            command.Parameters.AddWithValue("id", id);
            sqlReader = command.ExecuteReader();

            sqlReader.Read();

            Book.Title = Convert.ToString(sqlReader["Name"]);
            Book.Genre = Convert.ToString(sqlReader["Genre"]);
            Book.ImagePath = Convert.ToString(sqlReader["Image"]);
            Book.Description = Convert.ToString(sqlReader["Description"]);
            Book.User = Convert.ToString(sqlReader["User"]);
            Book.Email = Convert.ToString(sqlReader["Email"]);
            Book.Trade = Convert.ToString(sqlReader["Trade"]);
            Book.Authors = GetAuthorBook(Convert.ToString(sqlReader["ISBN"]));



            return Book;
        }

        string GetAuthorBook(string ISBN)
        {
            string Author = "";

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select Author from [BookAuthor] where [Book] = @Book", sqlConnection);
            command.Parameters.AddWithValue("Book", ISBN);
            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                Author += Convert.ToString(sqlReader["Author"]) + " ";
            }


            return Author;
        }

        public string ISBNBook(string ISBN)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            sqlReader = null;
            command = new SqlCommand("select Name from [Book] where ISBN=@ISBN", sqlConnection);
            command.Parameters.AddWithValue("ISBN", ISBN);
            sqlReader = command.ExecuteReader();

            if (sqlReader.Read())
            {
                return "Книга: " + Convert.ToString(sqlReader["Name"]);
            }
            else
            {
                sqlConnection.Close();
                return "Книга не найдена";
            }
        }

        public void AddUserBook(string user, string ISBN, string description, string trade, string ImagePath)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO [UserBook] ([UserBook].[User], Description, Trade, ISBN, Image, Date) Values (@User, @Description, @Trade, @ISBN, @Image, @Date)", sqlConnection);
            command.Parameters.AddWithValue("User", user);
            command.Parameters.AddWithValue("Description", description);
            command.Parameters.AddWithValue("Trade", trade);
            command.Parameters.AddWithValue("ISBN", ISBN);
            if (ImagePath == "")
            {
                ImagePath = @"C:\Users\Afflicted\Desktop\BookSharing\WpfApp1\logo.png";
            }
            command.Parameters.AddWithValue("Image", ImagePath);
            DateTime time = DateTime.Now;
            command.Parameters.AddWithValue("Date", time);
            command.ExecuteNonQuery();

            this.NewBookReport(user, time);
        }

        public void DeleteUserBook(int id)
        {
            DeleteBookReport(id);
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [UserBook] WHERE [Id]=@id", sqlConnection);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
        }

        public void BookUpdate(string Image, string description, string trade, int id, string user)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("UPDATE [UserBook] SET Description=@Description, Trade=@Trade, Image=@Image WHERE [UserBook].Id=@Id", sqlConnection);
            command.Parameters.AddWithValue("Id", id);
            command.Parameters.AddWithValue("Description", description);
            command.Parameters.AddWithValue("Trade", trade);
            command.Parameters.AddWithValue("Image", Image);
            command.ExecuteNonQuery();

            if (this.RightsCheck(user) == "admin")
            {
                this.DeleteBookReport(id);
            }
            else
            {
                this.NewReport(id, user, "книга изменина");
            }

        }
    }
}
