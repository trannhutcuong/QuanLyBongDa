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
using QuanLyDoiBong.Views;
using System.Collections.ObjectModel;
using QuanLyDoiBong.Business;


namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for ThemBanThang.xaml
    /// </summary>
    public partial class ThemBanThang : Window
    {
        private V_BANTHANG m_BanThang;
        private ObservableCollection<V_BANTHANG> m_DsBanThang;
        private List<V_LOAIBANTHANG> m_LoaiBanThangs;
        private ObservableCollection<string> m_TenLoaiBanThangs;
        private ObservableCollection<string> m_TenDoiBongs;
        

        public delegate void MyDelegateType(V_BANTHANG a_BanThang);
        public event MyDelegateType HandlerBT;


        public ThemBanThang(ObservableCollection<V_BANTHANG> dsBanThang, List<V_LOAIBANTHANG> LoaiBanThangs,
            ObservableCollection<string> TenDoiBongs)
        {
            InitializeComponent();

            // Khởi tạo, sao chép
            m_BanThang = new V_BANTHANG();
            m_DsBanThang = new ObservableCollection<V_BANTHANG>();
            m_TenLoaiBanThangs = new ObservableCollection<string>();

            for(int i = 0; i < dsBanThang.Count; i++)
            {
                m_DsBanThang.Add(dsBanThang[i].Clone());
            }

            m_LoaiBanThangs = new List<V_LOAIBANTHANG>();
            for(int i = 0; i< LoaiBanThangs.Count; i++)
            {
                m_LoaiBanThangs.Add(LoaiBanThangs[i].Clone());
                m_TenLoaiBanThangs.Add(LoaiBanThangs[i].TENBT);
            }

            m_TenDoiBongs = new ObservableCollection<string>();
            for(int i=0; i<TenDoiBongs.Count; i++)
            {
                m_TenDoiBongs.Add(TenDoiBongs[i]);
            }

            cbbLoaiBanThang.ItemsSource = m_TenLoaiBanThangs;
            cbbDoi.ItemsSource = m_TenDoiBongs;
            this.DataContext = m_BanThang;
        }

        private void txtMaCauThu_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtHoTen.Text = B_CAUTHU.LayTenCauThuByMaCT(txtMaCauThu.Text);
        }

        private void btnThemBanThang_Click(object sender, RoutedEventArgs e)
        {
            // Gán các giá trị thuộc tính
            m_BanThang.m_TENCT = txtHoTen.Text;
            m_BanThang.m_LOAIBANTHANG = this.GetMaLoaiBT(m_BanThang.m_TENLOAIBANTHANG);
            m_BanThang.m_MADB = B_DOIBONG.LayMaDoiBongByTenDoiBong(m_BanThang.m_TENDB);
            V_BANTHANG newBanThang = m_BanThang.Clone();

            // Kiểm tra lỗi
            int retCode = B_BANTHANG.KiemTraBanThang(m_DsBanThang, newBanThang);
            if(retCode!= 0)
            {
                if(retCode == 1)
                {
                    MessageBox.Show("VUI LÒNG ĐIỀN ĐẦY ĐỦ THÔNG TIN!", "ERROR");
                }
                else if(retCode == 2)
                {
                    MessageBox.Show("BÀN THẮNG NÀY ĐÃ TỒN TẠI!", "ERROR");
                }
                else if(retCode == 3)
                {
                    MessageBox.Show("THỜI GIAN GHI BÀN KHÔNG THỎA QUY ĐỊNH CỦA GIẢI ĐẤU!\n[0-" +
                        MainWindow.QUYDINHGIAIBONG.m_THOI_DIEM_GHI_BAN_MAX + "]", "ERROR");
                }
                else if(retCode == 4)
                {
                    MessageBox.Show("CẦU THỦ KHÔNG THUỘC VỀ ĐỘI BÓNG NÀY!", "ERROR");
                }
                
                return;
            }


            // Tiến hành lưu

            if(HandlerBT != null)
            {
                HandlerBT(newBanThang);
            }

            m_DsBanThang.Add(newBanThang);
            MessageBox.Show("THÊM THÀNH CÔNG!", "SUCCESS");
            this.SetDefault();
        }

        private void btnThoatBanThang_Click(object sender, RoutedEventArgs e)
        {
            if(this.isDataChanged())
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

        private char GetMaLoaiBT(string a_TenLoaiBanThang)
        {
            char a = (char)0;
            for(int i=0; i<m_LoaiBanThangs.Count; i++)
            {
                if(m_LoaiBanThangs[i].TENBT == a_TenLoaiBanThang)
                {
                    return m_LoaiBanThangs[i].MABT;
                }
            }
            return a;
        }

        private void SetDefault()
        {
            txtMaCauThu.Text = "";
            txtMaCauThu.Focus();

            txtThoiDiem.Text = "0";
        }

        private bool isDataChanged()
        {
            if (txtMaCauThu.Text != "" || txtThoiDiem.Text != "0")
                return true;
            return false;
        }

        private void EnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnThemBanThang_Click(sender, e);
            }
        }
        
    }
}
