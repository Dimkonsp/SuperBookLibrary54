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
using System.Windows.Shapes;
using MyLibrary.DBModel;
using MyLibrary.ClassHelper;

namespace MyLibrary
{
    /// <summary>
    /// Логика взаимодействия для AddDeliverWindow.xaml
    /// </summary>
    public partial class AddDeliverWindow : Window
    {
        public AddDeliverWindow()
        {
            InitializeComponent();
            cmbBook.ItemsSource = AppDate.Context.Book.ToList();
            cmbBook.DisplayMemberPath = "Title";
            cmbBook.SelectedIndex = 0;

            cmbReader.ItemsSource = AppDate.Context.Reader.ToList();
            cmbReader.DisplayMemberPath = "LastName";
            cmbReader.SelectedIndex = 0;

            cmbEmployer.ItemsSource = AppDate.Context.Emplovee.ToList();
            cmbEmployer.DisplayMemberPath = "LastName";
            cmbEmployer.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation visabilityAnim = new DoubleAnimation();
            visabilityAnim.From = 0.0;
            visabilityAnim.To = 1.0;
            visabilityAnim.Duration = TimeSpan.FromSeconds(2);
            MainGrid.BeginAnimation(Grid.OpacityProperty, visabilityAnim);
        }

        private void btnAddDeliver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultClick == MessageBoxResult.Yes)
                {
                    //Добавление нового читателя
                    DBModel.BookRental bookRental = new DBModel.BookRental();
                    bookRental.IDBook = cmbBook.SelectedIndex + 1;
                    bookRental.IDReader = cmbReader.SelectedIndex + 1;
                    bookRental.IDEmplovee = cmbEmployer.SelectedIndex + 1;
                    bookRental.StartDate = dtDateStart.DisplayDate;
                   

                    AppDate.Context.BookRental.Add(bookRental);
                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Запись успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
