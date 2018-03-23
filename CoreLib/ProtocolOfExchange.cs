using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public enum ProtocolOfExchange
    {
        AskNewMessages,NewMessages,AskNewTasks,NewTasks,
        NewMessageToUser,NewMessageToUserOk,NewTaskForUser,CheckConnection, CheckConnectionOK,
        AskUserInfoList,UserInfoListOk,AddUser,AddUserOK,TryAuth,AuthOk,AuthFail,SendMessage,MessageDelivered,MessageWaitDelivery,SendMessageFail,
        SyncMessages,SyncMessagesStart,SyncMessagesEnd,MessageRead,


    }
    
}
