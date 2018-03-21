using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLib
{
    [Serializable]
    public class Message
    {
        private string m_Msg;
        private FileAttachments m_FileAttachments;
        private bool m_IsReaded;
        private bool m_IsSync;
        private UserInfo m_FromId;
        private UserInfo m_ToId;
        private string m_UidMsg;

        private DateTime m_DateTimeOfMessage;



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

      

        public string UidMsg
        {
            get
            {
                return m_UidMsg;
            }


        }

        public UserInfo FromId
        {
            get
            {
                return m_FromId;
            }

          
        }

        public UserInfo ToId
        {
            get
            {
                return m_ToId;
            }

            
        }

        public DateTime DateTimeOfMessage
        {
            get
            {
                return m_DateTimeOfMessage;
            }

            
        }

        public Message(string msg,UserInfo from,UserInfo to)
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
            m_DateTimeOfMessage = DateTime.Now;

        }
    }
}
