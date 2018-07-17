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
    /// Interaction logic for AddScheduleScreen.xaml
    /// </summary>
    public partial class AddScheduleScreen : Window
    {
        public delegate void VongDau(int vongdau);
        public VongDau SenderVD;

        List<V_DOIBONG> dsdb = new List<V_DOIBONG>();
        List<string> tenDB = new List<string>();
        List<string> tenSan = new List<string>();
        ObservableCollection<string> TenSans = new ObservableCollection<string> { "A", "B" };

        ObservableCollection<V_TRONGTAI> dsListTrongTai = new ObservableCollection<V_TRONGTAI>();
        List<V_TRONGTAI> dsCbbTrongTai;
        List<string> dsMaTrongTai = new List<string>();



        int m_vongdau = 0;
        public AddScheduleScreen()
        {
            InitializeComponent();

            dsdb = Business.B_DOIBONG.DanhSachDB();

            foreach (V_DOIBONG n in dsdb)
            {
                tenDB.Add(n.m_TenDoiBong);
            }

            cbbDoi1.ItemsSource = tenDB;
            cbbDoi1.SelectedIndex = 0;
            cbbDoi2.ItemsSource = tenDB;
            cbbDoi2.SelectedIndex = 0;




            cbbTenSan.SelectionChanged += cbbTenSan_SelectionChanged;
            cbbTenSan.ItemsSource = TenSans;

            SenderVD = new VongDau(SetVD);


            dsCbbTrongTai = B_TRONGTAI.DanhSachTrongTai();
            for(int i=0; i<dsCbbTrongTai.Count; i++)
            {
                string text = dsCbbTrongTai[i].m_MaTrongTai + "-" + dsCbbTrongTai[i].m_HoTen;
                dsMaTrongTai.Add(text);
            }
            cbbTrongTai.ItemsSource = dsMaTrongTai;
            lvTT.ItemsSource = dsListTrongTai;

        }

        private void SetVD(int vongdau)
        {
            m_vongdau = vongdau;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            V_TRANDAU tranDau = new V_TRANDAU();

            //bắt lỗi
            //mã trận đấu đã đã tồn tại
            if (Business.B_TRANDAU.KiemTraMaTD(txbMaTD.Text) == false)
                return;

            //chưa điền đầy đủ thông tin
            if (txbMaTD.Text == "")
            {
                MessageBox.Show("Chưa điền đầy đủ thông tin!");
                return;
            }

            //mã đội trùng nhay
            if (cbbDoi1.Text.CompareTo(cbbDoi2.Text) == 0)
            {
                MessageBox.Show("Mã đội bóng phải khác nhau!");
                return;
            }

            foreach (V_DOIBONG n in dsdb)
            {
                if (cbbDoi1.Text.CompareTo(n.m_TenDoiBong) == 0 || cbbDoi2.Text.CompareTo(n.m_TenDoiBong) == 0)
                    if (Business.B_DOIBONG.KiemTraDBTrongVD(n.m_MaDoiBong, m_vongdau) == false)
                    {
                        MessageBox.Show("Đội bóng đã tồn tại trong vòng đấu");
                        return;
                    }

            }
            //đội bóng đã xuất hiện trong vòng đấu
            tranDau.m_MaTD = txbMaTD.Text;

            foreach(V_DOIBONG n in dsdb)
            {
                if(cbbDoi1.Text.CompareTo(n.m_TenDoiBong)==0)
                    tranDau.m_MaDB1 = n.m_MaDoiBong;
                if (cbbDoi2.Text.CompareTo(n.m_TenDoiBong) == 0)
                    tranDau.m_MaDB2 = n.m_MaDoiBong;
            }

            DateTime myDate = dtNgay.DisplayDate;
            tranDau.m_NgayDienRa = myDate;

            tranDau.m_TenSan = cbbTenSan.Text;
            tranDau.m_SCORED1 = 0;
            tranDau.m_SCORED2 = 0;
            tranDau.m_VongDau = (byte)m_vongdau;
            tranDau.m_GhiNhan = false;
            if (Business.B_TRANDAU.TaoTranDau(tranDau) == true)
                MessageBox.Show("Lưu thành công!");
            else
                MessageBox.Show("Lưu thất bại!");
        }

        private void cbbDoi1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string TenDoiBong = cbbDoi1.SelectedValue.ToString();
            string TenSan = this.LayTenSanNha(TenDoiBong);
            TenSans[0] = TenSan;
            cbbTenSan.SelectedIndex = 0;
        }

        private void cbbDoi2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string TenDoiBong = cbbDoi2.SelectedValue.ToString();
            string TenSan = this.LayTenSanNha(TenDoiBong);
            TenSans[1] = TenSan;
            cbbTenSan.SelectedIndex = 0;
        }

        private void cbbTenSan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            cbbTenSan.ItemsSource = null;
            foreach (V_DOIBONG n in dsdb)
            {
                if (cbbDoi1.Text.CompareTo(n.m_TenDoiBong) == 0 || cbbDoi2.Text.CompareTo(n.m_TenDoiBong) == 0)
                    tenSan.Add(n.m_TenSanNha);
            }
            cbbTenSan.SelectedIndex = 0;
            cbbTenSan.ItemsSource = tenSan;
            */
        }

        private string LayTenSanNha(string a_TenDB)
        {

            // lay tu ten doi bong ok
            foreach (V_DOIBONG db in dsdb)
            {
                if (db.m_TenDoiBong == a_TenDB)
                {
                    return db.m_TenSanNha;
                }

            }
            return "";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            string text = cbbTrongTai.SelectedValue.ToString();
            string MACT = "";
            for(int i=0; i<text.Length; i++)
            {
                if(text[i] != '-')
                {
                    MACT += text[i].ToString();
                   
                }
                else
                {
                    break;
                }
            }
            

            for (int i=0; i< dsCbbTrongTai.Count; i++)
            {
                if(dsCbbTrongTai[i].m_MaTrongTai == MACT)
                {
                    V_TRONGTAI tt = new V_TRONGTAI
                    {
                        m_MaTrongTai = dsCbbTrongTai[i].m_MaTrongTai,
                        m_HoTen = dsCbbTrongTai[i].m_HoTen,
                    };
                    tt.m_STT = dsListTrongTai.Count + 1;
                    dsListTrongTai.Add(tt);
                }
            }
        }
    }
}
