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
    // 파일을 지정된 서버(IP, Port)로 전송하는 클라이언트 클래스
    public class FileSendClient
    {
        // 한 번에 전송할 최대 패킷 크기(바이트)
        const int MAX_PACK_SIZE = 1024;

        // 파일 전송 진행 상황을 알리는 이벤트
        public event SendFileDataEventHandler SendFileDataEventHandler = null;

        // 서버 IP 주소
        public string IPStr { get; private set; }
        // 서버 포트 번호
        public int Port { get; private set; }

        // 생성자: 서버 IP와 포트 설정
        public FileSendClient(string ip, int port)
        {
            IPStr = ip;
            Port = port;
        }

        // 비동기 파일 전송을 위한 델리게이트 정의
        delegate void SendDele(string fname);

        // 파일 전송을 비동기로 실행
        public void SendAsync(string fname)
        {
            SendDele dele = Send;
            dele.BeginInvoke(fname, null, null);
        }

        // 파일을 서버로 전송하는 메서드
        public void Send(string fname)
        {
            // 파일이 존재하지 않으면 종료
            if (File.Exists(fname) == false)
            {
                return;
            }

            // TCP 소켓 생성 및 서버 연결
            Socket sock = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IPStr), Port);
            sock.Connect(iep);

            // 1. 파일 이름 전송
            byte[] packet = new byte[MAX_PACK_SIZE];
            MemoryStream ms = new MemoryStream(packet);
            BinaryWriter bw = new BinaryWriter(ms);
            bw.Write(fname);
            bw.Close();
            ms.Close();
            sock.Send(packet);

            // 2. 파일 크기(8바이트) 전송
            FileStream fs = File.OpenRead(fname);
            ms = new MemoryStream(packet);
            bw = new BinaryWriter(ms);
            bw.Write(fs.Length);
            sock.Send(packet, 0, 8, SocketFlags.None);
            ms.Close();
            bw.Close();

            // 3. 파일 데이터 전송 (1024바이트씩 분할)
            long remain = fs.Length;
            int sl;
            while (remain >= MAX_PACK_SIZE)
            {
                // 파일에서 데이터 읽기
                fs.Read(packet, 0, MAX_PACK_SIZE);
                // 데이터 전송
                sl = sock.Send(packet);
                // 패킷이 완전히 전송될 때까지 반복
                while (sl < MAX_PACK_SIZE)
                {
                    sl += sock.Send(packet, sl, MAX_PACK_SIZE - sl, SocketFlags.None);
                }
                // 진행 상황 이벤트 발생
                if (SendFileDataEventHandler != null)
                {
                    SendFileDataEventHandler(this, new SendFileDataEventArgs(fname, remain));
                }
                remain -= MAX_PACK_SIZE;
            }

            // 4. 남은 데이터(1024바이트 미만) 전송
            fs.Read(packet, 0, (int)remain);
            sl = sock.Send(packet);
            while (sl < remain)
            {
                sl += sock.Send(packet, sl, (int)remain - sl, SocketFlags.None);
            }
            remain = 0;

            // 마지막 진행 상황 이벤트 발생
            if (SendFileDataEventHandler != null)
            {
                SendFileDataEventHandler(this, new SendFileDataEventArgs(fname, remain));
            }

            // 파일 스트림 및 소켓 닫기
            fs.Close();
            sock.Close();
        }
    }
}
