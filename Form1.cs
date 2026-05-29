using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example610_1
{
    public partial class Form1 : Form
    {
        TcpClient tc = new TcpClient();
        NetworkStream ns;

        byte[] recv_buff = new byte[255];

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //접속버튼 눌렀다 뭐할래?
            string ip_addr = textBox1.Text;
            int port = int.Parse(textBox2.Text);

            tc.Connect(ip_addr, port);

            if (tc.Connected)
            {
                //여기서 실제로 아두이노와 연결됨!
                ns = tc.GetStream();
                //접속하자마자 뭔가를 전송해줘야 접속이되네?
                string mydata = "hello?\n";
                byte[] test = Encoding.UTF8.GetBytes(mydata);
                ns.Write(test, 0, test.Length);
                MessageBox.Show("연결됨!");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //타이머가 1밀리초마다 해야할일이 뭐냐?
            //C#윈폼과 아두이노가 TCP로 연결되어야하고
            //수신버퍼에 데이터가 있으면 읽어들인다!
            if (tc.Connected)
            {
                if(tc.Available > 0)
                {
                    //지금 수신버퍼에 있는거 몽땅 다 읽어서 반환한다
                    int len = ns.Read(recv_buff, 0, recv_buff.Length);
                    string text = Encoding.UTF8.GetString(recv_buff, 0, len);
                    textBox3.Text = text;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tc.Connected)
            {
                string mydata = "1\n";
                byte[] test = Encoding.UTF8.GetBytes(mydata);
                ns.Write(test, 0, test.Length);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tc.Connected)
            {
                string mydata = "0\n";
                byte[] test = Encoding.UTF8.GetBytes(mydata);
                ns.Write(test, 0, test.Length);
            }
        }
    }
}
