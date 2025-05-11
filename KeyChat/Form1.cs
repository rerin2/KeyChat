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

        private void btn_my_fset_Click(object sender, EventArgs e)
        {
            string ip = tbox_my_ip.Text;
            int port = 0;

            if(int.TryParse(tbox_my_fport.Text, out port) == false)
            {
                MessageBox.Show("포트 잘못 입력함");
                return;
            }
            SmsgServer sms = new SmsgServer(ip, port);
            sms.SmsgRecvEventHandler += Sms_SmsgRecvEventHandler;
            if(sms.Start() == false)
            {
                MessageBox.Show("메시지 서버 가동 실패");
            }
            else
            {
                tbox_my_ip.Enabled = tbox_my_fport.Enabled = btn_my_fset.Enabled = false;
            }
        }

        private void Sms_SmsgRecvEventHandler(object sender, SmsgRecvEventArgs e)
        {
            AddMessage(string.Format("{0}:{1}→{2}", e.IPStr, e.Port, e.Msg));
        }

        delegate void MyDele(string msg);
        private void AddMessage(string msg)
        {
            if (lbox_msg.InvokeRequired)    // 현재 스레드가 UI 스레드인지 확인 
            {
                MyDele dele = AddMessage;   
                object[] objs = new object[] { msg };
                lbox_msg.BeginInvoke(dele, objs);
            }
            else lbox_msg.Items.Add(msg);   
        }

        string other_ip;
        int other_port = 10300;

        private void btn_other_fset_Click(object sender, EventArgs e)
        {
            other_ip = tbox_other_ip.Text;
            if(int.TryParse(tbox_other_fport.Text, out other_port) == false)
            {
                MessageBox.Show("포트 번호 정수 변환 불가");
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            SmsgClient.SendMsgAsync(other_ip, other_port, tbox_msg.Text);   // 비동기 메시지 전송
            lbox_msg.Items.Add(string.Format("{0}:{1}←{2}", other_ip, other_port, tbox_msg.Text));  // 채팅 기록
            tbox_msg.Text = ""; // 입력 부분 초기화 
        }
    }
}
