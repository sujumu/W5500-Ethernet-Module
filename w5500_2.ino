#include <SPI.h>
#include <Ethernet.h>
#define myled 2

//이더넷 장치의 MAC 주소
byte mac[] = {0x02, 0xAA, 0xBB, 0xCC, 0xDD, 0x01};

//아두이도 서버에 사용할 고정 IP 주소 정보
IPAddress ip(192, 168, 219, 106);
IPAddress gateway(192, 168, 219, 1);
IPAddress subnet(255, 255,255, 0);

//TCP 서버 객체 생성
EthernetServer server(502);

//일정 주기로 LED 상태를 클라이언트에게 보내기 위한 타이머 변수
unsigned long send_t=0;

void setup(){
  //시리얼 모니터 출력 시작
  Serial.begin(9600);
  delay(1000);

  //LED 핀 출력 설정
  pinMode(myled, OUTPUT);
  digitalWrite(myled, LOW);

  //W5500 CS 핀 설정
  Ethernet.init(10);

  Serial.println("Ethernet start...");

  //고정 IP, 게이트웨이, 서브넷 설정
  Ethernet.begin(mac, ip, gateway, subnet);
  delay(1000);

  //TCP 서버 시작
  server.begin();

  //현재 서버 IP 출력
  Serial.print("server is at");
  Serial.print(Ethernet.localIP());
}

void loop(){
  //접속한 클라이언트가 있는지 확인
  EthernetClient client = server.available();

  if (client){
    Serial.println("새로운 클라이언트 등장");

    //클라이언트가 연결되어 있는 동안 반복
    while (client.connected()){
      //1000ms 마다 현재 LED의 상태를 C# 클라이언트에게 전송
      if (millis()-send_t > 1000){

        bool led_state = digitalRead(myled);

        if (led_state){
          char server_msg[] = "ON\n";
          client.write(server_msg, sizeof(server_msg));
        }
        else {
          char server_msg[]="OFF\n";
          client.write(server_msg, sizeof(server_msg));
        }
      }
      //C# 클라이언트가 보낸 데이터가 있는지 확인
      if (client.available()>0){
        String data = client.readStringUntil('\n');

        //공백, \n 제거 
        data.trim();
        
        //C#에서 접속 확인 메시지를 보낸 경우
        if (data=="hello?"){
          Serial.println("C# 윈폼에서 접속했습니다");
        }

        //C#에서 "0"을 보내면 LED OFF
        else if(data=="0"){
          digitalWrite(myled, LOW);
          Serial.println("LED OFF 명령 수신");
        }

        //C#에서 "1"을 보내면 LED ON
        else if(data=="1"){
          digitalWrite(myled, HIGH);
          Serial.println("LED ON 명령 수신");
        }

        else{
          Serial.print("수신 데이터: ");
          Serial.println(data);

        }
      }
    }
    //클라이언트 연결 종료
    client.stop();
    Serial.println("클라이언트와 접속이 해재됨");
  }
}
