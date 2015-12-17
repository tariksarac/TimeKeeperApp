using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace timeBase
{
    public class timeSign
    {
        public string getSignature(byte[] secret, byte[] apikey)
        {
            int secLen = secret.Length;
            if (secLen < apikey.Length) secLen = apikey.Length;

            byte[] signal = new byte[secLen];

            int i = 0, j = 0, k = 0;

            while (k < secLen)
            {
                int B = secret[i] + 19 * apikey[j];
                signal[k] = (byte)(B % 128);
                if (++k >= secLen) break;
                if (++i >= secret.Length) i = 0;
                if (++j >= apikey.Length) j = 0;
            }

            return Convert.ToBase64String(signal);
        }

        public byte[] getBytes(string str)
        {
            byte[] bytes = new byte[str.Length];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public string getString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

    }
}