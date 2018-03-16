using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    [Serializable]
    public class UsersDataBase:IEnumerable
    {
        private List<User> m_Users;
        public UsersDataBase()
        {
            m_Users = new List<User>();
        }

       
       
        /// <summary>
        /// Добавляет пользователя в базу
        /// </summary>
        /// <param name="item">объект класса user</param>
        /// <returns></returns>
        public bool Add(User item)
        {
           foreach (User user in m_Users)
            {
                if (user.Id == item.Id)
                    return false;
            }
            m_Users.Add(item);
            return true;
        }

        /// <summary>
        /// Удаляет пользователя из массива
        /// </summary>
        /// <param name="item">Объект пользователя(удаляется при совпадении ID)</param>
        /// <returns></returns>
        public bool RemoveUser(User item)
        {
            for(int i=0;i<m_Users.Count;i++)
            {
                if (m_Users[i].Id == item.Id)
                {
                    m_Users.RemoveAt(i);
                    return true;
                }
            }
           
            return false;
        }

        /// <summary>
        /// Удаляет пользователя из массива
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns></returns>
        public bool RemoveUser(int id)
        {
            for (int i = 0; i < m_Users.Count; i++)
            {
                if (m_Users[i].Id ==id)
                {
                    m_Users.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// возвращает объект класса User по индексу
        /// </summary>
        /// <param name="index">индекс объекта в массиве</param>
        /// <returns></returns>
        public User this[int index]
        {
            get { return m_Users[index]; }
            
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)m_Users).GetEnumerator();
        }
    }
}
