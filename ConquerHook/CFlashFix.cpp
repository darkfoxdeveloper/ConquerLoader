/**
* This fix has been created by Ultimation from Elitepvpers :]
* From: https://www.elitepvpers.com/forum/co2-pserver-guides-releases/4858019-release-flash-fix-old-clients.html
*/
#include "stdafx.h"
#include "CFlashFix.h"
#include <tchar.h>

CFlashFix::HookLoadLibrary CFlashFix::pLoadLibrary;
void CFlashFix::Hook()
{
    auto m = GetModuleHandleA("kernelbase.dll");
    auto proc = GetProcAddress(m, "LoadLibraryExW");
    pLoadLibrary = reinterpret_cast<CFlashFix::HookLoadLibrary>(proc);
    DetourTransactionBegin();
    DetourUpdateThread(GetCurrentThread());
    DetourAttach(&reinterpret_cast<PVOID&>(pLoadLibrary), LoadLibraryDetour);
    DetourTransactionCommit();
}

HMODULE _stdcall CFlashFix::LoadLibraryDetour(LPCWSTR lpLibFileName, HANDLE hFile, DWORD dwFlags)
{

    size_t   i;
    char* pMBBuffer = (char*)malloc(256);
    wcstombs_s(&i, pMBBuffer, (size_t)256,
        lpLibFileName, (size_t)256);
    auto s1 = std::string(pMBBuffer);
    auto s2 = std::string("Flash.ocx");
    if (strstr(s1.c_str(), s2.c_str()))
    {
        lpLibFileName = L"./Flash.ocx";
    }
    return pLoadLibrary(lpLibFileName, hFile, dwFlags);;
}