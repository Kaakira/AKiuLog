using System;
using System.IO;

namespace AKiuLog
{
  /// <summary>
  /// 队列日志
  /// </summary>
  public class AKiuLogger
  {
    /// <summary>
    /// 日志文件目录
    /// </summary>
    public static string LogRootPath;
    public static object _lock = new object();

    private static AKiuLogger logger
    {
      get
      {
        return logger;
      }
      set
      {
        if (logger != null)
          throw new Exception("logger只允许被初始化一次");
        else
          logger = value;
      }
    }

    private AKiuLogger() { }

    public static AKiuLogger GetCurrentLogger()
    {
      if (logger == null)
        throw new Exception("使用logger类前，需要先执行RegisterAkiuLog()以初始化");
      else
        return logger;
    }

    public static void RegisterAkiuLog(string rootPath)
    {
      // 初始化日志类
      LogRootPath = rootPath;
      logger = new AKiuLogger();
      // 创建日志文件夹
      if (!Directory.Exists(rootPath + "\\Log\\Debug"))
        Directory.CreateDirectory(rootPath);
      if (!Directory.Exists(rootPath + "\\Log\\Info"))
        Directory.CreateDirectory(rootPath);
      if (!Directory.Exists(rootPath + "\\Log\\Error"))
        Directory.CreateDirectory(rootPath);
      if (!Directory.Exists(rootPath + "\\Log\\Warning"))
        Directory.CreateDirectory(rootPath);
    }

    private void WriteLog()
    {

    }

    public void Error(Exception ex)
    {
      AKiuLogErrorMessage err = new AKiuLogErrorMessage(ex);
      string errMsg = err.ToString();
    }

    public void Debug(AKiuLogMessage meesage)
    {
      meesage.Level = AkiuLogLevel.Debug;
    }
  }
}
