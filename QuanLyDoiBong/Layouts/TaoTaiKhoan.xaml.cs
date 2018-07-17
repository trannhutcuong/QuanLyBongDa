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

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for CreateAccountScreen.xaml
    /// </summary>
    public partial class CreateAccountScreen : Window
    {
        public CreateAccountScreen()
        {
            InitializeComponent();
        }

        private void buttonTaoTK_Click(object sender, RoutedEventArgs e)
        {
            if (tbTaiKhoan.Text == "" || CBB_LoaiTK.Text == "")
                MessageBox.Show("HÃY ĐIỀN ĐỦ THÔNG TIN", "Thông báo");
            else
            {
                V_TAIKHOAN a_NewAccount = new V_TAIKHOAN();
                Random rand = new Random();

                a_NewAccount.USERNAME = tbTaiKhoan.Text;
                // Tạo mật khẩu
                int lenPhanChu = rand.Next(4, 6);     // Phần chữ từ 4 đến 6 kí tự
                int lenPhanSo = rand.Next(2, 3);      // Phần số từ 2 đến 3 kí tự
                a_NewAccount.PASS = Business.B_TAIKHOAN.RandomString(lenPhanChu, lenPhanSo) ;  
                
                a_NewAccount.NGAYLAP = DateTime.Now;
                a_NewAccount.TYPEACC = CBB_LoaiTK.Text;

                int ketQua = Business.B_TAIKHOAN.ThemTaiKhoan(a_NewAccount);

                if (ketQua == 1)
                {
                    tbThongBao.Text = "";
                    MessageBox.Show("THÊM TÀI KHOẢN THÀNH CÔNG", "Thông báo");
                }
                else if (ketQua == 2)
                    tbThongBao.Text = "Tài khoản đã tồn tại";
                else if (ketQua == 3)
                    tbThongBao.Text = "Tên tài khoản không được chứa\n         các ký tự đặc biệt";
                else
                {
                    tbThongBao.Text = "Tên tài khoản không được\n       vượt quá 50 ký tự";
                }
            }
        }

        private void TextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            TB_Thoat.FontSize = 31;
        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            TB_Thoat.FontSize = 30;
        }

        private void TB_Thoat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                buttonTaoTK_Click(sender, e);
            }
        }

        private void tbTaiKhoan_KeyDown(object sender, KeyEventArgs e)
        {
            tbThongBao.Text = "";
        }
    }
}
