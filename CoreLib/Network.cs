using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public class Network
    {
        private TcpModule m_TcpModule;

        public Network(int port,bool errorOutputConole)
        {
            m_TcpModule = new TcpModule(true);
            m_TcpModule.Accept += M_TcpModule_Accept;
            m_TcpModule.Connected += M_TcpModule_Connected;
            m_TcpModule.Disconnected += M_TcpModule_Disconnected;
            m_TcpModule.Receive += M_TcpModule_Receive;
        }

        private void M_TcpModule_Receive(object sender, ReceiveEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void M_TcpModule_Disconnected(object sender, string result)
        {
            throw new NotImplementedException();
        }

        private void M_TcpModule_Connected(object sender, string result)
        {
            throw new NotImplementedException();
        }

        private void M_TcpModule_Accept(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
