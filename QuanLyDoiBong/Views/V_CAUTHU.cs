using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    public class V_CAUTHU
    {
        public V_CAUTHU()
        {
            m_STT = 0;
            m_MaCauThu = "";
            m_HoTen = "";
            m_NgaySinh = new DateTime(2018, 1, 1);
            m_NgaySinhStr = m_NgaySinh.ToShortDateString();
            m_DiaChi = "";
            m_QuocTich = "";
            m_SoNamKinhNghiem = 0;
            m_SoBanThang = 0;
            m_ViTri = "";
            m_AVT = "";
            m_MaDB = "";
        }

        public int m_STT
        {
            get; set;
        }

        public string m_MaCauThu
        {
            get;set;
        }

        public string m_HoTen
        {
            get;set;
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

        public int m_SoBanThang
        {
            get;set;
        }

        public string m_ViTri
        {
            get; set;
        }

        public string m_AVT
        {
            get; set;
        }

        public string m_MaDB
        {
            get; set;
        }
        public V_CAUTHU Clone()
        {
            return new V_CAUTHU
            {
                m_STT = this.m_STT,
                m_MaCauThu = this.m_MaCauThu,
                m_HoTen = this.m_HoTen,
                m_NgaySinh = this.m_NgaySinh,
                m_NgaySinhStr = this.m_NgaySinhStr,
                m_DiaChi = this.m_DiaChi,
                m_QuocTich = this.m_QuocTich,
                m_SoNamKinhNghiem = this.m_SoNamKinhNghiem,
                m_SoBanThang = this.m_SoBanThang,
                m_ViTri = this.m_ViTri,
                m_AVT = this.m_AVT,
                m_MaDB = this.m_MaDB,
            };
        }
    }
}
