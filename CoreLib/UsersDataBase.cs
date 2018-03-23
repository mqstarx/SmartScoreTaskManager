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
                if (user.PostName == item.PostName)
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
        /// изменение пользователя начальника пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="id_parent_new"></param>
        /// <returns></returns>
        public bool ChangeUserParent(int id,int id_parent_new)
        {
            for (int i = 0; i < m_Users.Count; i++)
            {
                if (m_Users[i].Id == id)
                {
                    m_Users[i].IdParent = id_parent_new;
                    
                    
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

        /// <summary>
        /// возвращает объект класса User по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetUserObject(int id)
        {
            foreach(User user in this)
            {
                if (user.Id == id)
                    return user;
            }
            return null;
        }

        /// <summary>
        ///  используется для передачи информации о пользователях по сети
        /// </summary>
        /// <returns> возвращает список объектов userinfo(без паролей)</returns>
        public List<UserInfo> GetListUserInfo()
        {
            List<UserInfo> _userList = new List<UserInfo>();
            foreach(User user in this)
            {
                _userList.Add(new UserInfo(user.Id, user.IdParent, user.FullName, user.PostName));
            }
            return _userList;
        }

        /// <summary>
        /// аутентификация пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="passwordSha256"></param>
        /// <returns></returns>
        public bool IsUserAuth(int id,string passwordSha256)
        {
            foreach(User user in this)
            {
                if(user.Id == id)
                {
                    if (Hash.getHashSha256(Hash.DecodeString(user.Password)) == passwordSha256)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// аутентификация пользователя
        /// </summary>
        /// <param name="info"></param>
        /// 
        /// <returns></returns>
        public bool IsUserAuth(AuthInfo info)
        {
            foreach (User user in this)
            {
                if (user.Id == info.UserId)
                {
                    if (Hash.getHashSha256(Hash.DecodeString(user.Password)) == info.Password)
                        return true;
                }
            }
            return false;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)m_Users).GetEnumerator();
        }
    }
}
