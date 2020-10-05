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
using static ListView.AppData;

namespace ListView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            ListUser.ItemsSource = context.User.ToList(); // источник данных для ЛистВью
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ListUser.SelectedItem is User user) // Если выбрана запись в ЛистВью
            {
                EditWindow editWin = new EditWindow(user.idUser); // создаем новую страницу и в конструктор передаем значение id пользователя
               
                editWin.nameUserTXT.Text = user.NameUser; // передаем имя пользователя в текстовое поле на новой странице
                editWin.ageUserTXT.Text = user.AgeUser.ToString(); // передаем возраст пользователя в текстовое поле на новой странице (конвертируем тип данных из целого числа в текст)
                editWin.ShowDialog(); // открываем новую страницу
                ListUser.ItemsSource = context.User.ToList(); // обновляем ЛистВью после закрытия страницы изменения данных
            }
        }        
    }
}
