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
        
       
        public MessageDataBase()
        {                        
        }

        public void NewMessage(Message msg)
        {
            // добавляем в  базу отправителя и сохраняем в файл сообщение  
            List<Message> msgArrayfrom = (List<Message>)Func.LoadConfig(@"Messages\From\"+msg.FromId.Id.ToString()+".bin");
            if (msgArrayfrom == null)
                msgArrayfrom = new List<Message>();
            msgArrayfrom.Add(msg);
            Func.SaveConfig(msgArrayfrom, @"Messages\From\" + msg.FromId.Id.ToString() + ".bin");

            // добавляем в  базу получателя и сохраняем в файл сообщение  
            List<Message> msgArrayTo= (List<Message>)Func.LoadConfig(@"Messages\To\" + msg.ToId.Id.ToString() + ".bin");
            if (msgArrayTo == null)
                msgArrayTo = new List<Message>();
            msgArrayTo.Add(msg);
            Func.SaveConfig(msgArrayTo, @"Messages\To\" + msg.ToId.Id.ToString() + ".bin");

        }




        /// <summary>
        ///   синхронизация входящих(исходящих) сообщений пользователя
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <param name="msgUid">список id сообщение которые уже есть у пользователя</param>
        /// <param name="from">Синхронизация исходящих или входящих сообщений</param>
        /// <returns></returns>

        public List<Message> SyncMessages(UserInfo user, string[] msgUid,bool from)
        {
            List<Message> userMessagaes = null;
            if(from)
                userMessagaes = (List<Message>)Func.LoadConfig(@"Messages\From\" + user.Id.ToString() + ".bin");
            else
                userMessagaes = (List<Message>)Func.LoadConfig(@"Messages\To\" + user.Id.ToString() + ".bin");

            if (userMessagaes == null)
                return null;

            List<Message> return_list = new List<Message>();


            foreach (Message msg in userMessagaes)
            {
                bool is_find = false;
                foreach (string uid in msgUid)
                {
                    if (uid == msg.UidMsg)
                    {
                        is_find = true;
                        break;
                    }
                }

                if (!is_find)
                {
                    if (from && user.Id == msg.FromId.Id ) 
                    return_list.Add(msg);

                    if(!from && user.Id == msg.ToId.Id)
                        return_list.Add(msg);
                }
            }
            if (return_list.Count == 0)
                return null;
            else
                return return_list;

        }


        /// <summary>
        /// Помечает сообщение как прочитанное пользователем
        /// </summary>
        /// <param name="usrTo">Пользователь прочитавший сообщение</param>
        /// <param name="msgUid">id сообщения</param>
        /// <returns></returns>
        public bool MessageReaded(UserInfo usrTo, string msgUid)
        {
            //
            List<Message>userMessagaes = (List<Message>)Func.LoadConfig(@"Messages\To\" + usrTo.Id.ToString() + ".bin");
            if (userMessagaes == null)
                return false;

            foreach(Message msg in userMessagaes)
            {
                if(msgUid == msg.UidMsg)
                {
                   

                    //получаем исходящие сообщения пользователя, который отправлял сообщение 
                    List<Message> userMessagefrom = (List<Message>)Func.LoadConfig(@"Messages\From\" + msg.FromId.Id.ToString() + ".bin");
                    if (userMessagefrom == null)
                        return false;
                    /////////////////
                    bool is_msgfromFind = false;
                    foreach (Message msgfrom in userMessagefrom)
                    {
                        
                        if(msgUid ==msgfrom.UidMsg)
                        {
                            msgfrom.IsReaded = true;
                            Func.SaveConfig(userMessagefrom, @"Messages\From\" + msg.FromId.Id.ToString() + ".bin");
                            is_msgfromFind = true;
                        
                        }

                    }
                    if (!is_msgfromFind)
                        return false;
                    
                    ///////////
                    msg.IsReaded = true;
                    //изменяем сообщение в папке входящие для пользователя который прочитал 
                    Func.SaveConfig(userMessagaes, @"Messages\To\" + usrTo.Id.ToString() + ".bin");
                    //////////////////

                    
                    return true;
                }
            }
            return false;

        }

    }
}
