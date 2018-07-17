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
    /// Interaction logic for DKDB_DangKyCauThu.xaml
    /// </summary>
    public partial class DKDB_DangKyCauThu : Window
    {
        private ObservableCollection<V_CAUTHU> data_DsCauThu;
        private V_CAUTHU CauThu;

        public delegate void MyDelegateType(V_CAUTHU a_CauThu);
        public event MyDelegateType Handler;

        public DKDB_DangKyCauThu(ObservableCollection<V_CAUTHU> data)
        {
            InitializeComponent();
            CauThu = new V_CAUTHU();
            data_DsCauThu = new ObservableCollection<V_CAUTHU>();
            for(int i=0; i<data.Count; i++)
            {
                data_DsCauThu.Add(data[i].Clone());
            }


            List<string> dsQuocGia = Business.B_XML.readXMLCOUNTRY();
            ObservableCollection<string> list = new ObservableCollection<string>();
            foreach (string country in dsQuocGia)
            {
                list.Add(country);
            }
            this.cbbQuocTich.ItemsSource = list;

            // Databinding cho control
            this.DataContext = CauThu;
        }

        private void btnThemHinhCauThu_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bmImage = Database.FILE_STREAM.LoadDuLieuOPImage();
            if (bmImage != null)
            {
                imgAVT.Source = bmImage;
            }

        }

        private void btnThemCauThu_Click(object sender, RoutedEventArgs e)
        {
            
            CauThu.m_NgaySinhStr = CauThu.m_NgaySinh.ToString("dd/MM/yyyy");

            V_CAUTHU newCauThu = CauThu.Clone();

            // XỬ LÝ NGOẠI LỆ
            int retCode = Business.B_CAUTHU.KiemTraCauThu(data_DsCauThu, newCauThu);
            if(retCode != 0)
            {
                if(retCode == 1)
                {
                    MessageBox.Show("VUI LÒNG ĐIỀN ĐẨY ĐỦ THÔNG TIN", "ERROR");
                }
                else if (retCode == 2)
                {
                    MessageBox.Show("MÃ CẦU THỦ NÀY ĐÃ TỒN TẠI", "ERROR");
                }
                else if (retCode == 3)
                {
                    string err = "TUỔI CẦU THỦ NẰM NGOÀI GIỚI HẠN CHO PHÉP CỦA GIẢI ĐẤU [" +
                        QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_AGE_MIN + " - " +
                        QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_AGE_MAX + "]";
                    MessageBox.Show(err, "ERROR");
                }
                else if(retCode == 4)
                {
                    MessageBox.Show("SỐ LƯỢNG CẦU THỦ NGOẠI VƯỢT SỐ LƯỢNG CẦU THỦ CHO PHÉP: "+ 
                        QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_FOREIGN_MAX + " !", "ERROR");
                }
                else if (retCode == 5)
                {
                    MessageBox.Show("SỐ NĂM KINH NGHIỆM PHẢI LỚN HƠN 0", "ERROR");
                }
                else if (retCode == 6)
                {
                    MessageBox.Show("SỐ NĂM KINH NGHIỆM LỚN HƠN TUỔI THẬT", "ERROR");
                }
                return;
            }
            if (data_DsCauThu.Count == MainWindow.QUYDINHGIAIBONG.m_PLAYER_NUM_MAX)
            {
                MessageBox.Show("SỐ LƯỢNG CẦU THỦ ĐÃ TỐI ĐA", "ERROR");
                return;
            }
                      
            String file_name = CauThu.m_MaCauThu + ".jpg";
            String folder_name = "IMAGESQLBD/CAUTHU";
            string folder_file_name = folder_name + "/" + file_name;
            String path = Environment.CurrentDirectory + "/" + folder_file_name;
            CauThu.m_AVT = folder_file_name;
            BitmapSource bm = (BitmapSource)imgAVT.Source;
            Database.FILE_STREAM.LuuImage(path, bm);

            newCauThu.m_AVT = CauThu.m_AVT;

            if (Handler != null)
            {
                Handler(newCauThu);
            }
            
            data_DsCauThu.Add(newCauThu);
            MessageBox.Show("Thêm thành công!", "SUCCESS");
            this.setDefaultValue();
            txtMaCauThu.Focus();
        }

        private void btnThoatCauThu_Click(object sender, RoutedEventArgs e)
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
            if (CauThu.m_MaCauThu != "" || CauThu.m_HoTen != "" || CauThu.m_DiaChi != ""
                || CauThu.m_SoNamKinhNghiem != 0 || CauThu.m_AVT != "")
                return false;
            return true;
        }

        private void setDefaultValue()
        {
            txtMaCauThu.Text = txtHoTen.Text = txtDiaChi.Text = "";
            txtSoNamKinhNghiem.Text = "0";
            CauThu.m_AVT = "";
            CauThu.m_MaCauThu = CauThu.m_HoTen = CauThu.m_DiaChi = "";
            CauThu.m_SoNamKinhNghiem = 0;
            cbbViTri.SelectedIndex = 0;
            String path = Environment.CurrentDirectory + "/IMAGESQLBD/avt.jpg";
            BitmapImage img = Database.FILE_STREAM.LoadImage(path);
            if(img != null)
            {
                imgAVT.Source = img;
            }
        }

        private void EnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.btnThemCauThu_Click(sender, e);
            }
        }
    }
}
