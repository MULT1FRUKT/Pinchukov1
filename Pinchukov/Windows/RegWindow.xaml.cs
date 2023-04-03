using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

using Pinchukov.ClassHelper;
using Pinchukov.Db;
using static Pinchukov.ClassHelper.EFClass;

namespace PinchukovLobutev9_14.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
            CmbGender.ItemsSource = context.Gender.ToList();
            CmbGender.SelectedIndex = 0;
            CmbGender.DisplayMemberPath = "Gender1";
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            RegPersWindow regpersWindow = new RegPersWindow();
            regpersWindow.Show();
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbLogin.Text))
            {
                MessageBox.Show("Логин не может быть пустым" + context.Client1.ToList().Where(i => i.FirstName == TbName.Text).FirstOrDefault());
                return;
            }
            if (string.IsNullOrWhiteSpace(TbPassword1.Password))
            {
                MessageBox.Show("Пароль не может быть пустым");
                return;
            }
            if (TbPassword1.Password != TbPassword2.Password)
            {
                MessageBox.Show("Пароли должны быть одинаковыми");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbName.Text))
            {
                MessageBox.Show("Имя не может быть пустым");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbPhone.Text))
            {
                MessageBox.Show("Телефон не может быть пустым");
                return;
            }
            bool result = Int64.TryParse(TbPhone.Text, out var number);
            if (result != true)
            {
                MessageBox.Show("Телефон должен быть заполнен числами");
                return;
            }
            if (string.IsNullOrWhiteSpace(DPB.Text))
            {
                MessageBox.Show("Не введена дата рождения");
                return;
            }

            var authUser = context.Authorization.ToList().Where(i => i.Login == TbLogin.Text).FirstOrDefault();
            if (authUser != null)
            {
                MessageBox.Show("Такой пользователь уже есть");
                return;
            }
            else
            {


                Db.Client1 client1 = new Db.Client1();
                client1.FirstName = TbName.Text;
                client1.Phone = TbPhone.Text;
                client1.DateOfBirth = DPB.SelectedDate.Value;
                client1.IdGender = (CmbGender.SelectedItem as Db.Gender).ID;
                context.Client1.Add(client1);

                context.SaveChanges();

                Db.Authorization authorization = new Db.Authorization();
                authorization.Login = TbLogin.Text;
                authorization.Password = TbPassword1.Password;
                authorization.IdClient = (context.Client1.ToList().Where(i => i.FirstName == TbName.Text).Last()).ID;
                context.Authorization.Add(authorization);

                context.SaveChanges();
            }





        }
    }
}