#include <SPI.h>
#include <Ethernet.h>

// 이더넷 장치의 MAC 주소
byte mac[] = { 0x02, 0xAA, 0xBB, 0xCC, 0xDD, 0x01 };

// 아두이노 서버에 사용할 고정 IP 주소 정보 세트
IPAddress ip(192, 168, 219, 106);
IPAddress gateway(192, 168, 219, 1);    // 공유기 IP (문지기)
IPAddress subnet(255, 255, 255, 0);    // 서브넷 마스크 (네트워크 범위)

// TCP 서버 객체 생성 (Modbus TCP 포트 502)
EthernetServer server(502);

// 1초마다 데이터를 보내기 위한 시간 저장 변수
unsigned long send_t = 0;

void setup() {
  // 시리얼 모니터 출력 시작
  Serial.begin(9600);
  delay(1000);

  // W5500 칩 하드웨어 제어 핀(10번)을 명시적으로 지정
  Ethernet.init(10);   

  Serial.println("Ethernet start...");

  // IP만 주지 않고, 공유기(gateway)와 범위(subnet) 정보까지 세트로 정확히 주입
  Ethernet.begin(mac, ip, gateway, gateway, subnet);
  delay(1000);

  // TCP 서버 시작
  server.begin();

  // 현재 아두이노에 설정된 IP 주소 출력
  Serial.print("server is at ");
  Serial.println(Ethernet.localIP());
}

void loop() {
  // 서버에 접속한 클라이언트가 있는지 확인
  EthernetClient client = server.available();

  // 클라이언트가 접속한 경우
  if (client) {
    Serial.println("새로운 클라이언트 등장!");

    // 클라이언트가 연결되어 있는 동안 반복
    while (client.connected()) {

      // 1초마다 클라이언트에게 메시지 전송
      if (millis() - send_t > 1000) {
        send_t = millis();

        char server_msg[] = "접속 중입니다\n";

        // 클라이언트로 문자열 전송
        client.write(server_msg, sizeof(server_msg));
      }

      // 클라이언트가 보낸 데이터가 있는지 확인
      if (client.available() > 0) {

        // 줄바꿈 문자까지 문자열 읽기
        String data = client.readStringUntil('\n');

        // 공백 문자나 줄바꿈 찌꺼기 제거
        data.trim(); 

        // C# WinForms에서 접속 확인 메시지를 보낸 경우
        if (data == "hello?") {
          Serial.println("C#윈폼에서 접속을 했습니다");
        } 
        // 그 외 데이터는 그대로 출력
        else {
          Serial.println(data);
        }
      }
    }

    // 클라이언트 연결 종료 처리
    client.stop();
    Serial.println("클라이언트와 접속이 해제됨!");
  }
}