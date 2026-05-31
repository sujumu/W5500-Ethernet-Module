//SPI 통신용 기본 라이브러리
#include <SPI.h>
//W5500 이더넷 아리브러리
#include <Ethernet.h>
//DHT11 온습도 센서 라이브러리
#include "DHT.h"
//DHT11 센서 연결핀
#define DHTPIN 2
//사용하는 DHT 센서 종류
#define DHTTYPE DHT11

//DHT 센서 객체 생성
DHT dht(DHTPIN, DHTTYPE);

//W5500 MAC 주소
byte mac[] = {
  0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED
};

//Arduino W5500 고정 IP 주소
IPAddress ip(192, 168, 219, 106);

//DNS 주소
IPAddress dns(192, 168, 219, 1);

//게이트웨이 주소
IPAddress gateway(192, 168, 219, 1);

//서브넷 마스크
IPAddress subnet(255, 255, 255, 0);

//TCP 서버 포트 번호
EthernetServer server(502);

//서버가 클라이언트로 일정 간격으로 전송하기 위한 타이머
unsigned long send_t = 0;

//서버가 클라이언트로 전송할 센서 데이터 구조체
struct {
  byte header; //데이터 구분용 헤더
  int16_t temp; //온도 데이터
  int16_t humi; //습도 데이터
  int16_t photen; //가변저항 데이터
} server_res;

//클라이언트가 서버로 보낼 수 있는 요청 데이터 구조체
struct {
  byte header; //요청 구분용 헤더
  float data1; //요청 데이터 1
  float data2; //요청 데이터 2
} server_req;

void setup() {
  Serial.begin(9600);
  dht.begin();
  Ethernet.init(10);
  Ethernet.begin(mac,ip,dns,gateway,subnet);
  server.begin();
  Serial.print("Sever is at : ");
  Serial.println(Ethernet.localIP());
}

void loop() {
  EthernetClient client = server.available();
   if (client) {
    Serial.println("클라이언트 등장");

    while (client.connected()) {
      //클라이언트로부터 수신된 데이터가 있는지 확인
      if (client.available()>0) {
        //클라이언트가 보낸 요청 데이터 1바이트 수신
        byte req[1];
        client.readBytes(req, sizeof(req));

        //접속 확인용 요청 처리
        if (req[0] == 0xAF) {
          Serial.println("클라이언트가 성공적으로 접속함");
        }

        //센서 데이터 요청 처리
        else{
          //수신 요청 데이터 출력
          Serial.print("REQ : ");
          for (int i = 0; i<sizeof(req); i++){
            Serial.print(req[i], HEX);
            Serial.print(",");
          }
          //줄바꿈용
          Serial.println();

          //DHT11 센서에서 습도 읽기
          float h = dht.readHumidity();

          //DHT11 센서에서 온도 읽기
          float t = dht.readTemperature();

          //센서 측정 실패 처리
          if (isnan(h) || isnan(t)) {
            h = -99.9;
            t = -99.9;
          }

          //응답 데이터 구성
          server_res.header = 0x00;

          //온도, 습도, 가변저항 값을 정수형을 변환
          server_res.temp = (int16_t)(t*10);
          server_res.humi = (int16_t)(h*10);
          server_res.photen = analogRead(A0);

          //구조체 데이터를 바이트 배열 형태로 변환하여 클라이언트로 전송
          client.write((byte*)&server_res, sizeof(server_res));
        }
      }
    }
    client.stop();
    Serial.println("접속 종료");
   }
}
