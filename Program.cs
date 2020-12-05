using System;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace server
{
    class Program
    {
        static void Main()
        {
            var wssv = new WebSocketServer("ws://127.0.0.1:7799");//监听端口
            wssv.Log.Level = LogLevel.Error;//Log级别
            wssv.AddWebSocketService("/demo", () => new Demo() { IgnoreExtensions = true });//监听地址

            wssv.Start();//开始监听

            if (wssv.IsListening)//监听后显示一下监听了哪些目录
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);
                foreach (var path in wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

            //按任意键结束监听
            Console.WriteLine("\nPress Enter key to stop the server...");
            Console.ReadLine();

            //结束服务
            wssv.Stop();
            Console.WriteLine("\nStopped");
        }

        //websocket服务器demo
        public class Demo : WebSocketBehavior
        {
            //连接
            protected override void OnOpen()
            {
                base.OnOpen();
                Console.WriteLine("新增一个连接");
            }

            //错误
            protected override void OnError(WebSocketSharp.ErrorEventArgs e)
            {
                base.OnError(e);
                Console.WriteLine("错误：" + e.Message);
            }

            //关闭
            protected override void OnClose(CloseEventArgs e)
            {
                base.OnClose(e);
                Console.WriteLine("断开一个连接");
            }

            //监听
            protected override void OnMessage(MessageEventArgs e)
            {
                base.OnMessage(e);

                if (e.IsPing)
                {
                    Console.WriteLine("客户端ping了一下");
                }
                else
                {
                    Console.WriteLine("收到: " + e.Data);
                    if (this.State == WebSocketState.Open)
                        Send("收到: " + e.Data);//发送
                }
            }
        }
    }
}
