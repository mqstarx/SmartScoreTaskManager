using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [Serializable]
    public class UserInfo : User
    {
        public UserInfo(int id, int idparent, string fullname, string postname) : base(id, idparent, "", fullname, postname)
        {
        }
       
    }
}
