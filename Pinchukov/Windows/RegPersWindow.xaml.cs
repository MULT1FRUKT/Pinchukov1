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
using System.Xml.Linq;

using Pinchukov.ClassHelper;
using Pinchukov.Db;
using static Pinchukov.ClassHelper.EFClass;

namespace PinchukovLobutev9_14.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegPersWindow.xaml
    /// </summary>
    public partial class RegPersWindow : Window
    {
        public RegPersWindow()
        {
            InitializeComponent();
            CmbGender.ItemsSource = context.Gender.ToList();
            CmbGender.SelectedIndex = 0;
            CmbGender.DisplayMemberPath = "Gender1";
        }

        private void TextBlock_MouseLeftButtonUpAuth(object sender, MouseButtonEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();

        }

        private void TextBlock_MouseLeftButtonUpReg(object sender, MouseButtonEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            this.Close();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbFirstName.Text))
            {
                MessageBox.Show("Имя не может быть пустым");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbLastName.Text))
            {
                MessageBox.Show("Фамилия не может быть пустой");
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
            if (string.IsNullOrWhiteSpace(TbEmail.Text))
            {
                MessageBox.Show("Почта не может быть пустой");
                return;
            }


            if (string.IsNullOrWhiteSpace(TbPassport.Text))
            {
                MessageBox.Show("Паспорт не может быть пустым");
                return;
            }
            bool result1 = Int64.TryParse(TbPassport.Text, out var number1);
            if (result != true)
            {
                MessageBox.Show("Паспорт должен быть заполнен числами");
                return;
            }


            if (string.IsNullOrWhiteSpace(DPB.Text))
            {
                MessageBox.Show("Не введена дата рождения");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbLogin.Text))
            {
                MessageBox.Show("Логин не может быть пустым");
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


            if (string.IsNullOrWhiteSpace(PbCode.Password))
            {
                MessageBox.Show("Код сотрудника не может быть пустым");
                return;
            }
            var authUser = context.Role.ToList().Where(i => i.PersonCode == Convert.ToInt32(PbCode.Password)).FirstOrDefault();
            if (authUser == null)
            {
                MessageBox.Show("Неверный код сотрудника");
                return;
            }

            var authUser1 = context.Authorization.ToList().Where(i => i.Login == TbLogin.Text).FirstOrDefault();
            if (authUser1 != null)
            {
                MessageBox.Show("Такой логин занят");
                return;
            }
            else
            {


                Db.Employee employee = new Db.Employee();
                employee.FirstName = TbFirstName.Text;
                employee.LastName = TbLastName.Text;
                employee.MidleName = TbMidlleName.Text;
                employee.Phone = TbPhone.Text;
                employee.Email = TbEmail.Text;
                employee.Passport = TbPassport.Text;
                employee.DateOfBirthday = DPB.SelectedDate.Value;
                employee.IdGender = (CmbGender.SelectedItem as Db.Gender).ID;
                employee.IdRole = (context.Role.ToList().Where(i => i.PersonCode == Convert.ToInt32(PbCode.Password)).FirstOrDefault()).ID;


                context.Employee.Add(employee);

                context.SaveChanges();

                Db.Authorization authorization = new Db.Authorization();
                authorization.Login = TbLogin.Text;
                authorization.Password = TbPassword1.Password;
                authorization.IdEmployee = (context.Employee.ToList().Where(i => i.FirstName == TbFirstName.Text).Last()).ID;
                context.Authorization.Add(authorization);

                context.SaveChanges();
            }





        }


    }
}