using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreLib
{
    public static class Func
    {
        
        public static bool SaveConfig(object obj, string filename)
        {
            FileStream fs = null;
            try
            {
                
                if(!Directory.Exists(Path.GetDirectoryName(Application.StartupPath + @"\" + filename)))
                  Directory.CreateDirectory(Path.GetDirectoryName(Application.StartupPath + @"\" + filename));
                fs = new FileStream(Application.StartupPath + @"\" + filename, FileMode.Create, FileAccess.Write, FileShare.Write);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                if (fs != null)
                    fs.Close();
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }


        }
        public static bool SaveFile(string filename,byte[] data)
        {
            FileStream fs = null;
            try
            {

                if (!Directory.Exists(Path.GetDirectoryName(Application.StartupPath + @"\" + filename)))
                    Directory.CreateDirectory(Path.GetDirectoryName(Application.StartupPath + @"\" + filename));
                fs = new FileStream(Application.StartupPath + @"\" + filename, FileMode.Create, FileAccess.Write, FileShare.Write);
                fs.Write(data, 0, data.Length);
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                if (fs != null)
                    fs.Close();
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }


        }
        public static object LoadConfig(string filename)
        {
            FileStream fs = null;
            try
            {

                fs = new FileStream(Application.StartupPath + @"\" + filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                object res = bf.Deserialize(fs);
                fs.Close();
                return res;
            }
            catch (Exception e)
            {
                if (fs != null)
                    fs.Close();
                return null;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }


        }
        public static byte[] LoadFile(string filename)
        {

            FileStream fs = null;
            try
            {

                fs = new FileStream(Application.StartupPath + @"\" + filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                byte[] result = new byte[fs.Length];
                fs.Read(result, 0, result.Length);
                fs.Close();
                return result;
            }
            catch (Exception e)
            {
                if (fs != null)
                    fs.Close();
                return null;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }


        }
    }
}
