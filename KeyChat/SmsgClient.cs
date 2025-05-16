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
        public static void SendMsgAsync(string other_ip, int other_port, string text)   // 비동기적으로 메시지를 보내는 메서드
        {
            SendDele dele = SendMsg;    // 메시지 전송을 위한 델리게이트 생성 및 비동기 호출
            dele.BeginInvoke(other_ip, other_port, text, null, null);
        }

        delegate void SendDele(string other_ip, int other_port, string text);   // 메시지 전송을 위한 델리게이트 정의
        public static void SendMsg(string other_ip, int other_port, string text)    // 메시지를 TCP를 통해 동기적으로 전송하는 메서드
        {
            try
            {
                byte[] packet = new byte[1024];

                MemoryStream ms = new MemoryStream(packet); // 메시지를 바이너리 형식으로 변환하여 저장
                BinaryWriter bw = new BinaryWriter(ms); 

                // 메시지를 바이너리 데이터로 변환하여 저장
                bw.Write(text);

                bw.Close();
                ms.Close();

                // TCP 소켓 생성 (IPv4, 스트림 방식, TCP 프로토콜 사용)
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(other_ip), other_port); // 대상 IP와 포트를 설정하여 엔드포인트 생성

                sock.Connect(iep);  // 서버에 연결
                sock.Send(packet);  // 데이터를 TCP를 통해 송신
                sock.Close();
            }

            catch
            {
                // 예외 발생 시 처리하지 않음
            }
        }

    }
}
