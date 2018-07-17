using Microsoft.Win32;
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

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for DKDB_DangKyHLV.xaml
    /// </summary>
    public partial class DKDB_DangKyHLV : Window
    {

        private ObservableCollection<V_HUANLUYENVIEN> data_DsHLV;
        private V_HUANLUYENVIEN HLV;

        public delegate void MyDelegateType(V_HUANLUYENVIEN a_CauThu);
        public event MyDelegateType Handler;

        public DKDB_DangKyHLV(ObservableCollection<V_HUANLUYENVIEN> data)
        {
            InitializeComponent();
            HLV = new V_HUANLUYENVIEN();
            data_DsHLV = new ObservableCollection<V_HUANLUYENVIEN>();
            for (int i = 0; i < data.Count; i++)
            {
                data_DsHLV.Add(data[i].Clone());
            }

            List<string> dsQuocGia = Business.B_XML.readXMLCOUNTRY();
            ObservableCollection<string> list = new ObservableCollection<string>();
            foreach (string country in dsQuocGia)
            {
                list.Add(country);
            }
            this.cbbQuocTich.ItemsSource = list;

            // Databinding cho control
            this.DataContext = HLV;
        }

        private void btnThemHinhHLV_Click(object sender, RoutedEventArgs e)
        {

            BitmapImage bmImage = Database.FILE_STREAM.LoadDuLieuOPImage();
            if (bmImage != null)
            {
                imgAVT.Source = bmImage;
            }
        }

        private void btnThemHLV_Click(object sender, RoutedEventArgs e)
        {
            // Cập nhật dữ liệu
            HLV.m_NgaySinhStr = HLV.m_NgaySinh.ToString("dd/MM/yyyy");
            if (HLV.m_LoaiHLVStr == "Chính")
            {
                HLV.m_LoaiHLV = true;
            }
            else if (HLV.m_LoaiHLVStr == "Phụ")
            {
                HLV.m_LoaiHLV = false;
            }

            V_HUANLUYENVIEN newHLV = HLV.Clone();

            // XỬ LÝ NGOẠI LỆ
            int retCode = Business.B_HUANLUYENVIEN.KiemTraHLV(data_DsHLV, newHLV);
            if (retCode != 0)
            {
                if (retCode == 1)
                {
                    MessageBox.Show("VUI LÒNG ĐIỀN ĐẨY ĐỦ THÔNG TIN", "ERROR");
                }
                else if (retCode == 2)
                {
                    MessageBox.Show("MÃ HUẤN LUYỆN VIÊN NÀY ĐÃ TỒN TẠI", "ERROR");
                }
                else if (retCode == 3)
                {
                    string err = "CHỈ NHẬN HLV TRÊN 25 TUỔI";
                    MessageBox.Show(err, "ERROR");
                }
                else if(retCode == 4)
                {
                    MessageBox.Show("SỐ NĂM KINH NGHIỆM PHẢI LỚN HƠN 0", "ERROR");
                }
                else if (retCode == 5)
                {
                    MessageBox.Show("SỐ NĂM KINH NGHIỆM LỚN HƠN TUỔI THẬT", "ERROR");
                }
                return;
            }



            // Lưu hình
            String file_name = HLV.m_MaHLV + ".jpg";
            String folder_name = "IMAGESQLBD/HUANLUYENVIEN";
            string folder_file_name = folder_name + "/" + file_name;
            String path = Environment.CurrentDirectory + "/" + folder_file_name;
            HLV.m_AVT = folder_file_name;
            BitmapSource bm = (BitmapSource)imgAVT.Source;
            Database.FILE_STREAM.LuuImage(path, bm);

            newHLV.m_AVT = HLV.m_AVT;

            if (Handler != null)
            {
                Handler(newHLV);
            }

            data_DsHLV.Add(newHLV);
            MessageBox.Show("Thêm thành công!", "SUCCESS");
            this.setDefaultValue();
            txtMaHLV.Focus();

        }

        private void btnThoatHLV_Click(object sender, RoutedEventArgs e)
        {
            if (this.isDataSaved() == false)
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


        private bool isDataSaved()
        {
            
            if (HLV.m_MaHLV != "" || HLV.m_HoTen != "" || HLV.m_DiaChi != ""
                || HLV.m_SoNamKinhNghiem != 0 || HLV.m_AVT != "")
                return false;
            return true;
        }

        private void setDefaultValue()
        {
            txtMaHLV.Text = txtHoTen.Text = txtDiaChi.Text = "";  
            txtSoNamKinhNghiem.Text = "0";
            HLV.m_MaHLV = HLV.m_HoTen = HLV.m_DiaChi = "";
            HLV.m_SoNamKinhNghiem = 0;
            HLV.m_AVT = "";

            cbbLoaiHLV.SelectedIndex = 0;
            String path = Environment.CurrentDirectory + "/IMAGESQLBD/avt.jpg";
            BitmapImage img = Database.FILE_STREAM.LoadImage(path);
            if (img != null)
            {
                imgAVT.Source = img;
            }          
        }

        private void EventEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnThemHLV_Click(sender, e);
            }
        }
    }
}
