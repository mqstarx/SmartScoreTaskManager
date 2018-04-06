using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [Serializable]
    public class NetworkTransferObjects
    {
        private AuthInfo m_AuthInfo;
        private Message m_Message;
        private Message m_Message_1;
        private UserInfo m_UserInfo;
        private string[] m_MessageUids;
        private string[] m_MessageUids_1;
        private List<UserInfo> m_ListUserInfo;
        private List<Message> m_ListMessages;
        private List<Message> m_ListMessages_1;
        private User m_User;
        private ProtocolOfExchange m_ProtocolMessage;
        private byte[] m_Data;
        public AuthInfo AuthInfo
        {
            get
            {
                return m_AuthInfo;
            }

            set
            {
                m_AuthInfo = value;
            }
        }

        public Message Message
        {
            get
            {
                return m_Message;
            }

            set
            {
                m_Message = value;
            }
        }

        public UserInfo UserInfo
        {
            get
            {
                return m_UserInfo;
            }

            set
            {
                m_UserInfo = value;
            }
        }

        public string[] MessageUids
        {
            get
            {
                return m_MessageUids;
            }

            set
            {
                m_MessageUids = value;
            }
        }

        public List<UserInfo> ListUserInfo
        {
            get
            {
                return m_ListUserInfo;
            }

            set
            {
                m_ListUserInfo = value;
            }
        }

        public List<Message> ListMessages
        {
            get
            {
                return m_ListMessages;
            }

            set
            {
                m_ListMessages = value;
            }
        }

        public User User
        {
            get
            {
                return m_User;
            }

            set
            {
                m_User = value;
            }
        }

        public List<Message> ListMessages_1
        {
            get
            {
                return m_ListMessages_1;
            }

            set
            {
                m_ListMessages_1 = value;
            }
        }

        public string[] MessageUids_1
        {
            get
            {
                return m_MessageUids_1;
            }

            set
            {
                m_MessageUids_1 = value;
            }
        }

        public Message Message_1
        {
            get
            {
                return m_Message_1;
            }

            set
            {
                m_Message_1 = value;
            }
        }

        public ProtocolOfExchange ProtocolMessage
        {
            get
            {
                return m_ProtocolMessage;
            }

            set
            {
                m_ProtocolMessage = value;
            }
        }

        public byte[] Data
        {
            get
            {
                return m_Data;
            }

            set
            {
                m_Data = value;
            }
        }

        public NetworkTransferObjects()
        { }
        public static NetworkTransferObjects GetObjectFromArray(byte[] data)
        {
            BinaryFormatter _bf = new BinaryFormatter();
            // _bf.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.XsdString;
            MemoryStream _ms = new MemoryStream(data.Length);
            _ms.Write(data, 0, data.Length);
            _ms.Position = 0;
            object res = _bf.Deserialize(_ms);

            _ms.Close();
            return (NetworkTransferObjects)res;

        }

        public byte[] GetByteArray()
        {
            BinaryFormatter _bf = new BinaryFormatter();
            //_bf.TypeFormat = System.Runtime.Serialization.Formatters.FormatterTypeStyle.XsdString;
            MemoryStream _ms = new MemoryStream();
            _bf.Serialize(_ms, this);
            byte[] res = new byte[_ms.Length];
            _ms.Position = 0;
            _ms.Read(res, 0, res.Length);
            _ms.Close();

            // object test = ObjectFromByteArray(res,0,res.Length);
            return res;
        }
    }
}
