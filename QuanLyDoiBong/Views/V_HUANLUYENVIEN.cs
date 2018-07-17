using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    public class V_HUANLUYENVIEN
    {
        public V_HUANLUYENVIEN()
        {
            m_STT = 0;
            m_MaHLV = "";
            m_MaDB = "";
            m_HoTen = "";
            m_NgaySinh = new DateTime(2018, 1, 1);
            m_NgaySinhStr = m_NgaySinh.ToShortDateString();
            m_DiaChi = "";
            m_QuocTich = "";
            m_SoNamKinhNghiem = 0;
            m_AVT = "";
            m_LoaiHLV = true;
            m_LoaiHLVStr = "Chính";
        }

        public int m_STT
        {
            get; set;
        }

        public string m_MaHLV
        {
            get; set;
        }

        public string m_MaDB
        {
            get; set;
        }

        public string m_HoTen
        {
            get; set;
        }

        public DateTime m_NgaySinh
        {
            get; set;
        }

        public string m_NgaySinhStr
        {
            get; set;
        }

        public string m_DiaChi
        {
            get; set;
        }

        public string m_QuocTich
        {
            get; set;
        }

        public int m_SoNamKinhNghiem
        {
            get; set;
        }

        public string m_AVT
        {
            get; set;
        }

        public bool m_LoaiHLV
        {
            get; set;
        }

        public string m_LoaiHLVStr
        {
            get;set;
        }

        public V_HUANLUYENVIEN Clone()
        {
            return new V_HUANLUYENVIEN
            {
                m_STT = this.m_STT,
                m_MaHLV = this.m_MaHLV,
                m_MaDB = this.m_MaDB,
                m_HoTen = this.m_HoTen,
                m_NgaySinh = this.m_NgaySinh,
                m_NgaySinhStr = this.m_NgaySinhStr,
                m_DiaChi = this.m_DiaChi,
                m_QuocTich = this.m_QuocTich,
                m_SoNamKinhNghiem = this.m_SoNamKinhNghiem,
                m_AVT = this.m_AVT,
                m_LoaiHLV = this.m_LoaiHLV,
                m_LoaiHLVStr = this.m_LoaiHLVStr,

            };
        }
    }
}
