#include "stdafx.h"

bool GetModuleSize( HANDLE hProcess, LPVOID ImageBase, DWORD& Size )
{
	bool bFound = false;
	MEMORY_BASIC_INFORMATION mbi;
	BYTE* QueryAddress = (BYTE*)ImageBase;
	while( !bFound )
	{
		if( VirtualQueryEx( hProcess, QueryAddress, &mbi, sizeof(mbi) ) != sizeof(mbi) )
			break;

		if( mbi.AllocationBase != ImageBase )
		{
			// Found, calculate the module size
			Size = QueryAddress - (BYTE*)ImageBase;
			bFound = true;
			break;
		}
		QueryAddress += mbi.RegionSize;
	}
	// Complete
	return bFound;
}

LPVOID FindMemoryPattern(PBYTE pattern, bool* wildCards, int len)
{
	HANDLE mod = GetModuleHandle(NULL);
	DWORD size;
	if (!GetModuleSize(GetCurrentProcess(), mod, size))
		return NULL;
	BYTE *buffer = new BYTE[size];
	ReadProcessMemory(GetCurrentProcess(), mod, buffer, size, NULL);
	LPVOID found = NULL;
	for (DWORD i = 0; i < size; i++)
	{
		bool match = true;
		for (int j = 0; j < len; j++)
		{
			if ((buffer[i+j] != pattern[j]) && !wildCards[j])
			{
				match = false;
				break;
			}
		}
		if (match)
		{
			found = (PBYTE)mod + i;
			break;
		}
	}
	delete[] buffer;
	return found; 
}