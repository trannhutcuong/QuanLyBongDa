using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyDoiBong.Database;
using QuanLyDoiBong.Views;

namespace QuanLyDoiBong.Business
{
    class B_TRONGTAI
    {

        public static List<V_TRONGTAI> DanhSachTrongTai()
        {
            List<TRONGTAI> dsct = Database.DB_QUERY.DanhSachTrongTai();
            List<V_TRONGTAI> ds = new List<V_TRONGTAI>();
            foreach (TRONGTAI v in dsct)
            {
                ds.Add(new V_TRONGTAI
                {
                    m_MaTrongTai = v.MATT,
                    m_HoTen = v.HOTEN,
                    m_NgaySinh = (DateTime)v.NGAYSINH
                });

            }
            return ds;
        }
        // Kiểm tra trọng tài  tồn tại trong database từ mã trọng tài
        public static bool TimTrongTai(string MATT)
        {
            return DB_QUERY.TimTrongTai(MATT);
        }


        // Thêm trọng tài (hàm này sẽ được gói bên phía Views, truyền vào các tham số ở màn hình đó)
        public static bool ThemTrongTai(V_TRONGTAI a_TrongTai)
        {
            // Sau khi kiểm tra ổn thỏa trọng tài hợp lệ thì nhét vào db.
            TRONGTAI newTrongTai = new TRONGTAI
            {
                MATT = a_TrongTai.m_MaTrongTai,
                HOTEN = a_TrongTai.m_HoTen,
                NGAYSINH = a_TrongTai.m_NgaySinh,
                DIACHI = a_TrongTai.m_DiaChi,
                QUOCTICH = a_TrongTai.m_QuocTich,
                SONAMKINHNGHIEM = (byte)a_TrongTai.m_SoNamKinhNghiem,
                LOAITRONGTAI = a_TrongTai.m_LoaiTrongTai,
                AVATAR = a_TrongTai.m_AVT,
            };
            // Kiểm tra và thêm vào db
            if (DB_QUERY.TimTrongTai(a_TrongTai.m_MaTrongTai) == false)           
            {
                Database.DB_INSERTING.ThemTrongTai(newTrongTai);
                return true;
            }
            return false;
        }

        #region Bình
        public static bool KiemTraTTTonTai(string a_TenTT)
        {
            return Database.DB_QUERY.KiemTraTTTonTai(a_TenTT);
        }

        public static List<V_TRONGTAI> TraCuuTT(string tenTrongTai)
        {
            List<V_TRONGTAI> v_TT = new List<V_TRONGTAI>();
            List<TRONGTAI> trongtai = new List<TRONGTAI>();
            trongtai = Database.DB_QUERY.TraCuuTT(tenTrongTai);


            foreach (TRONGTAI t in trongtai)
            {
                V_TRONGTAI temp = new V_TRONGTAI();
                temp.m_MaTrongTai = t.MATT;
                temp.m_HoTen = t.HOTEN;
                temp.m_NgaySinh = (DateTime)t.NGAYSINH;
                temp.m_DiaChi = t.DIACHI;
                temp.m_QuocTich = t.QUOCTICH;
                temp.m_SoNamKinhNghiem = (byte)t.SONAMKINHNGHIEM;
                temp.m_AVT = t.AVATAR;
                temp.m_LoaiTrongTai = (bool)t.LOAITRONGTAI;
                v_TT.Add(temp);
            }

            return v_TT;
        }

        public static void ChinhSuaTrongTai(V_TRONGTAI a_TrongTai)
        {
            TRONGTAI tt = new TRONGTAI();
            tt.MATT = a_TrongTai.m_MaTrongTai;
            tt.HOTEN = a_TrongTai.m_HoTen;
            tt.NGAYSINH = a_TrongTai.m_NgaySinh;
            tt.DIACHI = a_TrongTai.m_DiaChi;
            tt.QUOCTICH = a_TrongTai.m_QuocTich;
            tt.SONAMKINHNGHIEM = (byte)a_TrongTai.m_SoNamKinhNghiem;
            tt.AVATAR = a_TrongTai.m_AVT;
            tt.LOAITRONGTAI = a_TrongTai.m_LoaiTrongTai;
            Database.DB_UPDATE.ChinhSuaTrongTai(tt);
        }

        public static void XoaTrongTai(string a_TrongTai)
        {
            Database.DB_DELETE.XoaTrongTai(a_TrongTai);
        }
        #endregion

    }
}
