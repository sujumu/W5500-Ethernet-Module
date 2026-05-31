//SPI 통신용 기본 라이브러리
#include <SPI.h>
//W5500 이더넷 라이브러리
#include <Ethernet.h>
//JSON 데이터 처리 라이브러리
#include <ArduinoJson.h>

#define led1 2
#define led2 3

//W5500 MAC 주소
byte mac[] = {
  0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED
};

//Arduino W5500 고정 IP 주소
IPAddress ip(192,168,219,106);

//게이트웨이 주소
IPAddress gateway(192,168,219,1);

//서브넷 마스크
IPAddress subnet(255,255,255,0);

//TCP 서버 포트 번호
EthernetServer server(502);

//주기적 전소용 타이머
unsigned long send_t = 0;

void setup() {
  Serial.begin(9600);

  //LED 핀 출력 설정
  pinMode(led1, OUTPUT);
  pinMode(led2, OUTPUT);

  digitalWrite(led1, LOW);
  digitalWrite(led2, LOW);

  //W5500 CS 핀 설정
  Ethernet.init(10);

  //W5500 네트워크 설정
  Ethernet.begin(mac, ip, gateway, subnet);

  //TCP 서버 시작
  server.begin();

  //서버 ip 주소 출력
  Serial.print("server is at");
  Serial.println(Ethernet.localIP());
}

void loop() {
  //접속한 클라이언트 확인
  EthernetClient client=server.available();

  //클라이언트가 접속한 경우
  if (client) {
    Serial.println("새로운 클라이언트 등장");

    //클라이언트 연결 유지 중 반복
    while (client.connected()) {
      if (client.available()>0) {
        String data = client.readStringUntil('\n');

        data.trim();

        if (data == "hello?") {
          Serial.println("C# 윈폼에서 접속했습니다");
        }
        else {
          //JSON 데이터 저장 객체
          StaticJsonDocument<64> doc;

          //JSON 문자열 파싱
          DeserializationError error=deserializeJson(doc, data);

          //JSON 파싱 실패 처리
          if (error) {
            Serial.print("deserializeJson() failed:");
            Serial.println(error.f_str());
            //실행종료 아래 코드는 실행 X
            return;
          }

          //수신 요청 출력
          Serial.print("REQ: ");
          Serial.println(data);

          //JSON 데이터에서 명령 추출
          String code = doc["code"];
          String cmd = doc["cmd"];

          //LED1 제어 요청
          if (code == "led1_control") {
            if (cmd == "ON") {
              digitalWrite(led1, HIGH);
              client.println(data);
            }
            else if(cmd == "OFF") {
              digitalWrite(led1, LOW);
              client.println(data);
            }
          }
         //LED2 제어 요청
         if (code == "led2_control") {
            if (cmd == "ON") {
              digitalWrite(led2, HIGH);
              client.println(data);
            }
            else if(cmd == "OFF") {
              digitalWrite(led2, LOW);
              client.println(data);
            }
          }

          //LED 전체 제어요청
          if (code == "all_control") {
            if (cmd == "ON") {
              digitalWrite(led1, HIGH);
              digitalWrite(led2, HIGH);
              client.println(data);
            }
            else if(cmd == "OFF") {
              digitalWrite(led1, LOW);
              digitalWrite(led2, LOW);
              client.println(data);
            }
          }

          //LED 상태 읽기 요청
          else if (code == "led_read") {
            // LED 상태를 문자열로 생성
            String led_state = String(digitalRead(led1)) + String(digitalRead(led2));

            //응답 JSON 생성
            String res = "{\"code\":\"led_read\",\"cmd\":\"" + led_state + "\"}";

            //LED 상태 응답 전송
            client.println(res);

          }
        }
      }
    }
    client.stop();
    Serial.println("연결이 종료됨");
  }
}

