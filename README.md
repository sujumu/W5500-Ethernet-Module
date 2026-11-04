# W5500 Ethernet Communication Controller

Arduino와 W5500 Ethernet 모듈을 활용하여 유선 LAN 기반 TCP/IP 통신 및 Modbus TCP 제어 시스템을 구현한 프로젝트입니다.

W5500 모듈과 Arduino 간 SPI 통신을 이용하여 Ethernet 네트워크를 구성하고, C# WinForms 프로그램과 데이터를 송수신하도록 구현하였습니다.

또한 Modbus TCP 프레임 구조를 분석하여 코일 상태 조회 및 출력 제어 기능을 실습하였습니다.

## 1. Development Environment

- **MCU:** Arduino
- **Ethernet Controller:** W5500
- **IDE:** Arduino IDE, Visual Studio
- **Language:** C/C++ (Arduino), C#
- **Communication:** SPI, Ethernet, TCP/IP, Modbus TCP

## 2. Hardware Components

| Component | Description |
|---|---|
| Arduino | TCP 데이터 처리 및 GPIO 제어 |
| W5500 Ethernet Module | 유선 TCP/IP 네트워크 통신 |
| LEDs | 원격 출력 제어 |
| DHT11 | 온습도 데이터 측정 |
| Potentiometer | 아날로그 입력 데이터 측정 |
| PC | C# WinForms 기반 모니터링 및 제어 |

## 3. Communication Interfaces

| Interface | Application |
|---|---|
| SPI | Arduino와 W5500 간 데이터 통신 |
| Ethernet | 유선 LAN 네트워크 연결 |
| TCP/IP | 서버·클라이언트 데이터 송수신 |
| Modbus TCP | 코일 데이터 조회 및 출력 제어 |
| GPIO | LED 출력 및 디지털 제어 |
| ADC | 가변저항 아날로그 값 측정 |

## 4. Main Features

### Ethernet Communication
- W5500 기반 유선 LAN 통신 구현
- MAC 주소, 고정 IP, 게이트웨이 및 서브넷 설정
- EthernetServer를 이용한 TCP 서버 구성
- 클라이언트 연결 및 데이터 송수신 처리

### Remote GPIO Control
- C# WinForms에서 TCP 제어 명령 전송
- 수신 명령에 따른 Arduino GPIO 출력 제어
- LED ON/OFF 동작 구현

### Sensor Data Monitoring
- DHT11 온습도 데이터 측정
- 가변저항 아날로그 입력 측정
- 센서 데이터를 TCP로 전송
- C# WinForms에서 수신 데이터 분석 및 표시

### Modbus TCP
- Modbus TCP 요청 프레임 구조 분석
- MBAP Header 및 Function Code 처리
- FC01 (Read Coils) 구현
- FC05 (Write Single Coil) 구현
- 코일 주소와 Arduino GPIO 매핑
- 요청에 따른 코일 상태 조회 및 LED 제어

## 5. Source Code

| File / Directory | Description |
|---|---|
| `W5500#1 아두이노 / 윈폼` | TCP 통신 기본 예제 |
| `W5500#2 아두이노 / 윈폼` | 원격 제어 예제 |
| `W5500#3 아두이노 / 윈폼` | 다중 GPIO 제어 예제 |
| `W5500#4 아두이노 / 윈폼` | 센서 데이터 송수신 예제 |
| `W5500#5 아두이노 / 윈폼` | Modbus TCP 통신 및 제어 예제 |

본 저장소는 W5500을 이용한 Ethernet 통신 및 Modbus TCP 기본 기능 구현 코드를 정리한 것입니다.

## 6. Project Documentation

프로젝트의 상세한 하드웨어 구성, 통신 프레임 분석, 구현 과정 및 검증 결과는 별도의 포트폴리오 자료에서 확인할 수 있습니다.

- **Project Report & Demonstration:** https://drive.google.com/drive/u/1/folders/1U9Mk0tjPaHliIiccRv8B_v6yICjyF6FC

## 7. Development Notes

본 프로젝트에서는 SPI를 활용한 Ethernet 컨트롤러 연동 및 TCP/IP 기반 원격 장치 제어 시스템을 구현하였습니다.

특히 Modbus TCP의 MBAP Header와 Function Code를 직접 분석하고, FC01 및 FC05 명령을 이용한 코일 제어 기능을 구현하여 산업용 네트워크 통신 프로토콜의 기본 구조를 학습하였습니다.
