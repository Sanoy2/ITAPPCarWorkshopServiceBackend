using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace ITAPP_CarWorkshopService.Authorization
{
    public class Encryption
    {
        static TripleDES CreateDES(string key)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            TripleDES tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key));
            tripleDES.IV = new byte[tripleDES.BlockSize / 8];
            return tripleDES;
        }

        public string Encrypt(string text, string password)
        {
            byte[] textBytes = Encoding.Unicode.GetBytes(text);

            MemoryStream memoryStream = new MemoryStream();

            TripleDES tripleDES = CreateDES(password);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, tripleDES.CreateEncryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(textBytes, 0, textBytes.Length);
            cryptoStream.FlushFinalBlock();

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public string Decrypt(string text, string password)
        {
            byte[] textBytes = Convert.FromBase64String(text);

            MemoryStream memoryStream = new MemoryStream();

            TripleDES tripleDES = CreateDES(password);

            CryptoStream cryptoStream = new CryptoStream(memoryStream, tripleDES.CreateDecryptor(), CryptoStreamMode.Write);

            cryptoStream.Write(textBytes, 0, textBytes.Length);
            cryptoStream.FlushFinalBlock();

            return Encoding.Unicode.GetString(memoryStream.ToArray());
        }
    }
}