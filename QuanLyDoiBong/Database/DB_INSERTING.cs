using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Database
{
    public static class DB_INSERTING
    {
        #region Chung
        // Hàm chèn thêm 1 cầu thủ vào database.
        public static void ThemCauThu(CAUTHU a_NewCauThu)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                db.CAUTHUs.InsertOnSubmit(a_NewCauThu);
                db.SubmitChanges();
            }
        }

        // Hàm chèn thêm 1 hlv vào database.
        public static void ThemHLV(HUANLUYENVIEN a_NewHLV)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                db.HUANLUYENVIENs.InsertOnSubmit(a_NewHLV);
                db.SubmitChanges();
            }
        }

        // Hàm chèn thêm một đội bóng vào database
        public static void ThemDoiBong(DOIBONG a_NewDoiBong)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                db.DOIBONGs.InsertOnSubmit(a_NewDoiBong);
                db.SubmitChanges();
            }
        }

        // Hàm thêm bàn thấng vào database
        public static void ThemBanThang(BANTHANG a_BanThang)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                db.BANTHANGs.InsertOnSubmit(a_BanThang);
                db.SubmitChanges();
            }
        }
        #endregion


        // Hàm thêm trong Database.INSERTING

        #region Nhựt Cường
        // Hàm thêm tài khoản vào database
        public static void ThemTaiKhoan(ACCOUNT a_NewAccount)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                db.ACCOUNTs.InsertOnSubmit(a_NewAccount);
                db.SubmitChanges();
            }
        }

        // Hàm thêm trận đấu
        public static void TaoTranDau(TRANDAU a_TranDau)
        {

            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                db.TRANDAUs.InsertOnSubmit(a_TranDau);
                db.SubmitChanges();
            }
        }

        #endregion

        #region Du
        // Hàm thêm trọng tài vào database
        public static void ThemTrongTai(TRONGTAI a_NewTrongTai)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {

                db.TRONGTAIs.InsertOnSubmit(a_NewTrongTai);
                db.SubmitChanges();
            }
        }
        #endregion

    }
}
