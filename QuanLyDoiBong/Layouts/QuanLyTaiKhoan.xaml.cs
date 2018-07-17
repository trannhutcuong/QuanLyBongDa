using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AccountManagementScreen.xaml
    /// </summary>
    public partial class AccountManagementScreen : Window
    {
        private List<V_TAIKHOAN> listTaiKhoan = Business.B_TAIKHOAN.LayDanhSachTaiKhoan();

        public AccountManagementScreen()
        {
            InitializeComponent();
            lvTaiKhoan.ItemsSource = listTaiKhoan;
            int soTaiKhoan = listTaiKhoan.Count;
            tbSoLuong.Text = soTaiKhoan.ToString();
        }

        private void Button_XemMK_Click(object sender, RoutedEventArgs e)
        {
            V_TAIKHOAN taiKhoan = lvTaiKhoan.SelectedItem as V_TAIKHOAN;

            if (taiKhoan != null)
            {
                int loaiTaiKhoan = QuanLyDoiBong.MainWindow.ACCOUNTSOFT.m_Type;

                if (loaiTaiKhoan == 2)                      // Chỉ có tài khoản BTC mới được xem mật khẩu
                {
                    string[] tenTaiKhoan = taiKhoan.USERNAME.Split(null);            // Bỏ các ký tự null
                    MessageBox.Show("MẬT KHẨU TÀI KHOẢN '" + tenTaiKhoan[0] + "' là:\n" + taiKhoan.PASS, "Mật khẩu");
                }
                else
                {
                    MessageBoxResult m_Result = MessageBox.Show("CHỈ CÓ BAN TỔ CHỨC MỚI XEM ĐƯỢC MẬT KHẨU,\n BẠN CÓ MUỐN ĐĂNG NHẬP LẠI ?", "Thông báo", MessageBoxButton.OKCancel);
                    if (m_Result == MessageBoxResult.OK)
                    {
                        this.Close();
                        Login dangNhap = new Login();
                        dangNhap.Show();
                    }
                }
            }
            else
                MessageBox.Show("HÃY CHỌN MỘT TÀI KHOẢN", "Thông báo");
        }

        private void Button_TaoLaiMK_Click(object sender, RoutedEventArgs e)
        {
            V_TAIKHOAN taiKhoan = lvTaiKhoan.SelectedItem as V_TAIKHOAN;

            if (taiKhoan != null)
            {
                string[] username = taiKhoan.USERNAME.Split(null);
                MessageBoxResult m_Result = MessageBox.Show("TẠO MỚI MẬT KHẨU TÀI KHOẢN '" + username[0] + "' ?", "Tạo lại", MessageBoxButton.OKCancel);
                if (m_Result == MessageBoxResult.OK)
                {
                    // Tạo mật khẩu mới
                    Random rand = new Random();
                    int lenPhanChu = rand.Next(4, 6);     // Phần chữ từ 4 đến 6 kí tự
                    int lenPhanSo = rand.Next(2, 3);      // Phần số từ 2 đến 3 kí tự

                    string matKhauMoi = Business.B_TAIKHOAN.RandomString(lenPhanChu, lenPhanSo);
                    Business.B_TAIKHOAN.TaoMoiMatKhau(taiKhoan.USERNAME, matKhauMoi);

                    MessageBox.Show("ĐÃ TẠO MỚI MẬT KHẨU TÀI KHOẢN '" + username[0] + "'", "Thông báo");
                    // Update lại Listview tài khoản
                    listTaiKhoan = Business.B_TAIKHOAN.LayDanhSachTaiKhoan();
                    lvTaiKhoan.ItemsSource = listTaiKhoan;
                }
            }
            else
                MessageBox.Show("HÃY CHỌN MỘT TÀI KHOẢN", "Thông báo");
        }

        private void Button_Xoa_Click(object sender, RoutedEventArgs e)
        {
            V_TAIKHOAN taiKhoan = lvTaiKhoan.SelectedItem as V_TAIKHOAN;

            if (taiKhoan != null)
            {
                string[] username = taiKhoan.USERNAME.Split(null);
                MessageBoxResult m_Result = MessageBox.Show("XÓA TÀI KHOẢN '" + username[0] + "' ?", "Xóa", MessageBoxButton.OKCancel);
                if (m_Result == MessageBoxResult.OK)
                {
                    if (username[0] == "admin")
                        MessageBox.Show("KHÔNG ĐƯỢC XÓA TÀI KHOẢN ADMIN", "Thông báo");
                    else
                    {
                        Business.B_TAIKHOAN.XoaTaiKhoan(taiKhoan);

                        // Update lại Listview tài khoản
                        listTaiKhoan = Business.B_TAIKHOAN.LayDanhSachTaiKhoan();
                        int soTaiKhoan = listTaiKhoan.Count;
                        lvTaiKhoan.ItemsSource = listTaiKhoan;
                        tbSoLuong.Text = soTaiKhoan.ToString();

                        MessageBox.Show("ĐÃ XÓA TÀI KHOẢN '" + username[0] + "'", "Thông báo");
                    }
                }
            }
            else
                MessageBox.Show("HÃY CHỌN MỘT TÀI KHOẢN", "Thông báo");

        }

        private void Button_Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
