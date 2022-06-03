using System;
using System.Numerics;
using static GOST_34_12_2015.Data;

namespace GOST_34_12_2015
{
    public class Kuznechik
    {
        private static int key = 32;
        public byte[] masterKey = new byte[key];
        Vector<byte>[] roundKeys = new Vector<byte>[10];
        public byte[] magicString = { 0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef,0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x00, 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77,
            0xfe, 0xdc, 0xba, 0x98, 0x76, 0x54, 0x32, 0x10, 0x01, 0x23, 0x45, 0x67, 0x89, 0xab, 0xcd, 0xef};
        public byte[] Decrypt(byte[] mess)
        {

            generateDencryptionRoundKeys(masterKey, ref roundKeys);
            Vector<byte> tmp;
            byte[] block = new byte[16];
            for (int i = 0; i < mess.Length / 16; i++)
            {
                tmp = new Vector<byte>(mess, i * 16);
                decrypt(ref tmp, roundKeys);
                tmp.CopyTo(block, 0);
                Buffer.BlockCopy(block, 0, mess, i * 16, 16);
            }
            return (mess);

        }


        //зашифровать файл
        public byte[] Encrypt(byte[] mess)
        {
            generateEncryptionRoundKeys(masterKey, ref roundKeys);

            if (mess.Length % 16 != 0)
            {
                mess.CopyTo(mess = new byte[mess.Length + 16 - (mess.Length % 16)], 0);
            }

            byte[] block = new byte[16];
            Vector<byte> tmp;


            for (int i = 0; i < mess.Length / 16; i++)
            {
                tmp = new Vector<byte>(mess, i * 16);
                encrypt(ref tmp, roundKeys);
                tmp.CopyTo(block, 0);
                Buffer.BlockCopy(block, 0, mess, i * 16, 16);
            }


            return (mess);
        }

        //генерация ключей для зашифрования
        public void generateEncryptionRoundKeys(byte[] masterKey, ref Vector<byte>[] roundKeys)
        {
            Vector<byte> left = new Vector<byte>(), right = new Vector<byte>();

            roundKeys[0] = new Vector<byte>(masterKey, 0);
            roundKeys[1] = new Vector<byte>(masterKey, 16);
            for (int i = 2; i < roundKeys.Length; i++)
            {
                roundKeys[i] = new Vector<byte>();
            }

            for (int i = 1; i <= 4; i++)
            {
                left = roundKeys[i * 2 - 2];
                right = roundKeys[i * 2 - 1];
                for (int j = 1; j <= 8; j++)
                {
                    functionF(8 * (i - 1) + j, ref left, ref right);
                }
                roundKeys[i * 2] = left;
                roundKeys[i * 2 + 1] = right;
            }
            byte[] temp = new byte[16];
            for (int i = 0; i < roundKeys.Length; i++)
            {
                roundKeys[i].CopyTo(temp, 0);
            }
        }

        //генерация ключей для расшифрования
        public void generateDencryptionRoundKeys(byte[] masterKey, ref Vector<byte>[] roundKeys)
        {
            Vector<byte> temp1 = new Vector<byte>(),
                temp2 = new Vector<byte>();
            generateEncryptionRoundKeys(masterKey, ref roundKeys);
            for (int i = 1; i <= 8; i++)
            {
                temp1 = roundKeys[i];
                functionS(ref temp1, pi);
                functionLS(temp1, ref temp2, precomputedInversedLSTable);
                roundKeys[i] = temp2;
            }
        }

        //итерация сети Фейстеля
        private void functionF(int constantIndex, ref Vector<byte> left, ref Vector<byte> right)
        {
            Vector<byte> temp1 = new Vector<byte>(roundConstants, 16 * (constantIndex - 1)),
                temp2 = Vector.Xor(left, temp1);
            functionLS(temp2, ref temp1, precomputedLSTable);
            right = Vector.Xor(right, temp1);
            swapBlocks(ref left, ref right);
        }

        //LS-преобразование
        private void functionLS(Vector<byte> input, ref Vector<byte> output, byte[] LStable)
        {
            output = new Vector<byte>();
            Vector<byte> temp;
            for (int i = 0; i < 16; i++)
            {
                temp = new Vector<byte>(LStable, i * 16 * 256 + input[i] * 16);
                output = Vector.Xor(temp, output);
            }
        }

        //S-преобразование
        private void functionS(ref Vector<byte> input, byte[] pi)
        {
            byte[] inputBytes = new byte[256];
            input.CopyTo(inputBytes, 0);
            for (int i = 0; i < inputBytes.Length; i++)
            {
                inputBytes[i] = pi[inputBytes[i]];
            }
            input = new Vector<byte>(inputBytes);
        }

        //замена блоков местами
        private void swapBlocks(ref Vector<byte> left, ref Vector<byte> right)
        {
            Vector<byte> temp = right;
            right = left;
            left = temp;
        }

        //зашифрование
        public void encrypt(ref Vector<byte> data, Vector<byte>[] roundKeys)
        {
            Vector<byte> temp = new Vector<byte>();
            for (int i = 0; i < 9; i++)
            {
                temp = Vector.Xor(data, roundKeys[i]);
                functionLS(temp, ref data, precomputedLSTable);
            }
            data = Vector.Xor(data, roundKeys[9]);
        }

        //расшифрование
        public void decrypt(ref Vector<byte> data, Vector<byte>[] roundKeys)
        {
            Vector<byte> temp1 = new Vector<byte>(),
                temp2 = new Vector<byte>();
            temp1 = Vector.Xor(data, roundKeys[9]);
            functionS(ref temp1, pi);
            functionLS(temp1, ref temp2, precomputedInversedLSTable);

            for (int i = 8; i > 0; i--)
            {
                functionLS(temp2, ref temp1, precomputedInversedLSTable);
                temp2 = Vector.Xor(temp1, roundKeys[i]);
            }
            functionS(ref temp2, inversedPi);
            data = Vector.Xor(temp2, roundKeys[0]);

        }

    }
}

