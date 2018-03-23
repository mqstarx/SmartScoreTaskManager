using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreLib
{
    /// <summary>
    /// Класс для передачи десериализированного контейнера при 
    /// возникновении события получения сетевых данных.
    /// </summary>
    public class ReceiveEventArgs : EventArgs
    {
        private SendInfo _sendinfo;
        private NetworkTransferObjects _object;
        public ReceiveEventArgs(SendInfo sendinfo,NetworkTransferObjects obj)
        {
            _sendinfo = sendinfo;
            _object = obj;
        }
       
        public NetworkTransferObjects NetDataObj
        {
            get { return _object; }
        }
        public SendInfo SendInfo
        {
            get { return _sendinfo; }
        }

    }


    /// <summary>
    /// Класс способный выступать в роли сервера или клиента в TCP соединении.
    /// Отправляет и получает по сети файлы и текстовые сообщения.
    /// </summary>
    public class TcpModule
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
        private TcpListener _tcpListener;

        // Удобный контейнер для подключенного клиента.
        private TcpClientData _tcpClient;

        /// <summary>
        /// Возможные режимы работы TCP модуля
        /// </summary>
        public enum Mode { indeterminately, Server, Client };

        /// <summary>
        /// Режим работы TCP модуля
        /// </summary>
        public Mode modeNetwork;

        #endregion


        #region Интерфейсная часть TCP модуля

        /// <summary>
        /// Запускает сервер, прослушивающий все IP адреса, и одновременно
        /// метод асинхронного принятия (акцептирования) клиентов.
        /// </summary>
        public void StartServer(int port)
        {
            if (modeNetwork == Mode.indeterminately)
            {
                try
                {
                    _tcpListener = new TcpListener(IPAddress.Any, port);
                    _tcpListener.Start();
                    _tcpListener.BeginAcceptTcpClient(AcceptCallback, _tcpListener);
                    modeNetwork = Mode.Server;

                    //Parent.ChangeBackColor(Color.Plum);
                }
                catch (Exception e)
                {
                    _tcpListener = null;
                  //  Parent.ShowReceiveMessage(e.Message);
                }
            }
            else
            {
                SoundError("error start server");
            }
        }


        /// <summary>
        /// Остановка сервера
        /// </summary>
        public void StopServer()
        {
            if (modeNetwork == Mode.Server)
            {
                modeNetwork = Mode.indeterminately;
                _tcpListener.Stop();
                _tcpListener = null;


                DeleteClient(_tcpClient);


              //  Parent.ChangeBackColor(Color.FromKnownColor(KnownColor.Control));
            }
        }


        /// <summary>
        /// Попытка асинхронного подключения клиента к серверу
        /// </summary>
        /// <param name="ipserver">IP адрес сервера</param>
        public void ConnectClient(string ipserver,int port)
        {
            if (modeNetwork == Mode.indeterminately)
            {
                _tcpClient = new TcpClientData();
                _tcpClient.tcpClient.BeginConnect(IPAddress.Parse(ipserver), port, new AsyncCallback(ConnectCallback), _tcpClient);

                modeNetwork = Mode.Client;
            }
            else
            {
               // SoundError();
           }
        }


        /// <summary>
        /// Отключение клиента от сервера
        /// </summary>
        public void DisconnectClient()
        {
            if (modeNetwork == Mode.Client)
            {
                modeNetwork = Mode.indeterminately;
                DeleteClient(_tcpClient);
            }
        }


        /// <summary>
        /// Завершение работы подключенного клиента
        /// </summary>
        private void DeleteClient(TcpClientData mtc)
        {
            if (mtc != null && mtc.tcpClient.Connected == true)
            {
                mtc.tcpClient.GetStream().Close(); // по настоянию MSDN закрываем поток отдельно у клиента
                mtc.tcpClient.Close(); // затем закрываем самого клиента
            }
        }


        /// <summary>
        /// Метод упрощенного создания заголовка с информацией о размере данных отправляемых по сети.
        /// </summary>
        /// <param name="length">длина данных подготовленных для отправки по сети</param>
        /// <returns>возращает байтовый массив заголовка</returns>
        private byte[] GetHeader(int length)
        {
            string header = length.ToString();
            if (header.Length < 9)
            {
                string zeros = null;
                for (int i = 0; i < (9 - header.Length); i++)
                {
                    zeros += "0";
                }
                header = zeros + header;
            }

            byte[] byteheader = Encoding.Default.GetBytes(header);


            return byteheader;
        }



        //public string SendFileName = null;
        public void SendData(NetworkTransferObjects obj,ProtocolOfExchange protokolMsg)
        {
            // Состав отсылаемого универсального сообщения
            // 1. Заголовок о следующим объектом класса подробной информации дальнейших байтов
            // 2. Объект класса подробной информации о следующих байтах
            // 3. Байты непосредственно готовых к записи в файл или для чего-то иного.
            try {
                SendInfo si = new SendInfo();
                si.ProtocolMsg = protokolMsg;


                //  Если нет сообщения и отсылаемого файла продолжать процедуру отправки нет смысла.
                //if (si.message == null && obj==null) return;

                byte[] _obj_arr;// = new byte[10];
                if (obj != null)
                {
                    BinaryFormatter _bf = new BinaryFormatter();
                    MemoryStream _ms = new MemoryStream();
                    _bf.Serialize(_ms, obj);
                    si.datasize = (int)_ms.Length;
                    _obj_arr = _ms.ToArray();
                    _ms.Close();

                }
                else
                {
                    si.datasize = 0;
                    _obj_arr = null;

                }

                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, si);
                ms.Position = 0;
                byte[] infobuffer = new byte[ms.Length];
                int r = ms.Read(infobuffer, 0, infobuffer.Length);
                ms.Close();

                byte[] header = GetHeader(infobuffer.Length);
                byte[] total = new byte[header.Length + infobuffer.Length + si.datasize];

                Buffer.BlockCopy(header, 0, total, 0, header.Length);
                Buffer.BlockCopy(infobuffer, 0, total, header.Length, infobuffer.Length);

                // Если путь файла указан, добавим его содержимое в отправляемый массив байтов
                if (si.datasize > 0 && _obj_arr != null)
                    Buffer.BlockCopy(_obj_arr, 0, total, header.Length + infobuffer.Length, si.datasize);

                // Отправим данные подключенным клиентам
                NetworkStream ns = _tcpClient.tcpClient.GetStream();
                // Так как данный метод вызывается в отдельном потоке рациональней использовать синхронный метод отправки
                ns.Write(total, 0, total.Length);
                

                // Обнулим все ссылки на многобайтные объекты и попробуем очистить память
                header = null;
                infobuffer = null;
                total = null;

                //Parent.labelFileName.Text = "";
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception e)
            {
                SoundError(e.Message);
            }
            // Подтверждение успешной отправки
          //  Parent.ShowReceiveMessage("Данные успешно отправлены!");
        }


        /// <summary>
        /// Универсальный метод останавливающий работу сервера и закрывающий все сокетыю
        /// вызывается в событии закрытия родительской формы.
        /// </summary>
        public void CloseSocket()
        {
            StopServer();
            DisconnectClient();
        }

        /// <summary>
        /// Звуковое сопровождение ошибок.
        /// </summary>
        private void SoundError(string err)
        {
            Console.Beep(3000, 30);
            Console.Beep(1000, 30);
            MessageBox.Show(err);
        }

        #endregion

        public override string ToString()
        {
            IPEndPoint ip = (IPEndPoint)_tcpClient.tcpClient.Client.RemoteEndPoint;
            return ip.Address.ToString();
        }

        #region Асинхронные методы сетевой работы TCP модуля


        /// <summary>
        /// Обратный метод завершения принятия клиентов
        /// </summary>
        public void AcceptCallback(IAsyncResult ar)
        {
            if (modeNetwork == Mode.indeterminately) return;

            TcpListener listener = (TcpListener)ar.AsyncState;
            try
            {
                _tcpClient = new TcpClientData();
                _tcpClient.tcpClient = listener.EndAcceptTcpClient(ar);


                // Немедленно запускаем асинхронный метод извлечения сетевых данных
                // для акцептированного TCP клиента
                NetworkStream ns = _tcpClient.tcpClient.GetStream();
                _tcpClient.buffer = new byte[global.LENGTHHEADER];
                ns.BeginRead(_tcpClient.buffer, 0, _tcpClient.buffer.Length, new AsyncCallback(ReadCallback), _tcpClient);


                // Продолжаем ждать запросы на подключение
                listener.BeginAcceptTcpClient(AcceptCallback, listener);

                // Активация события успешного подключения клиента
                if (Accept != null)
                {
                    Accept.BeginInvoke(this, null, null);
                }
            }
            catch(Exception e)
            {
                // Обработка исключительных ошибок возникших при акцептирования клиента.
                SoundError(e.Message);
            }
        }


        /// <summary>
        /// Метод вызываемый при завершении попытки поключения клиента
        /// </summary>
        public void ConnectCallback(IAsyncResult ar)
        {
            string result = "OK";
            try
            {
                // Получаем подключенного клиента
                TcpClientData myTcpClient = (TcpClientData)ar.AsyncState;
                NetworkStream ns = myTcpClient.tcpClient.GetStream();
                myTcpClient.tcpClient.EndConnect(ar);

                // Запускаем асинхронный метод чтения сетевых данных для подключенного TCP клиента
                myTcpClient.buffer = new byte[global.LENGTHHEADER];
                ns.BeginRead(myTcpClient.buffer, 0, myTcpClient.buffer.Length, new AsyncCallback(ReadCallback), myTcpClient);
              //  Parent.ChangeBackColor(Color.Goldenrod);

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                // Обработка ошибок подключения
                DisconnectClient();
                result = "ERR";
               // SoundError();
            }

            // Активация события успешного или неуспешного подключения к серверу,
            // здесь серверу можно отослать ознакомительные данные о себе (например, имя клиента)
            if (Connected != null)
                Connected.BeginInvoke(this, result, null, null);
        }


        /// <summary>
        /// Метод асинхронно вызываемый при наличие данных в буферах приема.
        /// </summary>

        public void ReadCallback(IAsyncResult ar)
        {
            if (modeNetwork == Mode.indeterminately) return;

            TcpClientData myTcpClient = (TcpClientData)ar.AsyncState;

            try
            {
                NetworkStream ns = myTcpClient.tcpClient.GetStream();

                int r = ns.EndRead(ar);

                if (r > 0)
                {
                    // Из главного заголовка получим размер массива байтов информационного объекта
                    string header = Encoding.Default.GetString(myTcpClient.buffer);
                    int leninfo = int.Parse(header);

                    // Получим и десериализуем объект с подробной информацией о содержании получаемого сетевого пакета
                    MemoryStream ms = new MemoryStream(leninfo);
                    byte[] temp = new byte[leninfo];
                    r = ns.Read(temp, 0, temp.Length);
                    ms.Write(temp, 0, r);
                    BinaryFormatter bf = new BinaryFormatter();
                    ms.Position = 0;
                    SendInfo sc = (SendInfo)bf.Deserialize(ms);
                    ms.Close();
                    object obj = null;
                    if (sc.datasize > 0)
                    {
                        // Создадим файл на основе полученной информации и массива байтов следующих за объектом информации
                        // FileStream fs = new FileStream(sc.filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, sc.filesize);
                        BinaryFormatter _bf = new BinaryFormatter();
                        MemoryStream _ms = new MemoryStream();
                        do
                        {
                            temp = new byte[global.MAXBUFFER];
                            r = ns.Read(temp, 0, temp.Length);

                            // Записываем строго столько байтов сколько прочтено методом Read()
                            _ms.Write(temp, 0, r);

                            // Как только получены все байты файла, останавливаем цикл,
                            // иначе он заблокируется в ожидании новых сетевых данных
                            if (_ms.Length == sc.datasize)
                            {
                                _ms.Position = 0;
                               obj =  _bf.Deserialize(_ms);
                                _ms.Close();
                                
                                break;
                            }
                        }
                        while (r > 0);

                        temp = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }




                    if (Receive != null)
                    {
                        
                        Receive(this, new ReceiveEventArgs(sc,(NetworkTransferObjects)obj));
                       

                    }

                    myTcpClient.buffer = new byte[global.LENGTHHEADER];
                    ns.BeginRead(myTcpClient.buffer, 0, myTcpClient.buffer.Length, new AsyncCallback(ReadCallback), myTcpClient);

                }
                else
                {
                    DeleteClient(myTcpClient);

                    // Событие клиент отключился
                    if (Disconnected != null)
                        Disconnected.BeginInvoke(this, "Клиент отключился!", null, null);
                }
            }
            catch (Exception e)
            {

                DeleteClient(myTcpClient);


                // Событие клиент отключился
                if (Disconnected != null)
                    Disconnected.BeginInvoke(this, "Клиент отключился аварийно!", null, null);

                SoundError(e.Message);

            }

        }


        #endregion

    }



    ///////////////////////////////////////////////////////////////////////////
    // ВСПОМОГАТЕЛЬНЫЕ КЛАССЫ ДЛЯ ОРГАНИЗАЦИИ СЕТЕВОЙ РАБОТЫ TCP МОДУЛЯ
    ///////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// Класс для организации непрерывного извлечения сетевых данных,
    /// для чего необходимо, как минимум, одновременно TcpClient
    /// и буфер приема.
    /// </summary>
    class TcpClientData
    {
        public TcpClient tcpClient = new TcpClient();

        // Буфер для чтения и записи данных сетевого потока
        public byte[] buffer = null;

        public TcpClientData()
        {
            tcpClient.ReceiveBufferSize = global.MAXBUFFER;
        }
    }


    /// <summary>
    /// Класс для отправки текстового сообщения и 
    /// информации о пересылаемых байтах следующих последними в потоке сетевых данных.
    /// </summary>
    [Serializable]
    public class SendInfo
    {
        
        public int datasize;       
        public ProtocolOfExchange ProtocolMsg;
        
    }

    ///////////////////////////////////////////////////////////////////////////
    static class global
    {
        public const int MAXBUFFER = 1048576;
        public const int LENGTHHEADER = 9; // установленный размер главного заголовка
    }
}
