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

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for DoiQuyDinh.xaml
    /// </summary>
    public partial class DoiQuyDinh : Window
    {
        public V_QUYDINH QuyDinh = Business.B_QUYDINH.LayDuLieuQuyDinh();
        List<V_LOAIBANTHANG> items = new List<V_LOAIBANTHANG>();

        public DoiQuyDinh()
        {
            InitializeComponent();
                   
            items = Business.B_LOAIBANTHANG.LayDuLieuLoaiBanThang();
            lvLBT.ItemsSource = items;
            // Databinding cho control
            this.DataContext = QuyDinh;
        }


        public class LoaiBanThang
        {
            public string m_Ma { get; set; }
            public string m_LBT { get; set; }
        }

        //Click nút thay đổi quy định
        private void btnThayDoiQD_Click(object sender, RoutedEventArgs e)
        {
            //Bắt lỗi người dùng nhập

            // Điểm Thắng > Hòa > Thua
            if (QuyDinh.m_SCORE_THANG <= QuyDinh.m_SCORE_HOA || QuyDinh.m_SCORE_HOA <= QuyDinh.m_SCORE_THUA)
                MessageBox.Show("Lưu ý: Điểm trận THẮNG > HÒA > THUA");
            else
               { 
                    // Số cầu thủ MAX > MIN
                    if (QuyDinh.m_PLAYER_NUM_MAX < QuyDinh.m_PLAYER_NUM_MIN || QuyDinh.m_PLAYER_NUM_MIN < 0)
                        MessageBox.Show("Lưu ý: Số lượng cầu thủ TỐI ĐA > TỐI THIỂU > 11");
                    else
                    {
                        // Số tuổi cầu thủ MAX > MIN
                        if (QuyDinh.m_PLAYER_AGE_MAX < QuyDinh.m_PLAYER_AGE_MIN || QuyDinh.m_PLAYER_AGE_MIN < 16)
                            MessageBox.Show("Lưu ý: Số tuổi cầu thủ TỐI ĐA > TỐI THIỂU > 15");
                        else
                        {
                            if (QuyDinh.m_PLAYER_FOREIGN_MAX > QuyDinh.m_PLAYER_NUM_MAX)
                            {
                                MessageBox.Show("Lưu ý: Số lượng cầu thủ NGOẠI < Số lượng cầu thủ TỐI ĐA");
                            }
                            else
                            {
                                //Sau khi kiểm tra xong nhét vào 
                                if (Business.B_QUYDINH.ThayDoiQuyDinh(QuyDinh) == true)
                                {
                                    MessageBox.Show("Thay đổi quy định thành công !");
                                    QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG = QuyDinh;
                                }
                                else
                                {
                                    MessageBox.Show("Thay đổi quy định không thành công, vui lòng kiểm tra lại!");
                                }
                            }
                            
                        }
                    }
                }
                
        }


        //Nhấn nút THÊM ở tab Bàn Thắng
        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
        }

        //Nhấn nút XÓA ở tab Bàn Thắng
        private void btnDEL_Click(object sender, RoutedEventArgs e)
        {
            if (lvLBT.SelectedItem == null)
            {
                MessageBox.Show("Chưa chọn loại bàn thắng cần xóa.");
            }
            else
            {
                char ma = (lvLBT.SelectedItem as V_LOAIBANTHANG).MABT;
                Database.DB_DELETE.XoaLoaiBanThang(ma);
                items = Business.B_LOAIBANTHANG.LayDuLieuLoaiBanThang();
                lvLBT.ItemsSource = items;
                MessageBox.Show("Xóa thành công loại bàn thắng.");
            }
        }

        //Nhấn nút SỬA ở tab Bàn Thắng
        private void btnEDI_Click(object sender, RoutedEventArgs e)
        {
        }


        private void EnterEvent(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                MessageBox.Show("Nhấn nút THAY ĐỔI để hoàn thành thay đổi quy định giải !");
            }
        }
    }
}
