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
    /// Логика взаимодействия для BookCreate.xaml
    /// </summary>
    public partial class BookCreate : Page
    {
        public BookCreate(string user)
        {
            InitializeComponent();
            UserLable.Content = user;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQLContol SQLContol = new SQLContol();
            ErrorLable.Content = SQLContol.ISBNBook(Convert.ToString(ISBNTextBox.Text));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLContol SQLContol = new SQLContol();
            if (!string.IsNullOrEmpty(ISBNTextBox.Text) && !string.IsNullOrEmpty(DescriptionTextBox.Text)&&!string.IsNullOrEmpty(TradeTextBox.Text))
            {
                if (SQLContol.ISBNBook(Convert.ToString(ISBNTextBox.Text))!= "Книга не найдена")
                {
                    SQLContol.AddUserBook(Convert.ToString(UserLable.Content), Convert.ToString(ISBNTextBox.Text), Convert.ToString(DescriptionTextBox.Text), Convert.ToString(TradeTextBox.Text), Convert.ToString(ImageLable.Content));
                    NavigationService.Navigate(new MyBookList(Convert.ToString(UserLable.Content)));
                }
                else
                {
                    ErrorLable.Content = "Книга не найдена";
                }
            }
            else
            {
                ErrorLable.Content = "Все поля должный быть заполнены";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ErrorLable.Content = "Изоброжение загружено";
                ImageLable.Content = System.IO.Path.GetDirectoryName(openFileDialog.FileName)+ @"\" + System.IO.Path.GetFileName(openFileDialog.FileName);                
            }
        }
    }
}
