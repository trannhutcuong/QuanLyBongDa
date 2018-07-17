using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Database
{
    public static class DB_DELETE
    {

        #region Chung
        // Xóa 1 cầu thủ trong database 
        public static void XoaCauThu(string a_MaCauThu)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                CAUTHU ct = (from c in db.CAUTHUs
                             where c.MACT == a_MaCauThu
                             select c).FirstOrDefault();

                if(ct != null)
                {
                    // Thực hiện cập nhật các bảng có tham chiếu tới MACT
                    // Giảm số lượng bên bảng DOIBONG
                    DB_UPDATE.DecreaseSLCTDoiBong(ct.MADB);
                    // Xóa đi các bàn thắng bên bảng BANTHANG
                    var query = (from c in db.BANTHANGs
                                 where c.MACT == a_MaCauThu
                                 select c);

                    foreach(BANTHANG bt in query)
                    {
                        db.BANTHANGs.DeleteOnSubmit(bt);
                    }

                    db.CAUTHUs.DeleteOnSubmit(ct);

                    db.SubmitChanges();
                }
            }

        }
        #endregion

        #region Nhựt Cường

        // Hàm xóa một tài khoản
        public static void XoaTaiKhoan(ACCOUNT taikhoanXoa)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                ACCOUNT account = (from n in db.ACCOUNTs
                                   where n == taikhoanXoa
                                   select n).FirstOrDefault();
                db.ACCOUNTs.DeleteOnSubmit(account);
                db.SubmitChanges();
            }
        }

        // Hàm xóa 1 trận đấu ra khỏi database
        public static bool XoaTranDau(string a_MaTD)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                TRANDAU tranDau = (from n in db.TRANDAUs
                                   where n.MATD == a_MaTD
                                   select n).FirstOrDefault();
                if (tranDau != null)
                {
                    db.TRANDAUs.DeleteOnSubmit(tranDau);
                    db.SubmitChanges();
                    return true;
                }
            }
            return false;
        }


        #endregion

        public static void XoaHuanLuyenVien(string a_MaHLV)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                HUANLUYENVIEN hlv = (from n in db.HUANLUYENVIENs
                                     where n.MAHLV == a_MaHLV
                                     select n).FirstOrDefault();
                if (hlv != null)
                {
                    db.HUANLUYENVIENs.DeleteOnSubmit(hlv);
                    db.SubmitChanges();
                }
            }
        }

        public static void XoaTrongTai(string a_MaTrongTai)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var tdtt = (from n in db.TRANDAU_TRONGTAIs
                               where n.MATT == a_MaTrongTai
                               select n);
                TRONGTAI tt = (from n in db.TRONGTAIs
                               where n.MATT == a_MaTrongTai
                               select n).FirstOrDefault();
                foreach(TRANDAU_TRONGTAI tdttt in tdtt)
                {
                    db.TRANDAU_TRONGTAIs.DeleteOnSubmit(tdttt);
                    db.SubmitChanges();
                }
                if (tt != null)
                {
                    
                    db.TRONGTAIs.DeleteOnSubmit(tt);
                    db.SubmitChanges();
                }
            }
        }

        //Xóa loai ban thang
        public static void XoaLoaiBanThang(char mabt)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                LOAIBANTHANG lbt = (from n in db.LOAIBANTHANGs
                                    where n.MABT == mabt
                                    select n).FirstOrDefault();
                if (lbt != null)
                {
                    db.LOAIBANTHANGs.DeleteOnSubmit(lbt);
                    db.SubmitChanges();
                }
            }
        }
    }
}
