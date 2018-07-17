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
using QuanLyDoiBong.Layouts;


namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for TimKiemScreen.xaml
    /// </summary>
    public partial class TimKiemScreen : Window
    {
        public class User
        {
            public int STT { get; set; }
            public string MA { get; set; }
            public string HOTEN { get; set; }
            public string NGAYSINH { get; set; }

        }
        public TimKiemScreen()
        {
            InitializeComponent();

            Database.FILE_STREAM.LoadChucNangUser(ref MainWindow.ChucNangGuess, ref MainWindow.ChucNangBQL,
                ref MainWindow.ChucNangBTC);

            int typeAccount = MainWindow.ACCOUNTSOFT.m_Type;

            if (typeAccount == 0)
                btnChiTiet.Visibility = Visibility.Hidden;
        }

        private void lvCauThu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GridViewColumn_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }

        private void cbbDoiTuong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //chi tiết cầu thủ được chọn
        List<V_CAUTHU> ct = new List<V_CAUTHU>();
        List<V_HUANLUYENVIEN> hlv = new List<V_HUANLUYENVIEN>();
        List<V_TRONGTAI> tt = new List<V_TRONGTAI>();


        public string LoaiBoKhoangTrang(string st)
        {
            string s = st;
            s = s.Trim();       // loai bỏ khoảng trắng 2 đầu

            //loại bỏ khoảng trắng thừa ở giữa
            int l = s.Length;
            for(int i = 0; i < l; i++)
            {
                if (s[i] == 32 && s[i + 1] == 32)
                {
                    s = s.Remove(i, 1);
                    l--;
                    i--;
                }
            }
            return s;
        }

        private void txbTim_Click(object sender, RoutedEventArgs e)
        {
            string ten;
            ten = txbTenCanTim.Text;

            ten = LoaiBoKhoangTrang(ten);

            int stt = 0;
            List<User> t = new List<User>();

            ct = Business.B_CAUTHU.TraCuuCauThu(ten);

            hlv = Business.B_HUANLUYENVIEN.TraCuuHLV(ten);

            tt = Business.B_TRONGTAI.TraCuuTT(ten);

            //loại bỏ khoảng trắng trong tên cần tìm
            //loại bỏ khoảng trắng 2 đầu
            

            //Bắt lỗi
            //Chưa nhập tên cần tìm và Chưa chọn đối tượng cần tìm
            if (txbTenCanTim.Text == "")
            {
                if(cbbDoiTuong.Text == "Cầu thủ")
                {
                    List<V_CAUTHU> ct = B_CAUTHU.DanhSachCT();

                    foreach (V_CAUTHU p in ct)
                    {
                        t.Add(new User()
                        {
                            STT = ++stt,
                            MA = p.m_MaCauThu,
                            HOTEN = p.m_HoTen,
                            NGAYSINH = p.m_NgaySinh.ToString()
                        });
                    }
                    lvTimKiem.ItemsSource = t;
                }
                else if (cbbDoiTuong.Text == "Huấn luyện viên")
                {
                    List<V_HUANLUYENVIEN> ct = B_HUANLUYENVIEN.DanhSacHLV();

                    foreach (V_HUANLUYENVIEN p in ct)
                    {
                        t.Add(new User()
                        {
                            STT = ++stt,
                            MA = p.m_MaHLV,
                            HOTEN = p.m_HoTen,
                            NGAYSINH = p.m_NgaySinh.ToString()
                        });
                    }
                    lvTimKiem.ItemsSource = t;
                }
                else if (cbbDoiTuong.Text == "Trọng tài")
                {
                    List<V_TRONGTAI> ct = B_TRONGTAI.DanhSachTrongTai();

                    foreach (V_TRONGTAI p in ct)
                    {
                        t.Add(new User()
                        {
                            STT = ++stt,
                            MA = p.m_MaTrongTai,
                            HOTEN = p.m_HoTen,
                            NGAYSINH = p.m_NgaySinh.ToString()
                        });
                    }
                    lvTimKiem.ItemsSource = t;
                }
                else if (cbbDoiTuong.Text == "Đội bóng")
                {
                     
                    

                        //if (e.Row.RowType == DataControlRowType.Header)
                        //{
                        //    e.Row.Cells[0].Text = "Date";
                        //}

                        grTimKiem.Columns[1].Header = "Mã đội bóng";
                        grTimKiem.Columns[2].Header = "Tên đội bóng";
                        grTimKiem.Columns[3].Header = "Sân nhà";

                        btnChiTiet.Visibility = Visibility.Hidden;

                        List<V_DOIBONG> dsdb = new List<V_DOIBONG>();
                        dsdb = Business.B_DOIBONG.DanhSachDB();
                        foreach (V_DOIBONG p in dsdb)
                        {
                            t.Add(new User()
                            {
                                STT = ++stt,
                                MA = p.m_MaDoiBong,
                                HOTEN = p.m_TenDoiBong,
                                NGAYSINH = p.m_TenSanNha
                            });
                        
                    }
                    lvTimKiem.ItemsSource = t;
                }
                //MessageBox.Show("Chưa điền thông tin đầy đủ");
                return;
            }

            if (cbbDoiTuong.Text == "Cầu thủ" && QuanLyDoiBong.Business.B_CAUTHU.KiemTraCTTonTai(ten) == true)
            {

                foreach (V_CAUTHU p in ct)
                {
                    t.Add(new User()
                    {
                        STT = ++stt,
                        MA = p.m_MaCauThu,
                        HOTEN = p.m_HoTen,
                        NGAYSINH = p.m_NgaySinh.ToString()
                    });
                }
            }
            else if (cbbDoiTuong.Text == "Huấn luyện viên" && QuanLyDoiBong.Business.B_HUANLUYENVIEN.KiemTraHLVTonTai(ten) == true)
            {

                foreach (V_HUANLUYENVIEN p in hlv)
                {
                    t.Add(new User()
                    {
                        STT = ++stt,
                        MA = p.m_MaHLV,
                        HOTEN = p.m_HoTen,
                        NGAYSINH = p.m_NgaySinh.ToString()
                    });
                }
            }
            else if (cbbDoiTuong.Text == "Trọng tài" && QuanLyDoiBong.Business.B_TRONGTAI.KiemTraTTTonTai(ten) == true)
            {
                foreach (V_TRONGTAI p in tt)
                {
                    t.Add(new User()
                    {
                        STT = ++stt,
                        MA = p.m_MaTrongTai,
                        HOTEN = p.m_HoTen,
                        NGAYSINH = p.m_NgaySinh.ToString()
                    });
                }
            }
            else
            {
                MessageBox.Show("Tên cần tìm không tồn tại");
            }

            lvTimKiem.ItemsSource = t;
        }

        private void btnChiTiet_Click(object sender, RoutedEventArgs e)
        {
            if (lvTimKiem.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn đối tượng");
            }
            else
            {
                string ma = (lvTimKiem.SelectedItem as User).MA;
                TraCuuThongTin view = new TraCuuThongTin();
                if (cbbDoiTuong.Text == "Cầu thủ")
                {
                    foreach (V_CAUTHU p in ct)
                    {
                        if (ma.CompareTo(p.m_MaCauThu) == 0)
                        {
                            view.SenderCT(p);
                        }
                    }
                }
                else if (cbbDoiTuong.Text == "Huấn luyện viên")
                {
                    foreach (V_HUANLUYENVIEN p in hlv)
                    {
                        if (ma.CompareTo(p.m_MaHLV) == 0)
                        {
                            view.SenderHLV(p);
                        }
                    }
                }
                else if (cbbDoiTuong.Text == "Trọng tài")
                {
                    foreach (V_TRONGTAI p in tt)
                    {
                        if (ma.CompareTo(p.m_MaTrongTai) == 0)
                        {
                            view.SenderTT(p);
                        }
                    }
                }

                view.Show();
            }

        }

    }
}