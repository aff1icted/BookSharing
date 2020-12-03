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
            

            SQLContol SQLContol = new SQLContol();
            BookForPage bookForPage= SQLContol.GetBookForPage(id);
            TitleTextBox.Text = bookForPage.Title;
            //BookImage.Source = new ImageSourceConverter().ConvertFromString(bookForPage.ImagePath) as ImageSource;
            AuthorBlock.Text= bookForPage.Authors;
            GenreLable.Content+= bookForPage.Genre;
            TredeBlock.Text = bookForPage.Trade;
            DescriptionBlock.Text = bookForPage.Description;
            EmailLable.Content = bookForPage.Email;

        }
    }
}
