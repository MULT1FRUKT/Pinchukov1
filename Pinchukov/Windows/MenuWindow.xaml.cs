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
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            GetProduct();
        }
        private void GetProduct()
        {
            List<Order> ProdList = new List<Order>();

            ProdList = context.Order.ToList();

            LVOrd.ItemsSource = ProdList;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            AddOrder menuWindow = new AddOrder();
            menuWindow.Show();
            this.Close();
        }


    }
}