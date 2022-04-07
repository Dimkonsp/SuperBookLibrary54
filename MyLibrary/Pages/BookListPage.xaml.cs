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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyLibrary.ClassHelper;
using MyLibrary.DBModel;

namespace MyLibrary.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookListPage.xaml
    /// </summary>
    public partial class BookListPage : Page
    {
        List<Book> bookList = new List<Book>();
        List<string> listSort = new List<string>() { "По умолчанию", "По названию", "По фамилии автора", "По имени автора", "По издательству" };

        public BookListPage()
        {
            InitializeComponent();
            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;
            Filter();
        }
 


        private void Filter()
        {
            bookList = AppDate.Context.Book.ToList();
            bookList = bookList.
                            Where(i => i.Title.ToLower().Contains(txtSearch.Text.ToLower()) ||
                            i.PublishHouse.NamePublishHouse.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    bookList = bookList.OrderBy(i => i.Title).ToList();
                    break;
                case 2:
                    bookList = bookList.OrderBy(i => i.Author.LastName).ToList();
                    break;
                case 3:
                    bookList = bookList.OrderBy(i => i.Author.FirstName).ToList();
                    break;
                case 4:
                    bookList = bookList.OrderBy(i => i.PublishHouse.NamePublishHouse).ToList();
                    break;
                default:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
            }

            listBook.ItemsSource = bookList;
        }


        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookWindow addBookWindow = new AddBookWindow();
            addBookWindow.ShowDialog();
            Filter();

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation visabilityAnim = new DoubleAnimation();
            visabilityAnim.From = 0.0;
            visabilityAnim.To = 1.0;
            visabilityAnim.Duration = TimeSpan.FromSeconds(2);
            MainGrid.BeginAnimation(Grid.OpacityProperty, visabilityAnim);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

            try
            {
                var item = listBook.SelectedItem as DBModel.Book;
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultClick == MessageBoxResult.Yes)
                {
                    AppDate.Context.Book.Remove(item);
                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Книга успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Filter();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Click_Change(object sender, RoutedEventArgs e)
        {
            var item = listBook.SelectedItem as DBModel.Book;
            ChangeBookWindow changebookwindow = new ChangeBookWindow(item);
            changebookwindow.ShowDialog();
            Filter();
        }
    }
}

