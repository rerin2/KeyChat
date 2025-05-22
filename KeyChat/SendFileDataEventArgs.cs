using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyChat
{
    // 파일 데이터 전송 이벤트를 처리하는 델리게이트 정의
    public delegate void SendFileDataEventHandler(object sender, SendFileDataEventArgs e);

    // 파일 데이터 전송 이벤트에 사용되는 이벤트 인자 클래스
    public class SendFileDataEventArgs
    {
        // 전송 중인 파일의 이름
        public string FileName { get; private set; }

        // 남은 데이터(바이트 단위)
        public long Remain { get; private set; }

        // 생성자: 파일 이름과 남은 데이터 크기를 받아서 속성에 저장
        public SendFileDataEventArgs(string fname, long remain)
        {
            FileName = fname;
            Remain = remain;
        }
    }
}
