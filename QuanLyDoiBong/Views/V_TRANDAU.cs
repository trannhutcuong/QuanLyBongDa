using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    public class V_TRANDAU
    {
        public V_TRANDAU()
        {
            m_MaTD = m_MaDB1 = m_MaDB2 = m_TenSan = "";
            m_SCORED1 = m_SCORED2 = 0;
            m_VongDau = 0;
            m_GhiNhan = false;
            m_NgayDienRa = new DateTime(2017, 12, 25);
        }

        public string m_MaTD
        {
            get;set;
        }

        public DateTime m_NgayDienRa
        {
            get;set;
        }

        public string m_MaDB1
        {
            get; set;
        }

        public string m_MaDB2
        {
            get; set;
        }

        public short m_SCORED1
        {
            get; set;
        }

        public short m_SCORED2
        {
            get; set;
        }

        public string m_TenSan
        {
            get; set;
        }

        public byte m_VongDau
        {
            get;set;
        }

        public bool m_GhiNhan
        {
            get; set;
        }
    }
}
