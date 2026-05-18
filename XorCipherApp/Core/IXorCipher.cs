using System.Text;

namespace XorCipherApp.Core
{
    /// <summary>
    /// Interface for XOR cipher operations
    /// </summary>
    public interface IXorCipher
    {
        string Encrypt(string text, string key);
        string Decrypt(string hexText, string key);
    }

    /// <summary>
    /// Implementation of XOR cipher with hex encoding
    /// </summary>
    public class XorCipher : IXorCipher
    {
        public string Encrypt(string text, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be empty", nameof(key));
            }

            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            
            byte[] resultBytes = new byte[textBytes.Length];
            
            for (int i = 0; i < textBytes.Length; i++)
            {
                resultBytes[i] = (byte)(textBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }
            
            // Convert to hex string for display
            StringBuilder sb = new StringBuilder();
            foreach (byte b in resultBytes)
            {
                sb.Append(b.ToString("X2"));
            }
            
            return sb.ToString();
        }

        public string Decrypt(string hexText, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be empty", nameof(key));
            }

            // Convert from hex string to bytes
            hexText = hexText.Replace(" ", "").Replace("\n", "").Replace("\r", "");
            byte[] textBytes = new byte[hexText.Length / 2];
            for (int i = 0; i < hexText.Length; i += 2)
            {
                textBytes[i / 2] = Convert.ToByte(hexText.Substring(i, 2), 16);
            }
            
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] resultBytes = new byte[textBytes.Length];
            
            for (int i = 0; i < textBytes.Length; i++)
            {
                resultBytes[i] = (byte)(textBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }
            
            return Encoding.UTF8.GetString(resultBytes);
        }
    }
}
