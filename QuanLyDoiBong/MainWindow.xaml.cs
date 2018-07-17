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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyDoiBong.Layouts;
using QuanLyDoiBong.Views;

namespace QuanLyDoiBong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public V_QUYDINH QUYDINHGIAIBONG = Business.B_QUYDINH.LayDuLieuQuyDinh();
        static public List<V_LOAIBANTHANG> QUYDINHLOAIBANTHANG = Business.B_LOAIBANTHANG.LayDuLieuLoaiBanThang();
        static public V_ACCOUNT ACCOUNTSOFT = new V_ACCOUNT();
        static public List<int> ChucNangGuess = new List<int>();
        static public List<int> ChucNangBQL = new List<int>();
        static public List<int> ChucNangBTC = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            
            
            Login ManHinhDangNhap = new Login();
            ManHinhDangNhap.Show();
            this.Close();
        }
    }
}
