using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PasswordHasher
{
    public static class BlockCipher
    {
        private const int BLOCK_SIZE = 8;
        private const int HALF_BLOCK_SIZE = 4;
        private const int KEY_SIZE = 4;
        private const int ROUNDS = 16;

        public static byte[] EncryptData(string saltedPassword)
        {
            byte[] data = Encoding.UTF8.GetBytes(saltedPassword);
            uint key = DeriveKey(data);

            int paddedLength = (data.Length + BLOCK_SIZE - 1) / BLOCK_SIZE * BLOCK_SIZE;
            byte[] paddedData = new byte[paddedLength];
            Array.Copy(data, paddedData, data.Length);

            byte[] lengthBytes = BitConverter.GetBytes(data.Length);
            Array.Copy(lengthBytes, 0, paddedData, paddedLength - 4, 4);

            byte[] encryptedData = new byte[paddedLength];
            uint[] roundKeys = GenerateRoundKeys(key);

            for (int i = 0; i < paddedLength; i += BLOCK_SIZE)
            {
                byte[] block = new byte[BLOCK_SIZE];
                Array.Copy(paddedData, i, block, 0, BLOCK_SIZE);

                byte[] encryptedBlock = EncryptBlock(block, roundKeys);
                Array.Copy(encryptedBlock, 0, encryptedData, i, BLOCK_SIZE);
            }

            return encryptedData;
        }

        private static uint[] GenerateRoundKeys(uint mainKey)
        {
            uint[] roundKeys = new uint[ROUNDS];
            uint tempKey = mainKey;

            for (int i = 0; i < ROUNDS; i++)
            {
                // Простая генерация раундовых ключей на основе циклического сдвига и XOR
                tempKey = RotateLeft(tempKey, 5) ^ (uint)(i * 0x9E3779B9); // Используем золотое число
                roundKeys[i] = tempKey;
            }

            return roundKeys;
        }

        private static byte[] EncryptBlock(byte[] block, uint[] roundKeys)
        {
            // Разделяем блок на две половины по 32 бита
            uint left = BytesToUInt(block, 0);
            uint right = BytesToUInt(block, HALF_BLOCK_SIZE);

            // Выполняем раунды сети Фейштеля
            for (int round = 0; round < ROUNDS; round++)
            {
                uint temp = left;
                left = right;
                right = temp ^ MakingFunction(right, roundKeys[round]);
            }

            // Собираем блок обратно
            byte[] result = new byte[BLOCK_SIZE];
            UIntToBytes(left, result, 0);
            UIntToBytes(right, result, HALF_BLOCK_SIZE);

            return result;
        }

        // Образующая функция
        private static uint MakingFunction(uint data, uint roundKey)
        {
            // 1. XOR с раундовым ключом
            uint result = data ^ roundKey;

            // 2. Нелинейное преобразование S-блоком
            result = SubstituteBytes(result);

            // 3. Линейное преобразование - циклический сдвиг
            result = RotateLeft(result, 7);

            // 4. Еще одно нелинейное преобразование
            result ^= 0x9E3779B9; // Добавляем константу

            return result;
        }

        private static uint SubstituteBytes(uint data)
        {
            // Простая замена байтов (S-блок)
            byte[] bytes = BitConverter.GetBytes(data);

            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = SBox(bytes[i]);

            return BitConverter.ToUInt32(bytes, 0);
        }

        private static byte SBox(byte input)
        {
            // Простой S-блок для демонстрации
            byte[] sBox = {
                0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
                0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
                0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
                0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75
            };

            return sBox[input % sBox.Length];
        }

        private static uint RotateLeft(uint value, int count) => (value << count) | (value >> (32 - count));

        private static uint BytesToUInt(byte[] bytes, int startIndex) => BitConverter.ToUInt32(bytes, startIndex);

        private static void UIntToBytes(uint value, byte[] buffer, int startIndex)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Copy(bytes, 0, buffer, startIndex, bytes.Length);
        }

        private static uint DeriveKey(byte[] input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(input);
                byte[] key = new byte[KEY_SIZE];
                Array.Copy(hash, 0, key, 0, KEY_SIZE);
                return BitConverter.ToUInt32(key, 0);
            }
        }
    }
}
