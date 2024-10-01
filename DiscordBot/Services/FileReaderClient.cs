namespace DiscordBot.Services
{
    public static class FileReaderClient
    {
        public static string ProcessFile(string filePath)
        {
            try
            {
                return AnyFileReaderFactory.GetFileReader(filePath).ReadFile(filePath);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
