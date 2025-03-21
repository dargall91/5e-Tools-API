namespace _5eTools.Services;

public interface ICryptographyService
{
    string Encrypt(string toEncrypt, string key);
    string Decrypt(string toDecrypt, string key);
}

public class CryptographyService : ICryptographyService
{
    public string Encrypt(string toEncrypt, string key)
    {
        return toEncrypt;
    }

    public string Decrypt(string toDecrypt, string key)
    {
        return toDecrypt;
    }
}
