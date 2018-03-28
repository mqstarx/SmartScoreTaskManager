using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLib
{
    public class TcpModuleLowLevel
    {
        #region Определение событий сетевого модуля

        // Типы делегатов для обработки событий в паре с соответствующим объектом события.

        // Обработчики события акцептирования (принятия клиентов) прослушивающим сокетом
        public delegate void AcceptEventHandler(object sender);
        public event AcceptEventHandler Accept;

        // Обработчики события подключения клиента к серверу
        public delegate void ConnectedEventHandler(object sender, string result);
        public event ConnectedEventHandler Connected;

        // Обработчики события отключения конечных точек (клиентов или сервера)
        public delegate void DisconnectedEventHandler(object sender, string result);
        public event DisconnectedEventHandler Disconnected;

        // Обработчики события извлечения данных 
        public delegate void ReceiveEventHandler(object sender, ReceiveEventArgs e);
        public event ReceiveEventHandler Receive;

        #endregion

        #region Объявления членов класса

        // Родительская форма необходима для визуальной информации 
        // о внутреннем состоянии и событиях работы сетвого модуля.
        //  public Form1 Parent;

        // Прослушивающий сокет для работы модуля в режиме сервера TCP
        private TcpListener m_TcpListener;
        private TcpClient m_TcpClient;

        private Thread m_ServerThread;
        private int m_Port;
        private string m_DebugMsg = "";

        public TcpModuleLowLevel(int port)
        {

            m_Port = port;
        }
        private void StartServerThread()
        {

            try
            {
                m_TcpListener = new TcpListener(IPAddress.Any, m_Port);
                m_TcpListener.Start();
                byte[] buffer = new byte[1024 * 1000];
                while (true)
                {
                    m_DebugMsg = "Ожидание соединения......";
                    TcpClient client = m_TcpListener.AcceptTcpClient();
                    m_DebugMsg = "Клиент подключился";
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    i = stream.Read(buffer, 0, buffer.Length);

                }
            }
            catch (Exception e)
            {
                m_TcpListener = null;
                throw e;
            }
        }





    }
}
#endregion