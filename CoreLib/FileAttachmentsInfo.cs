using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLib
{
    public class FileAttachmentsInfo
    {
        private string m_AtachUID;
        private List<UserFileInfo> m_FilesInfo;
        public FileAttachmentsInfo()
        {
            Random rnd1 = new Random((int)DateTime.Now.Ticks);
            Thread.Sleep(10);
            Random rnd2 = new Random((int)DateTime.Now.Ticks + rnd1.Next());
            Thread.Sleep(10);
            m_AtachUID = rnd2.Next().ToString();
        }

        public string AtachUID
        {
            get
            {
                return m_AtachUID;
            }

 
        }

        public List<UserFileInfo> FilesInfo
        {
            get
            {
                return m_FilesInfo;
            }

            set
            {
                m_FilesInfo = value;
            }
        }
    }
}
