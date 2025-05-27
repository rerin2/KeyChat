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
    // TCP 메시지 전송 클라이언트 클래스
    internal class SmsgClient
    {
        /// <summary>
        /// 비동기적으로 메시지를 보내는 메서드
        /// </summary>
        /// <param name="ip">상대방 IP 주소</param>
        /// <param name="port">상대방 포트 번호</param>
        /// <param name="msg">보낼 메시지</param>
        /// <param name="callback">전송 결과 콜백(true: 성공, false: 실패)</param>
        public static void SendMsgAsync(string ip, int port, string msg, Action<bool> callback)
        {
            // 별도의 스레드(Task)에서 메시지 전송
            Task.Run(() =>
            {
                bool result = SendMsg(ip, port, msg); // 동기 전송 메서드 호출
                callback?.Invoke(result); // 결과 콜백 호출
            });
        }

        /// <summary>
        /// 메시지를 TCP를 통해 동기적으로 전송하는 메서드
        /// </summary>
        /// <param name="ip">상대방 IP 주소</param>
        /// <param name="port">상대방 포트 번호</param>
        /// <param name="msg">보낼 메시지</param>
        /// <returns>전송 성공 여부</returns>
        public static bool SendMsg(string ip, int port, string msg)
        {
            try
            {
                // TCP 소켓 생성
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(ip), port);
                sock.Connect(iep); // 서버에 연결

                // 메시지 전송을 위한 1024바이트 버퍼 생성
                byte[] packet = new byte[1024];
                using (MemoryStream ms = new MemoryStream(packet))
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(msg); // 메시지를 바이너리로 작성
                }
                sock.Send(packet); // 서버로 메시지 전송

                // --- ACK 수신 대기 ---
                byte[] ack = new byte[16]; // ACK 수신 버퍼
                sock.ReceiveTimeout = 2000; // 2초 동안 응답 대기
                int len = sock.Receive(ack); // ACK 수신
                sock.Close(); // 소켓 닫기

                string ackStr = Encoding.UTF8.GetString(ack, 0, len); // 수신한 ACK 문자열 변환
                return ackStr == "ACK"; // ACK가 맞으면 성공 반환
            }
            catch
            {
                // 예외 발생 시 실패 반환
                return false;
            }
        }
    }
}
