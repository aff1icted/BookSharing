using System;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace BookSharing
{

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

        public ObservableCollection<Book> GetBookList()
        {
            ObservableCollection<Book> Books = new ObservableCollection<Book>();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select * from [UserBook] INNER JOIN [Book] on [Book].ISBN = [UserBook].ISBN order by Date", sqlConnection);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Books.Add(new Book {id= Convert.ToInt32(sqlReader["Id"]), ImagePath = Convert.ToString(sqlReader["Image"]), Title = Convert.ToString(sqlReader["Name"]), Description= Convert.ToString(sqlReader["Description"]) });                
            }

            return Books;
        }

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
            Book.Authors = GetAuthorBook(Convert.ToString(sqlReader["ISBN"]));


            return Book;
        }

        string GetAuthorBook(string ISBN) 
        {
            string Author="";

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

    }
}
