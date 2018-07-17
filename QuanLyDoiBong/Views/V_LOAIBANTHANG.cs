using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    public class V_LOAIBANTHANG
    {
        public V_LOAIBANTHANG()
        {
            MABT = '\0';
            TENBT = "";
        }
        public char MABT { get; set; }
        public string TENBT { get; set; }

        public V_LOAIBANTHANG Clone()
        {
            return new V_LOAIBANTHANG
            {
                MABT = this.MABT,
                TENBT = this.TENBT,
            };
        }
    }
}
