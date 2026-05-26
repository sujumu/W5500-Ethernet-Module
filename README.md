# Arduino W5500 TCP/Modbus TCP 통신 프로젝트

## 1. 프로젝트 개요

본 프로젝트는 Arduino와 W5500 Ethernet 모듈을 이용하여 유선 LAN 기반 TCP 통신 구조를 구현하는 것을 목적으로 한다.

Arduino는 W5500 모듈을 SPI 통신으로 제어하며, W5500은 Ethernet 통신을 담당한다.  
PC에서는 C# WinForms 프로그램을 실행하여 TCP 클라이언트 역할을 수행하고, Arduino + W5500 장치는 TCP 서버 역할을 수행한다.

초기 단계에서는 사람이 읽을 수 있는 문자열 기반 명령을 사용하여 LED를 제어하고 상태값을 확인한다.  
이후에는 문자열 통신을 바이트 스트림 기반 통신 구조로 확장하여, 최종적으로 Modbus TCP 프로토콜 구현을 목표로 한다.
