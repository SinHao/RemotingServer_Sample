using System;

namespace RemotingServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //// 使用tcp建立通道，port為8050
                var tcpChannel = new System.Runtime.Remoting.Channels.Tcp.TcpChannel(8050);
                //// 註冊剛剛建立的tcp channel，這邊使用簡單的範例所以將ensureSecurity設為false
                System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(tcpChannel, false);

                //// 使用http建立通道，port為8051
                System.Runtime.Remoting.Channels.Http.HttpChannel httpChannecl = new System.Runtime.Remoting.Channels.Http.HttpChannel(8051);
                //// 註冊剛剛建立的http channel，這邊使用簡單的範例所以將ensureSecurity設為false
                System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(httpChannecl, false);

                //// 註冊 Remoting 服務的物件與物件啟動的方式(SingleCall)
                //// WellKnownObjectMode有兩種
                //// Singleton 每個內送訊息都是由新的物件執行個體 (Instance) 服務。
                //// SingleCall 每個內送訊息都是由同一個物件執行個體服務。
                System.Runtime.Remoting.RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(InterfaceOfWork), 
                    "RemotingTest", 
                    System.Runtime.Remoting.WellKnownObjectMode.SingleCall);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }

        public class InterfaceOfWork : MarshalByRefObject, RemotingInterface.IMethods
        {
            public void Method1(string value1)
            {
                Console.WriteLine(value1);
            }

            public string Method2(string value1)
            {
                return value1;
            }
        }
    }
}
