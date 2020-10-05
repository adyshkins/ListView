using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ListView.AppData;

namespace ListView
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class EditWindow : Window, INotifyPropertyChanged
    {
        public int idUser; // переменная для хранения id выбранного пользователя
        public string pathPhoto; // переменная для хранения пути к изображению

        public event PropertyChangedEventHandler PropertyChanged;

        private byte[] _photo; 
        public byte[] photo
        {
            get => _photo;
            set
            {
                _photo = value;

                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("photo"));
                }
            }
        }
        public EditWindow(int idUser)
        {
            this.idUser = idUser;

            InitializeComponent();


            var us = context.User.Where(i => i.idUser == idUser).Select(i => i.PhotoUser).FirstOrDefault(); // переменная us хранит поле Фото выбранного пользователя
            if (us != null) // если поле не пустое то выполняем выгрузку изображения из БД в image 
            {
                //using (MemoryStream stream = new MemoryStream(context.User.Where(i => i.idUser == idUser).Select(i => i.PhotoUser).FirstOrDefault()))
                //{
                //    BitmapImage bitmapImage = new BitmapImage();
                //    bitmapImage.BeginInit();
                //    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                //    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                //    bitmapImage.StreamSource = stream;
                //    bitmapImage.EndInit();
                //    photoUser.Source = bitmapImage; // присваиваем Image источник
                //}
                photo = us;
            }                   
        }

        private void EditPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                photoUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathPhoto = openFileDialog.FileName;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = context.User.Where(i => i.idUser == idUser).FirstOrDefault();            
            user.NameUser = nameUserTXT.Text;
            user.AgeUser = Int32.Parse(ageUserTXT.Text);
            user.PhotoUser = File.ReadAllBytes(pathPhoto);
            context.SaveChanges();
            MessageBox.Show("Gooood )) ");
            this.Close();

        }
    }
}
