using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDoiBong.Views
{
    public class V_BANTHANG
    {
        public V_BANTHANG()
        {
            m_MACT = m_TENCT = m_MADB = m_TENDB = m_MATD  = m_TENLOAIBANTHANG = "";
            m_PHUTGHIBAN = m_SOTT = 0;
            m_LOAIBANTHANG = '\0';
        }

        public int m_SOTT
        {
            get;set;
        }

        public string m_MACT
        {
            get;set;
        }

        public string m_TENCT
        {
            get;set;
        }

        public string m_MADB
        {
            get;set;
        }

        public string m_TENDB
        {
            get;set;
        }

        public string m_MATD
        {
            get;set;
        }

        public int m_PHUTGHIBAN
        {
            get;set;
        }

        public char m_LOAIBANTHANG
        {
            get;set;
        }

        public string m_TENLOAIBANTHANG
        {
            get;set;
        }


        public V_BANTHANG Clone()
        {
            return new V_BANTHANG
            {
                m_SOTT = this.m_SOTT,
                m_MACT = this.m_MACT,
                m_TENCT = this.m_TENCT,
                m_MADB = this.m_MADB,
                m_TENDB = this.m_TENDB,
                m_MATD = this.m_MATD,
                m_PHUTGHIBAN = this.m_PHUTGHIBAN,
                m_LOAIBANTHANG = this.m_LOAIBANTHANG,
                m_TENLOAIBANTHANG = this.m_TENLOAIBANTHANG,
            };
        }
    }
}
