using System;
using System.IO;

namespace AKiuLog.FileLog
{
  public class AKiuLogFile
  {

    public static FileStream Create(string fullPath)
    {
      string name = Path.GetFileName(fullPath);
      string path = Path.GetDirectoryName(fullPath);

      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      if (!File.Exists(fullPath))
      {
        return File.Create(fullPath);
      }
      else
      {
        return File.Open(fullPath, FileMode.Append);
      }
    }
  }
}
