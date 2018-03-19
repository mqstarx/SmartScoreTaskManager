using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [Serializable]
    public class MessageDataBase
    {
       
        private List<Message> m_Messages;
        public MessageDataBase()
        {
            m_Messages = new List<Message>();   
            
        }
    }
}
