using System;
using System.Security.Cryptography;
using System.Text;

namespace ToolBox
{
    public static class TAes
    {
        /// <summary>
        /// Aes加解密钥必须32位
        /// </summary>
        //public const string defaultAesKey = "asekey";
        public const CipherMode defaultCipherMode = CipherMode.ECB;
        public const PaddingMode defaultPaddingMode = PaddingMode.PKCS7;
        public const int defaultBitLength = 128;
        public const string defultIv = null;

        /// <summary>
        /// 获取Aes密钥
        /// </summary>
        /// <param name="key">Aes密钥字符串</param>
        /// <param name="bitLenght">密钥格式化位数</param>
        /// <returns>Aes密钥</returns>
        private static byte[] GetAesKey(string key, int bitLenght)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Aes密钥不能为空");
            }

            int byteLen = bitLenght / 8;

            byte[] temp = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[byteLen];

            int copyLen = temp.Length < keyBytes.Length ? temp.Length : byteLen;
            Array.Copy(temp, keyBytes, copyLen);

            return keyBytes;
        }

        /// <summary>
        /// 获取初始向量
        /// </summary>
        /// <param name="ivStr">初始向量字符串</param>
        /// <returns>初始向量</returns>
        private static byte[] GetIv(string ivStr)
        {
            byte[] iv = new byte[16];
            if (string.IsNullOrEmpty(ivStr)) return iv;

            byte[] temp = Encoding.UTF8.GetBytes(ivStr);

            int copyLen = temp.Length < iv.Length ? temp.Length : 16;
            Array.Copy(temp, iv, copyLen);

            return iv;
        }

        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string source, string key)
        {
            return Encrypt(source, key, defaultBitLength, defultIv, defaultCipherMode, defaultPaddingMode);
        }

        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥</param>
        /// <param name="bitLength">密钥格式化位数</param>
        /// <param name="ivString">初始化向量字符串</param>
        /// <param name="cipherMode">加密模式</param>
        /// <param name="paddingMode">加密填充类型</param>
        /// <returns>加密后字符串</returns>
        public static string Encrypt(string source, string key, int bitLength, string ivString, CipherMode cipherMode, PaddingMode paddingMode)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.KeySize = bitLength;
                aesProvider.Key = GetAesKey(key, bitLength);
                aesProvider.IV = GetIv(ivString);
                aesProvider.Mode = cipherMode;
                aesProvider.Padding = paddingMode;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] inputBuffers = Encoding.UTF8.GetBytes(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string source, string key)
        {
            return Decrypt(source, key, defaultBitLength, defultIv, defaultCipherMode, defaultPaddingMode);
        }

        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥</param>
        /// <param name="bitLength">密钥格式化位数</param>
        /// <param name="ivString">初始化向量字符串</param>
        /// <param name="cipherMode">加密模式</param>
        /// <param name="paddingMode">加密填充类型</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string source, string key, int bitLength, string ivString, CipherMode cipherMode, PaddingMode paddingMode)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.KeySize = bitLength;
                aesProvider.Key = GetAesKey(key, bitLength);
                aesProvider.IV = GetIv(ivString);
                aesProvider.Mode = cipherMode;
                aesProvider.Padding = paddingMode;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] inputBuffers = Convert.FromBase64String(source);
                    byte[] results = cryptoTransform.TransformFinalBlock(inputBuffers, 0, inputBuffers.Length);
                    aesProvider.Clear();
                    return Encoding.UTF8.GetString(results);
                }
            }
        }
    }
}
