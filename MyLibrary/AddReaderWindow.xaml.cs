using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using MyLibrary.ClassHelper;

namespace MyLibrary
{
    /// <summary>
    /// Логика взаимодействия для AddReaderWindow.xaml
    /// </summary>
    public partial class AddReaderWindow : Window
    {
        string pathPhoto = null;
        public AddReaderWindow()
        {
            InitializeComponent();
            cmbGender.ItemsSource = AppDate.Context.Gender.ToList();
            cmbGender.DisplayMemberPath = "NameGender";
            cmbGender.SelectedIndex = 0;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Валидация
            //Проверка на пустоту

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Поле Email не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Поле Адрес не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка на количество символов

            if (txtLastName.Text.Length > 100)
            {
                MessageBox.Show("В поле Фамилия недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtFirstName.Text.Length > 100)
            {
                MessageBox.Show("В поле Имя недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Length > 20)
            {
                MessageBox.Show("В поле Телефон недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtEmail.Text.Length > 100)
            {
                MessageBox.Show("В поле Email недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtAddress.Text.Length > 100)
            {
                MessageBox.Show("В поле Адрес недопустимое количество символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //Проверка на ошибки в БД

            try
            {
                var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultClick == MessageBoxResult.Yes)
                {
                    //Добавление нового читателя
                    DBModel.Reader reader = new DBModel.Reader();
                    reader.LastName = txtLastName.Text;
                    reader.FirstName = txtFirstName.Text;
                    reader.Phone = txtPhone.Text;
                    reader.Email = txtEmail.Text;
                    reader.Address = txtAddress.Text;
                    reader.IDGender = cmbGender.SelectedIndex + 1;
                    if (pathPhoto != null)
                    {
                        reader.Photo = File.ReadAllBytes(pathPhoto);
                    }
                    AppDate.Context.Reader.Add(reader);
                    AppDate.Context.SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    DoubleAnimation visabilityAnim = new DoubleAnimation();
                    visabilityAnim.From = 1.0;
                    visabilityAnim.To = 0.0;
                    visabilityAnim.Duration = TimeSpan.FromSeconds(2);
                    MainGrid.BeginAnimation(Grid.OpacityProperty, visabilityAnim);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //Валидация
        //Запрет на ввод некоректных данных
        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox txtPhone)
            {
                txtPhone.Text = new string(txtPhone.Text.Where(ch => ch == '+' || ch == '-' || (ch >= '0' && ch <= '9') || ch == '(' || ch == ')').ToArray());
                txtPhone.SelectionStart = e.Changes.First().Offset + 1;
                txtPhone.SelectionLength = 0;
            }
        }


        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                pathPhoto = openFileDialog.FileName;
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
