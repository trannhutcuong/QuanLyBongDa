using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyDoiBong.Database;
using QuanLyDoiBong.Views;

namespace QuanLyDoiBong.Business
{
    public static class B_TRANDAU
    {
        #region Chung

        // Hàm lấy ra danh sách mã trận đấu, lưu vào 1 list
        public static List<string> LayDanhSachMaTranDauChuaGhiNhan()
        {
            return Database.DB_QUERY.LayDanhSachMaTranDauChuaGhiNhan();
        }

        // Lấy ra trận đấu theo mã trận đấu
        public static V_TRANDAU LayTranDauByMaTranDau(string a_MaTD)
        {
            V_TRANDAU v_TD = new V_TRANDAU();
            TRANDAU td = Database.DB_QUERY.LayTranDau(a_MaTD);
            if(td != null)
            {
                v_TD.m_MaTD = td.MATD;
                v_TD.m_NgayDienRa = (DateTime)td.NGAYDIENRA;
                v_TD.m_MaDB1 = td.DOI1;
                v_TD.m_MaDB2 = td.DOI2;
                v_TD.m_SCORED1 = (short)td.SCORED1;
                v_TD.m_SCORED2 = (short)td.SCORED2;
                v_TD.m_TenSan = td.TENSAN;
                v_TD.m_VongDau = (byte)td.VONGDAU;

            }

            return v_TD;
        }

        // Cập nhật các thông số của đội bóng từ việc ghi nhận
        public static void CapNhatKetQuaTranDau(string a_MaTD, int a_Score1, int a_Score2)
        {
            Database.DB_UPDATE.CapNhatKetQuaTranDauToTranDau(a_MaTD, a_Score1, a_Score2);
        }

        #endregion

        #region NC
        // Hàm xóa trận đấu
        public static bool XoaTranDau(string a_MaTD)
        {
            return Database.DB_DELETE.XoaTranDau(a_MaTD);

        }

        // Hàm xem tất cả trận đấu theo vòng đấu
        public static List<V_TRANDAU> XemVongDau(byte a_VongDau)
        {
            List<V_TRANDAU> v_TD = new List<V_TRANDAU>();
            List<TRANDAU> m_TranDau = Database.DB_QUERY.LayDanhSachTranDau();

            foreach (TRANDAU t in m_TranDau)
            {
                if (t.VONGDAU == a_VongDau)
                {
                    V_TRANDAU temp = new V_TRANDAU();
                    temp.m_MaTD = t.MATD;
                    temp.m_NgayDienRa = (DateTime)t.NGAYDIENRA;
                    temp.m_MaDB1 = t.DOI1;
                    temp.m_MaDB2 = t.DOI2;
                    temp.m_SCORED1 = (short)t.SCORED1;
                    temp.m_SCORED2 = (short)t.SCORED2;
                    temp.m_TenSan = t.TENSAN;
                    temp.m_VongDau = (byte)t.VONGDAU;
                    temp.m_GhiNhan = (bool)t.GHINHAN;
                    v_TD.Add(temp);
                }
            }
            return v_TD;
        }

        // Hàm tìm tất cả dánh sách trận đấu theo mã đội
        public static List<V_TRANDAU> XemDanhSachTranDauTheoDoi(string a_MaDoi)
        {
            List<V_TRANDAU> listTranDau = new List<V_TRANDAU>();
            List<TRANDAU> m_TranDau = Database.DB_QUERY.LayDanhSachTranDau();

            foreach(TRANDAU t in m_TranDau)
            {
                if(t.DOI1 == a_MaDoi || t.DOI2 == a_MaDoi)
                {
                    listTranDau.Add(new V_TRANDAU() {
                        m_MaTD = t.MATD,
                        m_NgayDienRa = (DateTime)t.NGAYDIENRA,
                        m_MaDB1 = t.DOI1,
                        m_MaDB2 = t.DOI2,
                        m_SCORED1 = (short)t.SCORED1,
                        m_SCORED2 = (short)t.SCORED2,
                        m_TenSan = t.TENSAN,
                        m_VongDau = (byte)t.VONGDAU,
                        m_GhiNhan = (bool)t.GHINHAN
                     });
                }
            }

            return listTranDau;
        }
        #endregion

        #region Bình
        //kiểm tra mã trận đấu trùng
        public static bool KiemTraMaTD(string a_MaTD)
        {
            return Database.DB_QUERY.KiemTraMaTD(a_MaTD);
        }

        //Hàm thêm trận đấu
        public static bool TaoTranDau(V_TRANDAU a_TranDau)
        {
            TRANDAU newTranDau = new TRANDAU
            {
                MATD = a_TranDau.m_MaTD,
                DOI1 = a_TranDau.m_MaDB1,
                DOI2 = a_TranDau.m_MaDB2,
                SCORED1 = a_TranDau.m_SCORED1,
                SCORED2 = a_TranDau.m_SCORED2,
                NGAYDIENRA = a_TranDau.m_NgayDienRa,
                VONGDAU = (byte)a_TranDau.m_VongDau,
                TENSAN = a_TranDau.m_TenSan,
                GHINHAN = a_TranDau.m_GhiNhan
            };

            // Kiểm tra và thêm vào db
            if (DB_QUERY.TimTranDau(a_TranDau.m_MaTD) == false)
            {
                Database.DB_INSERTING.TaoTranDau(newTranDau);
                return true;
            }
            return false;
        }

        public static bool TimTranDau(string a_MATD)
        {
            return DB_QUERY.TimTranDau(a_MATD);
        }
        #endregion
    }
}
