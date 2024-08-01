// ConquerCiperrHook.cpp : Defines the exported functions for the DLL application.

#include "stdafx.h"
#include "hook.h"
#include <iostream>
#include "LegacyCipher.h"
#include <fstream>
#include <sstream>
#include <string>
#include "buffer.h"
#pragma comment(lib, "ws2_32.lib")
#pragma comment(lib, "user32.lib")

using namespace std;
using namespace ConquerCipherHookBufferUtils;

#define lib_func(lib, func) (GetProcAddress(GetModuleHandleA(lib), func))

char tqFormat[] = { 0x25, 0x73, 0xA3, 0xAC, 0xA1, 0xA3, 0x66, 0x64, 0x6A, 0x66, 0x2C, 0x6A, 0x6B, 0x67, 0x66, 0x6B, 0x6C, 0x00 };
char szPassword[32];
HOOK_STUB snprintf_stub, send_stub, connect_stub, shellexec_stub;
CLegacyCipher* legacy;


int csv3_snprintf(char* str, int len, const char* format, ...)
{
	va_list args;
	va_start(args, format);
	if (strcmp(format, tqFormat) == 0)
	{
		char* password = va_arg(args, PCHAR);
		strcpy(szPassword, password);
		str[0] = NULL;
		strcat(str, password);
		strcat(str, &tqFormat[2]);

		//MessageBoxA(NULL, szPassword, "Password", MB_OK);

		return strlen(str);
	}
	else
	{
		return vsnprintf(str, len, format, args);
	}
}
int __stdcall csv3_connect(SOCKET s, sockaddr_in* name, int len)
{
	int rPort;

	rPort = ntohs(name->sin_port);
	if (rPort >= 9950 && rPort <= 9970)
	{
		legacy = new CLegacyCipher();
		legacy->GenerateIV(0x13FA0F9D, 0x6D5C7962);
	}

	typedef int (__stdcall *LPFCONNECT)(SOCKET,sockaddr_in*,int);
	return ((LPFCONNECT)connect_stub.Address)(s, name, len);
}
//char password_encryption_key[8] = { 'C', 'O', 'L', '1', '0', '0', '5', 'R' }; // End with R for Releases, T for Test Version
////XOR Cryptor
//string XOR_PasswordEncrypt(string toEncrypt) {
//	string output = toEncrypt;
//
//	for (int i = 0; i < toEncrypt.size(); i++)
//		output[i] = toEncrypt[i] ^ password_encryption_key[i % (sizeof(password_encryption_key) / sizeof(char))];
//
//	return output;
//}
int __stdcall csv3_send(SOCKET s, PBYTE buf, int len, int flags)
{
	if (legacy)
	{
		legacy->Decrypt(buf, len);

		// Password without encryption
		strcat((char*)&buf[72], szPassword);

		// Password with encryption for Canyon Source
		//buffer::encrypt(szPassword, strlen(szPassword));
		//buffer::write_uint16(buf, 1542, 2);
		//buffer::write_char(buf, szPassword, strlen(szPassword), 60);

		// Password with XOR encription
		/*char* szPasswordEncrypted = new char[XOR_PasswordEncrypt(szPassword).length() + 1];
		strcpy(szPasswordEncrypted, XOR_PasswordEncrypt(szPassword).c_str());
		strcat((char*)&buf[72], szPasswordEncrypted);
		delete[] szPasswordEncrypted;*/

		legacy->Encrypt(buf, len);
		delete legacy;
		legacy = NULL;
	}

	// invoke old method
	typedef int (__stdcall *LPFSEND)(SOCKET,PBYTE,int,int);
	return ((LPFSEND)send_stub.Address)(s,buf,len,flags);
}
int __stdcall Shellexec(HWND hWnd, CHAR* lpOperation, CHAR* lpFile, CHAR* lpParams, CHAR* lpDir, INT nCmd) {
	if (strcmp("http://co.99.com/signout/", lpFile) == 0) {
		lpFile = "https://www.conquerloader.com";
	}
	typedef INT(__stdcall* Shellexec)(HWND, CHAR*, CHAR*, CHAR*, CHAR*, INT);
	return ((Shellexec)shellexec_stub.Address)(hWnd, lpOperation, lpFile, lpParams, lpDir, nCmd);
}

BOOL __stdcall csv3_handler(PBYTE msg)
{
	return TRUE; // allow TQ to process this packet
}

PBYTE tq_handler;
__declspec(naked) BOOL __stdcall csv3_handler_hook()
{
	__asm
	{
		nop
		push ebp
		mov ebp, esp
		push dword ptr[ebp+12]
		push dword ptr[ebp+8]
		// ecx is already set
		mov eax, tq_handler		// this method cleans up after itself __stdcall
		call eax
		test eax,eax
		jz _leave
		// we have a packet... pass it to csv3_handler
		push dword ptr[ebp+12]
		call csv3_handler
		//
		_leave:
		pop ebp
		ret 8 // clean up after self
	}
}

PBYTE tq_send;
__declspec(naked) DWORD __stdcall csv3_tqsend(PBYTE msg, DWORD len)
{
	__asm
	{
		nop
		mov eax, tq_send
		jmp eax
	}
}

void csv3_init(HMODULE hModule)
{
	float f = 0; // load support for floating operations -- thx ntl3fty!
	//CFlashFix flash;
	//flash.Hook();
	memset(szPassword, 0, 32);
	CreateHook32(lib_func("msvcr90.dll", "_snprintf"), csv3_snprintf, &snprintf_stub);
	CreateHook32(lib_func("ws2_32.dll", "send"), csv3_send, &send_stub);
	CreateHook32(lib_func("ws2_32.dll", "connect"), csv3_connect, &connect_stub);
	CreateHook32(lib_func("Shell32.dll", "ShellExecuteA"), Shellexec, &shellexec_stub);
}