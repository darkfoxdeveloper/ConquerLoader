#ifndef _HOOK_H_
#define _HOOK_H_

#include <windows.h>
#include <malloc.h>

typedef struct {
	LPVOID Address;
	size_t Size;
} HOOK_STUB;

int IsKnownHookHeader32(LPVOID Address, int Default=FALSE);
void CreateHook32(LPVOID Address, LPVOID Target, HOOK_STUB* Stub);
void CreateHook32JumpTable(LPVOID JumpAddr, LPVOID Target, HOOK_STUB* Stub);
void DeleteHook32(LPVOID Address, LPVOID HookedPtr);
void DeleteHook32JumpTable(LPVOID JumpAddr, LPVOID HookedPtr);

DWORD GetDestAddr32(DWORD StartAddr, DWORD GotoAddr);
LPVOID AddrFromJumpTable32(LPVOID Address);
void StdCallToThisCall(LPVOID Address);

#define CreateHook32DWordPtr CreateHook32
#define DeleteHook32DWordPtr DeleteHook32

#define ThisCallToPtr(PtrVar, _ThisCall, UseRegister) \
	__asm  { mov UseRegister, _ThisCall }; \
	__asm  { mov PtrVar, UseRegister }

#define CDeclToThisCall StdCallToThisCall
#define StoreThisPointer(PtrVar) \
	PtrVar; \
	__asm mov PtrVar, ecx
#define ExitThisCall(argumentStackSize) \
	__asm { pop edi }; \
	__asm { pop esi }; \
	__asm { pop ebx };  \
	__asm { mov esp, ebp }; \
	__asm { pop ebp }; \
	__asm { ret argumentStackSize }

#endif