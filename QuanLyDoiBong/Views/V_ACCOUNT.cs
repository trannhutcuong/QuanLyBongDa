using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    public class V_ACCOUNT
    {
        public V_ACCOUNT()
        {
            m_UserName  = "";
            m_Type = 0;
        }

        public string m_UserName
        {
            get; set;
        }

        public int m_Type
        {
            get;set;
        }       
    }
}
