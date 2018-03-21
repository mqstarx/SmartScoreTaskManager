using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [Serializable]
    public class User
    {
        private int m_Id;
        private int m_IdParent;
        private string m_Password;
        private string m_FullName;
        private string m_PostName;
        private int m_UserBalans;
        private User usr;

        /// <summary>
        /// Идентификатор пользователя (должен быть уникальным)
        /// </summary>
        public int Id
        {
            get
            {
                return m_Id;
            }          
        }

        /// <summary>
        /// Идентификатор пользователя начальника
        /// </summary>
        public int IdParent
        {
            get
            {
                return m_IdParent;
            }

            set
            {
                m_IdParent = value;
            }
        }

        /// <summary>
        /// Пароль для доступа к учетной записи
        /// </summary>
        public string Password
        {
            get
            {
                return m_Password;
            }

            set
            {
                m_Password = value;
            }
        }

        /// <summary>
        /// ФИО сотрудника
        /// </summary>
        public string FullName
        {
            get
            {
                return m_FullName;
            }

            set
            {
                m_FullName = value;
            }
        }

        /// <summary>
        /// Наименование должности
        /// </summary>
        public string PostName
        {
            get
            {
                return m_PostName;
            }

            set
            {
                m_PostName = value;
            }
        }

        /// <summary>
        /// Текущий балланс пользователя
        /// </summary>
        public int UserBalans
        {
            get
            {
                return m_UserBalans;
            }

            set
            {
                m_UserBalans = value;
            }
        }

        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="id"> Идентификатор пользователя (должен быть уникальным)</param>
        /// <param name="idparent">Идентификатор пользователя начальника</param>
        /// <param name="password">Пароль для доступа к учетной записи</param>
        /// <param name="fullname"> ФИО сотрудника</param>
        /// <param name="postname">Наименование должности</param>
        public User(int id,int idparent,string password,string fullname,string postname)
        {
            m_Id = id;
            m_IdParent = idparent;
            m_Password = password;
            m_FullName = fullname;
            m_PostName = postname;
            m_UserBalans = 0;
        }

        public User(User usr)
        {
            this.usr = usr;
        }

        public override string ToString()
        {
            return m_PostName + ": " + m_FullName;
        }

    }
}
