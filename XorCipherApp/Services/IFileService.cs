namespace XorCipherApp.Services
{
    /// <summary>
    /// Interface for file operations
    /// </summary>
    public interface IFileService
    {
        string ReadFile(string path);
        void WriteFile(string path, string content);
        Task<string> ReadFileAsync(string path);
        Task WriteFileAsync(string path, string content);
    }

    /// <summary>
    /// Implementation of file service
    /// </summary>
    public class FileService : IFileService
    {
        public string ReadFile(string path)
        {
            return System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
        }

        public void WriteFile(string path, string content)
        {
            System.IO.File.WriteAllText(path, content, System.Text.Encoding.UTF8);
        }

        public async Task<string> ReadFileAsync(string path)
        {
            return await System.IO.File.ReadAllTextAsync(path, System.Text.Encoding.UTF8);
        }

        public async Task WriteFileAsync(string path, string content)
        {
            await System.IO.File.WriteAllTextAsync(path, content, System.Text.Encoding.UTF8);
        }
    }
}
