using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace CoreLib
{
    /// <summary>
    /// класс контейнер хранит имя файла и значок 
    /// </summary>
    public class UserFileInfo
    {
        private string m_FileName;
        private Icon m_Icon;
        public UserFileInfo(string filename, Icon icon)
        {
            m_FileName = filename;
            m_Icon = icon;           
        }

        public string FileName
        {
            get
            {
                return m_FileName;
            }

            
        }

        public Icon Icon
        {
            get
            {
                return m_Icon;
            }

           
        }
    }
}
