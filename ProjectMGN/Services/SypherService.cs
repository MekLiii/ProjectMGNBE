using System.Security.Cryptography;
using System.Text;
using ProjectMGN.Interfaces.Services;

namespace ProjectMGN.Services
{
    public class SypherService : ISypherService
    {
        private readonly byte[] _encryptionKey;
        private readonly byte[] _encryptionIv;

        public SypherService(IConfiguration configuration)
        {
            var keyString = configuration["Sypher:key"];
            _encryptionKey = Convert.FromBase64String(keyString);

            using var encryptor = Aes.Create();
            {
                encryptor.GenerateIV();
                _encryptionIv = encryptor.IV;
            }
        }

        public string Encrypt(string clearText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            using var encryptor = Aes.Create();
            {
                var pdb = new Rfc2898DeriveBytes(_encryptionKey, _encryptionIv, 10000, HashAlgorithmName.SHA256);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = _encryptionIv;
                using var ms = new MemoryStream();
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                    }
                    clearText = Convert.ToBase64String(_encryptionIv.Concat(ms.ToArray()).ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            var iv = cipherBytes.Take(16).ToArray(); 
            var encryptedData = cipherBytes.Skip(16).ToArray();

            using var encryptor = Aes.Create();
            {
                var pdb = new Rfc2898DeriveBytes(_encryptionKey, iv, 10000, HashAlgorithmName.SHA256);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = iv;
                using var ms = new MemoryStream();
                {
                    using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
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
