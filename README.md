# Arduino W5500 TCP/Modbus TCP 통신 프로젝트

무선으로 구현했던 TCP/IP 프로젝트와는 달리 Arduino와 W5500 Ethernet 모듈을 이용하여 유선 LAN 기반 TCP 통신 구조를 구현하였습니다.

## 1. 프로젝트 개요
C# WinForms 프로그램
        │
        │ TCP/IP
        ▼
공유기 또는 스위치 허브
        │
        │ LAN 케이블 (10Base-T / 100Base-TX)
        ▼
W5500 Ethernet 모듈
        │
        │ SPI 통신
        ▼
Arduino
        │
        │ GPIO 제어
        ▼
LED 또는 센서
