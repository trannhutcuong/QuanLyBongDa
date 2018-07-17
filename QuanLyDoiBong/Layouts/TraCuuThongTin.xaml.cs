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
using System.IO;
using QuanLyDoiBong.Business;
using QuanLyDoiBong.Views;
using QuanLyDoiBong.Layouts;
using QuanLyDoiBong.Database;

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for TraCuuThongTin.xaml
    /// </summary>
    public partial class TraCuuThongTin : Window
    {
        public delegate void ChiTietCT(V_CAUTHU cauthu);
        public delegate void ChiTietHLV(V_HUANLUYENVIEN hlv);
        public delegate void ChiTietTT(V_TRONGTAI tt);
        public ChiTietCT SenderCT;
        public ChiTietHLV SenderHLV;
        public ChiTietTT SenderTT;

        int m = -1;

        public TraCuuThongTin()
        {

            InitializeComponent();

            SenderCT = new ChiTietCT(SetCT);
            SenderHLV = new ChiTietHLV(SetHLV);
            SenderTT = new ChiTietTT(SetTT);
        }

        private void SetCT(V_CAUTHU cauthu)
        {
            txbMaCT.Text = cauthu.m_MaCauThu;
            txbMaDB.Text = cauthu.m_MaDB;
            txbHoTen.Text = cauthu.m_HoTen;
            txbDiaChi.Text = cauthu.m_DiaChi;
            txbNamKinhNghiem.Text = cauthu.m_SoNamKinhNghiem.ToString();
            txbSoBanThang.Text = cauthu.m_SoBanThang.ToString();
            txbChucVu.Text = cauthu.m_ViTri;
            txbQueQuan.Text = cauthu.m_QuocTich;

            DateTime myDate = cauthu.m_NgaySinh;
            dtNgaySinh.DisplayDate = myDate;
            dtNgaySinh.SelectedDate = myDate;

            // load hinh cau thu
            //string path = cauthu.m_AVT;
            //BitmapImage bmImage = Database.FILE_STREAM.LoadImage(path);
            //if (bmImage != null)
            //{
            //    imgAvatar.Source = bmImage;
            //}
            string fileName = Environment.CurrentDirectory + "/" + cauthu.m_AVT;
            if (File.Exists(fileName))
            {
                imgAvatar.Source = Database.FILE_STREAM.LoadImage(fileName);
            }
           
            m = 0;
        }

        private void SetHLV(V_HUANLUYENVIEN hlv)
        {
            lbMa.Content = "Mã HLV: ";
            txbMaCT.Text = hlv.m_MaHLV;
            txbMaDB.Text = hlv.m_MaDB;
            txbHoTen.Text = hlv.m_HoTen;
            txbDiaChi.Text = hlv.m_DiaChi;
            txbQueQuan.Text = hlv.m_QuocTich;
            txbNamKinhNghiem.Text = hlv.m_SoNamKinhNghiem.ToString();
            lbSoBanThang.Content = "Loại HLV:";
            txbSoBanThang.Text = hlv.m_LoaiHLVStr;
            lbChucVu.Content = "Tuổi: ";

            DateTime myDate = hlv.m_NgaySinh;
            dtNgaySinh.DisplayDate = myDate;
            dtNgaySinh.SelectedDate = myDate;

            string fileName = Environment.CurrentDirectory + "/" + hlv.m_AVT;
            if (File.Exists(fileName))
            {
                imgAvatar.Source = Database.FILE_STREAM.LoadImage(fileName);
            }
            lbChucVu.Visibility = Visibility.Hidden;
            txbChucVu.Visibility = Visibility.Hidden;

            m = 1;
        }

        private void SetTT(V_TRONGTAI tt)
        {
            lbMa.Content = "Mã TT: ";
            txbMaCT.Text = tt.m_MaTrongTai;
            txbHoTen.Text = tt.m_HoTen;

            DateTime myDate = tt.m_NgaySinh;
            dtNgaySinh.DisplayDate = myDate;
            dtNgaySinh.SelectedDate = myDate;

            txbDiaChi.Text = tt.m_DiaChi;
            txbQueQuan.Text = tt.m_QuocTich;
            txbNamKinhNghiem.Text = tt.m_SoNamKinhNghiem.ToString();
            lbSoBanThang.Content = "Loại TT:";

            if (tt.m_LoaiTrongTai == true)
                txbSoBanThang.Text = "Chính";
            else
                txbSoBanThang.Text = "Phụ";
            string fileName = Environment.CurrentDirectory + "/" + tt.m_AVT;
            if (File.Exists(fileName))
            {
                imgAvatar.Source = Database.FILE_STREAM.LoadImage(fileName);
            }
            lbMaDB.Visibility = Visibility.Hidden;
            txbMaDB.Visibility = Visibility.Hidden;
            lbChucVu.Visibility = Visibility.Hidden;
            txbChucVu.Visibility = Visibility.Hidden;

            //load hinh cau thu

            m = 2;
        }

        private void btnThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Bạn có muốn xóa không?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                if (m == 0)
                {
                    Business.B_CAUTHU.XoaCauThu(txbMaCT.Text);
                }
                else if (m == 1)
                {
                    Business.B_HUANLUYENVIEN.XoaHLV(txbMaCT.Text);
                }
                else if (m == 2)
                {
                    Business.B_TRONGTAI.XoaTrongTai(txbMaCT.Text);
                }
                MessageBox.Show("Thành công!");
                this.Close();
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (m == 0)
            {
                V_CAUTHU cauthu = new V_CAUTHU();

                cauthu.m_MaCauThu = txbMaCT.Text;
                cauthu.m_MaDB = txbMaDB.Text;
                cauthu.m_HoTen = txbHoTen.Text;

                DateTime myDate = dtNgaySinh.DisplayDate;
                cauthu.m_NgaySinh = myDate;
                cauthu.m_DiaChi = txbDiaChi.Text;

                int x = Int32.Parse(txbNamKinhNghiem.Text);
                cauthu.m_SoNamKinhNghiem = (byte)x;

                int y = Int32.Parse(txbSoBanThang.Text);
                cauthu.m_SoBanThang = (byte)y;
                cauthu.m_ViTri = txbChucVu.Text;
                cauthu.m_QuocTich = txbQueQuan.Text;

                String file_name = txbMaCT.Text.Trim() + ".jpg";
                String folder_name = "IMAGESQLBD/CAUTHU";
                string folder_file_name = folder_name + "/" + file_name;
                String path = Environment.CurrentDirectory + "/" + folder_file_name;
                cauthu.m_AVT = folder_file_name;

                // xoa file da ton tai 
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                BitmapSource bm = (BitmapSource)imgAvatar.Source;
                Database.FILE_STREAM.LuuImage(path, bm);

                Business.B_CAUTHU.ChinhSuaCauThu(cauthu);
                MessageBox.Show("Thành công!");
            }
            else if (m == 1)
            {
                V_HUANLUYENVIEN hlv = new V_HUANLUYENVIEN();

                hlv.m_MaHLV = txbMaCT.Text;
                hlv.m_MaDB = txbMaDB.Text;
                hlv.m_HoTen = txbHoTen.Text;

                DateTime myDate = dtNgaySinh.DisplayDate;
                hlv.m_NgaySinh = myDate;
                hlv.m_DiaChi = txbDiaChi.Text;

                int x = Int32.Parse(txbNamKinhNghiem.Text);
                hlv.m_SoNamKinhNghiem = (byte)x;

                if (txbSoBanThang.Text.CompareTo("Chính") == 0)
                {
                    hlv.m_LoaiHLV = true;
                }
                else { hlv.m_LoaiHLV = false; }

                hlv.m_QuocTich = txbQueQuan.Text;

                String file_name = txbMaCT.Text.Trim() + ".jpg";
                String folder_name = "IMAGESQLBD/CAUTHU";
                string folder_file_name = folder_name + "/" + file_name;
                String path = Environment.CurrentDirectory + "/" + folder_file_name;
                hlv.m_AVT = folder_file_name;

                // xoa file da ton tai 
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                BitmapSource bm = (BitmapSource)imgAvatar.Source;
                Database.FILE_STREAM.LuuImage(path, bm);

                Business.B_HUANLUYENVIEN.ChinhSuaHLV(hlv);
                MessageBox.Show("Thành công!");
            }
            else if (m == 2)
            {
                V_TRONGTAI tt = new V_TRONGTAI();

                tt.m_MaTrongTai = txbMaCT.Text;
                tt.m_HoTen = txbHoTen.Text;

                DateTime myDate = dtNgaySinh.DisplayDate;
                tt.m_NgaySinh = myDate;
                tt.m_DiaChi = txbDiaChi.Text;

                int x = Int32.Parse(txbNamKinhNghiem.Text);
                tt.m_SoNamKinhNghiem = (byte)x;

                if (txbSoBanThang.Text.CompareTo("Chính") == 0)
                {
                    tt.m_LoaiTrongTai = true;
                }
                else { tt.m_LoaiTrongTai = false; }

                tt.m_QuocTich = txbQueQuan.Text;

                String file_name = txbMaCT.Text.Trim() + ".jpg";
                String folder_name = "IMAGESQLBD/CAUTHU";
                string folder_file_name = folder_name + "/" + file_name;
                String path = Environment.CurrentDirectory + "/" + folder_file_name;
                tt.m_AVT = folder_file_name;

                // xoa file da ton tai 
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                BitmapSource bm = (BitmapSource)imgAvatar.Source;
                Database.FILE_STREAM.LuuImage(path, bm);

                Business.B_TRONGTAI.ChinhSuaTrongTai(tt);
                MessageBox.Show("Thành công!");
            }
        }

        private void tbnThayDoi_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage bmImage = Database.FILE_STREAM.LoadDuLieuOPImage();
            if (bmImage != null)
            {
                imgAvatar.Source = bmImage;
            }
        }
    }
}
