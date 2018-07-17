using QuanLyDoiBong.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyDoiBong.Database;

namespace QuanLyDoiBong.Business
{
    class B_QUYDINH
    {
        public static V_QUYDINH LayDuLieuQuyDinh()
        {
            V_QUYDINH quyDinh = new V_QUYDINH();
            QUYDINH quyDinhDB = Database.DB_QUERY.LayDuLieuQuyDinh();
            quyDinh.m_PLAYER_NUM_MAX = (int)quyDinhDB.PLAYER_NUM_MAX;
            quyDinh.m_PLAYER_NUM_MIN = (int)quyDinhDB.PLAYER_NUM_MIN;
            quyDinh.m_PLAYER_AGE_MAX = (int)quyDinhDB.PLAYER_AGE_MAX;
            quyDinh.m_PLAYER_AGE_MIN = (int)quyDinhDB.PLAYER_AGE_MIN;
            quyDinh.m_PLAYER_FOREIGN_MAX = (int)quyDinhDB.PLAYER_FOREIGN_MAX;
            quyDinh.m_THOI_DIEM_GHI_BAN_MAX = (int)quyDinhDB.THOI_DIEM_GHI_BAN_MAX;
            quyDinh.m_SCORE_THANG = (int)quyDinhDB.SCORE_THANG;
            quyDinh.m_SCORE_HOA = (int)quyDinhDB.SCORE_HOA;
            quyDinh.m_SCORE_THUA = (int)quyDinhDB.SCORE_THUA;
            return quyDinh;
        }

        public static bool ThayDoiQuyDinh(V_QUYDINH a_QuyDinh)
        {
            //Gán giá trị Quy Định
            QUYDINH newQuyDinh = new QUYDINH()
            {
                PLAYER_NUM_MAX = a_QuyDinh.m_PLAYER_NUM_MAX,
                PLAYER_NUM_MIN = a_QuyDinh.m_PLAYER_NUM_MIN,
                PLAYER_AGE_MAX = a_QuyDinh.m_PLAYER_AGE_MAX,
                PLAYER_AGE_MIN = a_QuyDinh.m_PLAYER_AGE_MIN,
                PLAYER_FOREIGN_MAX = a_QuyDinh.m_PLAYER_FOREIGN_MAX,
                THOI_DIEM_GHI_BAN_MAX = a_QuyDinh.m_THOI_DIEM_GHI_BAN_MAX,
                SCORE_THANG = a_QuyDinh.m_SCORE_THANG,
                SCORE_HOA = a_QuyDinh.m_SCORE_HOA,
                SCORE_THUA = a_QuyDinh.m_SCORE_THUA,
            };
            //Thay đổi trong database
            return DB_UPDATE.ThayDoiQuyDinhDB(newQuyDinh);            
        }
    }
}
