using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [Serializable]
    public class AuthInfo
    {
        private int m_UserId;
        private string m_Password;


        public AuthInfo(int id,string password)
        {
            m_UserId = id;
            m_Password = Hash.getHashSha256(password);
        }
        public int UserId
        {
            get
            {
                return m_UserId;
            }

           
        }

        public string Password
        {
            get
            {
                return m_Password;
            }

           
        }
    }
}
