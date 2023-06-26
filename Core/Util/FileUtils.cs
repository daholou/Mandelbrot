namespace Core.Util
{
  public class FileUtils
  {
    private FileUtils()
    {
    }

    public static string GetFilePath(string folderPath, string filename)
    {
      if (!Directory.Exists(folderPath))
      {
        Directory.CreateDirectory(folderPath);
      }
      return Path.Combine(folderPath, filename);
    }
  }
}
