using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    public class V_QUYDINH
    {
        public V_QUYDINH()
        {
            m_ACCOUNT = 0;
            m_PLAYER_NUM_MAX = 0;
            m_PLAYER_NUM_MIN = 0;
            m_PLAYER_AGE_MAX = 0;
            m_PLAYER_AGE_MIN = 0;
            m_PLAYER_FOREIGN_MAX = 0;
            m_THOI_DIEM_GHI_BAN_MAX = 0;
            m_SCORE_THANG = 0;
            m_SCORE_HOA = 0;
            m_SCORE_THUA = 0;
            m_LOAI_BAN_THANG = new Dictionary<string, string>();
        }
        public int m_ACCOUNT { get; set; }
        public int m_PLAYER_NUM_MAX { get; set; }
        public int m_PLAYER_NUM_MIN { get; set; }
        public int m_PLAYER_AGE_MAX { get; set; }
        public int m_PLAYER_AGE_MIN { get; set; }
        public int m_PLAYER_FOREIGN_MAX { get; set; }
        public int m_THOI_DIEM_GHI_BAN_MAX { get; set; }
        public int m_SCORE_THANG { get; set; }
        public int m_SCORE_HOA { get; set; }
        public int m_SCORE_THUA { get; set; }
        public Dictionary<String, String> m_LOAI_BAN_THANG { get; set; }
    }
}
