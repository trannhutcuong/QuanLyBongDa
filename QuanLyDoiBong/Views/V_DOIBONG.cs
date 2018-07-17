using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    
    public class V_DOIBONG
    {
        public V_DOIBONG()
        {
            m_MaDoiBong = "";
            m_TenDoiBong = "";
            m_NgayThanhLap = new DateTime(2017,1,1);
            m_TenSanNha = "";
            m_TinhThanh = "";
            m_DanhSachCauThu = new ObservableCollection<V_CAUTHU>();
            m_DanhSachHLV = new ObservableCollection<V_HUANLUYENVIEN>();
            m_SoBanThang = 0;
            m_SoBanThua = 0;
            m_TongDiem = 0;
            m_SoTranThang = 0;
            m_SoTranHoa = 0;
            m_SoTranThua = 0;
            m_Hang = 0;
            m_HieuSo = 0;
        }

        public String m_MaDoiBong
        {
            get;set;
        }
        public String m_TenDoiBong
        {
            get;set;
        }
        public DateTime m_NgayThanhLap
        {
            get; set;
        }
        public String m_TenSanNha
        {
            get; set;
        }
        public String m_TinhThanh
        {
            get; set;
        }
        public ObservableCollection<V_CAUTHU> m_DanhSachCauThu
        {
            get; set;
        }
        public ObservableCollection<V_HUANLUYENVIEN> m_DanhSachHLV
        {
            get; set;
        }
        public int m_SoBanThang { get; set; }
        public int m_SoBanThua { get; set; }
        public int m_TongDiem { get; set; }
        public int m_SoTranThang { get; set; }
        public int m_SoTranThua { get; set; }
        public int m_SoTranHoa { get; set; }
        public int m_Hang { get; set; }
        public int m_HieuSo { get; set; }
    }
}
