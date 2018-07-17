using QuanLyDoiBong.Database;
using QuanLyDoiBong.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Business
{
    public class B_LOAIBANTHANG
    {
        #region Chung
        // Lấy ra list<string> các loại bàn thắng
        public static List<string> LayLoaiStrBanThang()
        {
            List<string> LoaiBanThangStr = new List<string>();
            List<LOAIBANTHANG> listLoaiBanThangDB = Database.DB_QUERY.LayDuLieuLoaiBanThang();
            for(int i=0; i<listLoaiBanThangDB.Count; i++)
            {
                LoaiBanThangStr.Add(listLoaiBanThangDB[i].TENBT);
            }
            return LoaiBanThangStr;
        }
        #endregion

        // Lấy dữ liệu loại bàn thắng
        public static List<V_LOAIBANTHANG> LayDuLieuLoaiBanThang()
        {
            List<V_LOAIBANTHANG> listLoaiBanThang = new List<V_LOAIBANTHANG>();
            List<LOAIBANTHANG> listLoaiBanThangDB = Database.DB_QUERY.LayDuLieuLoaiBanThang();
            int soQuyDinh = listLoaiBanThangDB.Count;
            for (int i = 0; i < soQuyDinh; ++i)
            {
                listLoaiBanThang.Add(new V_LOAIBANTHANG()
                {
                    MABT = listLoaiBanThangDB[i].MABT,
                    TENBT = listLoaiBanThangDB[i].TENBT
                });
            }
            return listLoaiBanThang;
        }
    }
}
