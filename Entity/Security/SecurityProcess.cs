﻿using Entity.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Security
{
    public class SecurityProcess
    {
        static SecurityProcess securityProcess;
        private SecurityProcess()
        {

        }
        public static string Encrypt(string value)
        {
            string key = Infos.EncryptValue;
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key))
            {
                return "";
            }

            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new((Stream)cryptoStream))
                {
                    streamWriter.Write(value);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(string value)
        {
            string key = Infos.EncryptValue;
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(key))
            {
                return "";
            }
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(value);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new MemoryStream(buffer);
                using CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader streamReader = new StreamReader((Stream)cryptoStream);
                return streamReader.ReadToEnd();
            }
        }
      
    }
}
