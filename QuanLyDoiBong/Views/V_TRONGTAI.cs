using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{

    public class V_TRONGTAI
    {
        public V_TRONGTAI()
        {
            m_STT = 0;
            m_MaTrongTai = "";
            m_HoTen = "";
            m_NgaySinh = new DateTime(2018, 1, 1);
            m_DiaChi = "";
            m_QuocTich = "";
            m_SoNamKinhNghiem = 0;
            m_LoaiTrongTai = false;
            m_AVT = "";

        }

        public int m_STT
        {
            get;set;
        }

        public string m_MaTrongTai
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

        public bool m_LoaiTrongTai
        {
            get; set;
        }

        public string m_AVT
        {
            get; set;
        }


    }
}

