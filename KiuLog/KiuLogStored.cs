using KiuLog.FileLog;
using KiuLog.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KiuLog
{
  /// <summary>
  /// 日志记录器
  /// </summary>
  public interface IKiuLogStored
  {
    string GetIdentity();
    // TODO: 子类实现：记录日志的逻辑（写入文件/写入数据库）
    void SaveLog(KiuLogContent message);
  }




  /// <summary>
  /// 默认文件日志记录器(保存
  /// </summary>
  public class KiuLogStoredFile : IKiuLogStored
  {
    static string path;
    static string identity = "KiuLogStoredFile";

    public KiuLogStoredFile(string rootPath)
    {
      KiuLogStoredFile.path = rootPath;
    }

    public string GetIdentity()
    {
      return identity;
    }

    public void SaveLog(KiuLogContent content)
    {
      DateTime date = content.Date;
      string path = Path.Combine(KiuLogStoredFile.path, date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd") + "-" + content.Level.ToString() + ".log");
      using (FileStream logFile = KiuLogFile.Create(path))
      using (StreamWriter writer = new StreamWriter(logFile))
      {
        writer.WriteLine(content.LogContent());
      }
    }

  }

}
