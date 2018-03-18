using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLib
{
    public class Message
    {
        private string m_Msg;
        private FileAttachments m_FileAttachments;
        private bool m_IsReaded;
        private bool m_IsSync;
        private int m_FromId;
        private int m_ToId;
        private string m_UidMsg;

        public string Msg
        {
            get
            {
                return m_Msg;
            }

            set
            {
                m_Msg = value;
            }
        }

        public FileAttachments FileAttachments
        {
            get
            {
                return m_FileAttachments;
            }

            set
            {
                m_FileAttachments = value;
            }
        }

        /// <summary>
        /// указывает прочтено ли сообщение
        /// </summary>
        public bool IsReaded
        {
            get
            {
                return m_IsReaded;
            }

            set
            {
                m_IsReaded = value;
            }
        }

        /// <summary>
        /// Указывает нужно ли синхронизировать
        /// </summary>
        public bool IsSync
        {
            get
            {
                return m_IsSync;
            }

            set
            {
                m_IsSync = value;
            }
        }

        public string UidMsg
        {
            get
            {
                return m_UidMsg;
            }


        }

        public Message(string msg,int from,int to)
        {
            m_Msg = msg;
            m_FromId = from;
            m_ToId = to;
            string sha =  Hash.getHashSha256(msg);
            Random rnd1 = new Random((int)DateTime.Now.Ticks);
            Thread.Sleep(10);
            Random rnd2 = new Random((int)DateTime.Now.Ticks+rnd1.Next());
            Thread.Sleep(10);
            m_UidMsg = rnd2.Next().ToString()+sha;

        }
    }
}
