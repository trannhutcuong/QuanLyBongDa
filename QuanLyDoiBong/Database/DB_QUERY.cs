using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyDoiBong.Database
{
    public static class DB_QUERY
    {
        // CLASS CAUTHU
        #region Chung
        // Hàm tìm kiếm cầu thủ đã tồn tại trong DB chưa
        // Nếu đã tồn tại trả về true, ngược lại trả false
        public static bool KiemTraCauThuTonTaiMACT(string a_MaCT)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                CAUTHU ct = (from c in db.CAUTHUs
                             where c.MACT == a_MaCT
                             select c).FirstOrDefault();
                if (ct != null)
                {
                    return true;
                }
                return false;
            }
        }

        // Hàm lấy tên cầu thủ bởi mã cầu thủ
        public static string LayTenCauThuByMaCT(string a_MaCT)
        {
            string name = "";
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                CAUTHU ct = (from c in db.CAUTHUs
                             where c.MACT == a_MaCT
                             select c).FirstOrDefault();
                if (ct != null)
                {
                    name = ct.HOTEN;
                }
            }
            return name;
        }

        // Kiểm tra một cầu thủ có thuộc về đội bóng đó không?
        // Nếu thuộc thì trả true, ngược lại trả false
        public static bool KiemTraCauThuThuocDoiBong(string a_MaCT, string a_MaDB)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                CAUTHU ct = (from c in db.CAUTHUs
                             where c.MACT == a_MaCT
                             select c).FirstOrDefault();
                if (ct != null)
                {
                    if (ct.MADB == a_MaDB)
                        return true;
                }
                return false;
            }
        }

        // CALSS TRONGTAI
        // Hàm tìm kiếm huấn luyện viên đã tồn tại trong DB chưa
        // Nếu đã tồn tại trả về true, ngược lại trả false
        public static bool KiemTraHLVTonTaiMaHLV(string a_MaHLV)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                HUANLUYENVIEN ct = (from c in db.HUANLUYENVIENs
                                    where c.MAHLV == a_MaHLV
                                    select c).FirstOrDefault();
                if (ct != null)
                {
                    return true;
                }
                return false;
            }
        }


        // CLASS DOIBONG
        // Lấy tên đội bóng bởi mã đội bóng
        public static string LayTenDoiBongByMaDoiBong(string a_MaDB)
        {
            string tenDoiBong = "";
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                tenDoiBong = (from c in db.DOIBONGs
                              where c.MADB == a_MaDB
                              select c.TENDOIBONG).FirstOrDefault();
            }
            return tenDoiBong;
        }

        // Lấy mã đội bóng bởi tên đội bóng
        public static string LayMaDoiBongByTenDoiBong(string a_TenDB)
        {
            string maDoiBong = "";
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                maDoiBong = (from c in db.DOIBONGs
                             where c.TENDOIBONG == a_TenDB
                             select c.MADB).FirstOrDefault();
            }
            return maDoiBong;
        }

        // Kiêm tra trong database đã có tên đội bóng a_TenDoiBong chưa?
        // Nếu có thì trả true, ngược lại trả false
        public static bool KiemTraTenDoiBongTrung(string a_TenDoiBong)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                DOIBONG team = (from c in db.DOIBONGs
                                where c.TENDOIBONG == a_TenDoiBong
                                select c).FirstOrDefault();
                if (team == null)
                    return false;
            }
            return true;
        }


        // CLASS TRANDAU
        // Hàm lấy ra danh sách các mã trận đấu chưa đươc ghi nhận tồn tại trong bảng Trận Đấu.
        public static List<string> LayDanhSachMaTranDauChuaGhiNhan()
        {
            List<string> dsTranDau = new List<string>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var query = from c in db.TRANDAUs
                            where c.GHINHAN == false
                            select c.MATD;

                dsTranDau = query.ToList<string>();
            }
            return dsTranDau;
        }

        // Lấy ra thông tin của một trận đấu bằng a_MaTD
        public static TRANDAU LayTranDau(string a_MaTD)
        {
            TRANDAU td = null;
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                td = (from c in db.TRANDAUs
                      where c.MATD == a_MaTD
                      select c).FirstOrDefault();
            }
            return td;
        }

        // Xử lý lỗi lấy dữ liệu từ sql nchar() cố định.
        // Ex: "chung     " -> "chung"
        public static string GetStrFieldNCHAR(string a_strField)
        {
            string res = "";
            for (int i = 0; i < a_strField.Length; i++)
            {
                string c = a_strField[i].ToString();
                if (c != " ")
                {
                    res += c;
                }
                else
                {
                    break;
                }
            }
            return res;
        }
        #endregion

        #region NHỰT CƯỜNG
        // Hàm tìm tài khoản có tồn tại hay không từ bảng ACCOUNT
        public static int TimTaiKhoan(string TenTaiKhoan, string MatKhau)
        {
            int f_KetQua = -1;                                                   // Mặc định không tìm thấy trả về -1
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.ACCOUNTs
                             select n);
                foreach (var n in Query)
                {
                    string[] username = n.USERNAME.Split(null);
                    string[] password = n.PASS.Split(null);
                    if (TenTaiKhoan == username[0] && MatKhau == password[0])
                        f_KetQua = (int)n.TYPEACC;                                // Tồn tại tài khoản, trả về kiểu tài khoản
                }
            }
            return f_KetQua;
        }

        // Lấy dữ liệu từ bảng QUYDINH
        public static QUYDINH LayDuLieuQuyDinh()
        {
            QUYDINH quyDinh = new QUYDINH();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.QUYDINHs
                             select n);
                foreach (var n in Query)
                {
                    quyDinh.PLAYER_NUM_MAX = (int)n.PLAYER_NUM_MAX;
                    quyDinh.PLAYER_NUM_MIN = (int)n.PLAYER_NUM_MIN;
                    quyDinh.PLAYER_AGE_MAX = (int)n.PLAYER_AGE_MAX;
                    quyDinh.PLAYER_AGE_MIN = (int)n.PLAYER_AGE_MIN;
                    quyDinh.PLAYER_FOREIGN_MAX = (int)n.PLAYER_FOREIGN_MAX;
                    quyDinh.SCORE_THANG = (int)n.SCORE_THANG;
                    quyDinh.SCORE_HOA = (int)n.SCORE_HOA;
                    quyDinh.SCORE_THUA = (int)n.SCORE_THUA;
                    quyDinh.THOI_DIEM_GHI_BAN_MAX = (int)n.THOI_DIEM_GHI_BAN_MAX;
                }
            }
            return quyDinh;
        }


        // Lấy danh sách các tài khoản
        public static List<ACCOUNT> LayDanhSachTaiKhoan()
        {
            List<ACCOUNT> listTaiKhoan = new List<ACCOUNT>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.ACCOUNTs
                             select n);
                foreach (var n in Query)
                {
                    listTaiKhoan.Add(new ACCOUNT() { USERNAME = n.USERNAME, PASS = n.PASS, NGAYLAP = n.NGAYLAP, TYPEACC = n.TYPEACC });
                }
            }
            return listTaiKhoan;
        }

        // Kiểm tra tên tài khoản tồn tại hay chưa
        public static bool KiemTraTenTaiKhoan(string tenTaiKhoan)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.ACCOUNTs
                             select n);
                foreach (var n in Query)
                {
                    string[] username = n.USERNAME.Split(null);
                    if (tenTaiKhoan == username[0])
                        return true;                      // Tìm thấy tài khoản    
                }
            }
            return false;
        }

        // Lấy dữ liệu loại bàn thắng
        public static List<LOAIBANTHANG> LayDuLieuLoaiBanThang()
        {
            List<LOAIBANTHANG> listLoaiBanThang = new List<LOAIBANTHANG>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.LOAIBANTHANGs
                             select n);
                foreach (var n in Query)
                {
                    listLoaiBanThang.Add(new LOAIBANTHANG() { MABT = n.MABT, TENBT = n.TENBT });
                }
            }
            return listLoaiBanThang;
        }

        // Lấy danh sách các TRANDAU
        public static List<TRANDAU> LayDanhSachTranDau()
        {
            List<TRANDAU> listTranDau = new List<TRANDAU>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.TRANDAUs
                             select n);
                foreach (var n in Query)
                {
                    listTranDau.Add(new TRANDAU()
                    {
                        MATD = n.MATD,
                        NGAYDIENRA = n.NGAYDIENRA,
                        DOI1 = n.DOI1,
                        DOI2 = n.DOI2,
                        SCORED1 = n.SCORED1,
                        SCORED2 = n.SCORED2,
                        TENSAN = n.TENSAN,
                        VONGDAU = n.VONGDAU,
                        GHINHAN = n.GHINHAN
                    });
                }
            }
            return listTranDau;
        }

        // Hàm lấy danh sách các đội bóng
        public static List<DOIBONG> LayDanhSachDoiBong()
        {
            List<DOIBONG> listDoiBong = new List<DOIBONG>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.DOIBONGs
                             select n);
                foreach (var n in Query)
                {
                    listDoiBong.Add(new DOIBONG()
                    {
                        MADB = n.MADB,
                        TENDOIBONG = n.TENDOIBONG,
                        SOLUONGCAUTHU = n.SOLUONGCAUTHU,
                        TENSANNHA = n.TENSANNHA,
                        TINHTHANH = n.TINHTHANH,
                        NGAYTHANHLAP = n.NGAYTHANHLAP,
                        SOBANTHANG = n.SOBANTHANG,
                        SOBANTHUA = n.SOBANTHUA,
                        TONGDIEM = n.TONGDIEM
                    });
                }
            }
            return listDoiBong;
        }


        #endregion

        #region Đăng Bình
        //kiểm tra mã đội bóng trùng
        public static bool KiemTraMaTD(string a_MaTD)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.TRANDAUs
                            select n;
                foreach (var n in Query)
                {
                    if (a_MaTD.CompareTo(n.MATD) == 0)
                        return false;
                }
            }
            return true;
        }

        //kiem tra doi bong ton tai torng vong dau
        public static bool KiemTraDBTrongVD(string a_MaDB, int a_VongDau)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.TRANDAUs
                            select n;
                foreach (var n in Query)
                {
                    if ((a_MaDB.CompareTo(n.DOI1) == 0 || a_MaDB.CompareTo(n.DOI2) == 0) && a_VongDau == n.VONGDAU)
                        return false;
                }
            }
            return true;
        }

        //Lấy danh sách đội bóng
        public static List<DOIBONG> DanhSachDB()
        {
            List<DOIBONG> dsdb = new List<DOIBONG>();

            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.DOIBONGs
                            select n;
                foreach (var n in Query)
                {
                    DOIBONG doibong = new DOIBONG();
                    doibong.MADB = n.MADB;
                    doibong.TENDOIBONG = n.TENDOIBONG;
                    doibong.TENSANNHA = n.TENSANNHA;
                    dsdb.Add(doibong);
                }
            }
            return dsdb;
        }

        //Lấy danh sách cầu thủ
        public static List<CAUTHU> DanhSachCT()
        {
            List<CAUTHU> dsct = new List<CAUTHU>();

            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.CAUTHUs
                            select n;
                foreach (CAUTHU ct  in Query)
                {
                     
                    //doibong.MADB = n.MADB;
                    //doibong.TENDOIBONG = n.TENDOIBONG;
                    //doibong.TENSANNHA = n.TENSANNHA;
                    dsct.Add(ct);
                }
            }
            return dsct;
        }

        //Lấy danh sách HLV
        public static List<HUANLUYENVIEN> DanhSacHLV()
        {
            List<HUANLUYENVIEN> dsct = new List<HUANLUYENVIEN>();

            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.HUANLUYENVIENs
                            select n;
                foreach (HUANLUYENVIEN ct in Query)
                {

                    //doibong.MADB = n.MADB;
                    //doibong.TENDOIBONG = n.TENDOIBONG;
                    //doibong.TENSANNHA = n.TENSANNHA;
                    dsct.Add(ct);
                }
            }
            return dsct;
        }

        //Lấy danh sách trongtai
        public static List<TRONGTAI> DanhSachTrongTai()
        {
            List<TRONGTAI> dsct = new List<TRONGTAI>();

            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.TRONGTAIs
                            select n;
                foreach (TRONGTAI ct in Query)
                {

                    //doibong.MADB = n.MADB;
                    //doibong.TENDOIBONG = n.TENDOIBONG;
                    //doibong.TENSANNHA = n.TENSANNHA;
                    dsct.Add(ct);
                }
            }
            return dsct;
        }


        public static List<CAUTHU> TraCuuCauThu(string tenCauThu)
        {
            List<CAUTHU> listCauThu = new List<CAUTHU>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.CAUTHUs
                            select n;
                foreach (var n in Query)
                {
                    if (string.Compare(tenCauThu, n.HOTEN, true) == 0)
                    {
                        listCauThu.Add(new CAUTHU()
                        {
                            MACT = n.MACT,
                            MADB = n.MADB,
                            HOTEN = n.HOTEN,
                            NGAYSINH = n.NGAYSINH,
                            DIACHI = n.DIACHI,
                            QUOCTICH = n.QUOCTICH,
                            SONAMKINHNGHIEM = n.SONAMKINHNGHIEM,
                            AVATAR = n.AVATAR,
                            SOBANTHANG = n.SOBANTHANG,
                            VITRI = n.VITRI
                        });
                    }
                }

            }
            return listCauThu;
        }

        public static List<HUANLUYENVIEN> TraCuuHLV(string tenHLV)
        {
            List<HUANLUYENVIEN> listHLV = new List<HUANLUYENVIEN>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.HUANLUYENVIENs
                            select n;
                foreach (var n in Query)
                {
                    if (string.Compare(tenHLV, n.HOTEN, true) == 0)
                    {
                        listHLV.Add(new HUANLUYENVIEN()
                        {
                            MAHLV = n.MAHLV,
                            MADB = n.MADB,
                            HOTEN = n.HOTEN,
                            NGAYSINH = n.NGAYSINH,
                            DIACHI = n.DIACHI,
                            QUOCTICH = n.QUOCTICH,
                            SONAMKINHNGHIEM = n.SONAMKINHNGHIEM,
                            AVATAR = n.AVATAR,
                            LOAIHLV = n.LOAIHLV
                        });
                    }
                }

            }
            return listHLV;
        }

        public static List<TRONGTAI> TraCuuTT(string tenTT)
        {
            List<TRONGTAI> listTT = new List<TRONGTAI>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.TRONGTAIs
                            select n;
                foreach (var n in Query)
                {
                    if (string.Compare(tenTT, n.HOTEN, true)  == 0)
                    {
                        listTT.Add(new TRONGTAI()
                        {
                            MATT = n.MATT,
                            HOTEN = n.HOTEN,
                            NGAYSINH = n.NGAYSINH,
                            DIACHI = n.DIACHI,
                            QUOCTICH = n.QUOCTICH,
                            SONAMKINHNGHIEM = n.SONAMKINHNGHIEM,
                            AVATAR = n.AVATAR,
                            LOAITRONGTAI = n.LOAITRONGTAI
                        });
                    }
                }

            }
            return listTT;
        }

        //Hàm tìm trận đấu trong db querry
        public static bool TimTranDau(string a_MATD)

        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.TRANDAUs
                             select n);
                foreach (var n in Query)
                {
                    if (a_MATD.CompareTo(n.MATD) == 0)
                        return true;                      // Tìm thấy
                }
            }
            return false;
        }


        //Kiểm tra cầu thủ tồn tại 
        public static bool KiemTraCTTonTai(string a_TenCT)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.CAUTHUs
                            select n;
                foreach (var n in Query)
                {
                    if (string.Compare(a_TenCT, n.HOTEN, true) == 0)
                        return true;
                }
            }
            return false;
        }

        //Kiểm tra huấn luyện viên tồn tại theo tên
        public static bool KiemTraHLVTonTai(string a_TenHLV)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.HUANLUYENVIENs
                            select n;
                foreach (var n in Query)
                {
                    if (string.Compare(a_TenHLV, n.HOTEN, true) == 0)
                        return true;
                }
            }
            return false;
        }

        //Kiểm tra trọng tài tồn tại 
        public static bool KiemTraTTTonTai(string a_TenTT)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.TRONGTAIs
                            select n;
                foreach (var n in Query)
                {
                    if (string.Compare(a_TenTT, n.HOTEN, true) == 0)
                        return true;
                }
            }
            return false;
        }

        //kiểm tra mã đội bóng có bị trung hay không
        public static bool KiemTraDoiBongChuaTonTai(string a_MaDoiBong)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {

                var Query = from n in db.DOIBONGs
                            select n;
                foreach (var c in Query)
                {
                    string t_MADB = GetStrFieldNCHAR(c.MADB);
                    if (t_MADB == a_MaDoiBong)
                        return false;

                }
            }
            return true;
        }

        // Load hình cầu thủ

        #endregion

        #region Du Đẹp Trai
        // Hàm tìm trọng tài có tồn tại hay không từ bảng TRONGTAI
        public static bool TimTrongTai(string MATT)

        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.TRONGTAIs
                             select n);
                foreach (var n in Query)
                {
                    string[] trongtai = n.MATT.Split(null);
                    if (MATT == trongtai[0])
                        return true;                      // Tìm thấy trọng tài 
                }
            }
            return false;
        }

        //Hàm lấy danh sách tất cả các cầu thủ
        public static List<CAUTHU> LayToanBoCauThu()
        {
            List<CAUTHU> listCauThu = new List<CAUTHU>();
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = (from n in db.CAUTHUs
                             select n);
                foreach (var n in Query)
                {
                    listCauThu.Add(new CAUTHU()
                    {
                        MACT = n.MACT,
                        MADB = n.MADB,
                        HOTEN = n.HOTEN,
                        NGAYSINH = n.NGAYSINH,
                        DIACHI = n.DIACHI,
                        QUOCTICH = n.QUOCTICH,
                        SOBANTHANG = n.SOBANTHANG,
                        SONAMKINHNGHIEM = n.SONAMKINHNGHIEM,
                        AVATAR = n.AVATAR,
                        VITRI = n.VITRI
                    });
                }
            }
            return listCauThu;
        }

        //Xóa các cầu thủ không ghi bàn, và gán mã đội bóng bằng tên đội bóng
        public static List<CAUTHU> XoaCauThuKhongGhiBan()
        {
            List<CAUTHU> listCauThu = new List<CAUTHU>();
            listCauThu = LayToanBoCauThu();

            int sl = listCauThu.Count();
            for (int i = 0; i < sl; i++)
            {
                //Gán mã đội bóng thành tên đội bóng để load lên listview ở Layout BÁO CÁO GIẢI
                listCauThu[i].MADB = LayTenDoiBongByMaDoiBong(listCauThu[i].MADB);

                //Xóa cầu thủ không có bàn thắng khỏi danh sách
                if (listCauThu[i].SOBANTHANG == 0)
                {
                    listCauThu.RemoveAt(i);
                    sl--;
                    i--;
                }
            }
            return listCauThu;
        }
        #endregion
    }

}
