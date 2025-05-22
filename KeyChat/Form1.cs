using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 내 메시지 서버 설정 버튼 클릭 시 실행
        private void btn_my_fset_Click(object sender, EventArgs e)
        {
            string ip = tbox_my_ip.Text;
            int port = 0;

            // 포트 번호가 올바른지 확인
            if(int.TryParse(tbox_my_fport.Text, out port) == false)
            {
                MessageBox.Show("포트 잘못 입력함");
                return;
            }
            // 메시지 서버 객체 생성 및 이벤트 핸들러 등록
            SmsgServer sms = new SmsgServer(ip, port);
            sms.SmsgRecvEventHandler += Sms_SmsgRecvEventHandler;
            // 서버 시작
            if(sms.Start() == false)
            {
                MessageBox.Show("메시지 서버 가동 실패");
            }
            else
            {
                // 서버 정보 입력 UI 비활성화
                tbox_my_ip.Enabled = tbox_my_fport.Enabled = btn_my_fset.Enabled = false;
            }
        }

        // 메시지 수신 이벤트 발생 시 실행되는 핸들러
        private void Sms_SmsgRecvEventHandler(object sender, SmsgRecvEventArgs e)
        {
            // 수신 메시지를 리스트박스에 추가
            AddMessage(string.Format("{0}:{1}→{2}", e.IPStr, e.Port, e.Msg));
        }

        // UI 스레드에서 안전하게 메시지를 추가하기 위한 델리게이트 선언
        delegate void MyDele(string msg);

        // 메시지를 리스트박스에 추가 (스레드 안전)
        private void AddMessage(string msg)
        {
            if (lbox_msg.InvokeRequired)    // UI 스레드가 아니면
            {
                MyDele dele = AddMessage;   
                object[] objs = new object[] { msg };
                lbox_msg.BeginInvoke(dele, objs); // UI 스레드에서 실행
            }
            else lbox_msg.Items.Add(msg);   // UI 스레드면 바로 추가
        }

        string other_ip;
        int other_port = 10300;

        // 상대방 서버 정보 설정 버튼 클릭 시 실행
        private void btn_other_fset_Click(object sender, EventArgs e)
        {
            other_ip = tbox_other_ip.Text;
            // 포트 번호가 올바른지 확인
            if(int.TryParse(tbox_other_fport.Text, out other_port) == false)
            {
                MessageBox.Show("포트 번호 정수 변환 불가");
            }
        }

        // 메시지 전송 버튼 클릭 시 실행
        private void btn_send_Click(object sender, EventArgs e)
        {
            // 비동기로 메시지 전송
            SmsgClient.SendMsgAsync(other_ip, other_port, tbox_msg.Text);
            // 내가 보낸 메시지를 리스트박스에 추가
            lbox_msg.Items.Add(string.Format("{0}:{1}←{2}", other_ip, other_port, tbox_msg.Text));
            tbox_msg.Text = ""; // 입력창 초기화
        }

        // 메시지 입력란에서 엔터 키를 누르면 메시지 전송
        private void tbox_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_send.PerformClick(); // 보내기 버튼 클릭과 동일하게 동작
                e.SuppressKeyPress = true; // 엔터 입력이 텍스트박스에 줄바꿈으로 들어가지 않게 함
            }
        }
    }
}
