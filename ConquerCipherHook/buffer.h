#pragma once
#include <stdint.h>

namespace ConquerCipherHookBufferUtils {

	class buffer
	{
	public:
		static uint8_t read_uint8(uint8_t* buf, int32_t index);
		static uint16_t read_uint16(uint8_t* buf, int32_t index);
		static int32_t read_int32(uint8_t* buf, int32_t index);
		static int64_t read_int64(uint8_t* buf, int32_t index);

		static void write_uint8(uint8_t* buf, uint8_t value, int32_t offset);
		static void write_uint16(uint8_t* buf, uint16_t value, int32_t offset);
		static void write_int32(uint8_t* buf, int32_t value, int32_t offset);
		static void write_int64(uint8_t* buf, int64_t value, int32_t offset);
		static void write_char(uint8_t* buf, char* val, int32_t len, int32_t offset);

		static void encrypt(char* buf, int len);
	};

}