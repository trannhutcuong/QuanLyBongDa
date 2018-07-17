using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyDoiBong.Views;
using QuanLyDoiBong.Database;
using System.Collections.ObjectModel;

namespace QuanLyDoiBong.Business
{
    class B_HUANLUYENVIEN
    {

        public static List<V_HUANLUYENVIEN> DanhSacHLV()
        {
            List<HUANLUYENVIEN> dsct = Database.DB_QUERY.DanhSacHLV();
            List<V_HUANLUYENVIEN> ds = new List<V_HUANLUYENVIEN>();
            foreach (HUANLUYENVIEN v in dsct)
            {
                ds.Add(new V_HUANLUYENVIEN
                {
                    m_MaHLV = v.MAHLV,
                    m_HoTen = v.HOTEN,
                    m_NgaySinh = (DateTime)v.NGAYSINH
                });

            }
            return ds;
        }
        #region Chung
        // Thêm cầu thủ (hàm này sẽ được gói bên phía Views, truyền vào các tham số ở màn hình đó)
        public static void ThemHLV(V_HUANLUYENVIEN a_HLV, string a_MaDoiBong)
        {
            // Sau khi kiểm tra ổn thỏa cầu thủ hợp lệ thì nhét vào db.
            HUANLUYENVIEN newHLV = new HUANLUYENVIEN
            {
                MAHLV = a_HLV.m_MaHLV,
                MADB = a_MaDoiBong,
                HOTEN = a_HLV.m_HoTen,
                NGAYSINH = a_HLV.m_NgaySinh,
                DIACHI = a_HLV.m_DiaChi,
                QUOCTICH = a_HLV.m_QuocTich,
                SONAMKINHNGHIEM = (byte)a_HLV.m_SoNamKinhNghiem,
                LOAIHLV = a_HLV.m_LoaiHLV,
                AVATAR = a_HLV.m_AVT,
            };

            Database.DB_INSERTING.ThemHLV(newHLV);
        }

        // Kiểm tra một cầu thủ có hợp lệ hay không?
        // Thành công trả về 0.
        // Thất bại: 1. Thông tin chưa được điền đầy đủ
        //           2. HLV đã tồn tại
        //           3. Kiểm tra tuổi của HLV
        //           4. Số năm kinh nghiệm âm
        //           5. Số năm kinh nghiệm lớn hơn tuổi thật
        public static int KiemTraHLV(ObservableCollection<V_HUANLUYENVIEN> a_dsHLV, V_HUANLUYENVIEN a_HLV)
        {
            if (KiemTraThongTinDayDu(a_HLV) == false)
                return 1;
            if (KiemTraHLVDaTonTai(a_dsHLV, a_HLV.m_MaHLV))
                return 2;
            if (KiemTraTuoiHLV(a_HLV.m_NgaySinh) == false)
                return 3;
            if (KiemTraSoNamKinhNghiem(a_HLV.m_SoNamKinhNghiem) == false)
                return 4;
            if (KiemTraSoNamKinhNghiemVoiTuoi(a_HLV.m_NgaySinh, a_HLV.m_SoNamKinhNghiem) == false)
                return 5;
            return 0;
        }

        // Kiểm tra thông tin có đủ hay không?
        // Nếu đủ return true, ngược lại return false
        private static bool KiemTraThongTinDayDu(V_HUANLUYENVIEN a_HLV)
        {
            if (string.IsNullOrEmpty(a_HLV.m_MaHLV) || string.IsNullOrEmpty(a_HLV.m_HoTen)
                || string.IsNullOrEmpty(a_HLV.m_DiaChi))
            {
                return false;
            }
            return true;
        }

        // Kiểm tra cầu thủ có tồn tại trong DB không?
        // Nếu có thì trả về true, không thì trả về false
        private static bool KiemTraHLVDaTonTai(ObservableCollection<V_HUANLUYENVIEN> a_dsHLV, string a_MaHLV)
        {
            bool f_ExistDB = Database.DB_QUERY.KiemTraHLVTonTaiMaHLV(a_MaHLV);
            bool f_ExitList = Business.B_HUANLUYENVIEN.KiemTraHLVDaTonTaiList(a_dsHLV, a_MaHLV);
            if (f_ExistDB || f_ExitList)
                return true;
            return false;
        }

        // Kiểm tra cầu thủ đã tồn tại trong List hay chưa?
        private static bool KiemTraHLVDaTonTaiList(ObservableCollection<V_HUANLUYENVIEN> a_dsHLV, string a_MaHLV)
        {
            for (int i = 0; i < a_dsHLV.Count; i++)
            {
                if (a_MaHLV == a_dsHLV[i].m_MaHLV)
                    return true;
            }
            return false;
        }

        // Chỉ chấp nhận HLV trên 25 tuổi?
        // Nếu thỏa thì trả true, ngược lại false
        private static bool KiemTraTuoiHLV(DateTime a_NgaySinh)
        {
            var today = DateTime.Today;
            int age = today.Year - a_NgaySinh.Year;
            return age >= 25 ? true : false;
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

        #endregion


        #region Binh
        public static bool KiemTraHLVTonTai(string a_TenHLV)
        {
            return Database.DB_QUERY.KiemTraHLVTonTai(a_TenHLV);
        }

        public static List<V_HUANLUYENVIEN> TraCuuHLV(string tenHLV)
        {
            List<V_HUANLUYENVIEN> vHLV = new List<V_HUANLUYENVIEN>();
            List<HUANLUYENVIEN> HLV = new List<HUANLUYENVIEN>();
            HLV = Database.DB_QUERY.TraCuuHLV(tenHLV);
            foreach (HUANLUYENVIEN p in HLV)
            {

                V_HUANLUYENVIEN temp = new V_HUANLUYENVIEN();
                temp.m_MaHLV = p.MAHLV;
                temp.m_MaDB = p.MADB;
                temp.m_HoTen = p.HOTEN;
                temp.m_NgaySinh = (DateTime)p.NGAYSINH;
                temp.m_DiaChi = p.DIACHI;
                temp.m_QuocTich = p.QUOCTICH;
                temp.m_SoNamKinhNghiem = (byte)p.SONAMKINHNGHIEM;
                temp.m_AVT = p.AVATAR;
                temp.m_LoaiHLV = (bool)p.LOAIHLV;
                vHLV.Add(temp);
            }
            return vHLV;
        }

        public static void ChinhSuaHLV(V_HUANLUYENVIEN a_HLV)
        {
            HUANLUYENVIEN hlv = new HUANLUYENVIEN();
            hlv.MAHLV = a_HLV.m_MaHLV;
            hlv.MADB = a_HLV.m_MaDB;
            hlv.HOTEN = a_HLV.m_HoTen;
            hlv.NGAYSINH = a_HLV.m_NgaySinh;
            hlv.DIACHI = a_HLV.m_DiaChi;
            hlv.QUOCTICH = a_HLV.m_QuocTich;
            hlv.SONAMKINHNGHIEM = (byte)a_HLV.m_SoNamKinhNghiem;
            hlv.AVATAR = a_HLV.m_AVT;
            hlv.LOAIHLV = a_HLV.m_LoaiHLV;
            Database.DB_UPDATE.ChinhSuaHLV(hlv);
        }

        public static void XoaHLV(string a_MaHLV)
        {
            Database.DB_DELETE.XoaHuanLuyenVien(a_MaHLV);
        }
        #endregion
    }
}
