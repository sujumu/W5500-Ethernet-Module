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

        int tcp_cnt = 1;

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
                //byte[] birth_msg = {0x00,0x00,0x00};
                //ns.Write(birth_msg, 0, birth_msg.Length);
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
                    //richTextBox1.Text += "RES수신!\n";
                    //richTextBox1.Text += "수신길이 : "+ len + "bytes\n";
                    //for(int i =0;i< len; i++)
                    //{
                    //    richTextBox1.Text += recv_buff[i].ToString("X") + ", ";
                    //}
                    //richTextBox1.Text += "\n";

                    if(recv_buff[7] == 0x01)
                    {
                        //read coils에 대한 응답!
                        //recv_buff[8] 바이트 갯수
                        //recv_buff[9] ~
                        byte nockanda_state = recv_buff[9];

                        int led1_state = nockanda_state & 0b00000001;
                        int led2_state = (nockanda_state >> 1) & 0b00000001;
                        int led3_state = (nockanda_state >> 2) & 0b00000001;
                        int led4_state = (nockanda_state >> 3) & 0b00000001;
                        int led5_state = (nockanda_state >> 4) & 0b00000001;
                        int led6_state = (nockanda_state >> 5) & 0b00000001;
                        //richTextBox1.Text += "read coils 결과\n";
                        //richTextBox1.Text += led1_state + "\n";
                        //richTextBox1.Text += led2_state + "\n";
                        //richTextBox1.Text += led3_state + "\n";
                        //richTextBox1.Text += led4_state + "\n";
                        //richTextBox1.Text += led5_state + "\n";
                        //richTextBox1.Text += led6_state + "\n";
                        if(led1_state == 0x01)
                        {
                            //ON
                            button7.BackColor = Color.Green;
                            button8.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            //OFF
                            button7.BackColor = SystemColors.Control;
                            button8.BackColor = Color.Red;
                        }
                        if (led2_state == 0x01)
                        {
                            //ON
                            button9.BackColor = Color.Green;
                            button10.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            //OFF
                            button9.BackColor = SystemColors.Control;
                            button10.BackColor = Color.Red;
                        }
                        if (led3_state == 0x01)
                        {
                            //ON
                            button11.BackColor = Color.Green;
                            button12.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            //OFF
                            button11.BackColor = SystemColors.Control;
                            button12.BackColor = Color.Red;
                        }
                        if (led4_state == 0x01)
                        {
                            //ON
                            button13.BackColor = Color.Green;
                            button14.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            //OFF
                            button13.BackColor = SystemColors.Control;
                            button14.BackColor = Color.Red;
                        }
                        if (led5_state == 0x01)
                        {
                            //ON
                            button15.BackColor = Color.Green;
                            button16.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            //OFF
                            button15.BackColor = SystemColors.Control;
                            button16.BackColor = Color.Red;
                        }
                        if (led6_state == 0x01)
                        {
                            //ON
                            button17.BackColor = Color.Green;
                            button18.BackColor = SystemColors.Control;
                        }
                        else
                        {
                            //OFF
                            button17.BackColor = SystemColors.Control;
                            button18.BackColor = Color.Red;
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                           //메시지번호 //modbus    //길이      //국번//FC  //코일주소 //제어명령
            byte[] req = { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x00, 0x00, 0xFF, 0x00 };
            ns.Write(req, 0, req.Length);
            tcp_cnt++;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //메시지번호 //modbus    //길이      //국번//FC  //코일주소 //제어명령
            byte[] req = { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x00, 0x00, 0x00, 0x00 };
            ns.Write(req, 0, req.Length);
            tcp_cnt++;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //textbox3에 있는 숫자를 2byte로 쪼개서 로그창에 출력한다!
            UInt16 msg_cnt = UInt16.Parse(textBox3.Text);
            UInt16 addr = UInt16.Parse(textBox4.Text);

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0xFF, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //textbox3에 있는 숫자를 2byte로 쪼개서 로그창에 출력한다!
            UInt16 msg_cnt = UInt16.Parse(textBox3.Text);
            UInt16 addr = UInt16.Parse(textBox4.Text);

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0x00, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 0;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0xFF, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 0;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0x00, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 1;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0xFF, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 1;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0x00, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 2;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0xFF, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 2;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0x00, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 3;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0xFF, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 3;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0x00, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 4;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0xFF, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 4;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0x00, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 5;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0xFF, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            UInt16 msg_cnt = 1;
            UInt16 addr = 5;

            //보낼때는 인덱스 1, 0 순으로 전송한다
            byte[] data1 = BitConverter.GetBytes(msg_cnt);
            byte[] data2 = BitConverter.GetBytes(addr);

            byte[] req = { data1[1], data1[0], 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, data2[1], data2[0], 0x00, 0x00 };
            ns.Write(req, 0, req.Length);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //0.1초간격으로 내가 뭘 반복하면되냐?
            byte[] req = { 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x01, 0x00, 0x00, 0x00, 0x06 };
            ns.Write(req, 0, req.Length);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
