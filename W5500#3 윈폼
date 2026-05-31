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
using Newtonsoft.Json.Linq;

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

                    try
                    {
                        //RES를 처리하는 부분!
                        JObject myjson = JObject.Parse(text);
                        string mycode = myjson["code"].ToString();
                        string mycmd = myjson["cmd"].ToString();
                        
                        //code가 제어하는쪽이면 별로 관심이 없음!
                        if(mycode == "led_read")
                        {
                            //led상태에 대한 응답
                            if(mycmd[0] == '0')
                            {
                                //led1 OFF
                                button2.BackColor = SystemColors.Control;
                                button3.BackColor = Color.Red;
                            }
                            else
                            {
                                //led1 ON
                                button2.BackColor = Color.Green;
                                button3.BackColor = SystemColors.Control;
                            }
                            if (mycmd[1] == '0')
                            {
                                //led2 OFF
                                button4.BackColor = SystemColors.Control;
                                button5.BackColor = Color.Red;
                            }
                            else
                            {
                                //led2 ON
                                button4.BackColor = Color.Green;
                                button5.BackColor = SystemColors.Control;
                            }
                        }
                        else
                        {
                            //제어하는쪽 명령이 여기에 해당함!
                            //아무것도 할 필요없음!
                        }

                    }
                    catch
                    {
                        //JSON 파싱과정에 문제가 발생함!
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mydata = "{\"code\":\"led1_control\",\"cmd\":\"ON\"}\n";
            byte[] test = Encoding.UTF8.GetBytes(mydata);
            ns.Write(test, 0, test.Length);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string mydata = "{\"code\":\"led1_control\",\"cmd\":\"OFF\"}\n";
            byte[] test = Encoding.UTF8.GetBytes(mydata);
            ns.Write(test, 0, test.Length);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string mydata = "{\"code\":\"led2_control\",\"cmd\":\"ON\"}\n";
            byte[] test = Encoding.UTF8.GetBytes(mydata);
            ns.Write(test, 0, test.Length);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string mydata = "{\"code\":\"led2_control\",\"cmd\":\"OFF\"}\n";
            byte[] test = Encoding.UTF8.GetBytes(mydata);
            ns.Write(test, 0, test.Length);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string mydata = "{\"code\":\"all_control\",\"cmd\":\"ON\"}\n";
            byte[] test = Encoding.UTF8.GetBytes(mydata);
            ns.Write(test, 0, test.Length);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string mydata = "{\"code\":\"all_control\",\"cmd\":\"OFF\"}\n";
            byte[] test = Encoding.UTF8.GetBytes(mydata);
            ns.Write(test, 0, test.Length);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //타이머가 실행되고나서 0.1초마다 내가 뭘하면되느냐?
            string mydata = "{\"code\":\"led_read\",\"cmd\":\"-\"}\n";
            byte[] test = Encoding.UTF8.GetBytes(mydata);
            ns.Write(test, 0, test.Length);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
