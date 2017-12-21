using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace FEHandler.Report
{

   
    /// <summary>
    /// ReportHandler 的摘要说明
    /// </summary>
    public class ReportHandler : IHttpHandler
    {
        private static Dictionary<string, WebSocket> CONNECT_POOL = new Dictionary<string, WebSocket>();//用户连接池
        private static Dictionary<string, List<string>> MESSAGE_POOL = new Dictionary<string, List<string>>();//离线消息池

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessChat);
            }
        }


        private async Task ProcessChat(AspNetWebSocketContext context)
        {
            WebSocket socket = context.WebSocket;
            string user = context.QueryString["user"].ToString();

            if (!CONNECT_POOL.ContainsKey(user))
            {
                CONNECT_POOL.Add(user, socket);//不存在，添加
            }
            else if (socket != CONNECT_POOL[user])//当前对象不一致，更新
            {
                CONNECT_POOL[user] = socket;
            }

            #region 离线消息处理
            //if (MESSAGE_POOL.ContainsKey(user))
            //{
            //    List<string> msgs = MESSAGE_POOL[user];
            //    foreach (string item in msgs)
            //    {
            //        ArraySegment<byte> buffer_lixian = new ArraySegment<byte>(new byte[2048]);
            //        WebSocketReceiveResult result = await socket.ReceiveAsync(buffer_lixian, CancellationToken.None);
            //        string userMsg = Encoding.UTF8.GetString(buffer_lixian.Array, 0, result.Count);
            //        userMsg = "你发送了：" + userMsg + "于" + DateTime.Now.ToLongTimeString();
            //        buffer_lixian = new ArraySegment<byte>(Encoding.UTF8.GetBytes(item));

            //        WebSocket destSocket = MESSAGE_POOL[item];
            //        await socket.SendAsync(buffer_lixian, WebSocketMessageType.Text, true, CancellationToken.None);
            //    }
            //    MESSAGE_POOL.Remove(user);//移除离线消息
            //}
            if (MESSAGE_POOL.ContainsKey(user))
            {
                List<string> msgs = MESSAGE_POOL[user];
                string all = "";
                foreach (string item in msgs)
                {
                    all = all + item +"&";
                }
                ArraySegment<byte> buffer_lixian = new ArraySegment<byte>(new byte[2048]);
                WebSocketReceiveResult result = await socket.ReceiveAsync(buffer_lixian, CancellationToken.None);
                string userMsg = Encoding.UTF8.GetString(buffer_lixian.Array, 0, result.Count);
                userMsg = "离线消息-" + user + "发送了：" + userMsg + "于" + DateTime.Now.ToLongTimeString();
                buffer_lixian = new ArraySegment<byte>(Encoding.UTF8.GetBytes(all));

                await socket.SendAsync(buffer_lixian, WebSocketMessageType.Text, true, CancellationToken.None);
                MESSAGE_POOL.Remove(user);//移除离线消息
            }

            #endregion

            while (true)
            {
                if (socket.State == WebSocketState.Open)
                {
                    ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);
                    WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);
                    string userMsg = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    userMsg = user+"发送了：" + userMsg + "          于" + DateTime.Now.ToLongTimeString();
                    buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMsg));



                    WebSocket destSocket =null;//目的客户端

                    foreach (string i in CONNECT_POOL.Keys)
                    {
                       
                        destSocket = CONNECT_POOL[i];
                        if (destSocket != null && destSocket.State == WebSocketState.Open)
                        {
                            await destSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        else
                        {
                           
                                if (!MESSAGE_POOL.ContainsKey(i))//将用户添加至离线消息池中
                                {
                                    List<string> listinfo = new List<string>();
                                    listinfo.Add("离线消息-" + userMsg);
                                    MESSAGE_POOL.Add(i, listinfo);
                                }
                                else
                                {
                                    MESSAGE_POOL[i].Add("离线消息-" + userMsg);
                                }
                         
                        }

                        
                    }
                  
                    //await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }

                else
                {
                    
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}