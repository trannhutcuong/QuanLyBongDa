using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace QuanLyDoiBong.Business
{
    public static class B_XML
    {
        #region Chung
        // Hàm đọc danh sách các thành phố từ file xml
        public static List<string> readXMLTINHTP()
        {
            List<string> cities = new List<string>();
            try
            {
                XElement xelement = XElement.Load(Environment.CurrentDirectory + "/XML/TINH_TP.xml");
                IEnumerable<XElement> city = xelement.Elements();
                foreach (string iCity in city)
                {
                    cities.Add(iCity);
                }

            }
            catch(Exception ex)
            {
                
            }

            return cities;
        }

        // Hàm đọc danh sách các quốc gia từ file xml
        public static List<string> readXMLCOUNTRY()
        {
            List<string> countries = new List<string>();
            try
            {
                XElement xelement = XElement.Load(Environment.CurrentDirectory + "/XML/COUNTRIES.xml");
                IEnumerable<XElement> country = xelement.Elements();
                foreach (string iCountry in country)
                {
                    countries.Add(iCountry);
                }

            }
            catch (Exception ex)
            {

            }

            return countries;
        }
        #endregion
    }
}
