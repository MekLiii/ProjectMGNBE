namespace ProjectMGN.Interfaces.Services
{
    public interface ISypherService
    {
        public string Encrypt(string sensitiveData);
        public string Decrypt(string encryptedData);
    }
}
