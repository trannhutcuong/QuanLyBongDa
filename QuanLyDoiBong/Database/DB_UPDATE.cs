using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Database
{
    public static class DB_UPDATE
    {
        #region Chung

        // Cập nhật bàn thấng cho cầu thủ từ việc ghi nhận
        public static void UpdateBanThangCauThu(string a_MaCT)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                CAUTHU ct = (from n in db.CAUTHUs
                             where n.MACT == a_MaCT
                             select n).FirstOrDefault();
                if(ct!=null)
                {
                    ct.SOBANTHANG += 1;
                    db.SubmitChanges();
                }
            }
        }

        // Cập nhật các thông số của đội bóng từ việc ghi nhận
        public static void CapNhatKetQuaTranDauToDoiBong(string a_MaDB, int a_SBThang, int a_SBThua, int a_ScoreThang, 
            int a_ScoreHoa, int a_ScoreThua)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                DOIBONG doibong = (from n in db.DOIBONGs
                                   where n.MADB == a_MaDB
                                   select n).FirstOrDefault();

                doibong.SOBANTHANG += (short)a_SBThang;
                doibong.SOBANTHUA += (short)a_SBThua;
                if(a_SBThang > a_SBThua)
                {
                    doibong.TONGDIEM += (short)a_ScoreThang;
                }
                else if(a_SBThang == a_SBThua)
                {
                    doibong.TONGDIEM += (short)a_ScoreHoa;
                }
                else
                {
                    doibong.TONGDIEM += (short)a_ScoreThua;
                }
                db.SubmitChanges();
            }
        }
        #endregion

        // Cập nhật các thông số của trận đấu từ việc ghi nhận
        public static void CapNhatKetQuaTranDauToTranDau(string a_MaTD, int a_Score1, int a_Score2)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                TRANDAU td = (from n in db.TRANDAUs
                              where n.MATD == a_MaTD
                              select n).FirstOrDefault();

                td.SCORED1 = (short)a_Score1;
                td.SCORED2 = (short)a_Score2;
                td.GHINHAN = true;
                db.SubmitChanges();
            }
        }

        #region Nhựt Cường
        // Hàm update mật khẩu mới của tài khoản vào database
        public static void TaoMoiMatKhau(string tenTaiKhoan, string matKhauMoi)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                ACCOUNT account = (from n in db.ACCOUNTs
                                   where n.USERNAME == tenTaiKhoan
                                   select n).FirstOrDefault();
                account.PASS = matKhauMoi;
                db.SubmitChanges();
            }
        }
        #endregion

        #region Bình
        // giảm số lượng cầu thủ của đội bóng đi 1 người
        public static bool DecreaseSLCTDoiBong(string a_MaDoiBong)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.DOIBONGs
                            select n;
                foreach (var n in Query)
                {
                    if (n.MADB.CompareTo(a_MaDoiBong) == 0)
                    {
                        n.SOLUONGCAUTHU--;
                        return true;
                    }

                }
            }
            return false;
        }

        public static bool ChinhSuaCauThu(CAUTHU a_CauThu)
        {
            using(MyDatabaseQLDBDataContext db=new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.CAUTHUs
                            where n.MACT==a_CauThu.MACT
                            select n;
                foreach(var n in Query)
                {
                    n.MACT = a_CauThu.MACT;
                    n.MADB = a_CauThu.MADB;
                    n.HOTEN = a_CauThu.HOTEN;
                    n.NGAYSINH = a_CauThu.NGAYSINH;
                    n.DIACHI = a_CauThu.DIACHI;
                    n.QUOCTICH = a_CauThu.QUOCTICH;
                    n.SONAMKINHNGHIEM = a_CauThu.SONAMKINHNGHIEM;
                    n.AVATAR = a_CauThu.AVATAR;
                    n.SOBANTHANG = a_CauThu.SOBANTHANG;
                    n.VITRI = a_CauThu.VITRI;

                    db.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        public static bool ChinhSuaHLV(HUANLUYENVIEN a_HLV)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.HUANLUYENVIENs
                            where n.MAHLV == a_HLV.MAHLV
                            select n;
                foreach (var n in Query)
                {
                    n.MAHLV = a_HLV.MAHLV;
                    n.MADB = a_HLV.MADB;
                    n.HOTEN = a_HLV.HOTEN;
                    n.NGAYSINH = a_HLV.NGAYSINH;
                    n.DIACHI = a_HLV.DIACHI;
                    n.QUOCTICH = a_HLV.QUOCTICH;
                    n.SONAMKINHNGHIEM = a_HLV.SONAMKINHNGHIEM;
                    n.AVATAR = a_HLV.AVATAR;
                    n.LOAIHLV = a_HLV.LOAIHLV;
                    db.SubmitChanges();
                    return true;
                }
            }
            return false;
        }

        public static bool ChinhSuaTrongTai(TRONGTAI a_TrongTai)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.TRONGTAIs
                            where n.MATT == a_TrongTai.MATT
                            select n;
                foreach (var n in Query)
                {
                    n.MATT = a_TrongTai.MATT;
                    n.HOTEN = a_TrongTai.HOTEN;
                    n.NGAYSINH = a_TrongTai.NGAYSINH;
                    n.DIACHI = a_TrongTai.DIACHI;
                    n.QUOCTICH = a_TrongTai.QUOCTICH;
                    n.SONAMKINHNGHIEM = a_TrongTai.SONAMKINHNGHIEM;
                    n.AVATAR = a_TrongTai.AVATAR;
                    n.LOAITRONGTAI = a_TrongTai.LOAITRONGTAI;
                    db.SubmitChanges();
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Du
        // Thay đổi giá trị các quy định giải đấu
        public static bool ThayDoiQuyDinhDB(QUYDINH a_QuyDinh)
        {
            using (MyDatabaseQLDBDataContext db = new MyDatabaseQLDBDataContext())
            {
                var Query = from n in db.QUYDINHs
                            select n;
                foreach (var n in Query)
                {
                    if (n.MAQD == 1)
                    {
                        n.PLAYER_NUM_MAX = a_QuyDinh.PLAYER_NUM_MAX;
                        n.PLAYER_NUM_MIN = a_QuyDinh.PLAYER_NUM_MIN;
                        n.PLAYER_AGE_MAX = a_QuyDinh.PLAYER_AGE_MAX;
                        n.PLAYER_AGE_MIN = a_QuyDinh.PLAYER_AGE_MIN;
                        n.PLAYER_FOREIGN_MAX = a_QuyDinh.PLAYER_FOREIGN_MAX;
                        n.THOI_DIEM_GHI_BAN_MAX = a_QuyDinh.THOI_DIEM_GHI_BAN_MAX;
                        n.SCORE_THANG = a_QuyDinh.SCORE_THANG;
                        n.SCORE_HOA = a_QuyDinh.SCORE_HOA;
                        n.SCORE_THUA = a_QuyDinh.SCORE_THUA;

                        db.SubmitChanges();
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
