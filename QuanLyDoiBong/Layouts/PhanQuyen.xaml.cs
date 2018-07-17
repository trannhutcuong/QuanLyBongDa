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

namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for PhanQuyen.xaml
    /// </summary>
    public partial class PhanQuyen : Window
    {
        List<CheckBox> BTCList = new List<CheckBox>();
        List<CheckBox> BQLList = new List<CheckBox>();
        List<CheckBox> GList = new List<CheckBox>();
        public PhanQuyen()
        {
            InitializeComponent();
            // Gán control vào List để dễ quản lý
            NapControlCheckBox();

            LoadControlTuDuLieuChucNangChuongTrinh();

        }

        private void NapControlCheckBox()
        {
            BTCList.Add(BTC0);
            BTCList.Add(BTC1);
            BTCList.Add(BTC2);
            BTCList.Add(BTC3);
            BTCList.Add(BTC4);
            BTCList.Add(BTC5);
            BTCList.Add(BTC6);
            BTCList.Add(BTC7);
            BTCList.Add(BTC8);
            BTCList.Add(BTC9);

            BQLList.Add(BQL0);
            BQLList.Add(BQL1);
            BQLList.Add(BQL2);
            BQLList.Add(BQL3);
            BQLList.Add(BQL4);
            BQLList.Add(BQL5);
            BQLList.Add(BQL6);
            BQLList.Add(BQL7);
            BQLList.Add(BQL8);
            BQLList.Add(BQL9);

            GList.Add(G0);
            GList.Add(G1);
            GList.Add(G2);
            GList.Add(G3);
            GList.Add(G4);
            GList.Add(G5);
            GList.Add(G6);
            GList.Add(G7);
            GList.Add(G8);
            GList.Add(G9);
        }

        private void LoadControlTuDuLieuChucNangChuongTrinh()
        {
            for(int i=0; i<MainWindow.ChucNangGuess.Count; i++)
            {
                GList[MainWindow.ChucNangGuess[i]].IsChecked = true;
            }

            for (int i = 0; i < MainWindow.ChucNangBQL.Count; i++)
            {
                BQLList[MainWindow.ChucNangBQL[i]].IsChecked = true;
            }

            for (int i = 0; i < MainWindow.ChucNangBTC.Count; i++)
            {
                BTCList[MainWindow.ChucNangBTC[i]].IsChecked = true;
            }
        }

        private void btnLuuPhanQuyen_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn lưu hay không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                this.Close();
            }
            else
            {
                //do yes stuff

                MainWindow.ChucNangBTC.Clear();
                for(int i=0; i<BTCList.Count; i++)
                {
                    if(BTCList[i].IsChecked == true)
                    {
                        MainWindow.ChucNangBTC.Add(i);
                    }
                }

                MainWindow.ChucNangBQL.Clear();
                for(int i=0; i<BQLList.Count; i++)
                {
                    if(BQLList[i].IsChecked == true)
                    {
                        MainWindow.ChucNangBQL.Add(i);
                    }
                }

                MainWindow.ChucNangGuess.Clear();
                for(int i=0; i < GList.Count;  i++)
                {
                    if(GList[i].IsChecked == true)
                    {
                        MainWindow.ChucNangGuess.Add(i);
                    }
                }      
                Database.FILE_STREAM.LuuChucNangUser(MainWindow.ChucNangGuess,
                    MainWindow.ChucNangBQL, MainWindow.ChucNangBTC);
                MessageBox.Show("Đã lưu thành công!", "SUCCESS");
                this.Close();

            }
        }

        private void EnterEvent(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnLuuPhanQuyen_Click(sender, e);
            }
        }
    }
}
