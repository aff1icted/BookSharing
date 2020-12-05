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
            Book.Trade = Convert.ToString(sqlReader["Trade"]);
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


        public ObservableCollection<Book> GetUserBookList(string user)
        {
            ObservableCollection<Book> Books = new ObservableCollection<Book>();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select * from [UserBook] INNER JOIN [Book] on [Book].ISBN = [UserBook].ISBN where [UserBook].[User] = @User order by Date", sqlConnection);
            command.Parameters.AddWithValue("User", user);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Books.Add(new Book { id = Convert.ToInt32(sqlReader["Id"]), ImagePath = Convert.ToString(sqlReader["Image"]), Title = Convert.ToString(sqlReader["Name"]), Description = Convert.ToString(sqlReader["Description"]) });
            }

            return Books;
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
                return "Книга: "+ Convert.ToString(sqlReader["Name"]);
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
            if (ImagePath =="")
            {
                ImagePath = @"C:\Users\Afflicted\Desktop\BookSharing\WpfApp1\logo.png";
            }
            command.Parameters.AddWithValue("Image", ImagePath);
            DateTime time = DateTime.Now;
            command.Parameters.AddWithValue("Date", time);
            command.ExecuteNonQuery();

            this.NewBookReport(user, time);
        }

        private void NewBookReport(string user, DateTime time) 
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
            command = new SqlCommand("INSERT INTO [Report] ([Report].[User],Book, Description, Date, Status) Values (@User,@Book, @Description, @Date)", sqlConnection);
            command.Parameters.AddWithValue("User", user);
            command.Parameters.AddWithValue("Book", id);
            command.Parameters.AddWithValue("Description", "книга добавлена");
            command.Parameters.AddWithValue("Date", time);           
            command.ExecuteNonQuery();
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

        private void DeleteBookReport(int id)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM [Report] WHERE [Book]=@Book", sqlConnection);
            command.Parameters.AddWithValue("Book", id);           
            command.ExecuteNonQuery();
        }


        public ObservableCollection<Book> GetReportList()
        {
            ObservableCollection<Book> Books = new ObservableCollection<Book>();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select [UserBook].Id as id, Image, Name, [Report].Description as Description  from [UserBook] INNER JOIN [Book] on [Book].ISBN = [UserBook].ISBN INNER JOIN [Report] on [Report].[Book] = [UserBook].[Id] order by [Report].Date", sqlConnection);
            
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Books.Add(new Book { id = Convert.ToInt32(sqlReader["id"]), ImagePath = Convert.ToString(sqlReader["Image"]), Title = Convert.ToString(sqlReader["Name"]), Description = Convert.ToString(sqlReader["Description"]) });
            }

            return Books;
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

            if (this.RightsCheck(user)=="admin")
            {
                this.DeleteBookReport(id);
            }
            else
            {
                this.NewReport(id, user, "книга изменина");
            }

        }

        private void NewReport(int book, string user, string description) 
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();            
            command = new SqlCommand("INSERT INTO [Report] ([Report].[User],Book, Description, Date, Status) Values (@User,@Book, @Description, @Date)", sqlConnection);
            command.Parameters.AddWithValue("User", user);
            command.Parameters.AddWithValue("Book", book);
            command.Parameters.AddWithValue("Description", description);
            command.Parameters.AddWithValue("Date", DateTime.Now);
            
            command.ExecuteNonQuery();
        }

        public ObservableCollection<string> GetAuthorList()
        {
            ObservableCollection<string> Authors = new ObservableCollection<string>();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select * from [Author]", sqlConnection);

            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Authors.Add(Convert.ToString(sqlReader["Name"]));
            }

            return Authors;
        }

        public ObservableCollection<string> GetGenreList()
        {
            ObservableCollection<string> Genre = new ObservableCollection<string>();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select * from [Genre]", sqlConnection);

            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Genre.Add(Convert.ToString(sqlReader["Title"]));
            }

            return Genre;
        }

    }
}
