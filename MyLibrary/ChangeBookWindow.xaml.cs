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
    /// Логика взаимодействия для ChangeBookWindow.xaml
    /// </summary>
    public partial class ChangeBookWindow : Window
    {
        DBModel.Book editbook = new Book();
        public ChangeBookWindow()
        {
            InitializeComponent();
        }
        public ChangeBookWindow(DBModel.Book book)
        {
            InitializeComponent();
            cmbSelection.ItemsSource = AppDate.Context.Selection.ToList();
            cmbSelection.DisplayMemberPath = "NameSelection";
            cmbSelection.SelectedIndex = 0;

            cmbPublishHouse.ItemsSource = AppDate.Context.PublishHouse.ToList();
            cmbPublishHouse.DisplayMemberPath = "NamePublishHouse";
            cmbPublishHouse.SelectedIndex = 0;

            cmbLastNameAuthor.ItemsSource = AppDate.Context.Author.ToList();
            cmbLastNameAuthor.DisplayMemberPath = "LastName";
            cmbLastNameAuthor.SelectedIndex = 0;

            cmbFirstNameAuthor.ItemsSource = AppDate.Context.Author.ToList();
            cmbFirstNameAuthor.DisplayMemberPath = "FirstName";
            cmbFirstNameAuthor.SelectedIndex = 0;
            editbook = book;
            txtTitle.Text = editbook.Title;
        }

        private void btnChangeBook_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Поле «Название книги» не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtTitle.Text.Length > 100)
            {
                MessageBox.Show("В поле «Название книги» недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultClick == MessageBoxResult.Yes)
                {

                    editbook.Title = txtTitle.Text;
                    editbook.IDAuthor = cmbLastNameAuthor.SelectedIndex + 1;
                    editbook.IDAuthor = cmbFirstNameAuthor.SelectedIndex + 1;
                    editbook.IDSection = cmbSelection.SelectedIndex + 1;
                    editbook.IDPublishHouse = cmbPublishHouse.SelectedIndex + 1;
                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Книга успешно отредактирована!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation visabilityAnim = new DoubleAnimation();
            visabilityAnim.From = 0.0;
            visabilityAnim.To = 1.0;
            visabilityAnim.Duration = TimeSpan.FromSeconds(2);
            MainGrid.BeginAnimation(Grid.OpacityProperty, visabilityAnim);
        }
    }
}
