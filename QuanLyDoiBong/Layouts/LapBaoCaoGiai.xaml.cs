using QuanLyDoiBong.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;



namespace QuanLyDoiBong.Layouts
{
    /// <summary>
    /// Interaction logic for LapBaoCaoGiai.xaml
    /// </summary>
    public partial class LapBaoCaoGiai : Window
    {
        List<V_DOIBONG> listDoiBong = Business.B_DOIBONG.LayDanhSachDoiBong();   // Lấy danh sách đội bóng đã sắp xếp thứ hạng
        V_QUYDINH quyDinh = QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG;

        List<V_CAUTHU> listCauThu = Business.B_CAUTHU.LayDanhSachCauThuGhiBan(); // Lấy danh sách cầu thủ ghi bàn
        

        public LapBaoCaoGiai()
        {
            InitializeComponent();
            lvBangXepHang.ItemsSource = listDoiBong;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvBangXepHang.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("m_Hang", ListSortDirection.Ascending));                   // Sort listview tăng dần theo thứ hạng
            
            lvDSGB.ItemsSource = listCauThu;
            
            CollectionView view1 = (CollectionView)CollectionViewSource.GetDefaultView(lvDSGB.ItemsSource);
            view1.SortDescriptions.Add(new SortDescription("m_SoBanThang", ListSortDirection.Descending));        // Sort listview tăng dần theo số bàn thắng
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            // chỉ lọc ra các file có định dạng Excel
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }

            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                return;
            }

            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    // đặt tên người tạo file
                    p.Workbook.Properties.Author = "BCCD";

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Báo cáo giải";

                    //Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add("BCCD sheet");

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[1];

                    // đặt tên cho sheet
                    ws.Name = "BCCD sheet";
                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 11;
                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    // Tạo danh sách các column header --Cầu thủ
                    string[] arrColumnHeader1 = {
                                                "Tên cầu thủ",
                                                "Đội bóng",
                                                "Số bàn thắng",
                                                "Hạng"
                                               };

                    // Tạo danh sách các column header --Đội bóng
                    string[] arrColumnHeader = {
                                                "Đội",
                                                "Thắng",
                                                "Hòa",
                                                "Thua",
                                                "Hiệu số",
                                                "Điểm",
                                                "Hạng"
                                               };

                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = arrColumnHeader.Count();

                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                    ws.Cells[1, 1].Value = "Bảng Xếp Hạng";
                    ws.Cells[1, 1].Style.Font.Size = 14;

                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    // in đậm
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    // căn giữa
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int colIndex = 1;
                    int rowIndex = 2;

                    //tạo các header từ column header đã tạo từ bên trên
                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Font.Bold = true;

                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;

                        //gán giá trị
                        cell.Value = item;

                        colIndex++;
                    }

                    // lấy ra danh sách UserInfo từ ItemSource của DataGrid
                    List<V_DOIBONG> listBXHExcel = lvBangXepHang.ItemsSource.Cast<V_DOIBONG>().ToList();

                    // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                    foreach (var item in listBXHExcel)
                    {
                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                        colIndex = 1;

                        // rowIndex tương ứng từng dòng dữ liệu
                        rowIndex++;

                        //gán giá trị cho từng cell -- Tên đội   
                        var cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_TenDoiBong;

                        //gán giá trị cho từng cell -- Trận Thắng
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_SoTranThang;

                        //gán giá trị cho từng cell -- Trận Hòa
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_SoTranHoa;

                        //gán giá trị cho từng cell -- Hạng
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_SoTranThua;

                        //gán giá trị cho từng cell -- Hiệu số
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_HieuSo;

                        //gán giá trị cho từng cell -- Điểm
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_TongDiem;

                        //gán giá trị cho từng cell -- Hạng
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_Hang;

                    }

                    colIndex = 1;
                    rowIndex = listBXHExcel.Count() + 5;
                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    countColHeader = arrColumnHeader1.Count();

                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                    ws.Cells[rowIndex, 1].Value = "Danh Sách Cầu Thủ Ghi Bàn";
                    ws.Cells[rowIndex, 1].Style.Font.Size = 14;

                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    countColHeader = arrColumnHeader1.Count();
                    ws.Cells[rowIndex, 1, rowIndex, countColHeader].Merge = true;
                    // in đậm
                    ws.Cells[rowIndex, 1, rowIndex, countColHeader].Style.Font.Bold = true;
                    // căn giữa
                    ws.Cells[rowIndex, 1, rowIndex, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                    rowIndex++;
                    //tạo các header từ column header đã tạo từ bên trên
                    foreach (var item in arrColumnHeader1)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Font.Bold = true;

                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;

                        //gán giá trị
                        cell.Value = item;

                        colIndex++;
                    }

                    // lấy ra danh sách UserInfo từ ItemSource của DataGrid
                    List<V_CAUTHU> listCauThuExcel = lvDSGB.ItemsSource.Cast<V_CAUTHU>().ToList();

                    // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                    foreach (var item in listCauThuExcel)
                    {
                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                        colIndex = 1;

                        // rowIndex tương ứng từng dòng dữ liệu
                        rowIndex++;

                        //gán giá trị cho từng cell -- Họ và tên   
                        var cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_HoTen;

                        //gán giá trị cho từng cell -- Đội bóng
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_MaDB;

                        //gán giá trị cho từng cell -- Số bàn thắng
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_SoBanThang;

                        //gán giá trị cho từng cell -- Hạng
                        cell = ws.Cells[rowIndex, colIndex];
                        //căn chỉnh các border
                        border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;
                        ws.Cells[rowIndex, colIndex++].Value = item.m_STT;


                        // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                        //ws.Cells[rowIndex, colIndex++].Value = item.Birthday.ToShortDateString();
                    }

                    //Lưu file lại
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuất excel thành công!");
            }
            catch (Exception EE)
            {
                MessageBox.Show("Có lỗi khi lưu file!");
            }

        }

        // Event nhấn phím ENTER
        private void EnterEvent(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MessageBox.Show("           Nhấn nút <Xuất Báo Cáo>\nđể xuất báo cáo sang file Excel nhé!");
            }
        }
    }
}
