﻿// -----------------------------------------------------------------------
//  <copyright file="AesHelper.cs" company="SF开源团队">
//      Copyright (c) 2014-2017 SF. All rights reserved.
//  </copyright>
//  <site>http://www.SF.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2017-12-30 15:41</last-date>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

using SF.Utility.Extensions;


namespace SF.Utility.Secutiry
{
    /// <summary>
    /// AEA加密解密辅助类
    /// </summary>
    public class AesHelper
    {
        private readonly bool _needIV;

        /// <summary>
        /// 初始化一个<see cref="AesHelper"/>类型的新实例
        /// </summary>
        public AesHelper(bool needIV = false)
            : this(GetRandomKey(), needIV)
        { }

        /// <summary>
        /// 初始化一个<see cref="AesHelper"/>类型的新实例
        /// </summary>
        /// <param name="key">加密密钥</param>
        /// <param name="needIV">是否需要向量</param>
        public AesHelper(string key, bool needIV = false)
        {
            Key = key;
            _needIV = needIV;
        }

        /// <summary>
        /// 获取 加密密钥
        /// </summary>
        public string Key { get; }

        #region 实例方法

        /// <summary>
        /// 加密字节数组
        /// </summary>
        public byte[] Encrypt(byte[] decodeBytes)
        {
            return Encrypt(decodeBytes, Key, _needIV);
        }

        /// <summary>
        /// 解密字节数组
        /// </summary>
        public byte[] Decrypt(byte[] encodeBytes)
        {
            return Decrypt(encodeBytes, Key, _needIV);
        }

        /// <summary>
        /// 加密字符串，输出为Base64编码的字符串
        /// </summary>
        public string Encrypt(string source)
        {
            return Encrypt(source, Key, _needIV);
        }

        /// <summary>
        /// 解密字符串，输入为Base64编码的字符串
        /// </summary>
        public string Decrypt(string source)
        {
            return Decrypt(source, Key, _needIV);
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        public void EncryptFile(string sourceFile, string targetFile)
        {
            EncryptFile(sourceFile, targetFile, Key, _needIV);
        }

        /// <summary>
        /// 解密文件
        /// </summary>
        public void DecryptFile(string sourceFile, string targetFile)
        {
            DecryptFile(sourceFile, targetFile, Key, _needIV);
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 加密字节数组
        /// </summary>
        public static byte[] Encrypt(byte[] decodeBytes, string key, bool needIV = false)
        {
            decodeBytes.CheckNotNull("decodeBytes");
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.Key = CheckKey(key);
                provider.Padding = PaddingMode.PKCS7;
                provider.Mode = CipherMode.ECB;
                byte[] ivBytes = { };
                if (needIV)
                {
                    provider.Mode = CipherMode.CBC;
                    provider.GenerateIV();
                    ivBytes = provider.IV;
                }
                using (ICryptoTransform encryptor = provider.CreateEncryptor())
                {
                    byte[] encodeBytes = encryptor.TransformFinalBlock(decodeBytes, 0, decodeBytes.Length);
                    provider.Clear();
                    return needIV ? ivBytes.Concat(encodeBytes).ToArray() : encodeBytes;
                }
            }
        }

        /// <summary>
        /// 解密字节数组
        /// </summary>
        public static byte[] Decrypt(byte[] encodeBytes, string key, bool needIV = false)
        {
            encodeBytes.CheckNotNull("source");
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.Key = CheckKey(key);
                provider.Padding = PaddingMode.PKCS7;
                provider.Mode = CipherMode.ECB;
                if (needIV)
                {
                    provider.Mode = CipherMode.CBC;
                    const int ivLength = 16;
                    byte[] ivBytes = new byte[ivLength], newEncodeBytes = new byte[encodeBytes.Length - ivLength];
                    Array.Copy(encodeBytes, 0, ivBytes, 0, ivLength);
                    provider.IV = ivBytes;
                    Array.Copy(encodeBytes, ivLength, newEncodeBytes, 0, newEncodeBytes.Length);
                    encodeBytes = newEncodeBytes;
                }
                using (ICryptoTransform decryptor = provider.CreateDecryptor())
                {
                    byte[] decodeBytes = decryptor.TransformFinalBlock(encodeBytes, 0, encodeBytes.Length);
                    provider.Clear();
                    return decodeBytes;
                }
            }
        }

        /// <summary>
        /// 加密字符串，输出为Base64字符串
        /// </summary>
        public static string Encrypt(string source, string key, bool needIV = false)
        {
            source.CheckNotNull("source");

            byte[] decodeBytes = source.ToBytes();
            byte[] encodeBytes = Encrypt(decodeBytes, key, needIV);
            return Convert.ToBase64String(encodeBytes, 0, encodeBytes.Length);
        }

        /// <summary>
        /// 解密字符串，输入为Base64字符串
        /// </summary>
        public static string Decrypt(string source, string key, bool needIV = false)
        {
            source.CheckNotNull("source");

            byte[] encodeBytes = Convert.FromBase64String(source);
            byte[] decodeBytes = Decrypt(encodeBytes, key, needIV);
            return decodeBytes.ToString2();
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        public static void EncryptFile(string sourceFile, string targetFile, string key, bool needIV = false)
        {
            sourceFile.CheckFileExists("sourceFile");
            targetFile.CheckNotNullOrEmpty("targetFile");

            using (FileStream ifs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read),
                    ofs = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
            {
                long length = ifs.Length;
                byte[] decodeBytes = new byte[length];
                ifs.Read(decodeBytes, 0, decodeBytes.Length);
                byte[] encodeBytes = Encrypt(decodeBytes, key, needIV);
                ofs.Write(encodeBytes, 0, encodeBytes.Length);
            }
        }

        /// <summary>
        /// 解密文件
        /// </summary>
        public static void DecryptFile(string sourceFile, string targetFile, string key, bool needIV = false)
        {
            sourceFile.CheckFileExists("sourceFile");
            targetFile.CheckNotNullOrEmpty("targetFile");

            using (FileStream ifs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read),
                    ofs = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
            {
                long length = ifs.Length;
                byte[] encodeBytes = new byte[length];
                ifs.Read(encodeBytes, 0, encodeBytes.Length);
                byte[] decodeBytes = Decrypt(encodeBytes, key, needIV);
                ofs.Write(decodeBytes, 0, decodeBytes.Length);
            }
        }

        /// <summary>
        /// 获取随机密钥
        /// </summary>
        public static string GetRandomKey()
        {
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.GenerateKey();
                Console.WriteLine(provider.Key.Length);
                return Convert.ToBase64String(provider.Key);
            }
        }

        /// <summary>
        /// 获取密钥的字节数组，AES加密密钥必须是32位，不是32位自动补全或者截断
        /// </summary>
        private static byte[] CheckKey(string key)
        {
            key.CheckNotNull("key");
            byte[] bytes, keyBytes = new byte[32];
            try
            {
                bytes = Convert.FromBase64String(key);
            }
            catch (FormatException)
            {
                bytes = key.ToBytes();
            }
            if (bytes.Length < 32)
            {
                Array.Copy(bytes, 0, keyBytes, 0, bytes.Length);
            }
            else if (bytes.Length > 32)
            {
                Array.Copy(bytes, 0, keyBytes, 0, 32);
            }
            else
            {
                keyBytes = bytes;
            }
            return keyBytes;
        }

        #endregion
    }
}