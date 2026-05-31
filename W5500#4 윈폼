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
                //0x00 0x00 0x00
                byte[] birth_msg = {0xAF};
                ns.Write(birth_msg, 0, birth_msg.Length);
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

                    //RES를 처리하는 부분
                    richTextBox1.Text += "RES수신!\n";
                    richTextBox1.Text += "수신길이 : "+ len + "bytes\n";
                    for(int i =0;i< len; i++)
                    {
                        richTextBox1.Text += recv_buff[i].ToString("X") + ", ";
                    }
                    richTextBox1.Text += "\n";

                    //0헤더, 온도, 습도, 가변저항
                    // 0     1 2    3 4   5 6
                    float temp = BitConverter.ToInt16(recv_buff, 1)/10.0f;
                    float humi = BitConverter.ToInt16(recv_buff, 3) / 10.0f;
                    Int16 photen = BitConverter.ToInt16(recv_buff, 5);
                    textBox3.Text = temp.ToString() + "'C";
                    textBox4.Text = humi.ToString() + "%";
                    textBox5.Text = photen.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //만약 타이머가 작동중인데 불행하게 tcp연결이 끊어지면
            //심각한 문제가 발생할 수 있음!
            if (tc.Connected)
            {
                byte[] req = { 0x00 };
                ns.Write(req, 0, req.Length);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
