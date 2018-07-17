using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    public class V_TAIKHOAN
    {
        public V_TAIKHOAN()
        {
            STT = 0;
            USERNAME = "";
            PASS = "";
            TYPEACC = "";
            NGAYLAP = new DateTime();
        }
        public int STT { get; set; }
        public string USERNAME { get; set; }
        public string PASS { get; set; }
        public string TYPEACC { get; set; }
        public DateTime NGAYLAP { get; set; }
    }
}
