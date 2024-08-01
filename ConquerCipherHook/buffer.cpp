#include "stdafx.h"
#include "buffer.h"

#include<cstring>

namespace ConquerCipherHookBufferUtils {

	uint8_t buffer::read_uint8(uint8_t* buf, int32_t index)
	{
		uint8_t value = (*(uint8_t*)(buf + index));

		return value;
	}

	uint16_t buffer::read_uint16(uint8_t* buf, int32_t index)
	{
		uint16_t value = (*(uint16_t*)(buf + index));

		return value;
	}

	int32_t buffer::read_int32(uint8_t* buf, int32_t index)
	{
		int32_t value = (*(int32_t*)(buf + index));

		return value;
	}

	int64_t buffer::read_int64(uint8_t* buf, int32_t index)
	{
		int64_t value = (*(int64_t*)(buf + index));

		return value;
	}

	void buffer::write_uint8(uint8_t* buf, uint8_t value, int32_t offset)
	{
		(*(uint8_t*)(buf + offset)) = value;
	}

	void buffer::write_uint16(uint8_t* buf, uint16_t value, int32_t offset)
	{
		(*(uint16_t*)(buf + offset)) = value;
	}

	void buffer::write_int32(uint8_t* buf, int32_t value, int32_t offset)
	{
		(*(int32_t*)(buf + offset)) = value;
	}

	void buffer::write_int64(uint8_t* buf, int64_t value, int32_t offset)
	{
		(*(int64_t*)(buf + offset)) = value;
	}

	void buffer::write_char(uint8_t* buf, char* val, int32_t len, int32_t offset)
	{
		for (int32_t i = 0; i < len; i++) {

			buf[offset + i] = val[i];
		}
	}

	void buffer::encrypt(char* buf, int len)
	{
		char pSeed[32] =
		{
			46, 22, 32, 87, 95, 48, 8, 2, 4, 34, 59, 83, 21, 2, 243, 1, 1, 2, 80, 37, 202, 31, 99, 75, 7, 4, 6, 23, 100, 221, 82, 134
		};
		int length = sizeof pSeed / sizeof(char);
		for (int x = 0; x < len; x++) {
			buf[x] ^= pSeed[(x * 6 % 4) % length];
			buf[x] ^= pSeed[(x * 12 % 8) % length];
			buf[x] ^= pSeed[(x * 24 % 16) % length];
			buf[x] ^= pSeed[(x * 48 % 32) % length];
		}
	}
}