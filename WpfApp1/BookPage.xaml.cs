using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace BookSharing
{
    /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        public BookPage(int id)
        {
            InitializeComponent();           

            SQLBook SQLBook = new SQLBook();
            BookForPage bookForPage= SQLBook.GetBookForPage(id);
            TitleTextBox.Text = bookForPage.Title;
            BookImage.Source = new BitmapImage(new Uri(bookForPage.ImagePath));
            AuthorBlock.Text= bookForPage.Authors;
            GenreLable.Content+= bookForPage.Genre;

            TredeBlock.Text = bookForPage.Trade;
            TredeBox.Text= bookForPage.Trade;

            DescriptionBlock.Text = bookForPage.Description;
            DescriptionBox.Text = bookForPage.Description;
            EmailLable.Content = bookForPage.Email;

        }

        public BookPage(int id, string user)
        {
            InitializeComponent();

            SQLBook SQLBook = new SQLBook();
            BookForPage bookForPage = SQLBook.GetBookForPage(id);
            TitleTextBox.Text = bookForPage.Title;
            BookImage.Source = new BitmapImage(new Uri(bookForPage.ImagePath));
            AuthorBlock.Text = bookForPage.Authors;
            GenreLable.Content += bookForPage.Genre;
            TredeBlock.Text = bookForPage.Trade;
            TredeBox.Text = bookForPage.Trade;
            DescriptionBlock.Text = bookForPage.Description;
            DescriptionBox.Text = bookForPage.Description;
            EmailLable.Content = bookForPage.Email;

            UserLable.Content = user;
            IdLable.Content = id;
            TredeBox.Visibility = Visibility.Visible;
            DescriptionBox.Visibility = Visibility.Visible;
            ButtonStack.Visibility = Visibility.Visible;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            SQLBook SQLBook = new SQLBook();
            SQLBook.DeleteUserBook(Convert.ToInt32(IdLable.Content));
            if (SQLBook.RightsCheck(Convert.ToString(UserLable.Content)) == "admin")
            {
                NavigationService.Navigate(new ReportPage(Convert.ToString(UserLable.Content)));
            }
            else
            {
                NavigationService.Navigate(new MyBookList(Convert.ToString(UserLable.Content)));
            }
        }

        private void BookImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(IdLable.Content is null))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    BookImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
        }

        private void Savebutton_Click(object sender, RoutedEventArgs e)
        {
            SQLBook SQLBook = new SQLBook();
            if (!string.IsNullOrEmpty(DescriptionBox.Text) && !string.IsNullOrEmpty(TredeBox.Text))
            {
                SQLBook.BookUpdate(Convert.ToString(BookImage.Source), Convert.ToString(DescriptionBox.Text), Convert.ToString(TredeBox.Text),Convert.ToInt32(IdLable.Content), Convert.ToString(UserLable.Content));
                if (SQLBook.RightsCheck(Convert.ToString(UserLable.Content))=="admin")
                {
                    NavigationService.Navigate(new ReportPage(Convert.ToString(UserLable.Content)));
                }
                else
                {
                    NavigationService.Navigate(new MyBookList(Convert.ToString(UserLable.Content)));
                }
            }
            else
            {
                ErrorLable.Visibility = Visibility.Visible;
            }

        }
    }
}
