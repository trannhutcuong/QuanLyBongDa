using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyDoiBong.Database;
using QuanLyDoiBong.Views;
using System.Collections.ObjectModel;

namespace QuanLyDoiBong.Business
{
    public static class B_BANTHANG
    {

        #region Chung

        // Thêm dữ liệu bàn thắng vào db
        public static void ThemBanThang(V_BANTHANG a_BanThang)
        {
            BANTHANG bt = new BANTHANG
            {
                MACT = a_BanThang.m_MACT,
                MADB = a_BanThang.m_MADB,
                MATD = a_BanThang.m_MATD,
                PHUTGHIBAN = (byte)a_BanThang.m_PHUTGHIBAN,
                LOAIBT = a_BanThang.m_LOAIBANTHANG,
            };

            Database.DB_INSERTING.ThemBanThang(bt);
        }


        // Kiểm tra một bàn thắng có hợp lệ hay không?
        // Thành công trả về 0.
        // Thất bại: 1. Thông tin chưa được điền đầy đủ
        //           2. Bộ dữ liệu trùng
        //           3. Phút ghi bàn có thỏa quy định trận đấu
        //           4. Kiểm tra mã cầu thủ có thuộc đội bóng hay không?
        //           5. Kiểm tra bàn thắng không được ghi lại cùng thời điểm
        public static int KiemTraBanThang(ObservableCollection<V_BANTHANG> a_DsBanThang, V_BANTHANG a_BanThang)
        {
            if(KiemTraThongTinDayDu(a_BanThang) == false)
            {
                return 1;
            }
            if(KiemTraDuLieuTrung(a_DsBanThang, a_BanThang))
            {
                return 2;
            }
            if (KiemTraPhutGhiBan(MainWindow.QUYDINHGIAIBONG.m_THOI_DIEM_GHI_BAN_MAX, a_BanThang) == false)
            {
                return 3;
            }
            if(KiemTraCauThuThuocDoiBong(a_BanThang) == false)
            {
                return 4;
            }
            if(KiemTraThoiDiemGhiBanTrung(a_DsBanThang, a_BanThang))
            {
                return 5;
            }
            return 0;
        }

        // Kiểm tra thông tin chưa đầy đủ
        // Nếu đủ return true, ngược lại false
        private static bool KiemTraThongTinDayDu(V_BANTHANG a_BanThang)
        {
            if (a_BanThang.m_MACT != "" && a_BanThang.m_TENDB != "" && a_BanThang.m_PHUTGHIBAN != 0
                && a_BanThang.m_LOAIBANTHANG != '\0')
                return true;
            return false;
        }

        // Kiểm tra dữ liệu có bị trùng hay không?
        // trùng return true, ngược lại return false
        private static bool KiemTraDuLieuTrung(ObservableCollection<V_BANTHANG> a_DsBanThang, V_BANTHANG a_BanThang)
        {
            for(int i=0; i< a_DsBanThang.Count; i++)
            {
                if (
                    String.Compare(a_BanThang.m_MACT, a_DsBanThang[i].m_MACT, StringComparison.OrdinalIgnoreCase)
                     == 0 && a_BanThang.m_TENDB == a_DsBanThang[i].m_TENDB
                     && a_BanThang.m_PHUTGHIBAN == a_DsBanThang[i].m_PHUTGHIBAN)
                    return true;
            }
            return false;
        }

        // Kiểm tra phút ghi bàn có thỏa quy định giải đấu không?
        // Nếu thỏa trả true, ngược lại trả false
        private static bool KiemTraPhutGhiBan(int TIME_MAX , V_BANTHANG a_BanThang)
        {
            if (a_BanThang.m_PHUTGHIBAN < 0 || a_BanThang.m_PHUTGHIBAN > TIME_MAX)
                return false;
            return true;
        }

        // Kiểm tra cầu thủ ghi bàn có thuộc về đội bóng không?
        // Nếu có thì trả true, ngược lại trả false
        private static bool KiemTraCauThuThuocDoiBong(V_BANTHANG a_BanThang)
        {
            return B_CAUTHU.KiemTraCauThuThuocDoiBong(a_BanThang.m_MACT, a_BanThang.m_MADB);
        }

        // Kiểm tra xem có việc ghi nhận 2 bàn thắng ghi cùng một thời điểm hay không?
        // Nếu có return true, ngược lại return false
        private static bool KiemTraThoiDiemGhiBanTrung(ObservableCollection<V_BANTHANG> a_DsBanThang, V_BANTHANG a_BanThang)
        {
            for(int i=0; i<a_DsBanThang.Count; i++)
            {
                if(a_DsBanThang[i].m_PHUTGHIBAN == a_BanThang.m_PHUTGHIBAN)
                {
                    return true;
                }
            }
            return false;
        }




        #endregion
    }
}
