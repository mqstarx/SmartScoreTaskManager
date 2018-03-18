using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CoreLib
{
    [Serializable]

    //104857600 maks len 100mb
    public class UserFile
    {
        private byte[] m_FileData;
        
        private string m_FileName;

        public UserFile(string path)
        {
            FileInfo info = new FileInfo(path);
            if(info.Exists)
            {
                try
                {
                    m_FileData = new byte[info.Length];
                    FileStream streamReader = new FileStream(path, FileMode.Open, FileAccess.Read);
                    streamReader.Read(m_FileData, 0, m_FileData.Length);
                    m_FileName = info.Name;
                }
                catch
                {
                    m_FileData = null;
                }
            }
        }

        /// <summary>
        /// для тестирования максимального размера файла
        /// </summary>
        /// <param name="len"></param>
        public UserFile(int len)
        {
            m_FileData = new byte[len];
            m_FileName = "test_" + len.ToString()+".txt";
            
        }
        public bool SaveToLocal(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                fs.Write(m_FileData, 0, m_FileData.Length);
                fs.Close();
                
               
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

       

        public string FileName
        {
            get
            {
                return m_FileName;
            }

           
        }
        public int FileLength
        {
            get
            {
                if (m_FileData == null)
                    return 0;
                else
                    return m_FileData.Length;

            }
        }
    }
}
