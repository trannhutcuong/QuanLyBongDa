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
    public static class B_DOIBONG
    {
        #region Chung
        public static void LuuDoiBong(V_DOIBONG a_DoiBong)
        {
            // Lưu vào database
            // Lưu đội bóng vào database
            DOIBONG newDoiBong = new DOIBONG
            {
                MADB = a_DoiBong.m_MaDoiBong,
                TENDOIBONG = a_DoiBong.m_TenDoiBong,
                SOLUONGCAUTHU = (byte)a_DoiBong.m_DanhSachCauThu.Count,
                TENSANNHA = a_DoiBong.m_TenSanNha,
                TINHTHANH = a_DoiBong.m_TinhThanh,
                NGAYTHANHLAP = a_DoiBong.m_NgayThanhLap,
                SOBANTHANG = 0,
                SOBANTHUA = 0,
                TONGDIEM = 0,
            };
            Database.DB_INSERTING.ThemDoiBong(newDoiBong);

            // Lưu cầu thủ vào database
            for (int i = 0; i < a_DoiBong.m_DanhSachCauThu.Count; i++)
            {
                V_CAUTHU vCauThu = a_DoiBong.m_DanhSachCauThu[i];
                Business.B_CAUTHU.ThemCauThu(vCauThu, newDoiBong.MADB);
            }

            // Lưu huấn luyện viên
            for (int i = 0; i < a_DoiBong.m_DanhSachHLV.Count; i++)
            {
                V_HUANLUYENVIEN vHLV = a_DoiBong.m_DanhSachHLV[i];
                Business.B_HUANLUYENVIEN.ThemHLV(vHLV, newDoiBong.MADB);
            }

        }

        // Kiểm tra đội bóng bắt lỗi
        // Thành công trả về 0.
        // Thất bại: 1. Thông tin chưa được điền đầy đủ
        //           2. Mã đội bóng đã tồn tại
        //           3. Tên đội bóng không được trùng với tên các đội bóng đã đăng ký
        //           4. Số lượng cầu thủ của đội bóng phải theo quy định của giải đấu        
        //           5. Đội bóng phải có HLV      
        //           6. Ngày thành lập đội bóng phải trước ngày đăng ký
        public static int KiemTraDoiBong(V_DOIBONG a_DoiBong)
        {
            if (KiemTraThongTinDayDu(a_DoiBong) == false)
                return 1;
            if (KiemTraDoiBongChuaTonTai(a_DoiBong.m_MaDoiBong) == false)
                return 2;
            if (KiemTraTenDoiBongTrung(a_DoiBong.m_TenDoiBong))
                return 3;
            if (KiemTraSLCT(a_DoiBong.m_DanhSachCauThu.Count) == false)
                return 4;
            if (KiemTraDaCoHLV(a_DoiBong.m_DanhSachHLV.Count) == false)
                return 5;
            if (KiemTraNgayThanhLap(a_DoiBong.m_NgayThanhLap) == false)
                return 6;
            return 0;
        }

        // Kiểm tra thông tin có đủ hay không?
        // Nếu đủ return true, ngược lại return false
        private static bool KiemTraThongTinDayDu(V_DOIBONG a_DoiBong)
        {
            if (string.IsNullOrEmpty(a_DoiBong.m_MaDoiBong) || string.IsNullOrEmpty(a_DoiBong.m_TenDoiBong)
                || string.IsNullOrEmpty(a_DoiBong.m_TenSanNha))
            {
                return false;
            }
            return true;
        }

        // Kiểm tra mã đội bóng có bị trùng hay không
        // Nếu chưa tồn tại thì trả true, ngược lại return false
        public static bool KiemTraDoiBongChuaTonTai(string a_MaDoiBong)
        {
            return Database.DB_QUERY.KiemTraDoiBongChuaTonTai(a_MaDoiBong);
        }

        // Kiểm tra tên đội bóng có bị trùng hay không?
        // Nếu trùng thì return true, ngược lại return false
        private static bool KiemTraTenDoiBongTrung(string a_TenDoiBong)
        {
            return Database.DB_QUERY.KiemTraTenDoiBongTrung(a_TenDoiBong);
        }

        // Kiểm tra số lượng cầu thủ truyền vào có thỏa quy định giải bóng không?
        // Nếu thỏa thì return true, return false
        private static bool KiemTraSLCT(int SLCT)
        {
            if (SLCT >= QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_NUM_MIN && SLCT <= QuanLyDoiBong.MainWindow.QUYDINHGIAIBONG.m_PLAYER_NUM_MAX)
                return true;
            return false;
        }

        // Kiểm tra đội bóng đã có HLV chưa?
        // Nếu chưa có thì trả false, ngược lại trả true
        private static bool KiemTraDaCoHLV(int SoLuongHLV)
        {
            return SoLuongHLV == 0 ? false : true;
        }

        // Kiểm tra ngày thành lập của giải bóng phải nhỏ hơn ngày đăng ký
        // Nếu nhỏ hơn thì trả true, ngược lại return false
        private static bool KiemTraNgayThanhLap(DateTime a_NgayThanhLap)
        {
            return a_NgayThanhLap < DateTime.Now ? true : false;
        }

        //  Lấy tên đội bóng bằng mà đội bóng
        public static string LayTenDoiBongByMaDB(string a_MaDB)
        {
            return Database.DB_QUERY.LayTenDoiBongByMaDoiBong(a_MaDB);
        }

        // Giảm SLCT của đội bóng đi 1
        public static bool DecreaseSLCTDoiBong(string a_MaDoiBong)
        {
            return Database.DB_UPDATE.DecreaseSLCTDoiBong(a_MaDoiBong);
        }


        // Lấy ra mã đội bóng bởi tên đội bóng
        public static string LayMaDoiBongByTenDoiBong(string a_TenDB)
        {
            return Database.DB_QUERY.LayMaDoiBongByTenDoiBong(a_TenDB);
        }

        // Cập nhật các thông số của đội bóng từ việc ghi nhận
        public static void CapNhatKetQuaTranDau(string a_MaDB, int a_SBThang, int a_SBThua, int a_ScoreThang,
            int a_ScoreHoa, int a_ScoreThua)
        {
            Database.DB_UPDATE.CapNhatKetQuaTranDauToDoiBong(a_MaDB, a_SBThang, a_SBThua, a_ScoreThang, a_ScoreHoa, a_ScoreThua);
        }


        #endregion

        #region Binh
        //kiem tra doi bong ton tai torng vong dau
        public static bool KiemTraDBTrongVD(string a_MaDB, int a_VongDau)
        {
            return Database.DB_QUERY.KiemTraDBTrongVD(a_MaDB,a_VongDau);
        }

        //lay danh sach doi bong
        public static List<V_DOIBONG> DanhSachDB()
        {
            List<V_DOIBONG> listDB = new List<V_DOIBONG>();
            List<DOIBONG> listDBDB = new List<DOIBONG>();
            listDBDB = Database.DB_QUERY.DanhSachDB();

            foreach(DOIBONG n in listDBDB)
            {
                V_DOIBONG doibong = new V_DOIBONG();
                doibong.m_MaDoiBong = n.MADB;
                doibong.m_TenDoiBong = n.TENDOIBONG;
                doibong.m_TenSanNha = n.TENSANNHA;
                listDB.Add(doibong);
            }
            return listDB;
        }
        #endregion

        #region Nhựt Cường + Du
        // Hàm lấy danh sách các đội bóng
        public static List<V_DOIBONG> LayDanhSachDoiBong()
        {
            List<V_DOIBONG> listDoiBong = new List<V_DOIBONG>();  
            List<DOIBONG> listDoiBongDB = Database.DB_QUERY.LayDanhSachDoiBong();
            int soDoiBong = listDoiBongDB.Count;                       // Số lượng đội bóng
            int[] DanhSachDiem = new int[soDoiBong];                   // Mảng lưu danh sách điểm của đội bóng
            for (int i = 0; i < soDoiBong; ++i)
            {
                listDoiBong.Add(new V_DOIBONG()
                {
                    m_MaDoiBong = listDoiBongDB[i].MADB,
                    m_TenDoiBong = listDoiBongDB[i].TENDOIBONG,
                    m_SoBanThang = (int)listDoiBongDB[i].SOBANTHANG,
                    m_SoBanThua = (int)listDoiBongDB[i].SOBANTHUA,
                    m_TongDiem = (int)listDoiBongDB[i].TONGDIEM,
                    m_HieuSo = (int)listDoiBongDB[i].SOBANTHANG - (int)listDoiBongDB[i].SOBANTHUA
                    /*Lấy danh sách cầu thủ*/
                    /*Lấy danh sách huấn luyện viên*/
                });
                DanhSachDiem[i] = (int)listDoiBongDB[i].TONGDIEM;
            }
            // Sắp xếp đội bóng giảm dần theo tổng điểm
            for(int i = 0; i < soDoiBong - 1; ++i)
                for(int j = i + 1; j < soDoiBong; ++j)
                    if(listDoiBong[j].m_TongDiem > listDoiBong[i].m_TongDiem)
                    {
                        V_DOIBONG temp = listDoiBong[i];
                        listDoiBong[i] = listDoiBong[j];
                        listDoiBong[j] = temp;
                    }
            // Sắp xếp đội bóng theo hiệu số
            for (int i = 0; i < soDoiBong - 1; ++i)
                for (int j = i + 1; j < soDoiBong; ++j)
                    if (listDoiBong[j].m_TongDiem == listDoiBong[i].m_TongDiem)
                        if (listDoiBong[j].m_HieuSo > listDoiBong[i].m_HieuSo)
                        {
                            V_DOIBONG temp = listDoiBong[i];
                            listDoiBong[i] = listDoiBong[j];
                            listDoiBong[j] = temp;
                        }
            // Gán thứ hạng cho các đội
            int index = 0;
            for (int i = 0; i < soDoiBong; ++i)
                listDoiBong[index++].m_Hang = i + 1;

            // Lấy số trận thắng, thua, hòa cho từng đội
            for (int i = 0; i < soDoiBong; ++i)
            {
                List<V_TRANDAU> listTranDau = B_TRANDAU.XemDanhSachTranDauTheoDoi(listDoiBong[i].m_MaDoiBong);
                int soTranDau = listTranDau.Count;
                int soTranThang = 0;
                int soTranHoa = 0;
                int soTranThua = 0;
                for(int j = 0; j < soTranDau; ++j)
                {
                    if(listTranDau[j].m_MaDB1 == listDoiBong[i].m_MaDoiBong)             // là đội 1
                    {
                        if (listTranDau[j].m_SCORED1 < listTranDau[j].m_SCORED2)
                            soTranThua++;
                        else if (listTranDau[j].m_SCORED1 == listTranDau[j].m_SCORED2)
                            soTranHoa++;
                        else
                            soTranThang++;
                    }

                    if(listTranDau[j].m_MaDB2 == listDoiBong[i].m_MaDoiBong)              // là đội 2
                    {
                        if (listTranDau[j].m_SCORED1 < listTranDau[j].m_SCORED2)
                            soTranThang++;
                        else if (listTranDau[j].m_SCORED1 == listTranDau[j].m_SCORED2)
                            soTranHoa++;
                        else
                            soTranThua++;
                    }
                }
                listDoiBong[i].m_SoTranThang = soTranThang;
                listDoiBong[i].m_SoTranThua = soTranThua;
                listDoiBong[i].m_SoTranHoa = soTranHoa;
            }
            return listDoiBong;
        }
        
        #endregion
    }
}
