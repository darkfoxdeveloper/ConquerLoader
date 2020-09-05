#include "stdafx.h"
#include "Hook.h"

// push ebp
// mov ebp,esp
// sub esp,#
static BYTE fHeader1[] = { 0x55, 0x8B, 0xEC, 0x81, 0xEC };
// mov edi,edi
// push ebp
// mov ebp,esp
static BYTE fHeader2[] = { 0x8B, 0xFF, 0x55, 0x8B, 0xEC };

int IsKnownHookHeader32(LPVOID Address, int Default)
{
	DWORD dwOld;
	VirtualProtect(Address, 5, PAGE_EXECUTE_READWRITE, &dwOld);
	VirtualProtect(fHeader1, 5, PAGE_EXECUTE_READWRITE, &dwOld);
	VirtualProtect(fHeader2, 5, PAGE_EXECUTE_READWRITE, &dwOld);
	if (memcmp(Address, &fHeader1[0], sizeof(fHeader1)) == 0)
		return 11;
	if (memcmp(Address, &fHeader2[0], sizeof(fHeader2)) == 0)
		return 5;
	return Default;
}
void CreateHook32(LPVOID Address, LPVOID Target, HOOK_STUB* Stub)
{
	DWORD dwOld;
	Stub->Size = IsKnownHookHeader32(Address, Stub->Size);
	
	PBYTE ptr = new BYTE[Stub->Size + 5];
	VirtualProtect(ptr, Stub->Size + 5, PAGE_EXECUTE_READWRITE, &dwOld);
	memcpy(ptr, Address, Stub->Size);
	ptr[Stub->Size] = 0xE9; // JMP
	*((DWORD*)&ptr[Stub->Size+1]) = GetDestAddr32((DWORD)&ptr[Stub->Size], (DWORD)Address + Stub->Size);

	BYTE patch[5];
	patch[0] = 0xE9; // JMP
	*((DWORD*)&patch[1]) = GetDestAddr32((DWORD)Address, (DWORD)Target);
	memcpy(Address, patch, 5);
	Stub->Address = ptr;
}
void CreateHook32JumpTable(LPVOID JumpAddr, LPVOID Target, HOOK_STUB* Stub)
{
	DWORD dwOld;
	VirtualProtect(JumpAddr, 6, PAGE_EXECUTE_READWRITE, &dwOld);
	CreateHook32((LPVOID)AddrFromJumpTable32(JumpAddr), Target, Stub); 
}
void DeleteHook32(LPVOID Address, LPVOID HookedPtr)
{
	memcpy(Address, HookedPtr, 5);
	delete[] HookedPtr;
}
void DeleteHook32JumpTable(LPVOID JumpAddr, LPVOID HookedPtr)
{
	LPVOID* pdw_addr = *((LPVOID**)((PBYTE)JumpAddr + 2));
	LPVOID addr = *pdw_addr;
	DeleteHook32(addr, HookedPtr);
}
//
DWORD GetDestAddr32(DWORD StartAddr, DWORD GotoAddr)
{
	DWORD addr = (DWORD)((StartAddr-GotoAddr)+4);
	return (DWORD)(0xFFFFFFFF - addr);
}
LPVOID AddrFromJumpTable32(LPVOID Address)
{
	DWORD pdw_addr = *((DWORD*)((PBYTE)Address + 1));
	pdw_addr = pdw_addr + (DWORD)Address + 4;
	pdw_addr = pdw_addr - 0xFFFFFFFF;
	return (LPVOID)pdw_addr;
}
void StdCallToThisCall(LPVOID Address)
{
	DWORD dwOld;
	PBYTE pAddr = (PBYTE)Address;
	VirtualProtect(&pAddr[18], 12, PAGE_EXECUTE_READWRITE, &dwOld);
	pAddr[18] = 0xB8; // this changes "mov ecx, value" to "mov eax, value"
	pAddr[23] = 0x51; // push ecx (store this pointer on the stack)
	pAddr[24] = 0x8B; // mov ecx,eax
	pAddr[25] = 0xC8; // '
	pAddr[26] = 0xF3; // rep stos    dword ptr es:[edi]
	pAddr[27] = 0xAB; // '
	pAddr[28] = 0x59; // pop ecx (get back out this pointer)
	pAddr[29] = 0x90; // NOP
}