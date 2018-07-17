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

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        public MainScreen()
        {
            InitializeComponent();

            //QuanLyDoiBong.MainWindow.ACCOUNTSOFT.m_Type = 3;
            //QuanLyDoiBong.MainWindow.ACCOUNTSOFT.m_UserName = "Chung ku";
            // Chung thêm vào load dữ liệu cho chức năng.

            Database.FILE_STREAM.LoadChucNangUser(ref MainWindow.ChucNangGuess, ref MainWindow.ChucNangBQL,
                ref MainWindow.ChucNangBTC);
            txtUserName.Text = MainWindow.ACCOUNTSOFT.m_UserName;
            int typeAccount = MainWindow.ACCOUNTSOFT.m_Type;
            if(typeAccount == 0)
            {
                txtTypeAccount.Text = "GUEST USER";
            }
            else if(typeAccount == 1)
            {
                txtTypeAccount.Text = "BAN QUẢN LÝ";
            }
            else if(typeAccount == 2)
            {
                txtTypeAccount.Text = "BAN TỔ CHỨC";
            }
            else if(typeAccount == 3)
            {
                txtTypeAccount.Text = "ADMIN";
            }


            List<Button> DanhSachButton = new List<Button>(); // 1 cái loại button
            List<StackPanel> DanhSachStackPanel = new List<StackPanel>();

            int n_WidthStack = 200;
            int n_HeightStack = 230;


            #region 0 ĐĂNG KÝ ĐỘI BÓNG
            // ĐĂNG KÝ ĐỘI BÓNG
            Image img_0 = new Image();
            img_0.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/DangKyDoiBong.png");

            TextBlock txt_0 = new TextBlock();
            txt_0.Text = "Đăng ký đội bóng";
            txt_0.FontSize = 20;
            txt_0.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_0 = new StackPanel();
            st_0.Width = n_WidthStack;
            st_0.Height = n_HeightStack;
            st_0.Background = Brushes.Beige;        // Set background

            st_0.Children.Add(img_0);
            st_0.Children.Add(txt_0);
            DanhSachStackPanel.Add(st_0);
            #endregion

            //----------------------------------------------------------------------

            #region 1 LƯU TRỮ TRỌNG TÀI
            // LƯU TRỮ TRỌNG TÀI
            Image img_1 = new Image();
            img_1.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/LuuTruTrongTai.png");

            TextBlock txt_1 = new TextBlock();
            txt_1.Text = "Lưu trữ trọng tài";
            txt_1.FontSize = 20;
            txt_1.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_1 = new StackPanel();
            st_1.Width = n_WidthStack;
            st_1.Height = n_HeightStack;
            st_1.Background = Brushes.Beige;        // Set background

            st_1.Children.Add(img_1);
            st_1.Children.Add(txt_1);
            DanhSachStackPanel.Add(st_1);
            #endregion

            //----------------------------------------------------------------------

            #region 2 LẬP LỊCH THI ĐẤU
            // LẬP LỊCH THI ĐẤU
            Image img_2 = new Image();
            img_2.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/TaoTranDau.png");

            TextBlock txt_2 = new TextBlock();
            txt_2.Text = "Lập lịch thi đấu";
            txt_2.FontSize = 20;
            txt_2.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_2 = new StackPanel();
            st_2.Width = n_WidthStack;
            st_2.Height = n_HeightStack;
            st_2.Background = Brushes.Beige;        // Set background

            st_2.Children.Add(img_2);
            st_2.Children.Add(txt_2);
            DanhSachStackPanel.Add(st_2);
            #endregion

            //----------------------------------------------------------------------

            #region 3 LẬP BÁO CÁO GIẢI
            // LẬP BÁO CÁO GIẢI
            Image img_3 = new Image();
            img_3.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/LapBaoCaoGiai.png");

            TextBlock txt_3 = new TextBlock();
            txt_3.Text = "Lập báo cáo giải";
            txt_3.FontSize = 20;
            txt_3.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_3 = new StackPanel();
            st_3.Width = n_WidthStack;
            st_3.Height = n_HeightStack;
            st_3.Background = Brushes.Beige;        // Set background

            st_3.Children.Add(img_3);
            st_3.Children.Add(txt_3);
            DanhSachStackPanel.Add(st_3);
            #endregion

            //----------------------------------------------------------------------

            #region 4 GHI NHẬN KẾT QUẢ
            // GHI NHẬN KẾT QUẢ
            Image img_4 = new Image();
            img_4.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/GhiNhanKetQua.png");

            TextBlock txt_4 = new TextBlock();
            txt_4.Text = "Ghi nhận kết quả";
            txt_4.FontSize = 20;
            txt_4.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_4 = new StackPanel();
            st_4.Width = n_WidthStack;
            st_4.Height = n_HeightStack;
            st_4.Background = Brushes.Beige;        // Set background

            st_4.Children.Add(img_4);
            st_4.Children.Add(txt_4);
            DanhSachStackPanel.Add(st_4);
            #endregion

            //----------------------------------------------------------------------

            #region 5 QUẢN LÝ TÀI KHOẢN
            // QUẢN LÝ TÀI KHOẢN
            Image img_5 = new Image();
            img_5.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/QuanLyTaiKhoan.png");

            TextBlock txt_5 = new TextBlock();
            txt_5.Text = "Quản lý tài khoản";
            txt_5.FontSize = 20;
            txt_5.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_5 = new StackPanel();
            st_5.Width = n_WidthStack;
            st_5.Height = n_HeightStack;
            st_5.Background = Brushes.Beige;        // Set background

            st_5.Children.Add(img_5);
            st_5.Children.Add(txt_5);
            DanhSachStackPanel.Add(st_5);
            #endregion

            //----------------------------------------------------------------------

            #region 6 TẠO TÀI KHOẢN
            // TẠO TÀI KHOẢN
            Image img_6 = new Image();
            img_6.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/TaoTaiKhoan.png");

            TextBlock txt_6 = new TextBlock();
            txt_6.Text = "Tạo tài khoản";
            txt_6.FontSize = 20;
            txt_6.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_6 = new StackPanel();
            st_6.Width = n_WidthStack;
            st_6.Height = n_HeightStack;
            st_6.Background = Brushes.Beige;        // Set background

            st_6.Children.Add(img_6);
            st_6.Children.Add(txt_6);
            DanhSachStackPanel.Add(st_6);
            #endregion

            //----------------------------------------------------------------------

            #region 7 PHÂN QUYỀN
            // PHÂN QUYỀN
            Image img_7 = new Image();
            img_7.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/PhanQuyen.png");

            TextBlock txt_7 = new TextBlock();
            txt_7.Text = "Phân quyền";
            txt_7.FontSize = 20;
            txt_7.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_7 = new StackPanel();
            st_7.Width = n_WidthStack;
            st_7.Height = n_HeightStack;
            st_7.Background = Brushes.Beige;        // Set background

            st_7.Children.Add(img_7);
            st_7.Children.Add(txt_7);
            DanhSachStackPanel.Add(st_7);
            #endregion

            //----------------------------------------------------------------------

            #region 8 THAY ĐỔI QUY ĐỊNH
            // THAY ĐỔI QUY ĐỊNH
            Image img_8 = new Image();
            img_8.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/ThayDoiQuyDinh.png");

            TextBlock txt_8 = new TextBlock();
            txt_8.Text = "Thay đổi quy định";
            txt_8.FontSize = 20;
            txt_8.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_8 = new StackPanel();
            st_8.Width = n_WidthStack;
            st_8.Height = n_HeightStack;
            st_8.Background = Brushes.Beige;        // Set background

            st_8.Children.Add(img_8);
            st_8.Children.Add(txt_8);
            DanhSachStackPanel.Add(st_8);
            #endregion

            //----------------------------------------------------------------------

            #region 9 TRA CỨU THÔNG TIN
            // TRA CỨU THÔNG TIN
            Image img_9 = new Image();
            img_9.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/TimKiem.png");

            TextBlock txt_9 = new TextBlock();
            txt_9.Text = "Tra cứu thông tin";
            txt_9.FontSize = 20;
            txt_9.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_9 = new StackPanel();
            st_9.Width = n_WidthStack;
            st_9.Height = n_HeightStack;
            st_9.Background = Brushes.Beige;        // Set background

            st_9.Children.Add(img_9);
            st_9.Children.Add(txt_9);
            DanhSachStackPanel.Add(st_9);
            #endregion

            //----------------------------------------------------------------------

            #region 10 THOÁT
            // THOÁT
            Image img_10 = new Image();
            img_10.Source = Database.FILE_STREAM.LoadImage(Environment.CurrentDirectory + "/Images/IconManHinh/EXIT.png");

            TextBlock txt_10 = new TextBlock();
            txt_10.Text = "Thoát";
            txt_10.FontSize = 20;
            txt_10.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel st_10 = new StackPanel();
            st_10.Width = n_WidthStack;
            st_10.Height = n_HeightStack;
            st_10.Background = Brushes.Beige;        // Set background

            st_10.Children.Add(img_10);
            st_10.Children.Add(txt_10);
            DanhSachStackPanel.Add(st_10);
            #endregion


            //----------------------//-----------------------------------------//

            // NẠP CÁC STACKPANEL vào danh sách các button
            for (int i = 0; i < DanhSachStackPanel.Count; i++)
            {

                Button btn = new Button();
                btn.Content = DanhSachStackPanel[i];
                btn.Margin = new Thickness(25, 10, 0, 0);
                DanhSachButton.Add(btn);      
            }

            // Nạp chức năng theo tài khoản
            int type = QuanLyDoiBong.MainWindow.ACCOUNTSOFT.m_Type;
            if (type == 0) // Tài khoản Guess
            {
                for(int i=0; i<MainWindow.ChucNangGuess.Count; i++)
                {
                    wp_DanhSachChucNangControl.Children.Add(DanhSachButton[MainWindow.ChucNangGuess[i]]);
                }
            }
            else if(type == 1) // Tài khoản QL
            {
                for (int i = 0; i < MainWindow.ChucNangBQL.Count; i++)
                {
                    wp_DanhSachChucNangControl.Children.Add(DanhSachButton[MainWindow.ChucNangBQL[i]]);
                }
            }
            else if(type == 2) // BTC
            {
                for (int i = 0; i < MainWindow.ChucNangBTC.Count; i++)
                {
                    wp_DanhSachChucNangControl.Children.Add(DanhSachButton[MainWindow.ChucNangBTC[i]]);
                }
            }
            else if (type == 3)
            {
                wp_DanhSachChucNangControl.Children.Add(DanhSachButton[5]);
                wp_DanhSachChucNangControl.Children.Add(DanhSachButton[6]);
                wp_DanhSachChucNangControl.Children.Add(DanhSachButton[7]);
            }
                    
            wp_DanhSachChucNangControl.Children.Add(DanhSachButton[10]);

            // Nạp event click cho các button
            DanhSachButton[0].Click += btnDangKyDoiBong_Click;
            DanhSachButton[1].Click += btnLuuTruTrongTai_Click;
            DanhSachButton[2].Click += btnLapLichThiDau_Click;
            DanhSachButton[3].Click += btnLapBaoCaoGiai_Click;
            DanhSachButton[4].Click += btnGhiNhanKetQua_Click;
            DanhSachButton[5].Click += btnQuanLyTaiKhoan_Click;
            DanhSachButton[6].Click += btnTaoTaiKhoan_Click;
            DanhSachButton[7].Click += btnPhanQuyen_Click;
            DanhSachButton[8].Click += btnDoiQuyDinh_Click;
            DanhSachButton[9].Click += btnTraCuuThongTin_Click;
            DanhSachButton[10].Click += btnExit_Click;

        }

        // Khai báo các hàm sự kiện chuột

        // 0. ĐĂNG KÝ ĐỘI BÓNG
        private void btnDangKyDoiBong_Click(object sender, RoutedEventArgs e)
        {
            DangKyDoiBong view = new DangKyDoiBong();
            view.ShowDialog();
        }

        // 1. LƯU TRỮ TRỌNG TÀI
        private void btnLuuTruTrongTai_Click(object sender, RoutedEventArgs e)
        {
            LuuTruTrongTai view = new LuuTruTrongTai();
            view.Show();
        }

        // 2. LẬP LỊCH THI ĐẤU
        private void btnLapLichThiDau_Click(object sender, RoutedEventArgs e)
        {
            SetScheduleScreen view = new SetScheduleScreen();
            view.ShowDialog();
        }

        // 3. LẬP BÁO CÁO GIẢI
        private void btnLapBaoCaoGiai_Click(object sender, RoutedEventArgs e)
        {
            LapBaoCaoGiai view = new LapBaoCaoGiai();
            view.Show();
        }

        // 4. GHI NHẬN KẾT QUẢ
        private void btnGhiNhanKetQua_Click(object sender, RoutedEventArgs e)
        {
            GhiNhanKetQuaTranDau view = new GhiNhanKetQuaTranDau();
            view.ShowDialog();
        }

        // 5. QUẢN LÝ TÀI KHOẢN
        private void btnQuanLyTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            AccountManagementScreen view = new AccountManagementScreen();
            view.Show();
        }

        // 6. TẠO TÀI KHOẢN
        private void btnTaoTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountScreen view = new CreateAccountScreen();
            view.Show();
        }

        // 7. PHÂN QUYỀN
        private void btnPhanQuyen_Click(object sender, RoutedEventArgs e)
        {
            PhanQuyen view = new PhanQuyen();
            view.ShowDialog();
        }

        // 8. ĐỔI QUY ĐỊNH
        private void btnDoiQuyDinh_Click(object sender, RoutedEventArgs e)
        {
            DoiQuyDinh view = new DoiQuyDinh();
            view.Show();
        }

        // 9. TRA CỨU THÔNG TIN
        private void btnTraCuuThongTin_Click(object sender, RoutedEventArgs e)
        {
            TimKiemScreen view = new TimKiemScreen();
            view.ShowDialog();
        }

        // 10. THOÁT
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }



        private void DangXuatEvent(object sender, MouseButtonEventArgs e)
        {
            Login view = new Login();
            view.Show();
            this.Close();
        }

       

        //-----------------------------------------------------
        // HANDLER
        private void ActiveEvent()
        {
            this.Activate();
        }
    }
}
