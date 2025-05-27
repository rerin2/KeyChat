using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyChat
{
    public partial class P2PChat : Form
    {
        public P2PChat()
        {
            InitializeComponent();

            // 다크 테마 설정
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.BackColor = Color.FromArgb(30, 30, 30);
                ctrl.ForeColor = Color.White;
            }

            // 내 IPv4 주소 자동 입력
            tbox_my_ip.Text = GetLocalIPv4();
        }

        // 로컬 IPv4 주소를 반환하는 메서드
        private string GetLocalIPv4()
        {
            string localIP = "";
            foreach (var ip in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
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
            SaveChatHistory(e.IPStr, e.Port); // 수신 후 저장
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
            if(int.TryParse(tbox_other_fport.Text, out other_port) == false)
            {
                MessageBox.Show("포트 번호 정수 변환 불가");
                return;
            }
            LoadChatHistory(other_ip, other_port); // 상대방 정보 입력 시 채팅 기록 불러오기
        }

        // 메시지 전송 버튼 클릭 시 실행
        private void btn_send_Click(object sender, EventArgs e)
        {
            string message = tbox_msg.Text;
            SmsgClient.SendMsgAsync(other_ip, other_port, message, (success) =>
            {
                string status = success ? "전송 성공" : "전송 실패";
                string log = $"{other_ip}:{other_port}←{message} ({status})";
                // UI 스레드에서 실행
                this.Invoke((Action)(() =>
                {
                    lbox_msg.Items.Add(log);
                    tbox_msg.Text = "";
                }));
            });
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

        private void LoadChatHistory(string ip, int port)
        {
            string fileName = $"{ip}_{port}.txt";       // 아이피와 포트를 입력하면 채팅 내용 불러옴 
            lbox_msg.Items.Clear();
            if (System.IO.File.Exists(fileName))
            {
                var lines = System.IO.File.ReadAllLines(fileName);
                lbox_msg.Items.AddRange(lines);
            }
        }

        private void SaveChatHistory(string ip, int port)   // 채팅 기록 저장
        {
            string fileName = $"{ip}_{port}.txt";
            var lines = lbox_msg.Items.Cast<string>().ToArray();
            System.IO.File.WriteAllLines(fileName, lines);
        }
    }
}
