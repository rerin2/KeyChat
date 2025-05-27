using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KeyChat
{
    // TCP 메시지 수신 서버 클래스
    internal class SmsgServer
    {
        public event SmsgRecvEventHandler SmsgRecvEventHandler = null;  // 메시지 수신 시 발생 이벤트 
        public string IPStr // IP주소 저장 속성
        {
            get;
            private set;
        }
        public int Port // 포트
        {
            get;
            private set;
        }
        public SmsgServer(string ipstr, int port)   // IP주소 설정 생성자
        {
            IPStr = ipstr;
            Port = port;
        }

        Socket sock;    // 소켓 객체
        public bool Start()
        {
            try
            {
                // TCP 소켓 생성(IPv4, 스트림 방식, TCP 프로토콜 사용)
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IPStr), Port);  // IP 주소와 포트를 사용하여 엔드포인트 생성

                sock.Bind(iep);     // 소켓을 지정된 엔드포인트에 바인딩
                sock.Listen(5);     // 연결 요청을 수락할 수 있도록 설정
                AcceptLoopAsync();      // 클라이언트 연결을 기다리는 루프 시작 (비동기)

                return true;    // 서버 시작 성공
            }
            catch
            {
                return false;       // 실패
            }
        }

        delegate void AcceptDele(); // 클라이언트 연결을 비동기적으로 수락하기 위한 델리게이트 정의
        private void AcceptLoopAsync()  // 클라이언트 연결을 비동기적으로 처리하는 메서드
        {
            AcceptDele dele = AcceptLoop;
            dele.BeginInvoke(null, null);       // 비동기 실행
        }
        private void AcceptLoop() // 클라이언트 연결을 수락하는 루프
        {
            Socket dosock;
            while(true)    // 클라이언트 연결 대기
            {
                dosock = sock.Accept();     // 클라 연결 수락
                DoIt(dosock);   // 메시지 처리
            }
        }

        private void DoIt(Socket dosock)    // 수신한 데이터를 읽고 이벤트 발생 
        {
            IPEndPoint remote = dosock.RemoteEndPoint as IPEndPoint; // 메시지를 보낸 클라이언트의 정보 가져오기
            byte[] packet = new byte[1024]; // 메시지 저장을 위한 패킷 버퍼 (최대 1024바이트)
            dosock.Receive(packet);

            // 복호화
            string msg = CryptoHelper.Decrypt(packet);

            // 메시지 수신 이벤트가 설정되어 있다면 호출
            if (SmsgRecvEventHandler != null)
            {
                SmsgRecvEventHandler(this, new SmsgRecvEventArgs(remote, msg));
            }

            // --- ACK 전송 추가 ---
            byte[] ack = Encoding.UTF8.GetBytes("ACK");
            dosock.Send(ack, 0, ack.Length, SocketFlags.None);

            dosock.Close();
        }
    }
}
