using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyDoiBong.Database;
using QuanLyDoiBong.Views;

namespace QuanLyDoiBong.Business
{
    public class B_TAIKHOAN
    {
        // Hàm tìm tài khoản trong database
        public static int TimTaiKhoan(string tenTaiKhoan, string matKhau)
        {
            return Database.DB_QUERY.TimTaiKhoan(tenTaiKhoan, matKhau);
        }

        // Hàm lấy danh sách tài khoản từ database
        public static List<V_TAIKHOAN> LayDanhSachTaiKhoan()
        {
            List<V_TAIKHOAN> listTaiKhoan = new List<V_TAIKHOAN>();
            List<ACCOUNT> listTaiKhoanDB = Database.DB_QUERY.LayDanhSachTaiKhoan();
            int soTaiKhoan = listTaiKhoanDB.Count;
            for (int i = 0; i < soTaiKhoan; ++i)
            {
                string loaiTaiKhoan = "";
                if (listTaiKhoanDB[i].TYPEACC == 1)
                    loaiTaiKhoan = "Ban quản lý";
                else if (listTaiKhoanDB[i].TYPEACC == 2)
                    loaiTaiKhoan = "Ban tổ chức";
                else
                    loaiTaiKhoan = "Administrator";
                listTaiKhoan.Add(new V_TAIKHOAN()
                {
                    STT = i + 1,
                    USERNAME = listTaiKhoanDB[i].USERNAME,
                    PASS = listTaiKhoanDB[i].PASS,
                    NGAYLAP = (DateTime)listTaiKhoanDB[i].NGAYLAP,
                    TYPEACC = loaiTaiKhoan
                }); ;
            }
            return listTaiKhoan;
        }

        // Hàm kiểm tra tên tài khoản có tồn tại
        public static bool KiemTraTenTaiKhoan(string tenTaiKhoan)
        {
            return Database.DB_QUERY.KiemTraTenTaiKhoan(tenTaiKhoan);
        }

        // Hàm thêm tài khoản vào database
        public static int ThemTaiKhoan(V_TAIKHOAN a_NewAccount)
        {
            int f_KetQua = 1;                                 // Mặc định bằng 1 là thêm thành công
            if (KiemTraTenTaiKhoan(a_NewAccount.USERNAME))
                f_KetQua = 2;                                 // Tài khoản đã tồn tại trong database 
            else
            {
                int Len = a_NewAccount.USERNAME.Length;
                for (int i = 0; i < Len; ++i)
                    if (a_NewAccount.USERNAME[i] < 48 || a_NewAccount.USERNAME[i] > 57 && a_NewAccount.USERNAME[i] < 65
                        || a_NewAccount.USERNAME[i] > 90 && a_NewAccount.USERNAME[i] < 97 || a_NewAccount.USERNAME[i] > 122)
                        return 3;                             //  Tài khoản chứa các kí tự đặc biệt
            }
            if (a_NewAccount.USERNAME.Length > 50)            // Tài khoản dài hơn 50 ký tự
                f_KetQua = 4;

            if (f_KetQua == 1)                                // Thêm thành công
            {
                ACCOUNT a_NewAccountDB = new ACCOUNT();
                a_NewAccountDB.USERNAME = a_NewAccount.USERNAME;
                a_NewAccountDB.PASS = a_NewAccount.PASS;
                a_NewAccountDB.NGAYLAP = a_NewAccount.NGAYLAP;
                if (a_NewAccount.TYPEACC == "Ban tổ chức")
                    a_NewAccountDB.TYPEACC = 2;
                if (a_NewAccount.TYPEACC == "Ban quản lý")
                    a_NewAccountDB.TYPEACC = 1;
                Database.DB_INSERTING.ThemTaiKhoan(a_NewAccountDB);
            }
            return f_KetQua;
        }

        // Hàm xóa một tài khoản
        public static void XoaTaiKhoan(V_TAIKHOAN a_TaiKhoanXoa)
        {
            ACCOUNT taiKhoanXoaDB = new ACCOUNT();
            taiKhoanXoaDB.USERNAME = a_TaiKhoanXoa.USERNAME;
            taiKhoanXoaDB.PASS = a_TaiKhoanXoa.PASS;
            taiKhoanXoaDB.NGAYLAP = a_TaiKhoanXoa.NGAYLAP;
            if (a_TaiKhoanXoa.TYPEACC == "Ban quản lý")
                taiKhoanXoaDB.TYPEACC = 1;
            if (a_TaiKhoanXoa.TYPEACC == "Ban tổ chức")
                taiKhoanXoaDB.TYPEACC = 2;
            Database.DB_DELETE.XoaTaiKhoan(taiKhoanXoaDB);
        }

        // Hàm tạo mới mật khẩu tài khoản
        public static void TaoMoiMatKhau(string tenTaiKhoan, string matKhauMoi)
        {
            Database.DB_UPDATE.TaoMoiMatKhau(tenTaiKhoan, matKhauMoi);
        }

        // Hàm tạo mật khẩu random : Tạo một chuỗi ngẩu nhiên với độ dài phần chữ là sizeChu
        //                           Độ dài phần số là sizeSo

        public static string RandomString(int sizeChu, int sizeSo)
        {
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < sizeChu; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(97, 122)));
                sb.Append(c);
            }
            for (int i = 0; i < sizeSo; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(48, 57)));
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
