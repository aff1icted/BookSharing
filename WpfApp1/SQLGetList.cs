using System;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace BookSharing
{
    class SQLGetList: SQLContol
    {
        public ObservableCollection<Book> GetBookList()
        {
            ObservableCollection<Book> Books = new ObservableCollection<Book>();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select * from [UserBook] INNER JOIN [Book] on [Book].ISBN = [UserBook].ISBN order by Date DESC", sqlConnection);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Books.Add(new Book { id = Convert.ToInt32(sqlReader["Id"]), ImagePath = Convert.ToString(sqlReader["Image"]), Title = Convert.ToString(sqlReader["Name"]), Description = Convert.ToString(sqlReader["Description"]) });
            }

            return Books;
        }

        public ObservableCollection<Book> GetUserBookList(string user)
        {
            ObservableCollection<Book> Books = new ObservableCollection<Book>();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("select * from [UserBook] INNER JOIN [Book] on [Book].ISBN = [UserBook].ISBN where [UserBook].[User] = @User order by Date DESC", sqlConnection);
            command.Parameters.AddWithValue("User", user);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Books.Add(new Book { id = Convert.ToInt32(sqlReader["Id"]), ImagePath = Convert.ToString(sqlReader["Image"]), Title = Convert.ToString(sqlReader["Name"]), Description = Convert.ToString(sqlReader["Description"]) });
            }

            return Books;
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

        public ObservableCollection<Book> SearchBook(string ISBN, string title, string genre, string author)
        {
            ObservableCollection<Book> Books = new ObservableCollection<Book>();

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string request = "SELECT * FROM [UserBook] INNER JOIN [Book] on [Book].ISBN = [UserBook].ISBN INNER JOIN [BookAuthor] on [Book].ISBN = [BookAuthor].[Book]";
            if (string.IsNullOrEmpty(ISBN))
            {

                if (!string.IsNullOrEmpty(title))
                {
                    request += "WHERE [Book].[Name]=@Title";
                }
                else
                {
                    if (!string.IsNullOrEmpty(genre))
                    {
                        request += "WHERE Genre=@Genre";
                        if (!string.IsNullOrEmpty(author))
                        {
                            request += " AND Author=@Author";
                        }
                    }
                    else
                    {
                        request += "WHERE Author=@Author";
                    }
                }
            }
            else
            {
                request += "WHERE [UserBook].ISBN=@ISBN";
            }


            SqlCommand command = new SqlCommand(request, sqlConnection);
            command.Parameters.AddWithValue("ISBN", ISBN);
            command.Parameters.AddWithValue("Title", title);
            command.Parameters.AddWithValue("Genre", genre);
            command.Parameters.AddWithValue("Author", author);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                Books.Add(new Book { id = Convert.ToInt32(sqlReader["Id"]), ImagePath = Convert.ToString(sqlReader["Image"]), Title = Convert.ToString(sqlReader["Name"]), Description = Convert.ToString(sqlReader["Description"]) });
            }

            return Books;
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
