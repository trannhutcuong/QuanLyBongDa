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
using System.Collections.ObjectModel;

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for GhiNhanKetQuaTranDau.xaml
    /// </summary>
    public partial class GhiNhanKetQuaTranDau : Window
    {
        private V_TRANDAU m_TranDau;
        private ObservableCollection<V_BANTHANG> dsBanThang;
        private List<V_LOAIBANTHANG> LoaiBanThangs;
        private ObservableCollection<string> TenDoiBongs;

        private int index_Cbb = -1;
        List<string> dsMaTranDau;
        public GhiNhanKetQuaTranDau()
        {
            InitializeComponent();

            dsBanThang = new ObservableCollection<V_BANTHANG>();
            LoaiBanThangs = B_LOAIBANTHANG.LayDuLieuLoaiBanThang();
            TenDoiBongs = new ObservableCollection<string>();
            TenDoiBongs.Add("aaa");
            TenDoiBongs.Add("bbb");

            dsMaTranDau = Business.B_TRANDAU.LayDanhSachMaTranDauChuaGhiNhan();
            cbbMaTranDau.ItemsSource = dsMaTranDau;


            // Gán dữ liệu cho control
            lvBanThang.ItemsSource = dsBanThang;
            this.DataContext = m_TranDau;
        }

        private void cbbMaTranDau_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dsMaTranDau.Count > 0)
            {
                bool flag = true;
                if (dsBanThang.Count > 0 && index_Cbb != cbbMaTranDau.SelectedIndex)
                {
                    if (MessageBox.Show("Bạn chưa lưu dữ liệu!Có chuyển hay không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        cbbMaTranDau.SelectedIndex = index_Cbb;
                        flag = false;

                    }
                }

                if (flag == true && index_Cbb != cbbMaTranDau.SelectedIndex)
                {
                    string maDB = cbbMaTranDau.SelectedItem.ToString();
                    m_TranDau = B_TRANDAU.LayTranDauByMaTranDau(maDB);
                    txtTenSan.Text = m_TranDau.m_TenSan;
                    txtTenDoi1.Text = B_DOIBONG.LayTenDoiBongByMaDB(m_TranDau.m_MaDB1);
                    txtTenDoi2.Text = B_DOIBONG.LayTenDoiBongByMaDB(m_TranDau.m_MaDB2);
                    TenDoiBongs[0] = txtTenDoi1.Text;
                    TenDoiBongs[1] = txtTenDoi2.Text;
                    index_Cbb = cbbMaTranDau.SelectedIndex;
                    dsBanThang.Clear();
                }
            }            
        }

        private void btnGhiNhanKQ_Click(object sender, RoutedEventArgs e)
        {
            if(dsBanThang.Count > 0)
            {
                // CẬP NHẬT DOIBONG (SOBANTHANG, SOBANTHUA, TONGDIEM)
                // Đội 1
                B_DOIBONG.CapNhatKetQuaTranDau(m_TranDau.m_MaDB1, m_TranDau.m_SCORED1, m_TranDau.m_SCORED2,
                    MainWindow.QUYDINHGIAIBONG.m_SCORE_THANG, MainWindow.QUYDINHGIAIBONG.m_SCORE_HOA, MainWindow.QUYDINHGIAIBONG.m_SCORE_THUA);
                // Đội 2
                B_DOIBONG.CapNhatKetQuaTranDau(m_TranDau.m_MaDB2, m_TranDau.m_SCORED2, m_TranDau.m_SCORED1,
                    MainWindow.QUYDINHGIAIBONG.m_SCORE_THANG, MainWindow.QUYDINHGIAIBONG.m_SCORE_HOA, MainWindow.QUYDINHGIAIBONG.m_SCORE_THUA);


                // CẬP NHẬT BẢNG TRANDAU (SCORED1, SCORED2, GHINHAN)
                B_TRANDAU.CapNhatKetQuaTranDau(m_TranDau.m_MaTD, m_TranDau.m_SCORED1, m_TranDau.m_SCORED2);

                // THÊM VÀO BANTHANG
                for (int i = 0; i < dsBanThang.Count; i++)
                {
                    // Cập nhật số bàn thắng cho cầu thủ
                    B_CAUTHU.UpdateBanThangCauThu(dsBanThang[i].m_MACT);
                    B_BANTHANG.ThemBanThang(dsBanThang[i]);
                }

                MessageBox.Show("Thêm thành công!", "SUCCESS");
                this.SetDefault();
                cbbMaTranDau.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("DỮ LIỆU RỖNG", "ERROR");
            }

        }


        private void btnAddBanThang_Click(object sender, RoutedEventArgs e)
        {
            if(dsMaTranDau.Count > 0)
            {
                ThemBanThang view = new ThemBanThang(dsBanThang, LoaiBanThangs, TenDoiBongs);
                view.HandlerBT += Screen_Handler_BanThang;
                view.ShowDialog();
            }
            else
            {
                MessageBox.Show("KHÔNG CÓ TRẬN ĐẤU NÀO CHƯA GHI NHẬN", "ERROR");
            }
            
        }

        private void Screen_Handler_BanThang(V_BANTHANG newBanThang)
        {

            newBanThang.m_SOTT = dsBanThang.Count + 1;
            newBanThang.m_MATD = m_TranDau.m_MaTD;
            dsBanThang.Add(newBanThang);
            this.CapNhatSoBanThang(newBanThang);
        }


        // CẬP NHẬT SỐ BÀN THẮNG CỦA 2 ĐỘI
        private void CapNhatSoBanThang(V_BANTHANG a_BanThang)
        {
            if(a_BanThang.m_MADB == m_TranDau.m_MaDB1)
            {
                if(a_BanThang.m_LOAIBANTHANG == 'C')
                {
                    m_TranDau.m_SCORED2 += 1;
                    txtSiSoDoi2.Text = m_TranDau.m_SCORED2.ToString();
                }
                else
                {
                    m_TranDau.m_SCORED1 += 1;
                    txtSiSoDoi1.Text = m_TranDau.m_SCORED1.ToString();
                }
            }
            else if(a_BanThang.m_MADB == m_TranDau.m_MaDB2)
            {
                if (a_BanThang.m_LOAIBANTHANG == 'C')
                {
                    m_TranDau.m_SCORED1 += 1;
                    txtSiSoDoi1.Text = m_TranDau.m_SCORED1.ToString();
                }
                else
                {
                    m_TranDau.m_SCORED2 += 1;
                    txtSiSoDoi2.Text = m_TranDau.m_SCORED2.ToString();
                }
            }
        }

        private void SetDefault()
        {
            dsBanThang.Clear();
            dsMaTranDau = Business.B_TRANDAU.LayDanhSachMaTranDauChuaGhiNhan();
            index_Cbb = -1;
            txtTenDoi1.Text = txtTenDoi2.Text = txtTenSan.Text = "";
            cbbMaTranDau.ItemsSource = dsMaTranDau;
            cbbMaTranDau.SelectedIndex = 0;
            txtSiSoDoi1.Text = txtSiSoDoi2.Text = "0";
            m_TranDau.m_SCORED1 = m_TranDau.m_SCORED2 = 0;
            

        }

        private void btnThoatGhiNhan_Click(object sender, RoutedEventArgs e)
        {
            if (dsBanThang.Count > 0)
            {
                if (MessageBox.Show("Bạn chưa lưu dữ liệu!Có thoát hay không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }
    }
}
