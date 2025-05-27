using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Net.Sockets;

namespace KeyChat
{
    public delegate void SmsgRecvEventHandler(object sender, SmsgRecvEventArgs e);  // 메시지 수신 이벤트 핸들러 델리게이트
    public class SmsgRecvEventArgs : EventArgs
    {
        public IPEndPoint RemoteEndPoint
        {
            get;
            private set;    // 외부에서 값을 변경할 수 없도록 설정 (읽기 전용)
        }
        public string Msg   // 메시지 내용
        {
            get;
            private set;    // 한 번 설정된 메시지는 변경되지 않음
        }
        public string IPStr // 메시지를 보낸 상대방의 IP주소를 문자열로 변환해 반환 
        {
            get
            {
                return RemoteEndPoint.Address.ToString();
            }
        }
        public int Port // 메시지를 보낸 상대방의 포트 번호를 반환
        {
            get
            {
                return RemoteEndPoint.Port;
            }
        }
        public SmsgRecvEventArgs(IPEndPoint remote, string msg)
        {
            RemoteEndPoint = remote;
            Msg = msg;
        }
    }

    public static class CryptoHelper
    {
        // 키와 IV는 반드시 송수신자가 동일하게 알고 있어야 함 (예시로 고정값 사용)
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890123456"); // 16바이트(128비트)
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("6543210987654321");  // 16바이트

        public static byte[] Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(plainText);
                    }
                    return ms.ToArray();
                }
            }
        }

        public static string Decrypt(byte[] cipherData)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(cipherData))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    public class MessageSender
    {
        private Socket sock;

        public MessageSender(Socket socket)
        {
            sock = socket;
        }

        public void SendMessage(string msg)
        {
            // 암호화
            byte[] encrypted = CryptoHelper.Encrypt(msg);
            byte[] lenBytes = BitConverter.GetBytes(encrypted.Length);

            // 길이(4바이트) + 암호화 데이터 전송
            sock.Send(lenBytes);
            sock.Send(encrypted);
        }
    }

    public class MessageReceiver
    {
        private Socket dosock;

        public MessageReceiver(Socket socket)
        {
            dosock = socket;
        }

        public string ReceiveMessage()
        {
            // 1. 길이(4바이트) 먼저 수신
            byte[] lenBytes = new byte[4];
            dosock.Receive(lenBytes, 0, 4, SocketFlags.None);
            int encLen = BitConverter.ToInt32(lenBytes, 0);

            // 2. 암호화 데이터 수신
            byte[] encData = new byte[encLen];
            int read = 0;
            while (read < encLen)
            {
                read += dosock.Receive(encData, read, encLen - read, SocketFlags.None);
            }

            // 3. 복호화
            int receivedBytes = dosock.Receive(encData);
            byte[] actualData = encData.Take(receivedBytes).ToArray();
            string msg = CryptoHelper.Decrypt(actualData);
            return msg;
        }
    }
}
