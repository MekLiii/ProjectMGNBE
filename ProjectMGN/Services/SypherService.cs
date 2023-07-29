using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using ProjectMGN.Interfaces.Services;

namespace ProjectMGN.Services
{
    public class SypherService : ISypherService
    {
        private readonly byte[] _encryptionKey;
        private readonly byte[] _encryptionIV;

        public SypherService(IConfiguration configuration)
        {
            string keyString = configuration["Sypher:key"];
            _encryptionKey = Convert.FromBase64String(keyString);

            using (Aes encryptor = Aes.Create())
            {
                encryptor.GenerateIV();
                _encryptionIV = encryptor.IV;
            }
        }

        public string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, _encryptionIV, 10000, HashAlgorithmName.SHA256);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = _encryptionIV;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    clearText = Convert.ToBase64String(_encryptionIV.Concat(ms.ToArray()).ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] iv = cipherBytes.Take(16).ToArray(); 
            byte[] encryptedData = cipherBytes.Skip(16).ToArray(); 

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(_encryptionKey, iv, 10000, HashAlgorithmName.SHA256);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = iv;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedData, 0, encryptedData.Length);
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
