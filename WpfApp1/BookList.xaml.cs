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
using System.Collections.ObjectModel;

namespace BookSharing
{
      
    public partial class BookList : Page
    {
       
        public BookList()
        {
            InitializeComponent();
            SQLGetList SQLGetList = new SQLGetList();


            BooksList.ItemsSource = SQLGetList.GetBookList();
        }

        public BookList(ObservableCollection<Book> books)
        {
            InitializeComponent();        


            BooksList.ItemsSource = books;
        }


        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book p = (Book)BooksList.SelectedItem;
            NavigationService.Navigate(new BookPage(p.id));
        }
    }
}
