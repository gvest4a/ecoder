namespace XorCipherApp.Models
{
    /// <summary>
    /// Represents cipher operation result
    /// </summary>
    public class CipherResult
    {
        public bool Success { get; set; }
        public string? Data { get; set; }
        public string? ErrorMessage { get; set; }

        public static CipherResult Ok(string data) => new CipherResult { Success = true, Data = data };
        public static CipherResult Fail(string error) => new CipherResult { Success = false, ErrorMessage = error };
    }

    /// <summary>
    /// Configuration for cipher operations
    /// </summary>
    public class CipherConfig
    {
        public string DefaultKey { get; set; } = "SecretKey123";
        public string InputFileFilter { get; set; } = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
        public string HexFileFilter { get; set; } = "Файлы (*.hex;*.txt)|*.hex;*.txt|Все файлы (*.*)|*.*";
        public string OutputHexFileName { get; set; } = "encrypted.hex";
        public string OutputTextFileName { get; set; } = "decrypted.txt";
    }
}
