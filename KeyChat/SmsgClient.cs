using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace KeyChat
{
    internal class SmsgClient   // TCP 메시지 전송 클라이언트 클래스
    {
        // 메시지 전송 시 암호화 적용
        public static void SendMsgAsync(string ip, int port, string msg)
        {
            Task.Run(() =>
            {
                try
                {
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
                    sock.Connect(iep);

                    // 암호화
                    byte[] encrypted = CryptoHelper.Encrypt(msg);
                    // 패킷 크기 맞추기 (1024바이트)
                    byte[] packet = new byte[1024];
                    Array.Copy(encrypted, packet, Math.Min(encrypted.Length, packet.Length));
                    sock.Send(packet);

                    sock.Close();
                }
                catch
                {
                    // 예외 처리
                }
            });
        }

        public static void SendMsgAsync(string ip, int port, string msg, Action<bool> callback)   // 비동기적으로 메시지를 보내는 메서드
        {
            Task.Run(() =>
            {
                bool result = SendMsg(ip, port, msg);
                callback?.Invoke(result);
            });
        }

        public static bool SendMsg(string ip, int port, string msg)    // 메시지를 TCP를 통해 동기적으로 전송하는 메서드
        {
            try
            {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
                sock.Connect(iep);

                // 메시지 전송
                byte[] packet = new byte[1024];
                using (MemoryStream ms = new MemoryStream(packet))
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(msg);
                }
                sock.Send(packet);

                // --- ACK 수신 대기 ---
                byte[] ack = new byte[16];
                sock.ReceiveTimeout = 2000; // 2초 대기
                int len = sock.Receive(ack);
                sock.Close();

                string ackStr = Encoding.UTF8.GetString(ack, 0, len);
                return ackStr == "ACK";
            }
            catch
            {
                return false;
            }
        }
    }
}
