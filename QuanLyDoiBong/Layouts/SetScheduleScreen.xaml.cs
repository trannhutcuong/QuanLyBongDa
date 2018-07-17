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
using System.Windows.Shapes;
using QuanLyDoiBong.Business;
using QuanLyDoiBong.Views;
using QuanLyDoiBong.Database;

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for SetScheduleScreen.xaml
    /// </summary>
    public partial class SetScheduleScreen : Window
    {
        public class User
        {
            public int STT { get; set; }
            public string MATD { set; get; }
            public string Doi1 { set; get; }
            public string Doi2 { set; get; }
            public string NgayGio { set; get; }
            public string San { set; get; }
        }
        public SetScheduleScreen()
        {
            InitializeComponent();

            List<byte> listVongDau = new List<byte>();
            listVongDau.Add(1);
            listVongDau.Add(2);
            listVongDau.Add(3);
            listVongDau.Add(4);
            listVongDau.Add(5);
            listVongDau.Add(6);
            listVongDau.Add(7);
            listVongDau.Add(8);
            listVongDau.Add(9);
            listVongDau.Add(10);
            cbbVongDau.ItemsSource = listVongDau;

            cbbVongDau.SelectedIndex = 0;
            cbbVongDau.SelectionChanged += cbbVongDau_SelectionChanged;

        }



        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                string dtXoa = (listView.SelectedItem as User).MATD;

                if (dtXoa == null)
                {
                    MessageBox.Show("Chưa chọn đội trận đấu cần xóa");
                    return;
                }

                if (Business.B_TRANDAU.XoaTranDau(dtXoa) == true)
                {
                    MessageBox.Show("Xóa thành công!");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
                cbbVongDau.SelectionChanged += cbbVongDau_SelectionChanged;

            }

        }

        private void cbbVongDau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int stt = 0;
            listView.ItemsSource = null;


            byte vd = (byte)Int32.Parse(cbbVongDau.SelectedItem.ToString());
            List<User> list = new List<User>();
            List<V_TRANDAU> l = new List<V_TRANDAU>();
            l = Business.B_TRANDAU.XemVongDau(vd);
            foreach (V_TRANDAU t in l)
            {

                list.Add(new User()
                {
                    STT = ++stt,
                    MATD = t.m_MaTD.ToString(),
                    Doi1 = t.m_MaDB1.ToString(),
                    Doi2 = t.m_MaDB2.ToString(),
                    NgayGio = t.m_NgayDienRa.ToString(),
                    San = t.m_TenSan
                });
            }
            listView.ItemsSource = list;
        }

        private void btnThem_Click(object sender, RoutedEventArgs e)
        {
            AddScheduleScreen view = new AddScheduleScreen();
            int x = Int32.Parse(cbbVongDau.Text);
            view.SenderVD(x);
            view.Show();
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
