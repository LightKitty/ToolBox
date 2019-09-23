using System;
using System.Security.Cryptography;
using System.Text;

namespace ToolBox
{
    public static class TAes
    {
        #region 公开字段
        /// <summary>
        /// 默认密钥
        /// </summary>
        public const string defaultKey = "asekey";

        /// <summary>
        /// 默认加密模式
        /// </summary>
        public const CipherMode defaultCipherMode = CipherMode.ECB;

        /// <summary>
        /// 默认填充类型
        /// </summary>
        public const PaddingMode defaultPaddingMode = PaddingMode.PKCS7;

        /// <summary>
        /// 默认密钥长度（位）
        /// </summary>
        public const int defaultKeySize = 128;

        /// <summary>
        /// 默认初始向量（ECB模式不需要）
        /// </summary>
        public const string defultIv = null;
        #endregion

        #region 公开方法
        /// <summary>
        /// 字符串转Aes密钥
        /// </summary>
        /// <param name="key">Aes密钥字符串</param>
        /// <param name="keySize">密钥长度(128 192 256)</param>
        /// <returns>Aes密钥</returns>
        public static byte[] GetAesKey(string key, int keySize)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Aes密钥不能为空");
            }

            int byteLen = keySize / 8;

            byte[] temp = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[byteLen];

            int copyLen = temp.Length < keyBytes.Length ? temp.Length : byteLen;
            Array.Copy(temp, keyBytes, copyLen);

            return keyBytes;
        }

        /// <summary>
        /// 字符串转初始向量
        /// </summary>
        /// <param name="ivStr">初始向量字符串</param>
        /// <returns>初始向量</returns>
        public static byte[] GetIv(string ivStr)
        {
            byte[] iv = new byte[16];
            if (string.IsNullOrEmpty(ivStr)) return iv;

            byte[] temp = Encoding.UTF8.GetBytes(ivStr);

            int copyLen = temp.Length < iv.Length ? temp.Length : 16;
            Array.Copy(temp, iv, copyLen);

            return iv;
        }
        #endregion

        #region 加密
        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string source, string key)
        {
            return Encrypt(source, key, defaultKeySize, defultIv, defaultCipherMode, defaultPaddingMode);
        }

        /// <summary>
        /// Aes加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥</param>
        /// <param name="keySize">密钥位数</param>
        /// <param name="iv">初始化向量字符串</param>
        /// <param name="cipherMode">加密模式</param>
        /// <param name="paddingMode">加密填充类型</param>
        /// <returns>加密后字符串</returns>
        public static string Encrypt(string source, string key, int keySize, string iv, CipherMode cipherMode, PaddingMode paddingMode)
        {
            byte[] sourceBytes= Encoding.UTF8.GetBytes(source);
            byte[] keyBytes = GetAesKey(key, keySize);
            byte[] ivBytes = GetIv(iv);
            byte[] resultBytes = Encrypt(sourceBytes, keyBytes, ivBytes, cipherMode, paddingMode);
            return Convert.ToBase64String(resultBytes, 0, resultBytes.Length);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="source">源数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始向量（16字节）</param>
        /// <param name="cipherMode">加密模式</param>
        /// <param name="paddingMode">加密填充类型</param>
        /// <returns>加密后数据</returns>
        public static byte[] Encrypt(byte[] source, byte[] key, byte[] iv, CipherMode cipherMode, PaddingMode paddingMode)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.KeySize = key.Length * 8;
                aesProvider.Key = key;
                aesProvider.IV = iv;
                aesProvider.Mode = cipherMode;
                aesProvider.Padding = paddingMode;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor())
                {
                    byte[] result = cryptoTransform.TransformFinalBlock(source, 0, source.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return result;
                }
            }
        }
        #endregion

        #region 解密
        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string source, string key)
        {
            return Decrypt(source, key, defaultKeySize, defultIv, defaultCipherMode, defaultPaddingMode);
        }

        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">aes密钥</param>
        /// <param name="keySize">密钥格式化位数</param>
        /// <param name="iv">初始化向量字符串</param>
        /// <param name="cipherMode">加密模式</param>
        /// <param name="paddingMode">加密填充类型</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string source, string key, int keySize, string iv, CipherMode cipherMode, PaddingMode paddingMode)
        {
            byte[] sourceBytes = Convert.FromBase64String(source);
            byte[] keyBytes= GetAesKey(key, keySize);
            byte[] ivBytes= GetIv(iv);
            byte[] resultBytes = Decrypt(sourceBytes, keyBytes, ivBytes, cipherMode, paddingMode);
            return Encoding.UTF8.GetString(resultBytes);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="source">源数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量（16字节）</param>
        /// <param name="cipherMode">加密模式</param>
        /// <param name="paddingMode">加密填充类型</param>
        /// <returns>解密后数据</returns>
        public static byte[] Decrypt(byte[] source, byte[] key, byte[] iv, CipherMode cipherMode, PaddingMode paddingMode)
        {
            using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
            {
                aesProvider.KeySize = key.Length * 8;
                aesProvider.Key = key;
                aesProvider.IV = iv;
                aesProvider.Mode = cipherMode;
                aesProvider.Padding = paddingMode;
                using (ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor())
                {
                    byte[] result = cryptoTransform.TransformFinalBlock(source, 0, source.Length);
                    aesProvider.Clear();
                    aesProvider.Dispose();
                    return result;
                }
            }
        }
        #endregion
    }
}
