using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyDoiBong.Database;
using QuanLyDoiBong.Views;
using System.Collections.ObjectModel;
using QuanLyDoiBong.Layouts;

namespace QuanLyDoiBong.Business
{
    public static class B_CAUTHU
    {
        public static List<V_CAUTHU> DanhSachCT()
        {
            List<CAUTHU> dsct = Database.DB_QUERY.DanhSachCT();
            List<V_CAUTHU> ds = new List<V_CAUTHU>();
            foreach(CAUTHU v in dsct)
            {
                ds.Add(new V_CAUTHU
                {
                    m_MaCauThu = v.MACT,
                    m_HoTen = v.HOTEN,
                    m_NgaySinh = (DateTime)v.NGAYSINH
                });
   
            }
            return ds;
        }


        #region Chung
        // Thêm cầu thủ (hàm này sẽ được gói bên phía Views, truyền vào các tham số ở màn hình đó)
        public static void ThemCauThu(V_CAUTHU a_CauThu, string a_MaDoiBong)
        {
          
            // Sau khi kiểm tra ổn thỏa cầu thủ hợp lệ thì nhét vào db.
            CAUTHU newCauThu = new CAUTHU
            {
                MACT = a_CauThu.m_MaCauThu,
                MADB = a_MaDoiBong,
                HOTEN = a_CauThu.m_HoTen,
                NGAYSINH = a_CauThu.m_NgaySinh,
                DIACHI = a_CauThu.m_DiaChi,
                QUOCTICH = a_CauThu.m_QuocTich,
                SONAMKINHNGHIEM = (byte)a_CauThu.m_SoNamKinhNghiem,
                VITRI = a_CauThu.m_ViTri,
                SOBANTHANG = 0,
                AVATAR = a_CauThu.m_AVT,
            };

            Database.DB_INSERTING.ThemCauThu(newCauThu);
        }

        // Cập nhật số bàn thắng cho cầu thủ
        public static void UpdateBanThangCauThu(string a_MaCT)
        {
            Database.DB_UPDATE.UpdateBanThangCauThu(a_MaCT);
        }

        // Kiểm tra một cầu thủ có hợp lệ hay không?
        // Thành công trả về 0.
        // Thất bại: 1. Thông tin chưa được điền đầy đủ
        //           2. Cầu thủ đã tồn tại
        //           3. Kiểm tra tuổi của cầu thủ
        //           4. Kiểm tra số cầu thủ ngoại cho phép
        //           5. Số năm kinh nghiệm âm
        //           6. Số năm kinh nghiệm lớn hơn tuổi thật
        public static int KiemTraCauThu(ObservableCollection<V_CAUTHU> a_dsCT, V_CAUTHU a_CauThu)
        {
            if (KiemTraThongTinDayDu(a_CauThu) == false)
                return 1;
            if (KiemTraCauThuDaTonTai(a_dsCT, a_CauThu.m_MaCauThu))
                return 2;
            if (KiemTraTuoiCauThu(a_CauThu.m_NgaySinh, QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_AGE_MAX,
                QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_AGE_MIN) == false)
                return 3;
            if (KiemTraSLCauThuNgoai(a_dsCT, QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_FOREIGN_MAX) == false && a_CauThu.m_QuocTich != "Việt Nam")
                return 4;
            if (KiemTraSoNamKinhNghiem(a_CauThu.m_SoNamKinhNghiem) == false)
                return 5;
            if (KiemTraSoNamKinhNghiemVoiTuoi(a_CauThu.m_NgaySinh, a_CauThu.m_SoNamKinhNghiem) == false)
                return 6;
            return 0;
        }

        // Kiểm tra cầu thủ có tồn tại trong DB không?
        // Nếu có thì trả về true, không thì trả về false
        private static bool KiemTraCauThuDaTonTai(ObservableCollection<V_CAUTHU> a_dsCT, string a_MaCT)
        {
            bool f_ExistDB =  Database.DB_QUERY.KiemTraCauThuTonTaiMACT(a_MaCT);
            bool f_ExitList = Business.B_CAUTHU.KiemTraCauThuDaTonTaiList(a_dsCT, a_MaCT);
            if (f_ExistDB || f_ExitList)
                return true;
            return false;
        }

        // Kiểm tra cầu thủ đã tồn tại trong List hay chưa?
        private static bool KiemTraCauThuDaTonTaiList(ObservableCollection<V_CAUTHU> a_dsCT, string a_MaCT)
        {
            for(int i=0; i < a_dsCT.Count; i++)
            {
                if (a_MaCT == a_dsCT[i].m_MaCauThu)
                    return true;
            }
            return false;
        }

        // Kiểm tra tuổi cầu thủ có thỏa yêu cầu không?
        // Nếu thỏa thì trả true, ngược lại false
        private static bool KiemTraTuoiCauThu(DateTime a_NgaySinh, int a_Age_Max, int a_Age_Min)
        {
            var today = DateTime.Today;
            int age = today.Year - a_NgaySinh.Year;
            if (age >= a_Age_Min && age <= a_Age_Max)
                return true;
            return false;
        }

        // Kiểm tra thông tin có đủ hay không?
        // Nếu đủ return true, ngược lại return false
        private static bool KiemTraThongTinDayDu(V_CAUTHU a_CauThu)
        {
            if (string.IsNullOrEmpty(a_CauThu.m_MaCauThu) || string.IsNullOrEmpty(a_CauThu.m_HoTen)
                || string.IsNullOrEmpty(a_CauThu.m_DiaChi))
            {
                return false;
            }
            return true;
        }

        // Kiểm tra số lượng cầu thủ ngoại?
        // Nếu thỏa thì trả true, ngược lại trả false
        private static bool KiemTraSLCauThuNgoai(ObservableCollection<V_CAUTHU> a_dsCT, int a_CTN_MAX)
        {
            int sl = 0;
            for(int i=0; i<a_dsCT.Count;i++)
            {
                if(a_dsCT[i].m_QuocTich != "Việt Nam")
                {
                    sl++;
                    if (sl >= a_CTN_MAX)
                        return false;
                }
                
            }
            return true;
        }

        // Kiểm tra số năm kinh nghiệm HLV có âm không?
        // Nếu > 0 thì trả true, ngược lại trả false
        private static bool KiemTraSoNamKinhNghiem(int snkn)
        {
            if (snkn <= 0)
                return false;
            return true;
        }

        // Kiểm tra số năm kinh nghiệm có lớn hơn số tuổi hay không?
        // Nếu số năm kinh nghiệm lớn hơn = return false, ngược lại true
        private static bool KiemTraSoNamKinhNghiemVoiTuoi(DateTime a_NgaySinh, int snk)
        {
            var today = DateTime.Today;
            int age = today.Year - a_NgaySinh.Year;
            if (snk >= age)
                return false;
            return true;
        }


        // Hàm xóa một cầu thủ trong DB bởi a_MaCauThu
        public static void XoaCauThu(string a_MaCauThu)
        {
            Database.DB_DELETE.XoaCauThu(a_MaCauThu);
        }

        // Hàm lấy tên một cầu thủ theo mã cầu thủ
        public static string LayTenCauThuByMaCT(string a_MaCT)
        {
            return Database.DB_QUERY.LayTenCauThuByMaCT(a_MaCT);
        }

        // Hàm kiểm tra một cầu thủ có thuộc về một đội bóng hay không?
        // Nếu thuộc thì trả true, ngược lại trả false
        public static bool KiemTraCauThuThuocDoiBong(string a_MaCT, string a_MaDB)
        {
            return Database.DB_QUERY.KiemTraCauThuThuocDoiBong(a_MaCT, a_MaDB);
        }

        #endregion


        #region Bình

        public static bool KiemTraCTTonTai(string a_TenCT)
        {
            return Database.DB_QUERY.KiemTraCTTonTai(a_TenCT);
        }

        public static List<V_CAUTHU> TraCuuCauThu(string tenCauThu)
        {
            List<V_CAUTHU> v_CT = new List<V_CAUTHU>();
            List<CAUTHU> cauThu = new List<CAUTHU>();
            cauThu = Database.DB_QUERY.TraCuuCauThu(tenCauThu);

            foreach (CAUTHU t in cauThu)
            {
                V_CAUTHU temp = new V_CAUTHU();
                temp.m_MaCauThu = t.MACT;
                temp.m_MaDB = t.MADB;
                temp.m_HoTen = t.HOTEN;
                temp.m_NgaySinh = (DateTime)t.NGAYSINH;
                temp.m_DiaChi = t.DIACHI;
                temp.m_SoNamKinhNghiem = (int)t.SONAMKINHNGHIEM;
                temp.m_AVT = t.AVATAR;
                temp.m_SoBanThang = (int)t.SOBANTHANG;
                temp.m_ViTri = t.VITRI;
                temp.m_QuocTich = t.QUOCTICH;
                v_CT.Add(temp);
            }

            return v_CT;
        }

        public static void ChinhSuaCauThu(V_CAUTHU a_CauThu)
        {
            CAUTHU ct = new CAUTHU();
            ct.MACT = a_CauThu.m_MaCauThu;
            ct.MADB = a_CauThu.m_MaDB;
            ct.HOTEN = a_CauThu.m_HoTen;
            ct.NGAYSINH = a_CauThu.m_NgaySinh;
            ct.DIACHI = a_CauThu.m_DiaChi;
            ct.QUOCTICH = a_CauThu.m_QuocTich;
            ct.SONAMKINHNGHIEM = (byte)a_CauThu.m_SoNamKinhNghiem;
            ct.AVATAR = a_CauThu.m_AVT;
            ct.SOBANTHANG = (byte)a_CauThu.m_SoBanThang;
            ct.VITRI = a_CauThu.m_ViTri;
            Database.DB_UPDATE.ChinhSuaCauThu(ct);
        }

        #endregion


        #region Du
        //Lấy các cầu thủ có bàn thắng (>0)
        public static List<V_CAUTHU> LayDanhSachCauThuGhiBan()
        {
            List<V_CAUTHU> listCauThu = new List<V_CAUTHU>();
            List<CAUTHU> listCauThuDB = Database.DB_QUERY.XoaCauThuKhongGhiBan();

            int slct = listCauThuDB.Count();

            //Tìm hạng của cầu thủ
            List<int> Hang = new List<int>();
            for (int i = 0; i < slct; i++)
            {
                int rank = 1;
                int temp = (int)listCauThuDB[i].SOBANTHANG;
                for (int j = 0; j < slct; j++)
                {
                    if (temp < (int)listCauThuDB[j].SOBANTHANG)
                        rank++;
                }
                Hang.Add(rank);
            }
            

            for (int i = 0; i < slct; i++)
            {
                listCauThu.Add(new V_CAUTHU()
                {
                    m_STT = Hang[i],
                    m_MaCauThu = listCauThuDB[i].MACT,
                    m_HoTen = listCauThuDB[i].HOTEN,
                    m_MaDB = listCauThuDB[i].MADB,
                    m_SoBanThang = (int)listCauThuDB[i].SOBANTHANG,
                    m_ViTri = listCauThuDB[i].VITRI,
                });
            }


            slct = listCauThu.Count();
            // Sắp xếp Cầu thủ giảm dần theo số bàn thắng
            for (int i = 0; i < slct - 1; ++i)
                for (int j = i + 1; j < slct; ++j)
                    if (listCauThu[j].m_SoBanThang > listCauThu[i].m_SoBanThang)
                    {
                        V_CAUTHU temp = listCauThu[i];
                        listCauThu[i] = listCauThu[j];
                        listCauThu[j] = temp;
                    }


            return listCauThu;
        }
        
        #endregion
    }
}
