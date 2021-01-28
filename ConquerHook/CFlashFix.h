/**
* This fix has been created by Ultimation from Elitepvpers :]
* From: https://www.elitepvpers.com/forum/co2-pserver-guides-releases/4858019-release-flash-fix-old-clients.html
*/

#include "stdafx.h"
#include <windows.h>
#include "detours.h"
#include <iostream>
#pragma once

class CFlashFix
{
	typedef HMODULE(_stdcall* HookLoadLibrary)(_In_ LPCWSTR lpLibFileName,
		_Reserved_ HANDLE hFile,
		_In_ DWORD dwFlags);
public:
	static void Hook();
	static HookLoadLibrary pLoadLibrary;
	static  HMODULE _stdcall LoadLibraryDetour(_In_ LPCWSTR lpLibFileName,
		_Reserved_ HANDLE hFile,
		_In_ DWORD dwFlags);
};