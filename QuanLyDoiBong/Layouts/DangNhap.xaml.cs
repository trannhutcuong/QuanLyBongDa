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

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void buttonDN_Click(object sender, RoutedEventArgs e)       // Đăng nhập với tài khoản và mật khẩu
        {
            if (tbTaiKhoan.Text == "" || pbMatKhau.Password == "")
            {
                MessageBox.Show("Hãy nhập đủ thông tin", "Thông báo");
            }
            else
            {
                int ketQuaTim = Business.B_TAIKHOAN.TimTaiKhoan(tbTaiKhoan.Text, pbMatKhau.Password);     // -1: không tìm thây ; 1,2,3: Tài khoản tồn tại

                if (ketQuaTim != -1)                                         // Đăng nhập thành công
                {
                    tbThongBao.Text = "";
                    QuanLyDoiBong.MainWindow.ACCOUNTSOFT.m_Type = ketQuaTim;
                    QuanLyDoiBong.MainWindow.ACCOUNTSOFT.m_UserName = tbTaiKhoan.Text;


                    MainScreen mainscr = new MainScreen();
                    mainscr.Show();
                    this.Close();
                }
                else                                                         // Sai thông tin
                    tbThongBao.Text = "Sai tài khoản hoặc mật khẩu,\n         vui lòng thử lại";
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)      // Đăng nhập dạng người dùng khách
        {
            tbThongBao.Text = "";
            QuanLyDoiBong.MainWindow.ACCOUNTSOFT.m_Type = 0;
            QuanLyDoiBong.MainWindow.ACCOUNTSOFT.m_UserName = "";

            MainScreen mainscr = new MainScreen();
            mainscr.Show();
            this.Close();
        }

       
        private void tbDangNhapKhach_MouseMove(object sender, MouseEventArgs e)
        {
            tbDangNhapKhach.FontSize = 31;
        }

        private void tbDangNhapKhach_MouseLeave(object sender, MouseEventArgs e)
        {
            tbDangNhapKhach.FontSize = 30;
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                buttonDN_Click(sender, e);
            }
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            tbThongBao.Text = "";
        }
    }
}
