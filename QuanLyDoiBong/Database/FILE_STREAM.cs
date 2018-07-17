using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;

namespace QuanLyDoiBong.Database
{
    public static class FILE_STREAM
    {
        #region Mở hộp thoại để load ảnh
        // Hỗ trợ ảnh jpg, jpeg, png
        public static BitmapImage LoadDuLieuOPImage()
        {
            BitmapImage bmImage = null;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG(*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                bmImage = new BitmapImage(new Uri(op.FileName));
            }
            return bmImage;
        }
        #endregion

        #region Lưu hình jpg
        // Hàm thực hiện việc nạp source từ 1 control image và tạo file có đường dẫn path
        public static void LuuImage(String path, BitmapSource img)
        {

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(img));
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }
        #endregion

        #region Load hình jpg
        // Hàm thực hiện việc load dữ liệu image từ 1 file path và trả về đối tượng BitmapImage
        public static BitmapImage LoadImage(String path)
        {
            BitmapImage bmImg = null;
            bmImg = new BitmapImage(new Uri(path));
            return bmImg;
        }
        #endregion

        #region Lưu chức năng xuống file AppFunction.dat
        public static void LuuChucNangUser(List<int> f0, List<int> f1, List<int> f2)
        {
            string fileName = "AppFunction.dat";
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                writer.Write(f0.Count);
                for (int i = 0; i < f0.Count; i++)
                {
                    writer.Write(f0[i]);
                }

                writer.Write(f1.Count);
                for (int i = 0; i < f1.Count; i++)
                {
                    writer.Write(f1[i]);
                }

                writer.Write(f2.Count);
                for (int i = 0; i < f2.Count; i++)
                {
                    writer.Write(f2[i]);
                }
            }
        }
        #endregion

        #region Load chức năng từ file AppFunction.dat
        public static void LoadChucNangUser(ref List<int> af0, ref List<int> af1, ref List<int> af2)
        {
            
            string fileName = "AppFunction.dat";
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    af0.Clear();
                    af1.Clear();
                    af2.Clear();
                    int num0, num1, num2;
                    num0 = num1 = num2 = 0;
                    
                    num0 = reader.ReadInt32();
                   
                    for (int i = 0; i < num0; i++)
                    {
                        int value = reader.ReadInt32();
                        af0.Add(value);
                        
                    }

                    num1 = reader.ReadInt32();
                    for (int i = 0; i < num1; i++)
                    {
                        int value = reader.ReadInt32();
                        af1.Add(value);
                    }

                    num2 = reader.ReadInt32();
                    for (int i = 0; i < num2; i++)
                    {
                        int value = reader.ReadInt32();
                        af2.Add(value);
                    }            
                }
            }
         
        }
        #endregion
    }
}
