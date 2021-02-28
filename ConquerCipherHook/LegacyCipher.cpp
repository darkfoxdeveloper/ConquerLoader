#include "StdAfx.h"
#include "LegacyCipher.h"
#include <assert.h>

const int32_t COSAC_IV = 512;
const int32_t COSAC_KEY = 512;

CLegacyCipher::CLegacyCipher()
{ 
	BufIV = NULL;
	BufKey = NULL;
	EncryptCounter = 0;
	DecryptCounter = 0;
}

CLegacyCipher::~CLegacyCipher()
{
	if (BufIV != NULL)
		delete[] BufIV;
	if (BufKey != NULL)
		delete[] BufKey;
}

void CLegacyCipher::GenerateIV(int32_t P, int32_t G)
{
	if (BufIV != NULL)
		delete[] BufIV;

	BufIV = new uint8_t[COSAC_IV];
	int16_t K = COSAC_IV / 2;

	uint8_t* pBufPKey = (uint8_t*)&P;
	uint8_t* pBufGKey = (uint8_t*)&G;

	for (int i = 0; i < K; i++)
	{
		BufIV[i + 0] = pBufPKey[0];
		BufIV[i + K] = pBufGKey[0];
		pBufPKey[0] = (int8_t)((pBufPKey[1] + (int8_t)(pBufPKey[0] * pBufPKey[2])) * pBufPKey[0] + pBufPKey[3]);
		pBufGKey[0] = (int8_t)((pBufGKey[1] - (int8_t)(pBufGKey[0] * pBufGKey[2])) * pBufGKey[0] + pBufGKey[3]);
	}
}
void CLegacyCipher::Encrypt(uint8_t* pBuf, int32_t Length)
{
	assert(pBuf != NULL);
	assert(Length > 0);

	int16_t K = COSAC_IV / 2;
	for (int32_t i = 0; i < Length; i++)
	{
		if (BufIV != NULL)
		{
			pBuf[i] ^= (int8_t)(BufIV[(int8_t)(EncryptCounter & 0xFF) + 0]);
			pBuf[i] ^= (int8_t)(BufIV[(int8_t)(EncryptCounter >> 8) + K]);
		}
		pBuf[i] = (int8_t)(pBuf[i] >> 4 | pBuf[i] << 4);
		pBuf[i] ^= (int8_t)0xAB;
		EncryptCounter++;
	}
}
void CLegacyCipher::Decrypt(uint8_t* pBuf, int32_t Length)
{
	assert(pBuf != NULL);
	assert(Length > 0);

	int16_t K = COSAC_IV / 2;
	if (BufKey != NULL)
		K = COSAC_KEY / 2;

	for (int32_t i = 0; i < Length; i++)
	{
		pBuf[i] ^= (int8_t)0xAB;
		pBuf[i] = (int8_t)(pBuf[i] >> 4 | pBuf[i] << 4);
		if (BufIV != NULL)
		{
			pBuf[i] ^= (int8_t)(BufIV[(int8_t)(DecryptCounter & 0xFF) + 0]);
			pBuf[i] ^= (int8_t)(BufIV[(int8_t)(DecryptCounter >> 8) + K]);
		}
		DecryptCounter++;
	}
}

void CLegacyCipher::ResetCounters() 
{ 
	DecryptCounter = 0; 
	EncryptCounter = 0;
}