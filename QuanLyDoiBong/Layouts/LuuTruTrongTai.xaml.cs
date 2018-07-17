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
using QuanLyDoiBong.Business;
using System.Collections.ObjectModel;

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for LuuTruTrongTai.xaml
    /// </summary>
    public partial class LuuTruTrongTai : Window
    {
        private V_TRONGTAI TrongTai;

        public LuuTruTrongTai()
        {
            InitializeComponent();
            TrongTai = new V_TRONGTAI();


            List<string> dsQuocGia = Business.B_XML.readXMLCOUNTRY();
            ObservableCollection<string> list = new ObservableCollection<string>();
            foreach (string country in dsQuocGia)
            {
                list.Add(country);
            }
            this.cbbQuocTich.ItemsSource = list;

            // Databinding cho control
            this.DataContext = TrongTai;
        }

        private void btnThemHinhTrongTai_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bmImage = Database.FILE_STREAM.LoadDuLieuOPImage();
            if (bmImage != null)
            {
                imgAVT.Source = bmImage;
            }
        }

        private void btnThemTrongTai_Click(object sender, RoutedEventArgs e)
        {
            //Kiểm tra tuổi trọng tài
            bool KiemTraTuoiTrongTai(V_TRONGTAI trongtai)
            {
                var today = DateTime.Today;
                int age = today.Year - trongtai.m_NgaySinh.Year;
                if (age < 25)
                    return false;
                return true;
            }



            //Bắt lỗi điền không đầy đủ thông tin
            if (TrongTai.m_MaTrongTai == "" || TrongTai.m_HoTen == "" ||
                TrongTai.m_DiaChi == "" || TrongTai.m_QuocTich == "")
                MessageBox.Show("Vui lòng điền đủ thông tin trọng tài !!!");
            else
            {
                if (KiemTraTuoiTrongTai(TrongTai) == false)
                {
                    MessageBox.Show("Tuổi trọng tài phải từ 25 tuổi trở lên");
                }
                else
                {
                    if (cbbLoaiTrongTai.Text == "Chính")
                        TrongTai.m_LoaiTrongTai = true;
                    else
                        TrongTai.m_LoaiTrongTai = false;

                    //Tên đường dẫn avatar trọng tài
                    String file_name = TrongTai.m_MaTrongTai + ".jpg";
                    String folder_name = "IMAGESQLBD/TRONGTAI";
                    string folder_file_name = folder_name + "/" + file_name;
                    String path = Environment.CurrentDirectory + "/" + folder_file_name;
                    TrongTai.m_AVT = folder_file_name;

                    // Kiểm tra mã trọng tài có bị trùng không
                    if (Business.B_TRONGTAI.ThemTrongTai(TrongTai))
                    {
                        BitmapSource bm = (BitmapSource)imgAVT.Source;
                        Database.FILE_STREAM.LuuImage(path, bm);
                        MessageBox.Show("Thêm thành công trọng tài !!!!");
                    }
                    else
                        MessageBox.Show("Mã trọng tài đã tồn tại vui lòng kiểm tra lại");
                }
                
            }
        }
        // Event nhấn phím ENTER
        private void EnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("Nhấn nút <Thêm> để thêm trọng tài!");
            }
        }
        
    }
}