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
    /// Interaction logic for DangKyDoiBong.xaml
    /// </summary>
    public partial class DangKyDoiBong : Window
    {
        private V_DOIBONG m_DoiBong
        {
            get; set;
        }


        public DangKyDoiBong()
        {
            InitializeComponent();
            m_DoiBong = new V_DOIBONG();

            List<string> dsTP = Business.B_XML.readXMLTINHTP();
            ObservableCollection<string> list = new ObservableCollection<string>();
            foreach (string city in dsTP)
            {
                list.Add(city);
            }
            this.cbbTinhTP.ItemsSource = list;


            // Gán resource cho listview CauThu
            lvCauThu.ItemsSource = m_DoiBong.m_DanhSachCauThu;
            lvHLV.ItemsSource = m_DoiBong.m_DanhSachHLV;
            this.DataContext = m_DoiBong;
        }

        private void btnAddCauThu_Click(object sender, RoutedEventArgs e)
        {
            if(m_DoiBong.m_DanhSachCauThu.Count >= QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_NUM_MAX)
            {
                MessageBox.Show("SỐ LƯỢNG THÀNH VIÊN ĐÃ TỐI ĐA!", "ERROR");
                return;
            }
            DKDB_DangKyCauThu dkct = new DKDB_DangKyCauThu(m_DoiBong.m_DanhSachCauThu);
            dkct.Handler += Screen_Handler_CauThu;

            dkct.ShowDialog();
            

        }

        private void Screen_Handler_CauThu(V_CAUTHU newCauThu)
        {

            newCauThu.m_STT = m_DoiBong.m_DanhSachCauThu.Count + 1;
            m_DoiBong.m_DanhSachCauThu.Add(newCauThu);
            txtSoLuongCauThu.Text = newCauThu.m_STT.ToString();

        }

        private void btnAddHLV_Click(object sender, RoutedEventArgs e)
        {
            DKDB_DangKyHLV dkhlv = new DKDB_DangKyHLV(m_DoiBong.m_DanhSachHLV);
            dkhlv.Handler += Screen_Handler_HLV;
            dkhlv.ShowDialog();

        }

        private void Screen_Handler_HLV(V_HUANLUYENVIEN newHLV)
        {

            newHLV.m_STT = m_DoiBong.m_DanhSachHLV.Count + 1;
            m_DoiBong.m_DanhSachHLV.Add(newHLV);
            txtSoLuongHLV.Text = newHLV.m_STT.ToString();
        }


        private void btnLuuDoiBong_Click(object sender, RoutedEventArgs e)
        {

            
            // KIỂM TRA QUY ĐỊNH VÀ LỖI
            int retCode = Business.B_DOIBONG.KiemTraDoiBong(m_DoiBong);
            if(retCode != 0)
            {
                if(retCode == 1)
                {
                    MessageBox.Show("VUI LÒNG ĐIỀN ĐẨY ĐỦ THÔNG TIN", "ERROR");
                }
                else if(retCode == 2)
                {
                    MessageBox.Show("MÃ ĐỘI BÓNG NÀY ĐÃ TỒN TẠI", "ERROR");
                }
                else if (retCode == 3)
                {
                    MessageBox.Show("TÊN ĐỘI BÓNG NÀY ĐÃ TỒN TẠI", "ERROR");
                }
                else if (retCode == 4)
                {
                    MessageBox.Show("SỐ LƯỢNG CẦU THỦ PHẢI NHIỀU HƠN " + QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_NUM_MIN, "ERROR");
                }
                else if (retCode == 5)
                {
                    MessageBox.Show("CHƯA NHẬP HUẤN LUYỆN VIÊN", "ERROR");
                }
                else if (retCode == 6)
                {
                    MessageBox.Show("NGÀY THÀNH LẬP PHẢI NHỎ HƠN NGÀY ĐĂNG KÝ", "ERROR");
                }

                return;
            }


            // Lưu dữ liệu vào DB
            Business.B_DOIBONG.LuuDoiBong(m_DoiBong);
            
            MessageBox.Show("Đăng Ký Thành Công!\nChúc đội tuyển có một mùa giải thi đấu thật tốt", "Thông báo");
            
            this.Close();
        }

        private void EnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnLuuDoiBong_Click(sender, e);
            }
        }
    }
}
