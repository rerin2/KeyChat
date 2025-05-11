using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KeyChat
{
    public delegate void SmsgRecvEventHandler(object sender, SmsgRecvEventArgs e);  // 메시지 수신 이벤트 핸들러 델리게이트
    internal class SmsgRecvEventArgs : EventArgs
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
}
